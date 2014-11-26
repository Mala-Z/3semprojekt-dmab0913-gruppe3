using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightRouteClient.ServiceReference1;

namespace FlightRouteClient
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            using (FlightServiceClient flightService = new FlightServiceClient())
            {
                var airplanes = flightService.GetAllAirplanes();

                var result = airplanes.OrderBy(a => a.airplaneID).ToList();

                dgvAirports.DataSource = result;
            }


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
