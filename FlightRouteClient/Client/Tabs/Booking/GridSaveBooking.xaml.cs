using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Booking
{
    public partial class GridSaveBooking : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly FlightService.Airport _from;
        private readonly FlightService.Airport _to;
        private readonly string _date;
        private readonly int _noOfPass;
        private readonly List<FlightService.Flight> _flights;
        private readonly List<FlightService.Person> _passengerList;
        private bool _isPassengersAdded;

        public GridSaveBooking(FlightService.Person customer, FlightService.Airport @from, FlightService.Airport to, string date, int noOfPass, List<FlightService.Flight> flights)
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            _passengerList = new List<FlightService.Person> {customer};
            _from = @from;
            _to = to;
            _date = date;
            _noOfPass = noOfPass;
            _flights = flights;
            InitializeTxtboxes();
            InitializeGridData();

            btnNewBooking.Visibility = Visibility.Hidden;

            if (_passengerList.Count < noOfPass)
            {
                contentControl.Content = new GridAddPassenger(this);
            }
            else
            {
                contentControl.Content = null;
                btnCreate.Visibility = Visibility.Visible;
            }

            
        }

        public void InitializeTxtboxes()
        {
            txtFrom.Text = _from.name;
            txtTo.Text = _to.name;
            txtDate.Text = _date;
            txtNoOfPass.Text = _noOfPass.ToString();
        }

        public void InitializeGridData()
        {
            var result = from f in _flights
                         select new
                         {
                             Fra = _fService.GetAirportByID(f.@from).name,
                             Til = _fService.GetAirportByID(f.@to).name,
                             Afgang = f.timeOfDeparture,
                             Ankomst = f.timeOfArrival,
                             Rejsetid = f.traveltime,
                             Pris = f.price,
                             TotalPris = f.price * _noOfPass
                         };
            dgChosen.ItemsSource = result;

            var fTotalCost = (from f in _flights
                              select f.price * _noOfPass).Sum();

            var fTotalTime = (from f in _flights
                              select f.traveltime).Sum();

            txtTotalCost.Text = fTotalCost.ToString();
            txtTotalTime.Text = fTotalTime.ToString();

            var passengers = from p in _passengerList
                select new
                {
                    Fornavn = p.fname,
                    Efternavn = p.lname
                };
            dgPassengers.ItemsSource = passengers;

        }

        public void AddPassengerToList(FlightService.Person passenger)
        {
            _passengerList.Add(passenger);
            var passengers = from p in _passengerList
                          select new
                          {
                              Fornavn = p.fname,
                              Efternavn = p.lname
                          };
            dgPassengers.ItemsSource = passengers;

            if (_passengerList.Count < _noOfPass)
            {
                contentControl.Content = new GridAddPassenger(this);
            }
            else
            {
                contentControl.Content = null;
                _isPassengersAdded = true;
                ContentControlSuccess.Content = null;
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).contentBooking.Content = new TabBooking();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (_isPassengersAdded) 
            { 
                FlightService.Flight[] fl = _flights.ToArray();
                FlightService.Person[] pl = _passengerList.ToArray();
                if (_fService.CreateNewBooking(fl, pl, txtTotalTime.Text, Double.Parse(txtTotalCost.Text)))
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Booking blev oprettet!");
                    btnCreate.Visibility = Visibility.Hidden;
                    btnCancel.Visibility = Visibility.Hidden;
                    btnNewBooking.Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow.ErrorMsg("Bookingen blev ikke oprettet. En af flyforbindelserne har ikke nok pladser.");
                }
            }
            else
            {
                ContentControlSuccess.Content = new DisplayError("Tilføj alle passagerer!");
            }
        }
    }
}
