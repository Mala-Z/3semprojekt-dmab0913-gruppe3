using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;
using Client.Tabs.Airplane;

namespace Client.Tabs
{
    public partial class TabAirplane : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly GridAddAirplane _gridAddAirplane = new GridAddAirplane();
        private readonly ContentTitle _addTitle = new ContentTitle("Tilføj nyt fly");
        private readonly ContentTitle _editTitle = new ContentTitle("Rediger fly");

        public TabAirplane()
        {
            InitializeComponent();
            ContentControlTitle.Content = _addTitle;
            ContentControlAddEdit.Content = _gridAddAirplane;
            _fService = new FlightServiceClient();
            InitGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            //ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);

        }

        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    bool success = false;
        //    int airplaneId = GetSelectedAirplaneID();
        //    var warningBox = MessageBox.Show("Vil du slette fly med ID: " + airplaneId + "?", "Slet",
        //        MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
        //    if (warningBox == MessageBoxResult.Yes)
        //        success = _fService.DeleteFlight(airplaneId);
        //    if (success)
        //    {
        //        InitGridData();
        //    }
        //    else
        //    {
        //        MainWindow.ErrorMsg("Fly ikke slettet");
        //    }
        //}

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = _addTitle;
            ContentControlAddEdit.Content = _gridAddAirplane;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            InitGridData();
        }

        public void InitGridData()
        {
            var result = from a in _fService.GetAllAirplanes()
                         select new { ID = a.airplaneID, Sæder = a.seats};

            dgAirplanes.ItemsSource = result;

        }

        private void tSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from a in _fService.GetAllAirplanes()
                where a.airplaneID.ToString().Contains(txtSearch.Text)
                         select new { ID = a.airplaneID, Sæder = a.seats};

            dgAirplanes.ItemsSource = result;
        }

        private void dgAirports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgAirports indeholder anonyme objekter. Enten skal vi lave en ny class og caste det anonyme objekt dertil,
            //eller bruge regex til at hente ID
            if (dgAirplanes.SelectedItem != null)
            {
                var airplane = _fService.GetAirplaneByID(GetSelectedAirplaneID());
                ContentControlTitle.Content = _editTitle;
                ContentControlAddEdit.Content = new GridEditAirplane(airplane);
            }

        }

        private int GetSelectedAirplaneID()
        {
            String airplaneString = dgAirplanes.SelectedItem.ToString().Trim();
            int airplaneID = Convert.ToInt32(Regex.Match(airplaneString, @"\d+").ToString());
            return airplaneID;
        }
    }
}
