using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DatabaseLayer;

namespace ControlLayer
{
    public class AirplaneCtr
{
    private readonly dmab0913_3DataContext _db;

        public AirplaneCtr(dmab0913_3DataContext db)
        {
            _db = db;
        }

        public List<Airplane> GetAllAirplanes()
        {
            
            var airplanes = _db.Airplanes.OrderBy(x => x.airplaneID).ToList();
            return airplanes;
        }

        public bool CreateNewAirplane(int seats)
        {
            bool returnValue = false;
           
            var airplane = new Airplane { seats = seats };
            _db.Airplanes.InsertOnSubmit(airplane);

            try
            {
                _db.SubmitChanges();
                returnValue = true;
            }
            catch (SqlException)
            {
                returnValue = false;
            }

            return returnValue;
        }

        public Airplane GetAirplaneByID(int id)
        {
            var airplane = _db.Airplanes.SingleOrDefault(a => a.airplaneID == id);

            return airplane;
        }

        public bool UpdateAirplane(int id, int seats)
        {
            bool returnValue = false;
          
            var airplane = GetAirplaneByID(id);

            if (airplane != null)
            {
                airplane.seats = seats;

            }
            try
            {
                _db.SubmitChanges();
                returnValue = true;
            }
            catch (SqlException)
            {
                returnValue = false;
            }

            return returnValue;
        }

        public bool DeleteAirplane(int id)
        {
            bool returnValue = true;
            
            var airplane = GetAirplaneByID(id);

            if (airplane != null)
            {
                _db.Airplanes.DeleteOnSubmit(airplane);

                try
                {
                    _db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            } 
            return returnValue;
        }

    }
}
