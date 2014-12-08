using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Client.Tabs.Customer;
using Client.Helpers;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// http://www.wpftutorial.net/DataGrid.html
    /// </summary>
    public partial class TabCustomer : UserControl
    {
        private FlightServiceClient fService;
        private GridAddCustomer gridAddCustomer = new GridAddCustomer();
        private ContentTitle addTitle = new ContentTitle("Tilføj ny person");
        private ContentTitle editTitle = new ContentTitle("Rediger person");

        public TabCustomer()
        {
            InitializeComponent();
            contentControl.Content = gridAddCustomer;
            ContentControlTitle.Content = addTitle;
            fService = new FlightServiceClient();
            InitGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            bool success = false;
            int customerID = GetSelectedPersonID();
            var warningBox = MessageBox.Show("Vil du slette kunden med ID: " + customerID + "?", "Slet",
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (warningBox == MessageBoxResult.Yes)
                success = fService.DeletePerson(customerID);
            if (!success)
                MainWindow.ErrorMsg("Kunden ikke slettet");

            ((MainWindow)System.Windows.Application.Current.MainWindow).tCustomer.UpdateDataGrid();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = addTitle;
            contentControl.Content = gridAddCustomer;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void InitGridData()
        {
            var result = from p in fService.GetAllPersons()
                        select new { ID = p.personID, Navn = p.fname, Efternavn = p.lname, Køn = p.gender, Adresse = p.address, 
                                     TelefonNr = p.phoneNo, Email = p.email, Fødselsdag = p.birthdate };
            dgCustomers.ItemsSource = result;
        }

        public void UpdateDataGrid()
        {
            InitGridData();
        }

        private void tSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from p in fService.GetAllPersons()
                         where p.fname.ToLower().Contains(txtSearch.Text.ToLower()) || p.lname.ToLower().Contains(txtSearch.Text.ToLower()) || 
                               p.gender.ToLower().Contains(txtSearch.Text.ToLower()) || p.address.ToLower().Contains(txtSearch.Text.ToLower()) ||
                               p.phoneNo.ToLower().Contains(txtSearch.Text.ToLower()) || p.email.ToLower().Contains(txtSearch.Text.ToLower()) || 
                               p.birthdate.ToLower().Contains(txtSearch.Text.ToLower())
                         select new
                         {
                             ID = p.personID,
                             Navn = p.fname,
                             Efternavn = p.lname,
                             Køn = p.gender,
                             Adresse = p.address,
                             TelefonNr = p.phoneNo,
                             Email = p.email,
                             Fødselsdag = p.birthdate,
                         };

            dgCustomers.ItemsSource = result;          
            //Application.Current.MainWindow 
        }

        private int GetSelectedPersonID()
        {
            String customerString = dgCustomers.SelectedItem.ToString().Trim();
            int customerID = Convert.ToInt32(Regex.Match(customerString, @"\d+").ToString());
            return customerID;
        }

        private void dgPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgAirports indeholder anonyme objecter. Enten skal vi lave en ny class og caste det anonyme object dertil,
            //ellers kan vi som her lave det om til en string, og via regular expression hente dets id
            if (dgCustomers.SelectedItem != null)
            {
                String customerString = dgCustomers.SelectedItem.ToString().Trim();
                int customerID = Convert.ToInt32(Regex.Match(customerString, @"\d+").ToString());
                var person = fService.GetPersonByID(customerID);
                ContentControlTitle.Content = editTitle;
                contentControl.Content = new GridEditCustomer(person); 
            }  
        }
    }
}
