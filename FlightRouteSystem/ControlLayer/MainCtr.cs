using DatabaseLayer;


namespace ControlLayer
{
    public class MainCtr
    {
        public AirplaneCtr AirplaneCtr { get; set; }
        public AirportCtr AirportCtr { get; set; }
        public BookingCtr BookingCtr { get; set; }
        public FlightCtr FlightCtr { get; set; }
        public PersonCtr PersonCtr { get; set; }
        

        public MainCtr()
        {
            var db = new dmab0913_3DataContext();
            AirplaneCtr = new AirplaneCtr(db);
            AirportCtr = new AirportCtr(db);
            BookingCtr = new BookingCtr(db);
            FlightCtr = new FlightCtr(db);
            PersonCtr = new PersonCtr(db);
        }
    }
}
