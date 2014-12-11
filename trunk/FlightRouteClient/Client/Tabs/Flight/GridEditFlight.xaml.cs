using System;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Flight
{
    /// <summary>
    /// Interaction logic for GridEditFlight.xaml
    /// </summary>
    public partial class GridEditFlight : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly FlightService.Flight _flight;

        public GridEditFlight(FlightService.Flight flight)
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            _flight = flight;
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
            if (_flight.from > 0 && _flight.to > 0)
            {
                cbFrom.SelectedIndex = Convert.ToInt32(_flight.from -1);
                cbTo.SelectedIndex = Convert.ToInt32(_flight.to -1);
                DatePickerDeparture.SelectedDate = DateTime.Parse(_flight.timeOfDeparture);
                DatePickerArrival.SelectedDate = DateTime.Parse(_flight.timeOfArrival);
                txtTravelTime.Text = _flight.traveltime.ToString();
                txtPrice.Text = _flight.price.ToString();
                cbAirplane.SelectedIndex = Convert.ToInt32(_flight.airplaneID - 1);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double price = 0.0;
            bool stop = false;

            try
            {
                price = System.Convert.ToDouble(txtPrice.Text);
            }
            catch (Exception)
            {
                ContentControlSuccess.Content = new DisplaySuccess("Pris skal være et tal!");
                stop = true;
            }

            if (!stop && cbTo.SelectedItem.ToString() == cbFrom.SelectedItem.ToString())
            {
                ContentControlSuccess.Content = new DisplayError("Fra og til skal være forskellige!");
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
                bool success = _fService.UpdateFlight(_flight.flightID,
                    DatePickerDeparture.SelectedDate.ToString(),
                    DatePickerArrival.SelectedDate.ToString(),
                    System.Convert.ToDouble(txtTravelTime.Text),
                    price,
                    Int32.Parse(((ComboBoxItem)cbFrom.SelectedItem).Tag.ToString()),
                    Int32.Parse(((ComboBoxItem)cbTo.SelectedItem).Tag.ToString()),
                    Int32.Parse(((ComboBoxItem)cbAirplane.SelectedItem).Tag.ToString()),
                    _flight.takenSeats
                    );
                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Flyforbindelse opdateret!");
                    ((MainWindow)System.Windows.Application.Current.MainWindow).TabFlight.UpdateDataGrid();   
                }
                else
                {
                    ContentControlSuccess.Content = new DisplayError("FEJL");
                }
            }
            else
            {
                if (!stop)
                    ContentControlSuccess.Content = new DisplayError("Alle felter skal udfyldes!");
            }
        }
    }
}
