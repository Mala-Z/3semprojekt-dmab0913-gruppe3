using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Tabs.Customer;
using Client.Helpers;

namespace Client.Tabs
{
    public partial class TabCustomer : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly GridAddCustomer _gridAddCustomer = new GridAddCustomer();
        private readonly ContentTitle _addTitle = new ContentTitle("Tilføj ny person");
        private readonly ContentTitle _editTitle = new ContentTitle("Rediger person");

        public TabCustomer()
        {
            InitializeComponent();
            contentControl.Content = _gridAddCustomer;
            ContentControlTitle.Content = _addTitle;
            _fService = new FlightServiceClient();
            InitGridData();
            ActionBar.RefreshClick += new RoutedEventHandler(RefreshClick);
            ActionBar.AddClick += new RoutedEventHandler(AddClick);
            //ActionBar.DeleteClick += new RoutedEventHandler(DeleteClick);
        }

        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    bool success = false;
        //    int customerID = GetSelectedPersonID();
        //    var warningBox = MessageBox.Show("Vil du slette kunden med ID: " + customerID + "?", "Slet",
        //        MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
        //    if (warningBox == MessageBoxResult.Yes)
        //        success = _fService.DeletePerson(customerID);
        //    if (!success)
        //        MainWindow.ErrorMsg("Kunden ikke slettet");

        //    ((MainWindow)System.Windows.Application.Current.MainWindow).TabCustomer.UpdateDataGrid();
        //}

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ContentControlTitle.Content = _addTitle;
            contentControl.Content = _gridAddCustomer;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void InitGridData()
        {
            var result = from p in _fService.GetAllPersons()
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
            var result = from p in _fService.GetAllPersons()
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
        }

        private int GetSelectedPersonID()
        {
            String customerString = dgCustomers.SelectedItem.ToString().Trim();
            int customerID = Convert.ToInt32(Regex.Match(customerString, @"\d+").ToString());
            return customerID;
        }

        private void dgPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomers.SelectedItem != null)
            {
                var person = _fService.GetPersonByID(GetSelectedPersonID());
                ContentControlTitle.Content = _editTitle;
                contentControl.Content = new GridEditCustomer(person); 
            }  
        }
    }
}
