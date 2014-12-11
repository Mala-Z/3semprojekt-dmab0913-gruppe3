using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using FlightServiceReference;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for ComboBoxItems
/// </summary>
    public class ListItems
    {
        private static readonly FlightServiceClient fService = new FlightServiceClient();

        public static List<ListItem> CustomerItems()
        {
            return fService.GetAllPersons().Select(p => new ListItem
            {
                Text = p.fname + " " + p.lname + ", " + p.address,
                Value = Convert.ToString(p.personID),
               }).ToList();
        }


        public static List<ListItem> AirportItems()
        {
            return fService.GetAllAirports().Select(a => new ListItem
            {
                Text = a.name + " " + a.location,
                Value = Convert.ToString(a.airportID),      
            }).ToList();
        }


    }
