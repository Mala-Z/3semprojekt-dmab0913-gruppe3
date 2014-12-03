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
            try
            {
                FlightService.Person customer =
                    fService.GetPersonByID(Int32.Parse(((ComboBoxItem) cbCustomer.SelectedItem).Tag.ToString()));
                FlightService.Airport fromA =
                    fService.GetAirportByID(Int32.Parse(((ComboBoxItem) cbFrom.SelectedItem).Tag.ToString()));
                FlightService.Airport toA =
                    fService.GetAirportByID(Int32.Parse(((ComboBoxItem) cbTo.SelectedItem).Tag.ToString()));

                if (txtNoOfPass.Text != "" && dpDate.SelectedDate != null)
                {
                    if (Int32.Parse(txtNoOfPass.Text) >= 1)
                    {
                        if (fromA.airportID != toA.airportID)
                        {
                            contentControl.Content = new GridFlightRoutes();
                        }
                        else
                        {
                            ErrorMessage(4);
                        }
                        
                    }
                    else
                    {
                        ErrorMessage(2);
                    }

                }
                else
                {
                    ErrorMessage(1);
                }
            }
            catch (NullReferenceException err)
            {
                ErrorMessage(1);
            }
            catch (FormatException err)
            {
                ErrorMessage(3);
            }
            
        }

        private void ErrorMessage(int errNo)
        {
            string messageBoxText = "";
            switch (errNo)
            {
                case 1:
                    messageBoxText = "Alle felterne skal være udfyldt før du kan søge";
                    break;
                case 2:
                    messageBoxText = "Der skal mindst være 1 passager";
                    break;
                case 3:
                    messageBoxText = "Antal passager skal være et tal";
                    break;
                case 4:
                    messageBoxText = "Rejs fra og rejs til skal være to forskellige lufthavne";
                    break;
                default:
                    messageBoxText = "Alle felterne skal være udfyldt før du kan søge";
                    break;
            }
            string caption = "Fejl";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        
    }
}
