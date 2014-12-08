using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.FlightService;
using Client.Helpers;
using Client.Tabs.Booking;

namespace Client.Tabs.Booking
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// </summary>
    public partial class GridSaveBooking : UserControl
    {
        private FlightServiceClient fService;
        private FlightService.Person customer;
        private FlightService.Airport fromA;
        private FlightService.Airport toA;
        private string date;
        private int noOfPass;
        private List<FlightService.Flight> flights;
        private List<FlightService.Person> passengerList;

        public GridSaveBooking(FlightService.Person customer, FlightService.Airport fromA, FlightService.Airport toA, string date, int noOfPass, List<FlightService.Flight> flights)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            passengerList = new List<FlightService.Person>();
            passengerList.Add(customer);
            this.customer = customer;
            this.fromA = fromA;
            this.toA = toA;
            this.date = date;
            this.noOfPass = noOfPass;
            this.flights = flights;
            InitializeTxtboxes();
            InitializeGridData();

            btnCreate.Visibility = Visibility.Hidden;

            if (passengerList.Count < noOfPass)
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
            txtFrom.Text = fromA.name;
            txtTo.Text = toA.name;
            txtDate.Text = date;
            txtNoOfPass.Text = noOfPass.ToString();
        }

        public void InitializeGridData()
        {
            dgChosen.ItemsSource = null;
            var chosenList = flights;
            var result = from f in chosenList
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

            dgChosen.ItemsSource = result;
            var fTotalCost = (from f in chosenList
                              select f.price * noOfPass).Sum();
            var fTotalTime = (from f in chosenList
                              select f.traveltime).Sum();
            txtTotalCost.Text = fTotalCost.ToString();
            txtTotalTime.Text = fTotalTime.ToString();

            dgPassengers.ItemsSource = null;
            var result2 = from p in passengerList
                select new
                {
                    Fornavn = p.fname,
                    Efternavn = p.lname
                };
            dgPassengers.ItemsSource = result2;

        }

        public void AddPassengerToList(FlightService.Person passenger)
        {
            passengerList.Add(passenger);
            dgPassengers.ItemsSource = null;
            var result2 = from p in passengerList
                          select new
                          {
                              Fornavn = p.fname,
                              Efternavn = p.lname
                          };
            dgPassengers.ItemsSource = result2;

            if (passengerList.Count < noOfPass)
            {
                contentControl.Content = null;
                contentControl.Content = new GridAddPassenger(this);
            }
            else
            {
                contentControl.Content = null;
                btnCreate.Visibility = Visibility.Visible;
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) System.Windows.Application.Current.MainWindow).contentBooking.Content = new TabBooking();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            FlightService.Flight[] fl = flights.ToArray();
            FlightService.Person[] pl = passengerList.ToArray();
            fService.CreateNewBooking(fl, pl, txtTotalTime.Text, Double.Parse(txtTotalCost.Text));
        }

        

        
    }
}
