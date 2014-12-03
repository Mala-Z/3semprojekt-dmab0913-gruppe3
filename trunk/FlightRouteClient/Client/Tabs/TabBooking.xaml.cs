using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// </summary>
    public partial class TabBooking : UserControl
    {
        private FlightServiceClient fService;

        public TabBooking()
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            InitializeComboboxes();
        }

        private void InitializeComboboxes()
        {
            cbCustomer.ItemsSource = ComboBoxItems.CustomerItems();
            cbFrom.ItemsSource = ComboBoxItems.AirportItems();
            cbTo.ItemsSource = ComboBoxItems.AirportItems();
        }

        private void bFindFlights_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem) cbCustomer.SelectedItem;
            int id = (int) item.Content;
          
           
           // FlightService.Person customer = fService.GetPersonByID(1);
            contentControl.Content = new GridFlightRoutes();
            
        }

        
    }
}
