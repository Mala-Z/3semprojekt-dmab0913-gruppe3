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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Flight
{
    /// <summary>
    /// Interaction logic for GridEditFlight.xaml
    /// </summary>
    public partial class GridEditFlight : UserControl
    {
        private FlightServiceClient fService;
        private FlightService.Flight flight;

        public GridEditFlight(FlightService.Flight flight)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.flight = flight;
            InitComboBox();
        }

        private void InitComboBox()
        {
            cbFrom.ItemsSource = ComboBoxItems.AirportItems();
            cbTo.ItemsSource = ComboBoxItems.AirportItems();
            cbAirplane.ItemsSource = ComboBoxItems.AirplaneItems();
            InsertFlightData();
        }

        private void InsertFlightData()
        {
            if (flight.from > 0 && flight.to > 0)
            {
                cbFrom.SelectedIndex = Convert.ToInt32(flight.from -1);
                cbTo.SelectedIndex = Convert.ToInt32(flight.to -1);
                DatePickerDeparture.SelectedDate = DateTime.Parse(flight.timeOfDeparture);
                DatePickerArrival.SelectedDate = DateTime.Parse(flight.timeOfArrival);
                txtTravelTime.Text = flight.traveltime.ToString();
                txtPrice.Text = flight.price.ToString();
                cbAirplane.SelectedIndex = Convert.ToInt32(flight.airplaneID - 1);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double price = 0.0;
            bool stop = false;
            bool success = false;

            try
            {
                price = System.Convert.ToDouble(txtPrice.Text);
            }
            catch (Exception)
            {
                MainWindow.ErrorMsg("Pris skal være et tal");
                stop = true;
            }

            if (!stop && cbTo.SelectedItem.ToString() == cbFrom.SelectedItem.ToString())
            {
                MainWindow.ErrorMsg("Fra og til skal være forskellige!");
                stop = true;
            }


            if (!stop &&
                cbTo.SelectedItem != null &&
                cbFrom.SelectedItem != null &&
                DatePickerDeparture.SelectedDate.HasValue &&
                DatePickerArrival.SelectedDate.HasValue &&
                txtTravelTime.Text != "" &&
                txtPrice.Text != "" &&
                cbAirplane.SelectedItem != null)
            {
                success = fService.UpdateFlight(flight.flightID,
                    DatePickerDeparture.SelectedDate.ToString(),
                        DatePickerArrival.SelectedDate.ToString(),
                        System.Convert.ToDouble(txtTravelTime.Text),
                        price,
                        Int32.Parse(((ComboBoxItem)cbFrom.SelectedItem).Tag.ToString()),
                        Int32.Parse(((ComboBoxItem)cbTo.SelectedItem).Tag.ToString()),
                        Int32.Parse(((ComboBoxItem)cbAirplane.SelectedItem).Tag.ToString()),
                        flight.takenSeats
                        );
                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Flyforbindelse opdateret!");
                    ((MainWindow)System.Windows.Application.Current.MainWindow).tFlight.UpdateDataGrid();   
                }
            }
            else
            {
                if (!stop)
                    MainWindow.ErrorMsg("Alle Felter skal udfyldes!");
            }
        }
    }
}
