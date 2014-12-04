using System;
using System.Collections.Generic;
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
using Client.Helpers;

namespace Client.Tabs.Flight
{
    /// <summary>
    /// Interaction logic for GridEditFlight.xaml
    /// </summary>
    public partial class GridEditFlight : UserControl
    {
        private FlightServiceClient fService;
        private FlightService.Flight flight;

        public GridEditFlight(FlightService.Flight flight)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.flight = flight;
            InitComboBox();
        }

        private void InitComboBox()
        {
            cbFrom.ItemsSource = ComboBoxItems.AirportItems();
            cbTo.ItemsSource = ComboBoxItems.AirportItems();
            cbAirplane.ItemsSource = ComboBoxItems.AirplaneItems();
            InsertFlightData();
        }

        private void InsertFlightData()
        {
            if (flight.from > 0 && flight.to > 0)
            {
                cbFrom.SelectedValuePath = flight.from.ToString();
                cbTo.SelectedValue = flight.to.ToString(); 
            }
            
            DatePickerDeparture.SelectedDate = DateTime.Parse(flight.timeOfDeparture);
            DatePickerArrival.SelectedDate = DateTime.Parse(flight.timeOfArrival);
            txtTravelTime.Text = flight.traveltime.ToString();
            txtPrice.Text = flight.price.ToString();
            cbAirplane.SelectedValue = flight.airplaneID.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
