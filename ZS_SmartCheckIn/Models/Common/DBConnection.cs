
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Common
{
    public class DBConnection
    {
        SqlConnection Connection;
        public DBConnection()
        {
            OpenConnection();
        }

        public SqlConnection DatabaseConnection
        {
            get { return Connection; }
        }


        //connection from web config
        public void OpenConnection()
        {
            if (Connection != null)
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
                Connection.Dispose();
            }

            Connection = new SqlConnection();           
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            try
            {
                Connection.Open();
            }
            catch (Exception e)
            {
               
            }
        }
    }
}