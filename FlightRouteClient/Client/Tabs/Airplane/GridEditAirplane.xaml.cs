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
using Client.Helpers;

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
            int seats = 0;
            bool success = false;

            try
            {
                seats = Convert.ToInt32(txtSeats.Text);
            }
            catch (Exception)
            {
                ContentControlSuccess.Content = new DisplayError("Sæder skal være et tal!");
            }

            if (seats > 0)
            {
                success = fService.UpdateAirplane(airplane.airplaneID, Convert.ToInt32(txtSeats.Text));

                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Fly er opdateret!");
                    ((MainWindow)Application.Current.MainWindow).tAirplane.InitGridData();
                }
                
            }
            
        } 

    }
}
