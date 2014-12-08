using System;
using System.Collections.Generic;
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
using Client.Helpers;
using Client.Tabs.Airplane;
using Client.Tabs.Airport;
using Client.Tabs.Flight;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// </summary>
    public partial class TabAirplane : UserControl
    {
        private FlightServiceClient fService;
        private GridAddAirplane gridAddAirplane = new GridAddAirplane();
        private ContentTitle addTitle = new ContentTitle("Tilføj nyt fly");
        private ContentTitle editTitle = new ContentTitle("Rediger fly");

        public TabAirplane()
        {
            InitializeComponent();
            ContentControlTitle.Content = addTitle;
            ContentControlAddEdit.Content = gridAddAirplane;
            fService = new FlightServiceClient();
            InitGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);

        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            bool success = false;
            int airplaneID = GetSelectedAirplaneID();
            var warningBox = MessageBox.Show("Vil du slette fly med ID: " + airplaneID + "?", "Slet",
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (warningBox == MessageBoxResult.Yes)
                success = fService.DeleteFlight(airplaneID);
            if (!success)
                MainWindow.ErrorMsg("Fly ikke slettet");
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = addTitle;
            ContentControlAddEdit.Content = gridAddAirplane;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            InitGridData();
        }

        public void InitGridData()
        {
            var result = from a in fService.GetAllAirplanes()
                         select new { ID = a.airplaneID, Sæder = a.seats};

            dgAirplanes.ItemsSource = result;

        }

        private void tSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from a in fService.GetAllAirplanes()
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
                var airplane = fService.GetAirplaneByID(GetSelectedAirplaneID());
                ContentControlTitle.Content = editTitle;
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
