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

                 Createtable();

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

        private void Createtable()
        {
            string[] headers = new string[] {
     "Kód",
     "Eladó",
     "Oldal",
     "Kerület",
     "Lift",
     "Szobák száma",
     "Alapterület (m2)",
     "Ár (mFt)",
     "Négyzetméter ár (Ft/m2)"};

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i+1] = headers[i];
            }
            object[,] values = new object[Flats.Count, headers.Length];
            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                if (f.Elevator)
                values[counter, 4] = "igen";
                else
                    values[counter, 4] = "nem";
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = "="+GetCell(counter+2,7)+"/"+GetCell(counter+2,8)+"*1000000";
                counter++;
            }
            xlSheet.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

                  
        }
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
