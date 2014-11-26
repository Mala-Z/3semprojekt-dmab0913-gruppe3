using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfService;

namespace FlightRouteSystem
{
    public partial class MainWindow : Form
    {
        private IWCFService wcfService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

            var airplanes = wcfService.GetAllAirplanes();

            var result = from a in airplanes
                         orderby a.airplaneID
                         select a;
                         //select new { Navn = c.Name, c.Email, Adresse = c.Address, Postnr = c.ZipCode };

            dgvPersons.DataSource = result;


        }
    }
}
