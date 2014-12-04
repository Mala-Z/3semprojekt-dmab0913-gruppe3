﻿using System;
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
using Client.Tabs.Airport;

namespace Client.Tabs
{
    /// <summary>
    /// Interaction logic for TabTest1.xaml
    /// http://www.wpftutorial.net/DataGrid.html
    /// </summary>
    public partial class TabAirport : UserControl
    {
        private FlightServiceClient fService;

        public TabAirport()
        {
            InitializeComponent();
            contentControl.Content = new GridAddAirport();

            fService = new FlightServiceClient();

            InitializeGridData();

        }

        private void InitializeGridData()
        {
            //dgAirports.ItemsSource = fService.GetAllAirports();

            var result = from a in fService.GetAllAirports()
                        select new { ID = a.airportID, Navn = a.name, Lokation = a.location };

            dgAirports.ItemsSource = result;

        }

        public void updateDataGrid()
        {
            InitializeGridData();
        }

        private void tSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = from a in fService.GetAllAirports()
                         where a.name.ToLower().Contains(txtSearch.Text.ToLower()) || a.location.ToLower().Contains(txtSearch.Text.ToLower())
                         select new { ID = a.airportID, Navn = a.name, Lokation = a.location };

            dgAirports.ItemsSource = result;
            
            //Application.Current.MainWindow 


        }

        private void dgAirports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgAirports indeholder anonyme objecter. Enten skal vi lave en ny class og caste det anonyme object dertil,
            //ellers kan vi som her lave det om til en string, og via regular expression hente dets id
            if (dgAirports.SelectedItem != null)
            {
                String airportString = dgAirports.SelectedItem.ToString().Trim();
                int airportID = Convert.ToInt32(Regex.Match(airportString, @"\d+").ToString());
                var airport = fService.GetAirportByID(airportID);
                contentControl.Content = new GridEditAirport(airport); 
            }
            
        }

        

    }
}