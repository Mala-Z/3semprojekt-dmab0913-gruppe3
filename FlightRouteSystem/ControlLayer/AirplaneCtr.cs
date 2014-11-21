using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    public class AirplaneCtr
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Airplane> GetAllAirplanes()
        {
            var db = DBConnection.GetInstance().GetConnection();
            var airplanes = db.Airplanes.OrderBy(x => x.airplaneID).ToList();
            return airplanes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seats"></param>
        public void CreateNewAirplanes(int seats)
        {
            var db = DBConnection.GetInstance().GetConnection();
            var airplane = new Airplane { seats = seats };
            db.Airplanes.InsertOnSubmit(airplane);
            db.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Airplane GetAirplaneByID(int id)
        {
            var db = DBConnection.GetInstance().GetConnection();

            var airplane = db.Airplanes.SingleOrDefault(a => a.airplaneID == id);

            return airplane;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seats"></param>
        public void UpdateAirplane(int id, int seats)
        {
            var db = DBConnection.GetInstance().GetConnection();
            var airplane = GetAirplaneByID(id);

            if (airplane != null)
            {
                airplane.seats = seats;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAirplane(int id)
        {
            var db = DBConnection.GetInstance().GetConnection();
            var airplane = GetAirplaneByID(id);

            if (airplane != null)
            {
                db.Airplanes.DeleteOnSubmit(airplane);
                db.SubmitChanges();
            }
        }

    }

}
