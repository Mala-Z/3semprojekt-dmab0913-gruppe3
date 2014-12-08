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

namespace Client.Tabs.Airport
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridAddAirport : UserControl
    {
        private FlightServiceClient fService;


        public GridAddAirport()
        {
            InitializeComponent();
            fService = new FlightServiceClient();
        }


        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtLocation.Text != "")
            {
                fService.CreateNewAirport(txtName.Text, txtLocation.Text);

                string messageBoxText = "Lufthavnen er blevet oprettet";
                string caption = "Succes";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);

                ((MainWindow)System.Windows.Application.Current.MainWindow).tAirport.UpdateDataGrid();
               
            }
            else
            {
                string messageBoxText = "Felterne navn og placering må ikke være tomme";
                string caption = "Fejl";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        } 

    }
}
