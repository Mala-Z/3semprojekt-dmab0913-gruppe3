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

namespace Client.Tabs.Airport
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridEditAirport : UserControl
    {
        private FlightServiceClient fService;
        private FlightService.Airport airport;

        public GridEditAirport(Object airport)
        {
            InitializeComponent();
            fService = new FlightServiceClient();
            this.airport = airport;
            InsertAiportData();
        }

        private void InsertAiportData()
        {
            txtName.Text = airport.name;
            txtLocation.Text = airport.location;
        }

    }
}
