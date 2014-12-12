using System;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;
using Client.Helpers;

namespace Client.Tabs.Airplane
{
    public partial class GridEditAirplane : UserControl
    {
        private readonly FlightServiceClient _fService;
        private readonly FlightService.Airplane _airplane;

        public GridEditAirplane(FlightService.Airplane airplane)
        {
            InitializeComponent();
            _fService = new FlightServiceClient();
            this._airplane = airplane;
            InsertAirplaneData();
        }

        private void InsertAirplaneData()
        {
            txtSeats.Text = _airplane.seats.ToString();
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
                bool success = _fService.UpdateAirplane(_airplane.airplaneID, Convert.ToInt32(txtSeats.Text));

                if (success)
                {
                    ContentControlSuccess.Content = new DisplaySuccess("Fly er opdateret!");
                    ((MainWindow)Application.Current.MainWindow).TabAirplane.LoadGridData();
                }
            }
        } 

    }
}
