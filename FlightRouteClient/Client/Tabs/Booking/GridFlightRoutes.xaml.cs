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


        public GridFlightRoutes(FlightService.Person customer, FlightService.Airport fromA, FlightService.Airport toA, string date, int noOfPass)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.customer = customer;
            this.fromA = fromA;
            this.toA = toA;
            this.date = date;
            this.noOfPass = noOfPass;

            InitializeGridData();
             
        }

        private IEnumerable<FlightService.Vertex> RunDijkstra(FlightService.Airport fromA, FlightService.Airport toA, string date, bool usePrice)
        {
            var result = fService.RunDijkstra(fromA, toA, date, usePrice);
            return result;
        }

        private void InitializeGridData()
        {
            //IEnumerable<Vertex> shortestPathByPriceList = null;
            //IEnumerable<Vertex> shortestPathByTravelTimeList = null;
            ////Create threads that runs RunDijkstra and saves to variables
            //var shortestPathByPriceThread = new Thread(() => shortestPathByPriceList = RunDijkstra(fromA, toA, "03-12-2014", true));
            //var shortestPathByTravelTimeThread = new Thread(() => shortestPathByTravelTimeList = RunDijkstra(fromA, toA, "03-12-2014", false));
            ////Start threads
            //shortestPathByPriceThread.SetApartmentState(ApartmentState.STA);
            //shortestPathByTravelTimeThread.SetApartmentState(ApartmentState.STA);

            //shortestPathByPriceThread.Start();
            //shortestPathByTravelTimeThread.Start();
            ////Join threads
            //shortestPathByPriceThread.Join();
            //shortestPathByTravelTimeThread.Join();

            var result = from sp in RunDijkstra(fromA, toA, "03-12-2014", false)
                         select new
                         {
                            Fra = sp.EdgeToUse.From.airport.name,
                            Til = sp.EdgeToUse.To.airport.name,
                            Afgang = sp.EdgeToUse.VertexEdge.timeOfDeparture,
                            Ankomst = sp.EdgeToUse.VertexEdge.timeOfArrival,
                            Rejsetid = sp.EdgeToUse.VertexEdge.traveltime,
                            Pris = sp.EdgeToUse.VertexEdge.price
                         };

            dgFastest.ItemsSource = result;

            //var result2 = from sp in RunDijkstra(fromA, toA, "03-12-2014", true)
            //             select new
            //             {
            //                 Fra = sp.EdgeToUse.From.airport.name,
            //                 Til = sp.EdgeToUse.To.airport.name,
            //                 Afgang = sp.EdgeToUse.VertexEdge.timeOfDeparture,
            //                 Ankomst = sp.EdgeToUse.VertexEdge.timeOfArrival,
            //                 Rejsetid = sp.EdgeToUse.VertexEdge.traveltime,
            //                 Pris = sp.EdgeToUse.VertexEdge.price
            //             };

            //dgCheapest.ItemsSource = result2;

        }

       
    }
}
