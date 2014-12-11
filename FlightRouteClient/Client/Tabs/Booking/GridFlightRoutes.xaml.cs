using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
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

namespace Client.Tabs.Booking
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridFlightRoutes : UserControl
    {
        private FlightServiceClient fService;
        private FlightService.Person customer;
        private FlightService.Airport fromA;
        private FlightService.Airport toA;
        private string date;
        private int noOfPass;
        private List<FlightService.Flight> fastestRoute;
        private List<FlightService.Flight> cheapestRoute;


        public GridFlightRoutes(FlightService.Person customer, FlightService.Airport fromA, FlightService.Airport toA, string date, int noOfPass)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.customer = customer;
            this.fromA = fromA;
            this.toA = toA;
            this.date = date;

            InitializeGridData();
           
        }

        private IEnumerable<FlightService.Flight> RunDijkstra(FlightService.Airport fromA, FlightService.Airport toA, string date, bool usePrice)
        {
            var result = fService.RunDijkstra(fromA, toA, date, usePrice);
            return result;
        }

        private void InitializeGridData()
        {
            dgFastest.ItemsSource = null;
            var fastestsList = fService.RunDijkstraFastest(fromA, toA, date);
            fastestRoute = fastestsList.ToList();
            var result = from f in fastestsList
                         select new
                         {
                             Fra = fService.GetAirportByID(f.@from).name,
                             Til = fService.GetAirportByID(f.@to).name,
                             Afgang = f.timeOfDeparture,
                             Ankomst = f.timeOfArrival,
                             Rejsetid = f.traveltime,
                             Pris = f.price,
                             TotalPris = f.price * noOfPass
                         };

            dgFastest.ItemsSource = result;
            var fTotalCost = (from f in fastestsList
                       select f.price*noOfPass).Sum();
            var fTotalTime = (from f in fastestsList
                               select f.traveltime).Sum();
            txtFTotalCost.Text = fTotalCost.ToString();
            txtFTotalTime.Text = fTotalTime.ToString();

            dgCheapest.ItemsSource = null;
            var cheapestList = fService.RunDijkstraCheapest(fromA, toA, date);
            cheapestRoute = cheapestList.ToList();
            var result2 = from f in cheapestList
                         select new
                         {
                             Fra = fService.GetAirportByID(f.@from).name,
                             Til = fService.GetAirportByID(f.@to).name,
                             Afgang = f.timeOfDeparture,
                             Ankomst = f.timeOfArrival,
                             Rejsetid = f.traveltime,
                             Pris = f.price,
                             TotalPris = f.price * noOfPass
                         };
            
            dgCheapest.ItemsSource = result2;
            var cTotalCost = (from f in cheapestList
                              select f.price * noOfPass).Sum();
            var cTotalTime = (from f in cheapestList
                              select f.traveltime).Sum();
            txtCTotalCost.Text = cTotalCost.ToString();
            txtCTotalTime.Text = cTotalTime.ToString();

            if (fastestRoute.Count == 0 && cheapestRoute.Count == 0)
            {
                MainWindow.ErrorMsg("Der er ingen ledige flyruter denne dag");
            }

        }

        private void bChooseFastest_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute(fastestRoute);
        }

        private void bChooseCheapest_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute(cheapestRoute);
        }

        private void ChooseRoute(List<FlightService.Flight> flights)
        {
            ((MainWindow) Application.Current.MainWindow).contentBooking.Content = new GridSaveBooking(customer, fromA,
                toA, date, noOfPass, flights);
        }

       
    }
}
