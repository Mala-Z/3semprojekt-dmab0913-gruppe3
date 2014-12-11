using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Client.FlightService;
using Client.Helpers;
using Client.Tabs.Flight;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabFlight.xaml
    /// </summary>
    public partial class TabFlight : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly GridAddFlight _gridAddFlight = new GridAddFlight();
        private readonly ContentTitle _addTitle = new ContentTitle("Tilføj ny flyforbindelse");
        private readonly ContentTitle _editTitle = new ContentTitle("Rediger flyforbindelse");

        public TabFlight()
        {
            InitializeComponent();
            ContentControlTitle.Content = _addTitle;
            ContentControlAddEdit.Content = _gridAddFlight;
            _fService = new FlightServiceClient();
            LoadGridData(DateTime.Now);
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            //ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);
        }

        private void LoadGridData(DateTime time)
        {
            Action workAction = () =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, args) => args.Result = GetFlightsToGridByDate(time);
                worker.RunWorkerCompleted += (o, args) => { dgFlights.ItemsSource = (IEnumerable) args.Result; };
                worker.RunWorkerAsync();
            };
            dgFlights.Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction);
        }


        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    bool success = false;
        //    int flightID = GetSelectedFlightID();
        //    var warningBox = MessageBox.Show("Vil du slette flyforbindelsen med ID: " + flightID + "?", "Slet",
        //        MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
        //    if (warningBox == MessageBoxResult.Yes)
        //        success = fService.DeleteFlight(flightID);
        //    if (success)
        //    {
        //        UpdateDataGrid();
        //    }
        //    else
        //    {
        //        MainWindow.ErrorMsg("Flyforbindelse ikke slettet");
        //    }
        //}

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = _addTitle;
            ContentControlAddEdit.Content = _gridAddFlight;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private IEnumerable<Object> GetFlightsToGridByDate(DateTime fromDate)
        {
            var result = from f in _fService.GetAllFlightsByDate(fromDate)
                select new
                {
                    ID = f.flightID,
                    Fra = _fService.GetAirportByID(f.@from).name,
                    Til = _fService.GetAirportByID(f.@to).name,
                    Afgang = f.timeOfDeparture,
                    Ankomst = f.timeOfArrival,
                    Rejsetid = f.traveltime,
                    Pris = f.price,
                    Ledige = _fService.GetAirplaneByID(Convert.ToInt32(f.airplaneID)).seats -= f.takenSeats
                };
            return result;
        }

        public void UpdateDataGrid()
        {
            var date = DatePickerFlightGrid.SelectedDate;
            LoadGridData(date != null ? date.Value : DateTime.Now);
        }


        private void dgFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFlights.SelectedItem != null)
            {
                var flight = _fService.GetFlightByID(GetSelectedFlightID());
                ContentControlTitle.Content = _editTitle;
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
