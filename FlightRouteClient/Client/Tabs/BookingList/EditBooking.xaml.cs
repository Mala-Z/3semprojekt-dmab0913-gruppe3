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
    /// Interaction logic for EditBooking.xaml
    /// </summary>
    public partial class EditBooking : UserControl
    {
        private FlightService.Booking booking;
        private FlightServiceClient fService = new FlightServiceClient(); 

        public EditBooking(FlightService.Booking booking)
        {
            InitializeComponent();
            this.booking = booking;
            LoadBookingPassengerData();
            LoadBookingFlightData();
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
            var result = from p in fService.GetPersonsFromBooking(booking.bookingID)
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
            var result = from f in fService.GetFlightsFromBooking(booking.bookingID)
                         select new
                         {
                             ID = f.flightID,
                             Fra = fService.GetAirportByID(f.@from).name,
                             Til = fService.GetAirportByID(f.@to).name,
                             Afgang = f.timeOfDeparture,
                             Ankomst = f.timeOfArrival
                         };
            return result;
        }
    }
}
