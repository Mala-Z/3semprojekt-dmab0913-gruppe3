using System;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;
using Client.Tabs.Booking;

namespace Client.Tabs
{
    public partial class TabBooking : UserControl
    {
        private readonly FlightServiceClient _fService;

        public TabBooking()
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            InitializeComboboxes();
            dpDate.SelectedDate = DateTime.Now;
            dpDate.DisplayDateStart = DateTime.Now;
            dpDate.DisplayDateEnd = new DateTime(2015, 2, 28);
            txtNoOfPass.Text = "1";
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
                    _fService.GetPersonByID(Int32.Parse(((ComboBoxItem) cbCustomer.SelectedItem).Tag.ToString()));
                FlightService.Airport @from =
                    _fService.GetAirportByID(Int32.Parse(((ComboBoxItem) cbFrom.SelectedItem).Tag.ToString()));
                FlightService.Airport to =
                    _fService.GetAirportByID(Int32.Parse(((ComboBoxItem) cbTo.SelectedItem).Tag.ToString()));
                int noOfPass = Int32.Parse(txtNoOfPass.Text);

                if (txtNoOfPass.Text != "" && dpDate.SelectedDate != null)
                {
                    if (noOfPass >= 1)
                    {
                        if (@from.airportID != to.airportID)
                        {
                            contentControl.Content = new GridFlightRoutes(customer, @from, to, dpDate.SelectedDate.ToString().Substring(0, 10), noOfPass);
                        }
                        else
                        {
                            MainWindow.ErrorMsg("Fra og til skal være forskellige lufthavne");
                        }
                        
                    }
                    else
                    {
                        MainWindow.ErrorMsg("Der skal mindst være 1 passager");
                    }

                }
                else
                {
                    MainWindow.ErrorMsg("Alle felter skal være udfyldt før du kan søge");
                }

                
            }
            catch (NullReferenceException err)
            {
                MainWindow.ErrorMsg("Alle felter skal være udfyldt før du kan søge");
            }
            catch (FormatException err)
            {
                MainWindow.ErrorMsg("Antal passagerer skal være et tal");
            }
        }
    }
}
