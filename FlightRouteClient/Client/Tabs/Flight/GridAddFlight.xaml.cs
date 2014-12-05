using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for GridAddFlight.xaml
    /// </summary>
    public partial class GridAddFlight : UserControl
    {
        private FlightServiceClient fService = new FlightServiceClient();

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
                success = fService.CreateNewFlight(DatePickerDeparture.SelectedDate.ToString(), 
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
                        ((MainWindow)Application.Current.MainWindow).tFlight.UpdateDataGrid();
                    }
                    else
                    {
                        ContentControlSuccess.Content = new DisplayError("FEJL");
                    }
                }
            else
            {
                if(!stop)
                MainWindow.ErrorMsg("Alle Felter skal udfyldes!");
            }
        }

    }
}
