
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Common
{
    public class SafeTransaction
    {
        private DBConnection DBCon = new DBConnection();
        private SqlTransaction _Trans = null;
        public SafeTransaction()
        {
            _Trans = DBCon.DatabaseConnection.BeginTransaction();
        }
        public SqlConnection DatabaseConnection
        {
            get { return DBCon.DatabaseConnection; }
        }
        public SqlTransaction Transaction
        {
            get { return this._Trans; }
            set { this._Trans = value; }
        }
        public void Commit()
        {
            _Trans.Commit();
            DBCon.DatabaseConnection.Close();
        }

        public void Rollback()
        {
            try
            {
                _Trans.Rollback();
                DBCon.DatabaseConnection.Close();
            }
            catch
            {

            }
        }
    }
}