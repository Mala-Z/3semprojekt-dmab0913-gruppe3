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


        public GridAddPassenger()
        {
            InitializeComponent();
            fService = new FlightServiceClient();
             
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "")
            {
                fService.CreateNewPersonBooking(txtFName.Text, txtLName.Text);
            }
            
        }

       
    }
}
