using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Client.Tabs.Booking
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridAddPassenger : UserControl
    {
        private FlightServiceClient fService;
        private GridSaveBooking grid;


        public GridAddPassenger(GridSaveBooking grid)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.grid = grid;

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "")
            {
                var passenger = fService.CreateNewPersonBooking(txtFName.Text, txtLName.Text);
                grid.AddPassengerToList(passenger);

            }
            
        }

       
    }
}
