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
            var items = new List<ComboBoxItem>();

            foreach (var a in fService.GetAllAirports())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = a.name + " " + a.location;
                comboBoxItem.Tag = a.airportID;
                items.Add(comboBoxItem);
            }

            cbFrom.ItemsSource = items;
        }
    }
}
