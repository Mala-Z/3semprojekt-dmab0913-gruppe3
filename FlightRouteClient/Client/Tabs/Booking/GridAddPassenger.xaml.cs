using System.Windows;
using System.Windows.Controls;
using Client.FlightService;

namespace Client.Tabs.Booking
{
    public partial class GridAddPassenger : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly GridSaveBooking _grid;


        public GridAddPassenger(GridSaveBooking grid)
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            _grid = grid;

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "")
            {
                var passenger = _fService.CreateNewPersonBooking(txtFName.Text, txtLName.Text);
                if(passenger != null)
                    _grid.AddPassengerToList(passenger);
                else
                {
                    MainWindow.ErrorMsg("Fejl! passenger er null");
                }
            }
            else
            {
                MainWindow.ErrorMsg("Begge felter skal udfyldes!");
            }
            
        }

       
    }
}
