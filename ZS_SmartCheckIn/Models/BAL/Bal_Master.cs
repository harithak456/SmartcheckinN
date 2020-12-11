using System;
using System.Collections.Generic;
using System.Data;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.DAL;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Models.BAL
{
    public class Bal_Master
    {
        #region Organization
        public int SaveOrganization(Ent_Organization entGuest, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.SaveOrganization(entGuest, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public Ent_Organization SelectOrganization()
        {
            Ent_Organization result = new Ent_Organization();
            try
            {
                Dal_Master dal = new Dal_Master();
                result = dal.SelectOrganization();
                return result;
            }
            catch
            {
                return result;
            }
        }
        #endregion

        #region Branch
        public int SaveBranch(Ent_Branch entGuest, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.SaveBranch(entGuest, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }      

        public List<Ent_Branch> SelectBranchList(int branch_id)
        {
            List<Ent_Branch> list = new List<Ent_Branch>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectBranchList(branch_id);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public Ent_Branch SelectBranch(int branch_id)
        {
            Ent_Branch list = new Ent_Branch();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectBranch(branch_id);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int DeleteBranch(Ent_Branch ent, SafeTransaction trans)
        {
            int dataResult;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.DeleteBranch(ent, trans);
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region ServerMaster
        public int UpdateOCRServer(int OcrServer, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.UpdateOCRServer(OcrServer, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        #region FRROMaster
        public int SaveFRRO(Ent_Branch entGuest, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.SaveFRRO(entGuest, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region User
        public int SaveUser(Ent_User entGuest, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.SaveUser(entGuest, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public List<Ent_User> SelectUserList(int user_id)
        {
            List<Ent_User> list = new List<Ent_User>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectUserList(user_id);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public Ent_User SelectUser(int user_id)
        {
            Ent_User list = new Ent_User();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectUser(user_id);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int DeleteUser(Ent_User ent, SafeTransaction trans)
        {
            int dataResult;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.DeleteUser(ent, trans);
                return dataResult;
            }
            catch
            {
                return -1;
            }
        }
        #endregion
        public DataTable SelectCountryList( )
        {
            DataTable dt = new DataTable();
            try
            {
                Dal_Master dal = new Dal_Master();
                dt = dal.SelectCountryList();
                return dt;
            }
            catch
            {
                dt.Clear();
            }
            return dt;
        }

        public DataTable SelectNationalityList()
        {
            DataTable dt = new DataTable();
            try
            {
                Dal_Master dal = new Dal_Master();
                dt = dal.SelectNationalityList();
                return dt;
            }
            catch
            {
                dt.Clear();
            }
            return dt;
        }

        public DataTable SelectPurposeList()
        {
            DataTable dt = new DataTable();
            try
            {
                Dal_Master dal = new Dal_Master();
                dt = dal.SelectPurposeList();
                return dt;
            }
            catch
            {
                dt.Clear();
            }
            return dt;
        }

        public DataTable SelectVisaType(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                Dal_Master dal = new Dal_Master();
                dt = dal.SelectVisaType(id);
                return dt;
            }
            catch
            {

                dt.Clear();
            }
            return dt;
        }

        public List<Ent_GuestData> SelectFRROList(DateTime FromDate, DateTime ToDate, int flag)
        {
            List<Ent_GuestData> list = new List<Ent_GuestData>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectFRROList(FromDate, ToDate, flag);
                return list;
            }
            catch
            {
                return list;
            }
        }

        #region Cuisine
        public List<Ent_Cuisine> SelectCuisine(int cuisineId)
        {
            List<Ent_Cuisine> list = new List<Ent_Cuisine>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectCuisine(cuisineId);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public List<Ent_CuisineType> SelectCuisineType()
        {
            List<Ent_CuisineType> list = new List<Ent_CuisineType>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectCuisineType();
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int SaveCuisine(Ent_Cuisine entcus, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.SaveCuisine(entcus, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteCuisine(Ent_Cuisine entcus, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.DeleteCuisine(entcus, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        #endregion Cuisine

        #region Amenities
        public List<Ent_Amenities> SelectAmenities(int amenitiesId)
        {
            List<Ent_Amenities> list = new List<Ent_Amenities>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectAmenities(amenitiesId);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int SaveAmenities(Ent_Amenities entcus, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.SaveAmenities(entcus, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteAmenities(Ent_Amenities entcus, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.DeleteAmenities(entcus, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        #endregion Cuisine

        #region NearbyPlaces
        public List<Ent_NearbyPlaces> SelectNearbyPlaces(int nearbyPlacesId)
        {
            List<Ent_NearbyPlaces> list = new List<Ent_NearbyPlaces>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectNearbyPlaces(nearbyPlacesId);
                return list;
            }
            catch
            {
                return list;
            }
        }

        public int SaveNearbyPlaces(Ent_NearbyPlaces entcus, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.SaveNearbyPlaces(entcus, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteNearbyPlaces(Ent_NearbyPlaces entcus, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.DeleteNearbyPlaces(entcus, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteUploadDetails(Ent_FileUpload entcus, SafeTransaction trans)
        {
            int dataResult = 0;
            try
            {
                Dal_Master dal = new Dal_Master();
                dataResult = dal.DeleteUploadDetails(entcus, trans);
                return dataResult;
            }
            catch
            {
                return 0;
            }
        }

        public List<Ent_FileUpload> SelectUploadDetails(int Id)
        {
            List<Ent_FileUpload> list = new List<Ent_FileUpload>();
            try
            {
                Dal_Master dal = new Dal_Master();
                list = dal.SelectUploadDetails(Id);
                return list;
            }
            catch
            {
                return list;
            }
        }

        #endregion NearbyPlaces
    }
}