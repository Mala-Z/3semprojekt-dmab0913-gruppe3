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
using Client.Tabs.Airplane;
using Client.Tabs.Airport;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// </summary>
    public partial class TabAirplane : UserControl
    {
        private FlightServiceClient fService;

        public TabAirplane()
        {
            InitializeComponent();
            contentControl.Content = new GridAddAirplane();

            fService = new FlightServiceClient();

            InitializeGridData();

        }

        private void InitializeGridData()
        {
            //dgAirports.ItemsSource = fService.GetAllAirports();

            var result = from a in fService.GetAllAirplanes()
                         select new { ID = a.airplaneID, Sæder = a.seats};

            dgAirplanes.ItemsSource = result;

        }

        public void updateDataGrid()
        {
            InitializeGridData();
        }

        private void tSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from a in fService.GetAllAirplanes()
                where a.airplaneID.ToString().Contains(txtSearch.Text)
                         select new { ID = a.airplaneID, Sæder = a.seats};

            dgAirplanes.ItemsSource = result;

            //Application.Current.MainWindow 


        }

        private void dgAirports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgAirports indeholder anonyme objecter. Enten skal vi lave en ny class og caste det anonyme object dertil,
            //ellers kan vi som her lave det om til en string, og via regular expression hente dets id
            if (dgAirplanes.SelectedItem != null)
            {
                String airplaneString = dgAirplanes.SelectedItem.ToString().Trim();
                int airplaneID = Convert.ToInt32(Regex.Match(airplaneString, @"\d+").ToString());
                var airplane = fService.GetAirplaneByID(airplaneID);
                contentControl.Content = new GridEditAirplane(airplane);
            }

        }



    }
}
