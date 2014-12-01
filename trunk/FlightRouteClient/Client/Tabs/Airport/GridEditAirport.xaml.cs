using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.FlightService;

namespace Client.Tabs.Airport
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridEditAirport : UserControl
    {
        private FlightServiceClient fService;
        private FlightService.Airport airport;

        public GridEditAirport(FlightService.Airport airport)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.airport = airport;
            InsertAirportData();
        }

        private void InsertAirportData()
        {
            txtName.Text = airport.name;
            txtLocation.Text = airport.location;
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtLocation.Text != "")
            {
                fService.UpdateAirport(airport.airportID, txtName.Text, txtLocation.Text);
            }
            else
            {
                
            }
        } 

    }
}
