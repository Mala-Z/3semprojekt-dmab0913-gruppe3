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
        private GridAddFlight gridAddFlight = new GridAddFlight();
        private ContentTitle addTitle = new ContentTitle("Tilføj ny flyforbindelse");
        private ContentTitle editTitle = new ContentTitle("Rediger flyforbindelse");

        public TabFlight()
        {
            InitializeComponent();
            ContentControlTitle.Content = addTitle;
            ContentControlAddEdit.Content = gridAddFlight;
            fService = new FlightServiceClient();
            InitGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            bool success = false;
            int flightID = GetSelectedFlightID();
            var warningBox = MessageBox.Show("Vil du slette flyforbindelsen med ID: " + flightID + "?", "Slet",
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (warningBox == MessageBoxResult.Yes)
                success = fService.DeleteFlight(flightID);
            if (success)
            {
                UpdateDataGrid();
            }
            else
            {
                MainWindow.ErrorMsg("Flyforbindelse ikke slettet");
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = addTitle;
            ContentControlAddEdit.Content = gridAddFlight;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void InitGridData()
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
                    Ledige = fService.GetAirplaneByID(Convert.ToInt32(f.airplaneID)).seats -= f.takenSeats
                };
            return result;
        }

        public void UpdateDataGrid()
        {
            var date = DatePickerFlightGrid.SelectedDate;
            if (date != null && date.GetType() == typeof(DateTime))
            {
                dgFlights.ItemsSource = GetFlightsToGridByDate(date.Value);
            }
            else
            {
                InitGridData();
            }
        }

     
        private void dgFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFlights.SelectedItem != null)
            {
                var flight = fService.GetFlightByID(GetSelectedFlightID());
                ContentControlTitle.Content = editTitle;
                ContentControlAddEdit.Content = new GridEditFlight(flight); 
            }
            
        }

        private int GetSelectedFlightID()
        {
            String flightString = dgFlights.SelectedItem.ToString().Trim();
            //brug regex til at få første digit fra string
            int flightID = Convert.ToInt32(Regex.Match(flightString, @"\d+").ToString());
            return flightID;
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
