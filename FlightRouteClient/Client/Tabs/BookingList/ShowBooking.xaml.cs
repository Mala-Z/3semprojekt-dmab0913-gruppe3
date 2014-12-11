using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Client.FlightService;

namespace Client.Tabs.BookingList
{
    /// <summary>
    /// Interaction logic for ShowBooking.xaml
    /// </summary>
    public partial class ShowBooking : UserControl
    {
        private readonly FlightService.Booking _booking;
        private readonly FlightServiceClient _fService = new FlightServiceClient(); 

        public ShowBooking(FlightService.Booking booking)
        {
            InitializeComponent();
            _booking = booking;
            LoadBookingPassengerData();
            LoadBookingFlightData();
            txtTraveltime.Text = _booking.totalTime;
            txtPrice.Text = _booking.totalPrice.ToString();
        }

        private void LoadBookingPassengerData()
        {
            Action workAction = () =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, args) => args.Result = GetPassengersToGrid();
                worker.RunWorkerCompleted += (o, args) => {DataGridCustomers.ItemsSource = (IEnumerable)args.Result; };
                worker.RunWorkerAsync();
            };
            DataGridCustomers.Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction);
        }

        private void LoadBookingFlightData()
        {
            Action workAction = () =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, args) => args.Result = GetFlightsToGrid();
                worker.RunWorkerCompleted += (o, args) => { DataGridFlights.ItemsSource = (IEnumerable)args.Result; };
                worker.RunWorkerAsync();
            };
            DataGridFlights.Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction);
        }

        private IEnumerable<Object> GetPassengersToGrid()
        {
            var result = from p in _fService.GetPersonsFromBooking(_booking.bookingID)
                         select new
                         {
                             ID = p.personID,
                             Fornavn = p.fname,
                             Efternavn = p.lname,
                             Adresse = p.address,
                             Køn = p.gender,
                             Telefon = p.phoneNo,
                             Email = p.email,
                             Fødselsdato = p.birthdate

                         };
            return result;
        }

        private IEnumerable<Object> GetFlightsToGrid()
        {
            var result = from f in _fService.GetFlightsFromBooking(_booking.bookingID)
                         select new
                         {
                             ID = f.flightID,
                             Fra = _fService.GetAirportByID(f.@from).name,
                             Til = _fService.GetAirportByID(f.@to).name,
                             Afgang = f.timeOfDeparture,
                             Ankomst = f.timeOfArrival
                         };
            return result;
        }
    }
}
