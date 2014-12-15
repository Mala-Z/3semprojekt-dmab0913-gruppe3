using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Airport
{
    public partial class GridAddAirport : UserControl
    {
        private readonly FlightServiceClient _fService;


        public GridAddAirport()
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
        }


        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtLocation.Text != "")
            {
                bool success = _fService.CreateNewAirport(txtName.Text, txtLocation.Text);

                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Lufthavn er oprettet!");
                    ((MainWindow)Application.Current.MainWindow).TabAirport.LoadGridData();
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
