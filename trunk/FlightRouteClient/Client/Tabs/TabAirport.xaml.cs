using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Client.FlightService;
using Client.Helpers;
using Client.Tabs.Airport;

namespace Client.Tabs
{
    public partial class TabAirport : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly GridAddAirport _gridAddAirport = new GridAddAirport();
        private readonly ContentTitle _addTitle = new ContentTitle("Tilføj ny lufthavn");
        private readonly ContentTitle _editTitle = new ContentTitle("Rediger lufthavn");

        public TabAirport()
        {
            InitializeComponent();
            ContentControlTitle.Content = _addTitle;
            contentControl.Content = _gridAddAirport;
            _fService = new FlightServiceClient();
            LoadGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            //ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);
        }

        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    bool success = false;
        //    int airportID = GetSelectedAirportID();
        //    var warningBox = MessageBox.Show("Vil du slette lufthavnen med ID: " + airportID + "?", "Slet",
        //        MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
        //    if (warningBox == MessageBoxResult.Yes)
        //        success = _fService.DeleteAirport(airportID);
        //    if (!success)
        //        MainWindow.ErrorMsg("Lufthavn ikke slettet");

        //    ((MainWindow)System.Windows.Application.Current.MainWindow).TabAirport.LoadGridData();
        //}

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = _addTitle;
            contentControl.Content = _gridAddAirport;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            LoadGridData();
        }

        public void LoadGridData()
        {
            Action workAction = () =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, args) =>
                {
                    args.Result = from a in _fService.GetAllAirports()
                                  select new { ID = a.airportID, Navn = a.name, Lokation = a.location };
                }; 
                worker.RunWorkerCompleted += (o, args) => { dgAirports.ItemsSource = (IEnumerable)args.Result; };
                worker.RunWorkerAsync();
            };
            dgAirports.Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction);
         }

        private void tSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from a in _fService.GetAllAirports()
                         where a.name.ToLower().Contains(txtSearch.Text.ToLower()) || a.location.ToLower().Contains(txtSearch.Text.ToLower())
                         select new { ID = a.airportID, Navn = a.name, Lokation = a.location };

            dgAirports.ItemsSource = result;
        }

        private int GetSelectedAirportID()
        {
            String airportString = dgAirports.SelectedItem.ToString().Trim();
            //brug regex til at få første digit fra string
            int airportID = Convert.ToInt32(Regex.Match(airportString, @"\d+").ToString());
            return airportID;
        }

        private void dgAirports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgAirports indeholder anonyme objekter. Enten skal vi lave en ny class og caste det anonyme objekt dertil,
            //ellers kan vi som her lave det om til en string, og via regular expression hente dets id
            if (dgAirports.SelectedItem != null)
            {
                var airport = _fService.GetAirportByID(GetSelectedAirportID());
                ContentControlTitle.Content = _editTitle;
                contentControl.Content = new GridEditAirport(airport); 
            }
        }
    }
}
