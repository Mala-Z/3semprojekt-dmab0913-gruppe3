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

namespace Client.Tabs.Customer
{
    /// <summary>
    /// Interaction logic for TabTest2.xaml
    /// </summary>
    public partial class GridAddCustomer : UserControl
    {
        private FlightServiceClient fService;


        public GridAddCustomer()
        {
            InitializeComponent();
            fService = new FlightServiceClient();
        }


        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "" && txtGender.Text != "" && txtAddress.Text != "" && txtPhoneNo.Text != "" && txtEmail.Text != "" && txtBirthdate.Text != "")
            {
                fService.CreateNewPerson(txtFName.Text, txtLName.Text, txtGender.Text, txtAddress.Text, txtPhoneNo.Text, txtEmail.Text, txtBirthdate.Text);

                string messageBoxText = "Kunde er blevet opdateret";
                string caption = "Succes";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);

                ((MainWindow)System.Windows.Application.Current.MainWindow).tCustomer.UpdateDataGrid();

            }
            else
            {
                string messageBoxText = "Felterne fornavn, efternavn, køn, adresse, telefonNr, email, og fødselsdag må ikke være tomme";
                string caption = "Fejl";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        } 

    }
}
