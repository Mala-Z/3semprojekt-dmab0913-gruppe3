using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            fService = new FlightServiceClient();

            InitializeGridData();

        }

        private void InitializeGridData()
        {
            DateTime fromDate = DateTime.ParseExact("03/12/2014 01:00", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Debug.WriteLine(fromDate.ToString());
            Debug.WriteLine(fromDate.ToShortDateString());
            var result = from f in fService.GetAllFlightsByDate(fromDate)
                //var result = from f in fService.GetAllFlights()
                select new
                {
                    ID = f.flightID,
                    Fra = fService.GetAirportByID(f.@from).name,
                    Til = fService.GetAirportByID(f.@to).name,
                    Afgang = f.timeOfDeparture,
                    Ankomst = f.timeOfArrival,
                    Rejsetid = f.traveltime,
                    Pris = f.price,
                    Ledige = fService.GetAirplaneByID((int)f.airplaneID).seats -= f.takenSeats };

            dgFlights.ItemsSource = result;

        }

        public void updateDataGrid()
        {
            InitializeGridData();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from f in fService.GetAllFlights()
                         where f.airplaneID.ToString() == txtSearch.Text || fService.GetAirportByID(f.@from).name.ToLower().Contains(txtSearch.Text.ToLower())
                         select new { ID = f.flightID, 
                             Fra = fService.GetAirportByID(f.@from).name, 
                             Til = fService.GetAirportByID(f.@to).name, 
                             Afgang = f.timeOfDeparture, 
                             Ankomst = f.timeOfArrival, 
                             Rejsetid = f.traveltime, 
                             Pris = f.price, 
                             Ledige = fService.GetAirplaneByID((int)f.airplaneID).seats -= f.takenSeats };

            dgFlights.ItemsSource = result;
            
            //Application.Current.MainWindow 


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
    }
}
