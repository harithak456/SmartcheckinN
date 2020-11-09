using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.DAL;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Models.BAL
{
    public class Bal_User
    {
        public List<Ent_User> SelectLogin(Ent_User entu)
        {
            List<Ent_User> result = new List<Ent_User>();
            try
            {
                Dal_User dal = new Dal_User();
                result = dal.SelectLogin(entu);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<Ent_Guest> SelectGuestLogin(Ent_Guest entu)
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            try
            {
                Dal_User dal = new Dal_User();
                result = dal.SelectGuestLogin(entu);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<Ent_Guest> SelectGuestLoginMail(Ent_Guest entu)
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            try
            {
                Dal_User dal = new Dal_User();
                result = dal.SelectGuestLoginMail(entu);
                return result;
            }
            catch
            {
                return result;
            }
        }


        public int UpdatePassword(Ent_Guest entu ,SafeTransaction trans)
        {
            int result = 0;
            try
            {
                Dal_User dal = new Dal_User();
                result = dal.UpdatePassword(entu, trans);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public int UpdateToken(string username, string email, string token)
        {
            int dataResult;
            try
            {
                Dal_User dal = new Dal_User();
                dataResult = dal.UpdateToken(username, email, token);
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }

        public int InsertLogActivity(Ent_Guest ent, int primaryid, string logaction)
        {
            int result = 0;
            try
            {
                Dal_User dal = new Dal_User();
                result = dal.InsertLogActivity(ent, primaryid, logaction);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<Ent_Guest> SelectLogActivity()
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            try
            {
                Dal_User dal = new Dal_User();
                result = dal.SelectLogActivity();
                return result;
            }
            catch
            {
                return result;
            }
        }
    }
}