using System;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Flight
{
    /// <summary>
    /// Interaction logic for GridAddFlight.xaml
    /// </summary>
    public partial class GridAddFlight : UserControl
    {
        private readonly FlightServiceClient _fService = new FlightServiceClient();

        public GridAddFlight()
        {
            InitializeComponent();
            InitComboBox();
        }

        private void InitComboBox()
        {
            cbFrom.ItemsSource = ComboBoxItems.AirportItems();
            cbTo.ItemsSource = ComboBoxItems.AirportItems();
            cbAirplane.ItemsSource = ComboBoxItems.AirplaneItems();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            double price = 0.0;
            bool stop = false;

            try
            {
                 price = Convert.ToDouble(txtPrice.Text);
            }
            catch (Exception)
            {
                ContentControlSuccess.Content = new DisplayError("Pris skal være et tal!");
                stop = true;
            }

            if (!stop && cbTo.SelectedItem != null && cbFrom != null)
            {
                if (!stop && cbTo.SelectedItem.ToString() == cbFrom.SelectedItem.ToString())
                {
                    ContentControlSuccess.Content = new DisplayError("Fra og til skal være forskellige!");
                    stop = true;
                }
            }
            else
            {
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
                bool success = _fService.CreateNewFlight(DatePickerDeparture.SelectedDate.ToString(), 
                    DatePickerArrival.SelectedDate.ToString(), 
                    Convert.ToDouble(txtTravelTime.Text),
                    price,
                    Int32.Parse(((ComboBoxItem)cbFrom.SelectedItem).Tag.ToString()),
                    Int32.Parse(((ComboBoxItem)cbTo.SelectedItem).Tag.ToString()),
                    Int32.Parse(((ComboBoxItem)cbAirplane.SelectedItem).Tag.ToString()),
                    0
                    );
                if (success)
                    {
                        ContentControlSuccess.Content = new DisplaySuccess("Flyforbindelse oprettet!");
                        ((MainWindow)Application.Current.MainWindow).TabFlight.UpdateDataGrid();
                    }
                    else
                    {
                        ContentControlSuccess.Content = new DisplayError("FEJL");
                    }
            }
            else
            {
                if(stop)
                    ContentControlSuccess.Content = new DisplayError("Alle felter skal udfyldes!");
            }
        }

    }
}
