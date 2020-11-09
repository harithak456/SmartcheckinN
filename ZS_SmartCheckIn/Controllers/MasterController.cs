using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZS_SmartCheckIn.Models.BAL;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Controllers
{
    public class MasterController : Controller
    {
        #region Declaration
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        Bal_Master balMaster = new Bal_Master();
        #endregion

        #region Organization
        // GET: Master
        public ActionResult Organization()
        {
            Ent_Organization ent = balMaster.SelectOrganization();
            return View(ent);
        }

        public int SaveOrganization(Ent_Organization model)
        {
            int result = 0;                     
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            model.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            model.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);        
            result = balMaster.SaveOrganization(model, trans);
            if (result > 0)
            {
                trans.Commit();                
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }
        #endregion

        #region Branch
        public ActionResult Branch()
        {
            int branch_id = Request.QueryString["branch_id"]!=null?Convert.ToInt32( Request.QueryString["branch_id"]) : 0;
            Ent_Branch ent = new Ent_Branch();
            if (branch_id != 0)
            {
                ent = balMaster.SelectBranch(branch_id);
            }

            HttpCookie OrgID = Request.Cookies["OrgID"];          
            HttpCookie OrgName = Request.Cookies["OrgName"];          

            ViewBag.OrgID = OrgID.Value.Split('=')[1];
                ViewBag.OrgName = OrgName.Value.Split('=')[1];                 
            return View(ent);
        }

        public int SaveBranch(Ent_Branch model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            model.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            model.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);    
            result = balMaster.SaveBranch(model, trans);
            if (result > 0)
            {
                trans.Commit();
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }

        public ActionResult BranchList()
        {
            List<Ent_Branch> BranchList = new List<Ent_Branch>();
            BranchList = balMaster.SelectBranchList(0);
            ViewBag.BranchList = BranchList;
            return View();
        }

        public int DeleteBranch(int Branch_ID)
        {
            Ent_Branch ent = new Ent_Branch();

            ent.Branch_ID = Branch_ID;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            var UserID = User_ID != null ? User_ID.Value.Split('=')[1] : "";
            ent.Modified_By = Convert.ToInt32(UserID);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            ent.Modified_Date = indiTime;
            SafeTransaction trans = new SafeTransaction();
            int i = balMaster.DeleteBranch(ent, trans);
            if (i > 0)
            {
                trans.Commit();
            }
            else
            {
                trans.Rollback();
            }

            return i;
        }
        #endregion

        #region FRRO
        public ActionResult FRRO()
        {
            HttpCookie BranchID = HttpContext.Request.Cookies["Branch_ID"];

            int branch_id = Convert.ToInt32(BranchID.Value.Split('=')[1]);
            Ent_Branch ent = new Ent_Branch();
            if (branch_id != 0)
            {
                ent = balMaster.SelectBranch(branch_id);
            }
            return View(ent);
        }

        public int SaveFRRO(Ent_Branch model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            model.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            model.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);
            result = balMaster.SaveFRRO(model, trans);
            if (result > 0)
            {
                trans.Commit();
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }
        #endregion

        #region User
        public ActionResult User()
        {
            int User_ID = Request.QueryString["User_ID"] != null ? Convert.ToInt32(Request.QueryString["User_ID"]) : 0;
            Ent_User ent = new Ent_User();
            if (User_ID != 0)
            {
                ent = balMaster.SelectUser(User_ID);
            }
            List<Ent_Branch> listBranch = new List<Ent_Branch>();
            listBranch = balMaster.SelectBranchList(0);
            ViewBag.listBranch = listBranch;
            return View(ent);
        }

        public int SaveUser(Ent_User model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            model.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            model.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);   
            result = balMaster.SaveUser(model, trans);
            if (result > 0)
            {
                trans.Commit();
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }

        public ActionResult UserList()
        {
            List<Ent_User> UserList = new List<Ent_User>();
            UserList = balMaster.SelectUserList(0);
            ViewBag.UserList = UserList;
            return View();
        }

        public int DeleteUser(int User_ID)
        {
            Ent_User ent = new Ent_User();

            ent.User_ID = User_ID;

            HttpCookie UserID = Request.Cookies["User_ID"];
            var UserId = UserID != null ? UserID.Value.Split('=')[1] : "";
            ent.Modified_By = Convert.ToInt32(UserId);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            ent.Modified_Date = indiTime;
            SafeTransaction trans = new SafeTransaction();
            int i = balMaster.DeleteUser(ent, trans);
            if (i > 0)
            {
                trans.Commit();
            }
            else
            {
                trans.Rollback();
            }

            return i;
        }
        #endregion

        #region Cuisine

        public ActionResult Cuisine()
        {
            List<Ent_Cuisine> CuisineList = new List<Ent_Cuisine>();
            List<Ent_Cuisine> CuisineList2 = new List<Ent_Cuisine>();
            List<Ent_CuisineType> CuisineTypeList = new List<Ent_CuisineType>();
            int CuisineId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
            Ent_Cuisine ent = new Ent_Cuisine();
            if (CuisineId != 0)
            {
                CuisineList2 = balMaster.SelectCuisine(CuisineId);
            }
            if (CuisineList2.Count > 0)
            {
                ent = CuisineList2[0];
            }
            CuisineList = balMaster.SelectCuisine(0);
            CuisineTypeList = balMaster.SelectCuisineType();
            ViewBag.CuisineList = CuisineList;
            ViewBag.CuisineTypeList = CuisineTypeList;
            return View(ent);
        }

        public int SaveCuisine(Ent_Cuisine model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            model.CreatedOn = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            model.CreatedBy = Convert.ToInt32(User_ID.Value.Split('=')[1]);       
            HttpPostedFileBase file = null;
            string filename = string.Empty;
            if (model.UploadImg != null)
            {
                file = model.UploadImg;
                filename = Convert.ToString(DateTime.Now.ToFileTime()) + "_" + Path.GetFileName(file.FileName);
            }
            model.DocName = filename;
            result = balMaster.SaveCuisine(model, trans);
            if (result > 0)
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string FilePath = Server.MapPath("~/FileUploads/Cuisine/");

                        string FullFilePath = FilePath + filename;
                        FileInfo fileInfo = new FileInfo(FilePath);
                        if (!fileInfo.Exists)
                        {
                            fileInfo.Directory.Create();
                        }
                        file.SaveAs(FullFilePath);
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return result = 0;
                    }
                }
                else
                {
                    trans.Commit();
                }
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }

        public int DeleteCuisine(Ent_Cuisine model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            string filename = string.Empty;
            result = balMaster.DeleteCuisine(model, trans);
            if (result > 0)
            {
                if (!string.IsNullOrEmpty(model.DocName))
                {
                    filename = model.DocName;
                    try
                    {
                        string FilePath = Server.MapPath("~/FileUploads/Cuisine/");
                        string FullFilePath = FilePath + filename;
                        if (System.IO.File.Exists(FullFilePath))
                        {
                            System.IO.File.Delete(FullFilePath);
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return result = 0;
                    }
                }
                else
                {
                    trans.Commit();
                }
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }


        public ActionResult CuisineDetails()
        {
            List<Ent_Cuisine> CuisineList = new List<Ent_Cuisine>();
            List<Ent_FileUpload> FileUploadList = new List<Ent_FileUpload>();
            int CuisineId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
            Ent_Cuisine ent = new Ent_Cuisine();
            if (CuisineId != 0)
            {
                CuisineList = balMaster.SelectCuisine(CuisineId);
                FileUploadList = balMaster.SelectUploadDetails(CuisineId);
            }
            if (CuisineList.Count > 0)
            {
                ent = CuisineList[0];
            }
            ViewBag.FileUploadList = FileUploadList;
            return View(ent);
        }


        #endregion

        #region Amenities

        public ActionResult Amenities()
        {
            List<Ent_Amenities> AmenitiesList = new List<Ent_Amenities>();
            List<Ent_Amenities> AmenitiesList2 = new List<Ent_Amenities>();
            int AmenitiesId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
            Ent_Amenities ent = new Ent_Amenities();
            if (AmenitiesId != 0)
            {
                AmenitiesList2 = balMaster.SelectAmenities(AmenitiesId);
            }
            if (AmenitiesList2.Count > 0)
            {
                ent = AmenitiesList2[0];
            }
            AmenitiesList = balMaster.SelectAmenities(0);
            ViewBag.AmenitiesList = AmenitiesList;
            return View(ent);
        }

        public int SaveAmenities(Ent_Amenities model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            model.CreatedOn = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            model.CreatedBy = Convert.ToInt32(User_ID.Value.Split('=')[1]);
            HttpPostedFileBase file = null;
            string filename = string.Empty;
            if (model.UploadImg != null)
            {
                file = model.UploadImg;
                filename = Convert.ToString(DateTime.Now.ToFileTime()) + "_" + Path.GetFileName(file.FileName);
            }
            model.DocName = filename;
            result = balMaster.SaveAmenities(model, trans);
            if (result > 0)
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string FilePath = Server.MapPath("~/FileUploads/Amenities/");

                        string FullFilePath = FilePath + filename;
                        FileInfo fileInfo = new FileInfo(FilePath);
                        if (!fileInfo.Exists)
                        {
                            fileInfo.Directory.Create();
                        }
                        file.SaveAs(FullFilePath);
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return result = 0;
                    }
                }
                else
                {
                    trans.Commit();
                }
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }

        public int DeleteAmenities(Ent_Amenities model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            string filename = string.Empty;
            result = balMaster.DeleteAmenities(model, trans);
            if (result > 0)
            {
                if (!string.IsNullOrEmpty(model.DocName))
                {
                    filename = model.DocName;
                    try
                    {
                        string FilePath = Server.MapPath("~/FileUploads/Amenities/");
                        string FullFilePath = FilePath + filename;
                        if (System.IO.File.Exists(FullFilePath))
                        {
                            System.IO.File.Delete(FullFilePath);
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return result = 0;
                    }
                }
                else
                {
                    trans.Commit();
                }
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }


        public ActionResult AmenitiesDetails()
        {
            List<Ent_Amenities> AmenitiesList = new List<Ent_Amenities>();
            List<Ent_FileUpload> FileUploadList = new List<Ent_FileUpload>();
            int AmenitiesId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
            Ent_Amenities ent = new Ent_Amenities();
            if (AmenitiesId != 0)
            {
                AmenitiesList = balMaster.SelectAmenities(AmenitiesId);
                FileUploadList = balMaster.SelectUploadDetails(AmenitiesId);
            }
            if (AmenitiesList.Count > 0)
            {
                ent = AmenitiesList[0];
            }
            ViewBag.FileUploadList = FileUploadList;
            return View(ent);
        }


        #endregion

        #region NearbyPlaces

        public ActionResult NearbyPlaces()
        {
            List<Ent_NearbyPlaces> NearbyPlacesList = new List<Ent_NearbyPlaces>();
            List<Ent_NearbyPlaces> NearbyPlacesList2 = new List<Ent_NearbyPlaces>();
            int NearbyPlacesId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
            Ent_NearbyPlaces ent = new Ent_NearbyPlaces();
            if (NearbyPlacesId != 0)
            {
                NearbyPlacesList2 = balMaster.SelectNearbyPlaces(NearbyPlacesId);
            }
            if (NearbyPlacesList2.Count > 0)
            {
                ent = NearbyPlacesList2[0];
            }
            NearbyPlacesList = balMaster.SelectNearbyPlaces(0);
            ViewBag.NearbyPlacesList = NearbyPlacesList;
            return View(ent);
        }

        public int SaveNearbyPlaces(Ent_NearbyPlaces model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            model.CreatedOn = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            model.CreatedBy = Convert.ToInt32(User_ID.Value.Split('=')[1]);          
            HttpPostedFileBase file = null;
            string filename = string.Empty;
            if (model.UploadImg != null)
            {
                file = model.UploadImg;
                filename = Convert.ToString(DateTime.Now.ToFileTime()) + "_" + Path.GetFileName(file.FileName);
            }
            model.DocName = filename;
            result = balMaster.SaveNearbyPlaces(model, trans);
            if (result > 0)
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string FilePath = Server.MapPath("~/FileUploads/NearbyPlaces/");

                        string FullFilePath = FilePath + filename;
                        FileInfo fileInfo = new FileInfo(FilePath);
                        if (!fileInfo.Exists)
                        {
                            fileInfo.Directory.Create();
                        }
                        file.SaveAs(FullFilePath);
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return result = 0;
                    }
                }
                else
                {
                    trans.Commit();
                }
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }

        public int DeleteNearbyPlaces(Ent_NearbyPlaces model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            string filename = string.Empty;
            result = balMaster.DeleteNearbyPlaces(model, trans);
            if (result > 0)
            {
                if (!string.IsNullOrEmpty(model.DocName))
                {
                    filename = model.DocName;
                    try
                    {
                        string FilePath = Server.MapPath("~/FileUploads/NearbyPlaces/");
                        string FullFilePath = FilePath + filename;
                        if (System.IO.File.Exists(FullFilePath))
                        {
                            System.IO.File.Delete(FullFilePath);
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return result = 0;
                    }
                }
                else
                {
                    trans.Commit();
                }
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }


        public ActionResult NearbyPlacesDetails()
        {
            List<Ent_NearbyPlaces> NearbyPlacesList = new List<Ent_NearbyPlaces>();
            List<Ent_FileUpload> FileUploadList = new List<Ent_FileUpload>();
            int NearbyPlacesId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
            Ent_NearbyPlaces ent = new Ent_NearbyPlaces();
            if (NearbyPlacesId != 0)
            {
                NearbyPlacesList = balMaster.SelectNearbyPlaces(NearbyPlacesId);
                FileUploadList = balMaster.SelectUploadDetails(NearbyPlacesId);
            }
            if (NearbyPlacesList.Count > 0)
            {
                ent = NearbyPlacesList[0];
            }
            ViewBag.FileUploadList = FileUploadList;
            return View(ent);
        }

        public int DeleteUploadDetails(Ent_FileUpload model)
        {
            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            string filename = string.Empty;
            result = balMaster.DeleteUploadDetails(model, trans);
            if (result > 0)
            {
                if (!string.IsNullOrEmpty(model.DocName))
                {
                    filename = model.DocName;
                    try
                    {
                        string FilePath = Server.MapPath("~/FileUploads/" + Enum.GetName(typeof(General.DocumentUploadType), model.DocType) + "/");
                        string FullFilePath = FilePath + filename;
                        if (System.IO.File.Exists(FullFilePath))
                        {
                            System.IO.File.Delete(FullFilePath);
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return result = 0;
                    }
                }
                else
                {
                    trans.Commit();
                }
            }
            else
            {
                trans.Rollback();
            }
            return result;
        }


        #endregion
    }
}