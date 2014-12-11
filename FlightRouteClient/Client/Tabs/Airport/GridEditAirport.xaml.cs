using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Airport
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridEditAirport : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly FlightService.Airport _airport;

        public GridEditAirport(FlightService.Airport airport)
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            _airport = airport;
            InsertAirportData();
        }

        private void InsertAirportData()
        {
            txtName.Text = _airport.name;
            txtLocation.Text = _airport.location;
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtLocation.Text != "")
            {
                bool success = _fService.UpdateAirport(_airport.airportID, txtName.Text, txtLocation.Text);

                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Lufthavn er opdateret!");
                    ((MainWindow)Application.Current.MainWindow).TabAirport.InitGridData();
                }
                else
                {
                    ContentControlSuccess.Content = new DisplayError("FEJL!");
                }
            }
            else
            {
                ContentControlSuccess.Content = new DisplayError("Alle felter skal udfyldes!");
            }
        } 

    }
}
