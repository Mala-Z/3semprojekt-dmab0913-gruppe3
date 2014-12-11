using System;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Airplane
{
    public partial class GridAddAirplane : UserControl
    {
        private readonly FlightServiceClient _fService;

        public GridAddAirplane()
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int seats = 0;

            try
            {
                seats = Convert.ToInt32(txtSeats.Text);
            }
            catch (Exception)
            {
                ContentControlSuccess.Content = new DisplayError("Sæder skal være et tal!");
            }

            if (seats > 0)
            {
                bool success = _fService.CreateNewAirplane(seats);

                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Fly er oprettet!");
                    ((MainWindow)Application.Current.MainWindow).TabAirplane.InitGridData();
                }
            }
        } 

    }
}
