using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Client.FlightService;

namespace Client.Helpers
{
    public static class ComboBoxItems
    {
        private static readonly FlightServiceClient FService = new FlightServiceClient();

        public static List<ComboBoxItem> CustomerItems()
        {
            return FService.GetAllPersons().Select(p => new ComboBoxItem
            {
                Content = p.fname + " " + p.lname + ", " + p.address,
                Tag = p.personID,
                Name = "ComboBoxCustomers",
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center
            }).ToList();
        }

        public static List<ComboBoxItem> AirportItems()
        {
            return FService.GetAllAirports().Select(a => new ComboBoxItem
            {
                Content = a.name + " " + a.location,
                Name = "ComboBoxAirports",
                Tag = a.airportID,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center
            }).ToList();
        }

        public static List<ComboBoxItem> AirplaneItems()
        {
            return FService.GetAllAirplanes().Select(a => new ComboBoxItem
            {
                Content = a.airplaneID + " seats: " + a.seats, Tag = a.airplaneID,
                Name = "ComboBoxAirplanes",
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center
            }).ToList();
        }
    }
}
