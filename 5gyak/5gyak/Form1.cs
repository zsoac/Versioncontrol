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
using System.Xml;

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
            getXmldata(Getrates());
        }

        private string Getrates()
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
            return result;
        }

        private void getXmldata(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement item in xml.DocumentElement)
            {
                var date = item.GetAttribute("date");

                var rate = (XmlElement)item.ChildNodes[0];
                var currency = rate.GetAttribute("curr");
                var unit =int.Parse( rate.GetAttribute("unit"));
                var value =decimal.Parse( rate.InnerText);

                var r = new Ratedata()
                {
                    Date = DateTime.Parse(date),
                    Currency = currency,
                   Value = unit!=0
                            ? value/unit
                            :0

                };
                Rates.Add(r);
            }
        }
    }
}
