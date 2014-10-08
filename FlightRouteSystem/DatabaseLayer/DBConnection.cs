﻿using System;
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
        private static dmab0913_3DataContext conn;

        private DBConnection()
        {
            conn = new dmab0913_3DataContext();
        }

        public static DBConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new DBConnection();
            }
            return instance;
        }

        public dmab0913_3DataContext GetConnection()
        {
            return conn;
        }


    }
}