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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TabAirplane tAirplane { get; set; }
        public TabAirport tAirport { get; set; }
        public TabBooking tBooking { get; set; }
        public TabCustomer tCustomer { get; set; }
        public TabFlight tFlight { get; set; }

        public MainWindow()
        {
            CultureInfo ci = new CultureInfo("da-DK");
            ci.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();
            InitializeTabs();
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
    }
}
