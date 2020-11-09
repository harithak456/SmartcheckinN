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
    public class Bal_Guest
    {
        public int SaveNewGuest(Ent_Guest entGuest, SafeTransaction trans,string mode)
        {
            int dataResult = 0;
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dataResult = dal.SaveNewGuest(entGuest, trans,mode);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public int UpdateProfile(Ent_GuestData entGuest, SafeTransaction trans )
        {
            int dataResult = 0;
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dataResult = dal.UpdateProfile(entGuest, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }


        //SELECT Guest Data by code
        public Ent_Guest SelectGuest(string Customer_Code)
        {
            Ent_Guest result = new Ent_Guest();
            try
            {
                Dal_Guest dal = new Dal_Guest();
                result = dal.SelectGuest(Customer_Code);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public Ent_Guest SelectGroupLeader(string Customer_Code)
        {
            Ent_Guest result = new Ent_Guest();
            try
            {
                Dal_Guest dal = new Dal_Guest();
                result = dal.SelectGroupLeader(Customer_Code);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public Ent_Guest SelectGuestSearch(string Guest_Email)
        {
            Ent_Guest result = new Ent_Guest();
            try
            {
                Dal_Guest dal = new Dal_Guest();
                result = dal.SelectGuestSearch(Guest_Email);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<Ent_Guest> SelectGuestList()
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            try
            {
                Dal_Guest dal = new Dal_Guest();
                result = dal.SelectGuestList();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<Ent_Guest> GuestReportSearch(DateTime FromDate, DateTime ToDate, int flag)
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            try
            {
                Dal_Guest dal = new Dal_Guest();
                result = dal.GuestReportSearch(FromDate, ToDate, flag);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public int DeleteGuest(Ent_Guest ent, SafeTransaction trans)
        {
            int dataResult;
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dataResult = dal.DeleteGuest(ent, trans);
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }

        public int UpdateActiveStatus(Ent_Guest ent, SafeTransaction trans)
        {
            int dataResult;
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dataResult = dal.UpdateActiveStatus(ent, trans);
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }


        public List<Ent_Guest> SelectGuestHistory(string User_Name)
        {
            List<Ent_Guest> list = new List<Ent_Guest>();
            try
            {
                Dal_Guest dal = new Dal_Guest();
                list = dal.SelectGuestHistory(User_Name);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int GetNotification()
        {
            int dataResult;
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dataResult = dal.GetNotification();
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }

        public int UpdateNotification()
        {
            int dataResult;
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dataResult = dal.UpdateNotification();
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }

        public int SaveGuestSignature(Ent_Guest entGuest, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dataResult = dal.SaveGuestSignature(entGuest, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable SelectYesterdayCount()
        {
            DataTable dt = new DataTable();
            try
            {
                Dal_Guest dal = new Dal_Guest();
                dt = dal.SelectYesterdayCount();
                return dt;
            }
            catch
            {
                dt.Clear();
            }
            return dt;
        }

    }
}