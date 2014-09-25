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
        private DBConnection conn;

        public DBAirport()
        {
            conn = DBConnection.GetInstance();
            conn.GetConnection();
        }

        public Airport getSelectedAirport(int idairport)
        {
            SqlDataReader reader = null;
            string query = "SELECT * FROM Airport WHERE idairport = " + "'" + idairport + "'";
            Airport airportObj = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
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
