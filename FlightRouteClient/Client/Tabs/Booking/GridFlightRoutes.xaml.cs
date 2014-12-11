﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
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
            this.noOfPass = noOfPass;

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
            dgCheapest.ItemsSource = null;
            Action workAction = () =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, args) =>
                {
                    args.Result = GetFastestRoute();
                };
                //worker.DoWork(() => GetFastestRoute());
                worker.RunWorkerCompleted += (o, args) =>
                {
                    dgFastest.ItemsSource = (IEnumerable)args.Result;
                    var fTotalCost = (from f in fastestRoute
                                      select f.price * noOfPass).Sum();
                    var fTotalTime = (from f in fastestRoute
                                      select f.traveltime).Sum();
                    txtFTotalCost.Text = fTotalCost.ToString();
                    txtFTotalTime.Text = fTotalTime.ToString();

                };
                worker.RunWorkerAsync();
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction);

            Action workAction2 = () =>
            {
                BackgroundWorker worker2 = new BackgroundWorker();
                worker2.DoWork += (o, args) =>
                {
                    args.Result = GetCheapestRoute();
                };
                worker2.RunWorkerCompleted += (o, args) =>
                {
                    dgCheapest.ItemsSource = (IEnumerable)args.Result;
                    var cTotalCost = (from f in cheapestRoute
                                      select f.price * noOfPass).Sum();
                    var cTotalTime = (from f in cheapestRoute
                                      select f.traveltime).Sum();
                    txtCTotalCost.Text = cTotalCost.ToString();
                    txtCTotalTime.Text = cTotalTime.ToString();
                };
                worker2.RunWorkerAsync();
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction2);


            //if (fastestRoute.Count == 0 && cheapestRoute.Count == 0)
            //{
            //    MainWindow.ErrorMsg("Der er ingen ledige flyruter denne dag");
            //}

        }

        private IEnumerable<Object> GetFastestRoute()
        {
            
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
            return result;
        }

        private IEnumerable<Object> GetCheapestRoute()
        {
            var cheapestList = fService.RunDijkstraCheapest(fromA, toA, date);
            cheapestRoute = cheapestList.ToList();
            var result = from f in cheapestList
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
            return result;
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
