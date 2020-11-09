using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.DAL;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Models.BAL
{
    public class Bal_GuestData
    {
        public int SaveGuestData(Ent_GuestData entGuest, SafeTransaction trans,bool IsEdit)
        {
            int dataResult = 0;
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                dataResult = dal.SaveGuestData(entGuest, trans, IsEdit);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }
        public int SaveGuestDocuments(Ent_GuestData entGuest,int id, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                dataResult = dal.SaveGuestDocuments(entGuest,id, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteGuestData(Ent_GuestData food, SafeTransaction trans)
        {
            int dataResult;
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                dataResult = dal.DeleteGuestData(food, trans);
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }


        public List<Ent_GuestData> SelectGuestsList(string Customer_Code)
        {
            List<Ent_GuestData> list = new List<Ent_GuestData>();
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                list = dal.SelectGuestsList(Customer_Code);
                return list;
            }
            catch
            {
                return list;
            }
        }
      

        public Ent_Guest SelectGuestData(string Guest_Code)
        {
            Ent_Guest ent = new Ent_Guest();
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                ent = dal.SelectGuestData(Guest_Code);
                return ent;
            }
            catch
            {
                return ent;
            }
        }

        //Added by Arun on 26-06-2020
        public int SaveWalkingGuestData(Ent_GuestData entGuest, SafeTransaction trans, bool IsEdit)
        {
            int dataResult = 0;
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                dataResult = dal.SaveWalkingGuestData(entGuest, trans, IsEdit);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        #region FRRO
        public List<Ent_GuestData> SelectGuestsListChromeExt(int Guestdata_id)
        {
            List<Ent_GuestData> list = new List<Ent_GuestData>();
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                list = dal.SelectGuestsListChromeExt(Guestdata_id);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int UpdateFRROStatus(Ent_GuestData entGuest)
        {
            int dataResult = 0;
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                dataResult = dal.UpdateFRROStatus(entGuest);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public List<Ent_GuestData> SelectGuestsListChromeExt(int Guestdata_id, int branch_ID)
        {
            List<Ent_GuestData> list = new List<Ent_GuestData>();
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                list = dal.SelectGuestsListChromeExt(Guestdata_id, branch_ID);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int UpdateFRROCheckOutStatus(Ent_GuestData entGuest)
        {
            int dataResult = 0;
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                dataResult = dal.UpdateFRROCheckOutStatus(entGuest);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public int UpdateFRROCheckOutFlag(Ent_GuestData entGuest)
        {
            int dataResult = 0;
            try
            {
                Dal_GuestData dal = new Dal_GuestData();
                dataResult = dal.UpdateFRROCheckOutFlag(entGuest);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}