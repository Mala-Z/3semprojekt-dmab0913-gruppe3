using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseLayer
{
    public class DBConnection
    {
        private static DBConnection instance = null;
        private static SqlConnection con;

        private DBConnection()
        {
            string connectionString = @"Data Source=balder.ucn.dk;Initial Catalog=dmab0913_3;User ID=dmab0913_3;Password=MaaGodt"; 
            try
            {
                con = new SqlConnection(connectionString);
            }
            catch(SqlException sqlException)
            {
                throw sqlException;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static DBConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new DBConnection();
            }
            return instance;
        }

        public SqlConnection GetConnection()
        {
            return con;
        }

        
    }
}
