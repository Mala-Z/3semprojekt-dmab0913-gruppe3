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
            dpDate.SelectedDate = DateTime.Now;
            dpDate.DisplayDateStart = DateTime.Now;
            dpDate.DisplayDateEnd = new DateTime(2015, 2, 28);
        }

        private void InitializeComboboxes()
        {
            cbCustomer.ItemsSource = ComboBoxItems.CustomerItems();
            cbFrom.ItemsSource = ComboBoxItems.AirportItems();
            cbTo.ItemsSource = ComboBoxItems.AirportItems();
        }

        private void bFindFlights_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                FlightService.Person customer =
                    fService.GetPersonByID(Int32.Parse(((ComboBoxItem) cbCustomer.SelectedItem).Tag.ToString()));
                FlightService.Airport fromA =
                    fService.GetAirportByID(Int32.Parse(((ComboBoxItem) cbFrom.SelectedItem).Tag.ToString()));
                FlightService.Airport toA =
                    fService.GetAirportByID(Int32.Parse(((ComboBoxItem) cbTo.SelectedItem).Tag.ToString()));
                int noOfPass = Int32.Parse(txtNoOfPass.Text);

                if (txtNoOfPass.Text != "" && dpDate.SelectedDate != null)
                {
                    if (noOfPass >= 1)
                    {
                        if (fromA.airportID != toA.airportID)
                        {
                            contentControl.Content = new GridFlightRoutes(customer, fromA, toA, dpDate.SelectedDate.ToString().Substring(0, 10), noOfPass);
                        }
                        else
                        {
                            MainWindow.ErrorMsg("Rejs fra og rejs til skal være to forskellige lufthavne");
                        }
                        
                    }
                    else
                    {
                        MainWindow.ErrorMsg("Der skal mindst være 1 passager");
                    }

                }
                else
                {
                    MainWindow.ErrorMsg("Alle felterne skal være udfyldt før du kan søge");
                }

                
            }
            catch (NullReferenceException err)
            {
                MainWindow.ErrorMsg("Alle felterne skal være udfyldt før du kan søge ");
            }
            catch (FormatException err)
            {
                MainWindow.ErrorMsg("Antal passager skal være et tal");
            }
            
        }

        
    }
}
