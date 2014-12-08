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
using Client.Tabs.Airport;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// http://www.wpftutorial.net/DataGrid.html
    /// </summary>
    public partial class TabAirport : UserControl
    {
        private FlightServiceClient fService;
        private GridAddAirport gridAddAirport = new GridAddAirport();
        private ContentTitle addTitle = new ContentTitle("Tilføj ny lufthavn");
        private ContentTitle editTitle = new ContentTitle("Rediger lufthavn");

        public TabAirport()
        {
            InitializeComponent();
            ContentControlTitle.Content = addTitle;
            contentControl.Content = gridAddAirport;
            fService = new FlightServiceClient();
            InitGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            bool success = false;
            int airportID = GetSelectedAirportID();
            var warningBox = MessageBox.Show("Vil du slette lufthavnen med ID: " + airportID + "?", "Slet",
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (warningBox == MessageBoxResult.Yes)
                success = fService.DeleteAirport(airportID);
            if (!success)
                MainWindow.ErrorMsg("Lufthavn ikke slettet");

            ((MainWindow)System.Windows.Application.Current.MainWindow).tAirport.InitGridData();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = addTitle;
            contentControl.Content = gridAddAirport;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            InitGridData();
        }

        public void InitGridData()
        {
            //dgAirports.ItemsSource = fService.GetAllAirports();

            var result = from a in fService.GetAllAirports()
                        select new { ID = a.airportID, Navn = a.name, Lokation = a.location };

            dgAirports.ItemsSource = result;

        }

        private void tSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from a in fService.GetAllAirports()
                         where a.name.ToLower().Contains(txtSearch.Text.ToLower()) || a.location.ToLower().Contains(txtSearch.Text.ToLower())
                         select new { ID = a.airportID, Navn = a.name, Lokation = a.location };

            dgAirports.ItemsSource = result;
            
            //Application.Current.MainWindow 


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
            //dgAirports indeholder anonyme objecter. Enten skal vi lave en ny class og caste det anonyme object dertil,
            //ellers kan vi som her lave det om til en string, og via regular expression hente dets id
            if (dgAirports.SelectedItem != null)
            {
                String airportString = dgAirports.SelectedItem.ToString().Trim();
                int airportID = Convert.ToInt32(Regex.Match(airportString, @"\d+").ToString());
                var airport = fService.GetAirportByID(airportID);
                ContentControlTitle.Content = editTitle;
                contentControl.Content = new GridEditAirport(airport); 
            }
            
        }

        

    }
}
