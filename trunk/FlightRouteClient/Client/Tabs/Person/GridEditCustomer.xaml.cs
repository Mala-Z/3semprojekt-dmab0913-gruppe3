using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Customer
{
    public partial class GridEditCustomer : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly FlightService.Person _person;

        public GridEditCustomer(FlightService.Person person)
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            this._person = person;
            InsertCustomerData();
        }

        private void InsertCustomerData()
        {
            txtFName.Text = _person.fname;
            txtLName.Text = _person.lname;
            txtGender.Text = _person.gender;
            txtAddress.Text = _person.address;
            txtPhoneNo.Text = _person.phoneNo;
            txtEmail.Text = _person.email;
            txtBirthdate.Text = _person.birthdate;
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFName.Text != "" && txtLName.Text != "" && txtGender.Text != "" && txtAddress.Text != "" && txtPhoneNo.Text != "" && txtEmail.Text != "" && txtBirthdate.Text != "")
            {
                bool success = _fService.UpdatePerson(_person.personID, txtFName.Text, txtLName.Text, txtGender.Text, txtAddress.Text, txtPhoneNo.Text, txtEmail.Text, txtBirthdate.Text);
                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Kunde er redigeret");
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
