using Gyak999.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak999
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép-teszt.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
        }
    public List<Person> GetPopulation(string csvpath)
        {
            var population = new List<Person>();
            using (var sr = new StreamReader(csvpath,Encoding.Default))
            {
                
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var p = new Person
                    {
                        BirthYear= int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = byte.Parse(line[2])
                    };
                    population.Add(p);
                }
            }
            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            var Birthprobabilities = new List<BirthProbability>();
            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
               
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var p = new BirthProbability
                    {
                        Age = int.Parse(line[0]),
                        
                        NbrOfChildren = byte.Parse(line[1]),
                        P = double.Parse(line[2])
                    };
                    Birthprobabilities.Add(p);
                }
            }
            return Birthprobabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            var deathprobablilities = new List<DeathProbability>();
            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
               
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var p = new DeathProbability
                    {
                       Age = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        P = double.Parse(line[2])
                    };
                    deathprobablilities.Add(p);
                }
            }
            return deathprobablilities;
        }
    }
}
