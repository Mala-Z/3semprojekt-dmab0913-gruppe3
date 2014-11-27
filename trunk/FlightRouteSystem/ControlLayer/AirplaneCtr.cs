using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    public class AirplaneCtr
{
    private dmab0913_3DataContext db;

        public AirplaneCtr(dmab0913_3DataContext db)
        {
            this.db = db;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Airplane> GetAllAirplanes()
        {
            
            var airplanes = db.Airplanes.OrderBy(x => x.airplaneID).ToList();
            return airplanes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seats"></param>
        public bool CreateNewAirplane(int seats)
        {
            bool returnValue = true;
           
            var airplane = new Airplane { seats = seats };
            db.Airplanes.InsertOnSubmit(airplane);
            db.SubmitChanges();

            try
            {
                db.SubmitChanges();
                returnValue = true;
            }
            catch (SqlException)
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Airplane GetAirplaneByID(int id)
        {
            

            var airplane = db.Airplanes.SingleOrDefault(a => a.airplaneID == id);

            return airplane;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seats"></param>
        public bool UpdateAirplane(int id, int seats)
        {
            bool returnValue = true;
          
            var airplane = GetAirplaneByID(id);

            if (airplane != null)
            {
                airplane.seats = seats;

            }
            try
            {
                db.SubmitChanges();
                returnValue = true;
            }
            catch (SqlException)
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteAirplane(int id)
        {
            bool returnValue = true;
            
            var airplane = GetAirplaneByID(id);

            if (airplane != null)
            {
                db.Airplanes.DeleteOnSubmit(airplane);

                try
                {
                    db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            } //end if
            return returnValue;

        }

    }
}
