using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using Client.Tabs.Airport;
using Client.Tabs.Flight;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// </summary>
    public partial class TabFlight : UserControl
    {
        private FlightServiceClient fService;

        public TabFlight()
        {
            InitializeComponent();
            contentControl.Content = new GridAddFlight();
            ContentControlActionBar.Content = new ActionBar();

            fService = new FlightServiceClient();

            InitializeGridData();

        }

        private void InitializeGridData()
        {
            DateTime fromDate = DateTime.Now;
            var result = GetFlightsToGridByDate(fromDate);
            dgFlights.ItemsSource = result;

        }

        private IEnumerable<Object> GetFlightsToGridByDate(DateTime fromDate)
        {
            var result = from f in fService.GetAllFlightsByDate(fromDate)
                select new
                {
                    ID = f.flightID,
                    Fra = fService.GetAirportByID(f.@from).name,
                    Til = fService.GetAirportByID(f.@to).name,
                    Afgang = f.timeOfDeparture,
                    Ankomst = f.timeOfArrival,
                    Rejsetid = f.traveltime,
                    Pris = f.price,
                    Ledige = fService.GetAirplaneByID((int) f.airplaneID).seats -= f.takenSeats
                };
            return result;
        }

        public void UpdateDataGrid()
        {
            InitializeGridData();
        }

     
        private void dgFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgAirports indeholder anonyme objecter. Enten skal vi lave en ny class og caste det anonyme object dertil,
            //ellers kan vi som her lave det om til en string, og via regular expression hente dets id
            if (dgFlights.SelectedItem != null)
            {
                String flightString = dgFlights.SelectedItem.ToString().Trim();
                int flightID = Convert.ToInt32(Regex.Match(flightString, @"\d+").ToString());
                var flight = fService.GetFlightByID(flightID);
                contentControl.Content = new GridEditFlight(flight); 
            }
            
        }

        private void DatePickerFlightGrid_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = DatePickerFlightGrid.SelectedDate;
            if (date != null && date.GetType() == typeof(DateTime))
            {
                dgFlights.ItemsSource = GetFlightsToGridByDate(date.Value);
            }
        }
    }
}
