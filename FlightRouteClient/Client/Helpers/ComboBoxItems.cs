using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Client.FlightService;

namespace Client.Helpers
{
    public static class ComboBoxItems
    {
        private static FlightServiceClient fService = new FlightServiceClient();

        public static List<ComboBoxItem> CustomerItems()
        {
            var items = new List<ComboBoxItem>();

            foreach (var p in fService.GetAllPersons())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = p.fname + " " + p.lname + ", " + p.address;
                comboBoxItem.Tag = p.personID;
                items.Add(comboBoxItem);
            }
            return items;
        }

        public static List<ComboBoxItem> AirportItems()
        {
            var items = new List<ComboBoxItem>();

            foreach (var a in fService.GetAllAirports())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = a.name + " " + a.location;
                comboBoxItem.Tag = a.airportID;
                items.Add(comboBoxItem);
            }
            return items;
        }

        public static List<ComboBoxItem> AirplaneItems()
        {
            var items = new List<ComboBoxItem>();

            foreach (var a in fService.GetAllAirplanes())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = a.airplaneID + " seats: " + a.seats;
                comboBoxItem.Tag = a.airplaneID;
                items.Add(comboBoxItem);
            }
            return items;
        }
    }
}
