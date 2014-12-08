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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.Tabs;
using Client.Tabs.Booking;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TabAirplane tAirplane;
        public TabAirport tAirport;
        public TabBooking tBooking = new TabBooking();
        public TabCustomer tCustomer;
        public TabFlight tFlight;

        public MainWindow()
        {
            InitializeComponent();
            contentBooking.Content = tBooking;
            //InitializeTabs();
        }

        public void InitializeTabs()
        {
            tAirplane = new TabAirplane();
            tAirport = new TabAirport();
            tBooking = new TabBooking();
            tCustomer = new TabCustomer();
            tFlight = new TabFlight();

            contentAirplane.Content = tAirplane;
            contentAirport.Content = tAirport;
            contentBooking.Content = tBooking;
            contentCustomer.Content = tCustomer;
            contentFlight.Content = tFlight;
        }

        public static void ErrorMsg(string msg)
        {
            MessageBox.Show(msg, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabItemAirplane.IsSelected)
            {
                if (tAirplane == null)
                    tAirplane = new TabAirplane();
                contentAirplane.Content = tAirplane;
            }
            if (TabItemAirport.IsSelected)
            {
                if (tAirport == null)
                    tAirport = new TabAirport();
                contentAirport.Content = tAirport;
            }
            if (TabItemCustomer.IsSelected)
            {
                if (tCustomer == null)
                    tCustomer = new TabCustomer();
                contentCustomer.Content = tCustomer;
            }
            if (TabItemFlight.IsSelected)
            {
                if (tFlight == null)
                    tFlight = new TabFlight();
                contentFlight.Content = tFlight;
            }   
            
        }

    }
}
