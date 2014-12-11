using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Client.Tabs;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TabAirplane TabAirplane;
        public TabAirport TabAirport;
        public TabBooking TabBooking;
        public TabCustomer TabCustomer;
        public TabFlight TabFlight;
        public TabBookingList TabBookingList;

        public MainWindow()
        {
            
            try
            {
                InitializeComponent();
                TabBooking = new TabBooking();
                contentBooking.Content = TabBooking;
            }
            catch (EndpointNotFoundException)
            {
                ErrorMsg("Serveren blev ikke fundet. Tjek om FlightRouteSystem WCF service er startet");
            }
            
        }

        public static void ErrorMsg(string msg)
        {
            MessageBox.Show(msg, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabItemAirplane.IsSelected)
            {
                if (TabAirplane == null)
                    TabAirplane = new TabAirplane();
                contentAirplane.Content = TabAirplane;
            }
            if (TabItemAirport.IsSelected)
            {
                if (TabAirport == null)
                    TabAirport = new TabAirport();
                contentAirport.Content = TabAirport;
            }
            if (TabItemCustomer.IsSelected)
            {
                if (TabCustomer == null)
                    TabCustomer = new TabCustomer();
                contentCustomer.Content = TabCustomer;
            }
            if (TabItemFlight.IsSelected)
            {
                if (TabFlight == null)
                    TabFlight = new TabFlight();
                contentFlight.Content = TabFlight;
            }
            if (TabItemBookingList.IsSelected)
            {
                if (TabBookingList == null)
                    TabBookingList = new TabBookingList();
                contentBookingList.Content = TabBookingList;
            }
        }

        public void SwitchToBookingTab()
        {
            TabItemBooking.IsSelected = true;
        }

    }
}
