using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Client.Tabs.Airplane
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridEditAirplane : UserControl
    {
        private FlightServiceClient fService;
        private FlightService.Airplane airplane;

        public GridEditAirplane(FlightService.Airplane airplane)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.airplane = airplane;
            InsertAirplaneData();
        }

        private void InsertAirplaneData()
        {
            txtSeats.Text = airplane.seats.ToString();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtSeats.Text != "")
            {
                fService.UpdateAirplane(airplane.airplaneID, Convert.ToInt32(txtSeats.Text));

                string messageBoxText = "asd";
                string caption = "Succes";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);

                ((MainWindow)System.Windows.Application.Current.MainWindow).tAirplane.InitGridData();
               
            }
            else
            {
                string messageBoxText = "Feltet Antal Sæder må ikke være tom";
                string caption = "Fejl";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        } 

    }
}
