using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZS_SmartCheckIn.Models.BAL;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Controllers
{
    public class HomeController : Controller
    {
        #region Declaration
        Bal_User balUser = new Bal_User();
        Bal_GuestData balGuestData = new Bal_GuestData();
        Bal_Guest balGuest = new Bal_Guest();
        Bal_Master balMaster = new Bal_Master();
        #endregion

        // Guest Dashboard
        public ActionResult Dashboard()
        {
            HttpCookie User_Name = Request.Cookies["User_Name"];
            var UserName = User_Name != null ? User_Name.Value.Split('=')[1] : "";
            if (UserName == "")
            {
                 return RedirectToAction("GuestLogin","User");
            }
            else
            {
                ViewBag.GuestName = UserName;
                HttpCookie OrgName = Request.Cookies["OrgName"];
                ViewBag.HotelName = OrgName.Value.Split('=')[1];

                HttpCookie Customer_Code = Request.Cookies["Customer_Code"];
                string CustomerCode = Customer_Code.Value.Split('=')[1];

                Ent_Guest entGuest = balGuest.SelectGuest(CustomerCode);

                ViewBag.BookingPortal = entGuest.Booking_Portal;
                ViewBag.IsActive = entGuest.Is_Active;
                ViewBag.Arrival_Date = entGuest.Arrival_Date.ToShortDateString();

                List<Ent_GuestData> listGuest = new List<Ent_GuestData>();
                listGuest = balGuestData.SelectGuestsList(CustomerCode);
                string uploadedname = "";
                foreach (var item in listGuest)
                {
                    uploadedname += item.Guest_Firstname + " " + item.Guest_Lastname + ",";
                }
                ViewBag.uploadedname = uploadedname;

                List<Ent_Amenities> AmenitiesList = new List<Ent_Amenities>();
                AmenitiesList = balMaster.SelectAmenities(0);
                ViewBag.AmenitiesList = AmenitiesList;

                List<Ent_NearbyPlaces> NearbyPlacesList = new List<Ent_NearbyPlaces>();
                NearbyPlacesList = balMaster.SelectNearbyPlaces(0);
                ViewBag.NearbyPlacesList = NearbyPlacesList;

                List<Ent_Cuisine> CuisineList = new List<Ent_Cuisine>();
                CuisineList = balMaster.SelectCuisine(0);
                ViewBag.CuisineList = CuisineList;

                int total = entGuest.Guest_Accompany + 1;
                int completed = listGuest.Count;
                ViewBag.Completed = completed;
                ViewBag.Pending = total - completed;
                return View();
            }
        }

        public ActionResult MoreCuisine()
        {
            List<Ent_Cuisine> CuisineList = new List<Ent_Cuisine>();
            CuisineList = balMaster.SelectCuisine(0);
            ViewBag.CuisineList = CuisineList;
            return View();
        }
            public ActionResult NearbyPlacesView()
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

        //update guest Password
        public int UpdatePassword(string Username, string Password,string Token)
        {
            int i = 0;
                       
                Ent_Guest entG = new Ent_Guest();
                entG.Guest_Username = Username;
                entG.Guest_Password = Password;
                entG.UserToken = Token;
                SafeTransaction trans = new SafeTransaction();
                int result = balUser.UpdatePassword(entG, trans);
                if (result > 0)
                 {
                    trans.Commit();
                      i = 1;                    
                }
                else {
                    trans.Rollback(); 
                        i = 0;
                }
           
            return i;
        }
    }
}