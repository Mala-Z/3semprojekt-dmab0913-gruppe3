using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Client.FlightService;

namespace Client.Tabs.Booking
{
    public partial class GridFlightRoutes : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly FlightService.Person _customer;
        private readonly FlightService.Airport _from;
        private readonly FlightService.Airport _to;
        private readonly string _date;
        private readonly int _noOfPass;
        private List<FlightService.Flight> _fastestRoute;
        private List<FlightService.Flight> _cheapestRoute;


        public GridFlightRoutes(FlightService.Person customer, FlightService.Airport @from, FlightService.Airport to, string date, int noOfPass)
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            _customer = customer;
            _from = @from;
            _to = to;
            _date = date;
            _noOfPass = noOfPass;

            InitializeGridData();
           
        }

        private IEnumerable<FlightService.Flight> RunDijkstra(FlightService.Airport @from, FlightService.Airport to, string date, bool usePrice)
        {
            var result = _fService.RunDijkstra(@from, to, date, usePrice);
            return result;
        }

        private void InitializeGridData()
        {
            Action fastestRouteAction = () =>
            {
                BackgroundWorker fastestRouteWorker = new BackgroundWorker();
                fastestRouteWorker.DoWork += (o, args) => args.Result = GetFastestRoute();
                fastestRouteWorker.RunWorkerCompleted += (o, args) =>
                {
                    dgFastest.ItemsSource = (IEnumerable)args.Result;
                    var fTotalCost = (from f in _fastestRoute
                                      select f.price * _noOfPass).Sum();
                    var fTotalTime = (from f in _fastestRoute
                                      select f.traveltime).Sum();
                    txtFTotalCost.Text = fTotalCost.ToString();
                    txtFTotalTime.Text = fTotalTime.ToString();

                };
                fastestRouteWorker.RunWorkerAsync();
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Background, fastestRouteAction);

            Action cheapestRouteAction = () =>
            {
                BackgroundWorker cheapesRouteWorker = new BackgroundWorker();
                cheapesRouteWorker.DoWork += (o, args) => args.Result = GetCheapestRoute();
                cheapesRouteWorker.RunWorkerCompleted += (o, args) =>
                {
                    dgCheapest.ItemsSource = (IEnumerable)args.Result;
                    var cTotalCost = (from f in _cheapestRoute
                                      select f.price * _noOfPass).Sum();
                    var cTotalTime = (from f in _cheapestRoute
                                      select f.traveltime).Sum();
                    txtCTotalCost.Text = cTotalCost.ToString();
                    txtCTotalTime.Text = cTotalTime.ToString();
                };
                cheapesRouteWorker.RunWorkerAsync();
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Background, cheapestRouteAction);


            //if (fastestRoute.Count == 0 && cheapestRoute.Count == 0)
            //{
            //    MainWindow.ErrorMsg("Der er ingen ledige flyruter denne dag");
            //}

        }

        private IEnumerable<Object> GetFastestRoute()
        {
            
            var fastestsList = _fService.RunDijkstraFastest(_from, _to, _date);
            _fastestRoute = fastestsList.ToList();
            var result = fastestsList.Select(f => new
            {
                Fra = _fService.GetAirportByID(f.@from).name,
                Til = _fService.GetAirportByID(f.@to).name,
                Afgang = f.timeOfDeparture,
                Ankomst = f.timeOfArrival,
                Rejsetid = f.traveltime,
                Ledige_Pladser = _fService.GetAirplaneByID(Convert.ToInt32(f.airplaneID)).seats - f.takenSeats,
                Pris = f.price,
                TotalPris = f.price*_noOfPass
            });
            return result;
        }

        private IEnumerable<Object> GetCheapestRoute()
        {
            var cheapestList = _fService.RunDijkstraCheapest(_from, _to, _date);
            _cheapestRoute = cheapestList.ToList();
            var result = cheapestList.Select(f => new
            {
                Fra = _fService.GetAirportByID(f.@from).name,
                Til = _fService.GetAirportByID(f.@to).name,
                Afgang = f.timeOfDeparture,
                Ankomst = f.timeOfArrival,
                Rejsetid = f.traveltime,
                Ledige_Pladser = _fService.GetAirplaneByID(Convert.ToInt32(f.airplaneID)).seats - f.takenSeats,
                Pris = f.price,
                TotalPris = f.price*_noOfPass
            });
            return result;
        }

        private void btnChooseFastest_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute(_fastestRoute);
        }

        private void btnChooseCheapest_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute(_cheapestRoute);
        }

        private void ChooseRoute(List<FlightService.Flight> flights)
        {
            ((MainWindow) Application.Current.MainWindow).contentBooking.Content = new GridSaveBooking(_customer, _from,
                _to, _date, _noOfPass, flights);
        }
    }
}
