using _5gyak.Entities;
using _5gyak.MNBServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5gyak
{
    public partial class Form1 : Form
    {
        BindingList<Ratedata> Rates = new BindingList<Ratedata>();
        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = Rates;
            Getrates();
        }

        private void Getrates()
        {
            MNBArfolyamServiceSoapClient mnbservice = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbservice.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
        }
    }
}
