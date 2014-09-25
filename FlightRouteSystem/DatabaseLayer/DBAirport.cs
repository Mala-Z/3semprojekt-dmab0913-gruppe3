using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ModelLayer;

namespace DatabaseLayer
{
    public class DBAirport
    {
        private SqlConnection conn;

        public DBAirport()
        {
            conn = DBConnection.GetInstance().GetConnection();
        }

        public Airport getAirportByID(int airportID)
        {
            SqlDataReader reader = null;
            Airport airportObj = null;

            try
            {
                conn.Open();
                string query = "SELECT * FROM Airport WHERE airportID = @airportID";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlParameter idParam = new SqlParameter("@airportID", airportID);
                cmd.Parameters.Add(idParam);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    airportObj = new Airport();
                    airportObj.AirportID = int.Parse(reader["airportID"].ToString());
                    airportObj.Name = reader["name"].ToString();
                    airportObj.Location = reader["location"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return airportObj;
        }
    }
}
