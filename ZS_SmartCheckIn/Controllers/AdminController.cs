using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZS_SmartCheckIn.Models.BAL;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Controllers
{
    public class AdminController : Controller
    {
        #region Declaration      
        Bal_Guest balGuest = new Bal_Guest();
        Bal_GuestData balGuestData = new Bal_GuestData();
        Bal_Master balMaster = new Bal_Master();
        Bal_User balUser = new Bal_User();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        #endregion

        // GET: Admin
        public ActionResult Dashboard()
        {           
                HttpCookie User_ID = Request.Cookies["User_ID"];
            var UserID = User_ID != null ? User_ID.Value.Split('=')[1] : "";
            if (UserID == "")
            {
                return RedirectToAction("Login","User");
            }
            else
            {
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd HH:m:s"));
                int s = indiTime.Hour;
                List<Ent_Guest> Guestlist = new List<Ent_Guest>();
                Guestlist = balGuest.SelectGuestList();
                ViewBag.TotalGuest = Guestlist.Count;
             
                ViewBag.PendingCheckIn = Guestlist.Where(x => x.Is_Active == 3 && Convert.ToDateTime(x.Arrival_Time).Hour < s).Count();
                ViewBag.NotVerified = Guestlist.Where(x => x.Is_Active == 2).Count();
                ViewBag.NewGuest = Guestlist.Where(x => x.Is_Active == 1).Count();
                ViewBag.CheckedIn = Guestlist.Where(x => x.Is_Active == 4).Count();

                List<Ent_Guest> checkinList = new List<Ent_Guest>();
                checkinList = Guestlist.Where(x => x.Is_Active == 4).ToList<Ent_Guest>();

                List<Ent_Guest> todayCheckinList = new List<Ent_Guest>();
              
                todayCheckinList = Guestlist.Where(x => x.Is_Active != 4 && x.Arrival_Date.ToShortDateString() == indiTime.ToShortDateString()).ToList<Ent_Guest>();

                DataTable dt = balGuest.SelectYesterdayCount();
                if (dt.Rows.Count > 0)
                {
                    ViewBag.WalkingCount = dt.Rows[0]["WalkingCount"];
                    ViewBag.BookingCount = dt.Rows[0]["BookingCount"];
                }


                ViewBag.checkinList = checkinList;
                ViewBag.todayCheckinList = todayCheckinList;
                ViewBag.logList = GetLogActivity();
                return View();
            }
        }

        public List<Ent_Guest> GetLogActivity()
        {
            List<Ent_Guest> logList = new List<Ent_Guest>();
            logList = balUser.SelectLogActivity();
            return logList;
        }

        public JsonResult GetNotification()
        {
            int result = 0;
            result = balGuest.GetNotification();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public RedirectToRouteResult UpdateNotification()
        {               
            int i = balGuest.UpdateNotification();
            return RedirectToAction("GuestList", "Guest",new { flag = "2" });         
        }

        public ActionResult Agreement(string id)
        {
            id = "CUSZU8";
            ViewBag.CustomerCode = id;
            return View();
        }
        public int SaveGuestSignature(Ent_Guest entGuest)
        {
            HttpFileCollectionBase files = Request.Files;
            int result = 0;
            Bal_Guest balGuest = new Bal_Guest();

            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            if (files.Count > 0)
            {                           
                entGuest.Created_Date = indiTime;
                HttpCookie User_ID = Request.Cookies["User_ID"];
                entGuest.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);

                HttpCookie BranchID = Request.Cookies["Branch_ID"];                   
                entGuest.Branch_ID = Convert.ToInt32(BranchID.Value.Split('=')[1]);

                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName);
                string File_Name = "Sig-" + entGuest.Customer_Code + ".jpg";
                file.SaveAs(Server.MapPath("~/Signature/" + File_Name));
                entGuest.Guest_Signature = File_Name;

                result = balGuest.SaveGuestSignature(entGuest, trans);            
                if (result > 0)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
            }
            return result;
        }

        public ActionResult Reports()
        {
            List<Ent_Guest> Guestlist = new List<Ent_Guest>();
            Guestlist = balGuest.SelectGuestList();
            ViewBag.Guestlist = Guestlist;
            return View();
        }

        public ActionResult FRROReport()
        {
            List<Ent_GuestData> Guestlist = new List<Ent_GuestData>();
            Guestlist = balMaster.SelectFRROList(DateTime.Now, DateTime.Now, 0);
            ViewBag.Guestlist = Guestlist;
            return View();
        }

        public ActionResult ReportDetail(string customercode)
        {
            int i = balGuest.UpdateNotification();
            List<Ent_GuestData> listGuest = new List<Ent_GuestData>();
            listGuest = balGuestData.SelectGuestsList(customercode);
            ViewBag.listGuest = listGuest;
            Ent_Guest entData = balGuest.SelectGroupLeader(customercode);

            try
            {
                if (Global.glbVisaTypeDT.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(entData.entGuestData.Guest_VisaType))
                    {

                        var queryy = from c in Global.glbVisaTypeDT.AsEnumerable()
                                     where c.Field<string>("visaid") == entData.entGuestData.Guest_VisaType.ToUpper()
                                     select new
                                     {
                                         visaTYpe = c.Field<string>("visatype")
                                     };
                        foreach (var m in queryy)
                        {
                            entData.entGuestData.Guest_VisaType = m.visaTYpe;
                        }
                    }
                }

                if (Global.glbPurposeDT.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(entData.entGuestData.Guest_PurposeofVisit))
                    {
                        var query = from c in Global.glbPurposeDT.AsEnumerable()
                                    where c.Field<string>("purposefrroid") == entData.entGuestData.Guest_PurposeofVisit
                                    select new
                                    {
                                        purposeofvisit = c.Field<string>("purposeofvisit")
                                    };
                        foreach (var m in query)
                        {
                            entData.entGuestData.Guest_PurposeofVisit = m.purposeofvisit;
                        }
                    }
                }
                if (Global.glbCountryDT.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(entData.entGuestData.Guest_Country))
                    {

                        var query = from c in Global.glbCountryDT.AsEnumerable()
                                    where c.Field<string>("Country_iso3") == entData.entGuestData.Guest_Country.ToUpper()
                                    select new
                                    {
                                        countryname = c.Field<string>("Country_name")
                                    };
                        foreach (var m in query)
                        {
                            entData.entGuestData.Guest_Country = m.countryname;
                        }
                    }

                    if (!string.IsNullOrEmpty(entData.entGuestData.Guest_Nationality))
                    {
                        var query2 = from c in Global.glbCountryDT.AsEnumerable()
                                     where c.Field<string>("Country_iso3") == entData.entGuestData.Guest_Nationality.ToUpper()
                                     select new
                                     {
                                         nationalityname = c.Field<string>("Country_name")
                                     };
                        foreach (var m in query2)
                        {
                            entData.entGuestData.Guest_Nationality = m.nationalityname;
                        }
                    }

                    if (!string.IsNullOrEmpty(entData.entGuestData.Guest_VisaPOICountry))
                    {
                        var query2 = from c in Global.glbCountryDT.AsEnumerable()
                                     where c.Field<string>("Country_iso3") == entData.entGuestData.Guest_VisaPOICountry.ToUpper()
                                     select new
                                     {
                                         nationalityname = c.Field<string>("Country_name")
                                     };
                        foreach (var m in query2)
                        {
                            entData.entGuestData.Guest_VisaPOICountry = m.nationalityname;
                        }
                    }
                }
            }
            catch
            {

            }
            return View(entData);
        }
        public JsonResult GuestReportSearch(DateTime FromDate, DateTime ToDate)
        {           
            List<Ent_Guest> listItemWise = new List<Ent_Guest>();
            listItemWise = balGuest.GuestReportSearch(FromDate, ToDate,1);
            return Json(listItemWise, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FRROReportSearch(DateTime FromDate, DateTime ToDate)
        {
            List<Ent_GuestData> listItemWise = new List<Ent_GuestData>();
            listItemWise = balMaster.SelectFRROList(FromDate, ToDate, 1);
            return Json(listItemWise, JsonRequestBehavior.AllowGet);
        }
    }
}