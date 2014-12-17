using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Client.FlightService;
using Client.Helpers;
using Client.Tabs.BookingList;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabBookingList.xaml
    /// </summary>
    public partial class TabBookingList : UserControl
    {
        private readonly FlightServiceClient _fService;
        private ContentTitle _title;

        public TabBookingList()
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            LoadGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).SwitchToBookingTab();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            LoadGridData();
        }

        private void LoadGridData()
        {
            Action workAction = () =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, args) =>
                {
                    args.Result = _fService.GetAllBookings().Select(b => new
                    {
                        ID = b.bookingID,
                        Tid = b.totalTime,
                        Pris = b.totalPrice,
                        Personer = _fService.GetBookingPassengers(b.bookingID).ToList().Count,
                        Flyforbindelser = _fService.GetBookingFlights(b.bookingID).ToList().Count
                    });

                };
                worker.RunWorkerCompleted += (o, args) => { dgBookings.ItemsSource = (IEnumerable) args.Result; };
                worker.RunWorkerAsync();
            };
            dgBookings.Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction);
        }

        private void DatePickerBookingsGrid_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Ikke implementeret");
            //TODO
        }

        private void DgBookings_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBookings.SelectedItem != null)
            {
                _title = new ContentTitle("Vis booking");
                ContentControlTitle.Content = _title;
                ContentControlAddEdit.Content = new ShowBooking(_fService.GetBookingByID(GetSelectedBookingID()));
            }
        }

        private int GetSelectedBookingID()
        {
            String bookingString = dgBookings.SelectedItem.ToString().Trim();
            //brug regex til at få første digit fra string
            int id = Convert.ToInt32(Regex.Match(bookingString, @"\d+").ToString());
            return id;
        }
    }
}