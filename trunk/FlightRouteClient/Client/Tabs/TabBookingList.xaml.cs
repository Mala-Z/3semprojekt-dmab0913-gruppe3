using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private readonly FlightServiceClient fService;
        private ContentTitle editTitle;

        public TabBookingList()
        {
            InitializeComponent();
            fService = new FlightServiceClient();
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
                    args.Result = fService.GetAllBookings().Select(b => new
                    {
                        ID = b.bookingID,
                        Tid = b.totalTime,
                        Pris = b.totalPrice,
                        Personer = fService.GetBookingPassengers(b.bookingID).ToList().Count,
                        Flyforbindelser = fService.GetBookingFlights(b.bookingID).ToList().Count
                    });

                };
                worker.RunWorkerCompleted += (o, args) => { dgBookings.ItemsSource = (IEnumerable) args.Result; };
                worker.RunWorkerAsync();
            };
            dgBookings.Dispatcher.BeginInvoke(DispatcherPriority.Background, workAction);
        }

        private void DatePickerBookingsGrid_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DgBookings_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBookings.SelectedItem != null)
            {
                editTitle = new ContentTitle("Rediger booking");
                ContentControlTitle.Content = editTitle;
                ContentControlAddEdit.Content = new EditBooking(fService.GetBookingByID(GetSelectedBookingID()));
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