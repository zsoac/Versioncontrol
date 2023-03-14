using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Excelgenerator2
{
    public partial class Form1 : Form
    {
        List<Flat> Flats;
        RealEstateEntities context = new RealEstateEntities();
        Excel.Application xlApp;
        Excel.Workbook xlWb;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            Createxcel();
        }

        void LoadData()
        {
            Flats = context.Flat.ToList();

        }

        void Createxcel()
        {
            try
            {



                xlApp = new Excel.Application();
                xlWb = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWb.ActiveSheet;

                // Createtable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string Errormsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(Errormsg, "Error");
                xlWb.Close(false, Type.Missing, Type.Missing);
               
                xlApp.Quit();
                xlApp = null;
                xlApp = null;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
