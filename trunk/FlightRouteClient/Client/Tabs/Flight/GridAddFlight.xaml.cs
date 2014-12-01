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
using Client.Helpers;

namespace Client.Tabs.Flight
{
    /// <summary>
    /// Interaction logic for GridAddFlight.xaml
    /// </summary>
    public partial class GridAddFlight : UserControl
    {
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
