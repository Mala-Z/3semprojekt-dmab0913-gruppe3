using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Customer
{
    public partial class GridAddCustomer : UserControl
    {
        private readonly FlightServiceClient _fService;

        public GridAddCustomer()
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
        }


        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "" && txtGender.Text != "" && txtAddress.Text != "" && txtPhoneNo.Text != "" && txtEmail.Text != "" && txtBirthdate.Text != "")
            {
                bool success = _fService.CreateNewPerson(txtFName.Text, txtLName.Text, txtGender.Text, txtAddress.Text, txtPhoneNo.Text, txtEmail.Text, txtBirthdate.Text);
                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Kunde er blevet oprettet");
                    ((MainWindow)Application.Current.MainWindow).TabCustomer.UpdateDataGrid();
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
