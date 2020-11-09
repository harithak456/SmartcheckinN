using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ZS_SmartCheckIn.Models.BAL;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Controllers
{
    public class GuestController : Controller
    {
        #region Declaration
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        Bal_Guest balGuest = new Bal_Guest();
        Bal_Master balMaster = new Bal_Master();
        Bal_GuestData balGuestData = new Bal_GuestData();

        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "123456789";
        const string SPECIALS = @"!@£$%^&*()#€";
        #endregion
        // GET: Guest     
        public ActionResult Register()
        {
            Ent_Guest entG = new Ent_Guest();
            string customercode = Request.QueryString["customercode"] == null ? "" : Request.QueryString["customercode"].ToString();
            if (customercode != "")
            {
                entG = balGuest.SelectGuest(customercode);

            }
            return View(entG);
        }

        public JsonResult SaveNewGuest(Ent_Guest entGuest)
        {
            string mode = "";
            int result = 0;
            Bal_Guest balGuest = new Bal_Guest();
            string CustomerCode = "";         
            if (string.IsNullOrEmpty(entGuest.Customer_Code))
            {
                CustomerCode = "CUS" + Global.GetCustomerCode();
                entGuest.Customer_Code = CustomerCode;
                if (entGuest.FrequentVisitor == 0)
                {             
                    entGuest.Guest_Password = GeneratePassword(true, false, true, false, 6);
                    mode = "New Guest";
                }               
            }
            else
            {
                CustomerCode = entGuest.Customer_Code;
                mode = "Guest Update";
            }

            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            entGuest.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            entGuest.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);
            HttpCookie BranchID = Request.Cookies["Branch_ID"];                  
            entGuest.Branch_ID = Convert.ToInt32(BranchID.Value.Split('=')[1]);
            result = balGuest.SaveNewGuest(entGuest, trans, mode);                    
            string result1 = "";
            if (result > 0)
            {
                trans.Commit();
                result1 = result + "," + CustomerCode;
                if (entGuest.sendmail == true)
                {
                    SendPasswordMail(CustomerCode);
                }
                if (entGuest.sendsms == true)
                {
                    SendPasswordSMS(CustomerCode);
                }
            }
            else if (result == -1)
            {
                result1 = "-1";
                trans.Rollback();
            }
            else if (result == -2)
            {
                result1 = "-2";
                trans.Rollback();
            }
            else
            {
                trans.Rollback();
            }
            return Json(result1, JsonRequestBehavior.AllowGet);
        }

        public int SendPasswordSMS(string customercode)
        {
            Ent_Guest entGuest = balGuest.SelectGuest(customercode);
            int msg = sendSMS(entGuest.Guest_MobileNo, entGuest.Guest_Password, entGuest.Guest_Username);
            return msg;
        }

        public int sendSMS(string mobile, string password, string username)
        {
            int retResult = 0;
            String result;
            string apiKey = "+YTBusBlkY0-FpeztF8UVZMxbRKJgUcaJU5Jdp5Os8";
            string numbers = mobile; // in a comma seperated list
            //string message = "Lemon Tree.Your booking is confirmed.Request you to fill the guest details by uploading ID's in following link. " +
            //                 "" + "https://zscheckin.atintellilabs.live/User/GuestLogin" + ", Username: " + username + ", Password: " + password + "";

            string message = "Lemon Tree.Your booking is confirmed.Request you to fill the guest details by uploading ID's in following link. " +
                           "" + "https://smartcheckin.atintellilabs.live/User/GuestLogin" + ", Username: " + username + ", Password: " + password + "";

            string sender = "TXTLCL";

            String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                JObject json = JObject.Parse(result);
                string d = json.SelectToken("status").Value<string>();
                // Close and clean up the StreamReader
                if (d == "failure")
                {
                    retResult = 0;
                }
                else if (d == "success")
                {
                    retResult = 1;
                }
                sr.Close();

            }
            return retResult;
        }

        public int sendConfirmSMS(string customercode)
        {
            Ent_Guest entGuest = balGuest.SelectGuest(customercode);
            string mobile = entGuest.Guest_MobileNo;
            int retResult = 0;
            String result;
            string apiKey = "+YTBusBlkY0-FpeztF8UVZMxbRKJgUcaJU5Jdp5Os8";
            string numbers = mobile; // in a comma seperated list
            string message = "Lemon Tree..Your ID's Verified..Check In Code is : " + entGuest.Confirmation_Code + "";
            string sender = "TXTLCL";
            String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                JObject json = JObject.Parse(result);
                string d = json.SelectToken("status").Value<string>();
                // Close and clean up the StreamReader
                if (d == "failure")
                {
                    retResult = 0;
                }
                else if (d == "success")
                {
                    retResult = 1;
                }
                sr.Close();

            }
            return retResult;
        }

        private string PopulatePasswordBody(Ent_Guest entguest)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlPage11.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Username}", entguest.Guest_Username);
            //body = body.Replace("{Url}", "https://zscheckin.atintellilabs.live/User/GuestLogin");
            body = body.Replace("{Url}", "https://smartcheckin.atintellilabs.live/User/GuestLogin");
            body = body.Replace("{Password}", entguest.Guest_Password);

            HttpCookie OrgName = Request.Cookies["OrgName"];
            body = body.Replace("{orgname}", OrgName.Value.Split('=')[1]);

            HttpCookie BranchMail = Request.Cookies["BranchMail"];
            body = body.Replace("{branchmail}", BranchMail.Value.Split('=')[1]);

            HttpCookie BranchPhone = Request.Cookies["BranchPhone"];
            body = body.Replace("{branchphone}", BranchPhone.Value.Split('=')[1]);

            HttpCookie BranchAddress = Request.Cookies["BranchAddress"];
            body = body.Replace("{branchaddress}", BranchAddress.Value.Split('=')[1]);
         
            //body = body.Replace("{orgname}", Global.OrgName);
            //body = body.Replace("{branchmail}", Global.BranchMail);
            //body = body.Replace("{branchphone}", Global.BranchPhone);
            //body = body.Replace("{branchaddress}", Global.BranchAddress);
            body = body.Replace("{CustomerCode}", entguest.Customer_Code);
            body = body.Replace("{ArrivalDate}", entguest.Arrival_Date.ToShortDateString());
            body = body.Replace("{GuestName}", entguest.Guest_Firstname + " " + entguest.Guest_Lastname);
            HttpCookie User_Name = Request.Cookies["User_Name"];
            var UserName = User_Name != null ? User_Name.Value.Split('=')[1] : "";
            body = body.Replace("{LoginUser}", UserName);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            body = body.Replace("{datetime}",indiTime.ToString());
            return body;
        }

        private string PopulateConfirmBody(Ent_Guest entguest)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlPage33.html")))
            {
                body = reader.ReadToEnd();
            }

            HttpCookie OrgName = Request.Cookies["OrgName"];
            body = body.Replace("{orgname}", OrgName.Value.Split('=')[1]);

            HttpCookie BranchMail = Request.Cookies["BranchMail"];
            body = body.Replace("{branchmail}", BranchMail.Value.Split('=')[1]);

            HttpCookie BranchPhone = Request.Cookies["BranchPhone"];
            body = body.Replace("{branchphone}", BranchPhone.Value.Split('=')[1]);

            HttpCookie BranchAddress = Request.Cookies["BranchAddress"];
            body = body.Replace("{branchaddress}", BranchAddress.Value.Split('=')[1]);


            //body = body.Replace("{orgname}", Global.OrgName);
            //body = body.Replace("{branchmail}", Global.BranchMail);
            //body = body.Replace("{branchphone}", Global.BranchPhone);
            //body = body.Replace("{branchaddress}", Global.BranchAddress);
            body = body.Replace("{GuestName}", entguest.Guest_Firstname + " " + entguest.Guest_Lastname);
            body = body.Replace("{ArrivalDate}", entguest.Arrival_Date.ToShortDateString());
            body = body.Replace("{ConfirmationCode}", entguest.Confirmation_Code);

            HttpCookie Branch_ContactPerson = Request.Cookies["Branch_ContactPerson"];
            body = body.Replace("{contactperson}", Branch_ContactPerson.Value.Split('=')[1]);

            //body = body.Replace("{contactperson}", Global.Branch_ContactPerson);
            HttpCookie User_Name = Request.Cookies["User_Name"];     
            body = body.Replace("{LoginUser}", User_Name.Value.Split('=')[1]);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            body = body.Replace("{datetime}",indiTime.ToString());
            return body;
        }


        private string PopulateCheckinBody(Ent_Guest entguest, string url)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlPage4.html")))
            {
                body = reader.ReadToEnd();
            }

            HttpCookie OrgName = Request.Cookies["OrgName"];
            body = body.Replace("{orgname}", OrgName.Value.Split('=')[1]);

            HttpCookie BranchMail = Request.Cookies["BranchMail"];
            body = body.Replace("{branchmail}", BranchMail.Value.Split('=')[1]);

            HttpCookie BranchPhone = Request.Cookies["BranchPhone"];
            body = body.Replace("{branchphone}", BranchPhone.Value.Split('=')[1]);

            HttpCookie BranchAddress = Request.Cookies["BranchAddress"];
            body = body.Replace("{branchaddress}", BranchAddress.Value.Split('=')[1]);

            HttpCookie Branch_ContactPerson = Request.Cookies["Branch_ContactPerson"];
            body = body.Replace("{contactperson}", Branch_ContactPerson.Value.Split('=')[1]);

            //body = body.Replace("{orgname}", Global.OrgName);
            //body = body.Replace("{branchmail}", Global.BranchMail);
            //body = body.Replace("{branchphone}", Global.BranchPhone);
            //body = body.Replace("{branchaddress}", Global.BranchAddress);
            //body = body.Replace("{contactperson}", Global.Branch_ContactPerson);
            body = body.Replace("{Url}", url);
          
            body = body.Replace("{GuestName}", entguest.Guest_Firstname + " " + entguest.Guest_Lastname);
            return body;
        }


        public int SendPasswordMail(string customercode)
        {
            Ent_Guest entGuest = balGuest.SelectGuest(customercode);
            string Body = "";
            Body = PopulatePasswordBody(entGuest);
            Email em = new Email();
            int r = em.SendMail(Body, entGuest.Guest_Email, "Your Hotel Booking Confirmed");

            //int r = SendMail(Body, entGuest.Guest_Email, "Your Hotel Booking Confirmed");
            Bal_User balUser = new Bal_User();

            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            entGuest.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            entGuest.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);
            int l = balUser.InsertLogActivity(entGuest, entGuest.Guest_ID, "Password Mail");
            return r;
        }

        public int SendCheckInMail(Ent_Guest entGuest)
        {

            string Body = "";
            Body = PopulateCheckinBody(entGuest, "http://localhost:4765/Admin/Agreement?id=customercode");
            Email em = new Email();
            int r = em.SendMail(Body, entGuest.Guest_Email, "Check-In Agreement Submission");
            //int r = SendMail(Body, entGuest.Guest_Email, "Check-In Agreement Submission");
            Bal_User balUser = new Bal_User();

            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            entGuest.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            entGuest.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);      
            int l = balUser.InsertLogActivity(entGuest, entGuest.Guest_ID, "CheckIn Mail");
            return r;
        }

        public int SendConfirmMail(string customercode)
        {
            Ent_Guest entGuest = balGuest.SelectGuest(customercode);
            string Body = "";
            Body = PopulateConfirmBody(entGuest);
            Email em = new Email();
            int r = em.SendMail(Body, entGuest.Guest_Email, "Your ID Details Have Been Confirmed");
            //int r = SendMail(Body, entGuest.Guest_Email, "Your ID Details Have Been Confirmed");
            Bal_User balUser = new Bal_User();

            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            entGuest.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            entGuest.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);
            int l = balUser.InsertLogActivity(entGuest, entGuest.Guest_ID, "Id Verified Mail");
            return r;
        }


        public int DeleteGuest(string Customer_Code)
        {
            Ent_Guest ent = new Ent_Guest();

            ent.Customer_Code = Customer_Code;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            var UserID = User_ID != null ? User_ID.Value.Split('=')[1] : "";
            ent.Modified_By = Convert.ToInt32(UserID);

            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            ent.Modified_Date = indiTime;          
            SafeTransaction trans = new SafeTransaction();
            int i = balGuest.DeleteGuest(ent, trans);
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


        public int UpdateCheckIn(string Customer_Code)
        {
            Ent_Guest entGuest = balGuest.SelectGuest(Customer_Code);
            Ent_Guest ent = new Ent_Guest();
            ent.Is_Active = 4;
            ent.Customer_Code = Customer_Code;

            HttpCookie User_ID = Request.Cookies["User_ID"];
            var UserID = User_ID != null ? User_ID.Value.Split('=')[1] : "";
            ent.Modified_By = Convert.ToInt32(UserID);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            ent.Modified_Date = indiTime;
            ent.Notification_Status = 0;
            ent.Confirmation_Code = "";
            SafeTransaction trans = new SafeTransaction();

            int i = balGuest.UpdateActiveStatus(ent, trans);
            if (i > 0)
            {
                trans.Commit();
                // SendCheckInMail(entGuest);
            }
            else
            {
                trans.Rollback();
            }

            return i;
        }

        public int ConfirmGuestDetail(string Customer_Code)
        {
            Ent_Guest ent = new Ent_Guest();
            ent.Is_Active = 3;
            ent.Customer_Code = Customer_Code;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            var UserID = User_ID != null ? User_ID.Value.Split('=')[1] : "";
            ent.Modified_By = Convert.ToInt32(UserID);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            ent.Modified_Date = indiTime;         
            ent.Notification_Status = 0;
            ent.Confirmation_Code = Global.GetConfirmationCode();
            SafeTransaction trans = new SafeTransaction();
            int i = balGuest.UpdateActiveStatus(ent, trans);
            if (i > 0)
            {
                trans.Commit();
                SendConfirmMail(Customer_Code);
                sendConfirmSMS(Customer_Code);
            }
            else
            {
                trans.Rollback();
            }

            return i;
        }

        public JsonResult SelectFrequentGuest(string Guest_Email)
        {
           
            Ent_Guest entData = balGuest.SelectGuestSearch(Guest_Email);
            return Json(entData,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewGuest(string customercode)
        {
            int i = balGuest.UpdateNotification();          
            List<Ent_GuestData> listGuest = new List<Ent_GuestData>();
            listGuest = balGuestData.SelectGuestsList(customercode);
            ViewBag.listGuest = listGuest;
            Ent_Guest entData = balGuest.SelectGroupLeader(customercode);

            if (Global.glbVisaTypeDT == null)
            {
                Global.glbVisaTypeDT = balMaster.SelectVisaType("");
            }
            try
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
            catch
            {

            }
            if (Global.glbPurposeDT == null)
            {
                Global.glbPurposeDT = balMaster.SelectPurposeList();
            }
            try
            {
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
            }
            catch
            {

            }
            if (Global.glbCountryDT == null)
            {
                Global.glbCountryDT = balMaster.SelectCountryList();
            }
            try
            {
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

     

        public ActionResult GuestList(string flag)
        {
            List<Ent_Guest> Guestlist = new List<Ent_Guest>();
            Guestlist = balGuest.SelectGuestList();
            ViewBag.Guestlist = Guestlist;
            ViewBag.flag = flag;
            return View();
        }

        public JsonResult UpdateProfile(Ent_GuestData ent)
        {
            int i = 0;
            string File_Name = "";
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFileBase file = files[0];
                if (files["Guest_ProfileImage"] != null)
                {
                    HttpPostedFileBase file2 = files["Guest_ProfileImage"];
                    string extension = Path.GetExtension(file2.FileName);
                    File_Name = "PIC-" + ent.Guestdata_id + ".jpg";
                    file2.SaveAs(Server.MapPath("~/ProfileImages/" + File_Name));
                    ent.Guest_ProfileImage = File_Name;

                    SafeTransaction trans = new SafeTransaction();
                    i = balGuest.UpdateProfile(ent, trans);
                    if (i > 0)
                    {
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                else
                {
                    ent.Guest_ProfileImage = "";
                }
            }
            return Json(File_Name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FRRO()
        {
            return View();
        }

        public string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
        {
            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            System.Random _random = new Random();
            int counter;
            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;
            if (useUppercase) charSet += UPPER_CAES;
            if (useNumbers) charSet += NUMBERS;
            if (useSpecial) charSet += SPECIALS;
            for (counter = 0; counter < passwordSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }
            return String.Join(null, _password);
        }
    
        //Added By Arun on 26-06-2020
        #region Walkin Guest
        

        public ActionResult WalkInGuest()
        {
            HttpCookie User_Name = Request.Cookies["User_Name"];
            var UserName = User_Name != null ? User_Name.Value.Split('=')[1] : "";
            if (UserName == "")
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                Ent_Guest entData = new Ent_Guest();
                List<Ent_GuestData> listGuest = new List<Ent_GuestData>();
                string guestcode = Request.QueryString["Guest_Code"] == null ? "" : Request.QueryString["Guest_Code"].ToString();
                if (!string.IsNullOrEmpty(guestcode))
                {
                    entData = balGuestData.SelectGuestData(guestcode);
                    ViewBag.CustomerCode = entData.entGuestData.Customer_Code;
                    ViewBag.MobileNo = entData.entGuestData.Guest_PhoneNo;
                    ViewBag.Email = entData.entGuestData.Guest_Email;
                    ViewBag.CustomerCode = entData.entGuestData.Customer_Code;
                    ViewBag.GuestID = entData.entGuestData.Guest_ID;
                    Ent_Guest entGuest = balGuest.SelectGuest(ViewBag.CustomerCode);
                    ViewBag.Guest_Accompany = entData.Guest_Accompany;
                }
                return View(entData);
            }
        }
        [HttpPost]
        public int Capture(string name)
        {
            int result = 0;
            try
            {
                string filename = string.Empty;
                string fname = string.Empty;
                HttpPostedFileBase file = null;
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        filename = Path.GetFileName(Request.Files[i].FileName);
                        string fileExt = Path.GetExtension(Request.Files[i].FileName);
                        file = files[i];
                        fname = "webcam_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
                        file.SaveAs(Path.Combine(Server.MapPath("~/scanned/Profile/"), fname));
                        result = 1;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        //Saving Walkin Customer
        public JsonResult SaveWalkinGuest(Ent_GuestData entGuest)
        {
            ArrayList arr = new ArrayList();
            bool IsEdit = false;
            string CustomerCode = "";
            if (string.IsNullOrEmpty(entGuest.Customer_Code))
            {
                CustomerCode = "CUS" + Global.GetCustomerCode();
                entGuest.Customer_Code = CustomerCode;
                //entGuest.Guest_Password = GeneratePassword(true, false, true, false, 6);
                //mode = "New Guest";
            }
            else
            {
                CustomerCode = entGuest.Customer_Code;
                //mode = "Guest Update";
            }
            if (entGuest.Guestdata_id > 0)
            {
                IsEdit = true;
            }
            else
            {
                if (entGuest.Is_GroupLeader == 1)
                {
                    entGuest.Guest_Code = entGuest.Customer_Code;
                }
                else
                {
                    string GuestCode = "WEB" + Global.GetGuestCode();
                    entGuest.Guest_Code = GuestCode;
                }
            }

            int result = 0;
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            entGuest.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            entGuest.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]);
            HttpCookie BranchID = Request.Cookies["Branch_ID"];
            entGuest.Branch_ID = Convert.ToInt32(BranchID.Value.Split('=')[1]);             
            entGuest.Arrival_Time= indianTime.ToString("HH:mm");
            result = balGuestData.SaveWalkingGuestData(entGuest, trans, IsEdit);
            if (result > 0)
            {
                //if (IsEdit == false)
                //{
                    HttpFileCollectionBase files = Request.Files;
                    if (files.Count > 0)
                    {
                        HttpPostedFileBase file = files["Guest_Document"];
                    if (file != null)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        string File_Name = "IDCard1-" + result + ".jpg";
                        file.SaveAs(Server.MapPath("~/CardImages/" + File_Name));
                        entGuest.Guest_Document = File_Name;
                    }
                    else
                    { entGuest.Guest_Document = ""; }

                    if (files["Guest_DocumentBack"] != null)
                        {
                            HttpPostedFileBase file2 = files["Guest_DocumentBack"];
                            string extension = Path.GetExtension(file2.FileName);
                            string File_Name = "IDCard2-" + result + ".jpg";
                            file2.SaveAs(Server.MapPath("~/CardImages/" + File_Name));
                            entGuest.Guest_DocumentBack = File_Name;
                        }
                        else
                        { entGuest.Guest_DocumentBack = ""; }

                        if (files["Guest_DocumentVisa"] != null)
                        {
                            HttpPostedFileBase file3 = files["Guest_DocumentVisa"];
                            string extension = Path.GetExtension(file3.FileName);
                            string File_Name = "VISA-" + result + ".jpg";
                            file3.SaveAs(Server.MapPath("~/CardImages/" + File_Name));
                            entGuest.Guest_DocumentVisa = File_Name;
                        }
                        else { entGuest.Guest_DocumentVisa = ""; }

                        int result1 = balGuestData.SaveGuestDocuments(entGuest, result, trans);
                        if (result1 > 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                        }
                    }
                //}
                //else
                //{
                //    if (result > 0)
                //    {
                //        trans.Commit();
                //    }
                //    else
                //    {
                //        trans.Rollback();
                //    }
                //}
            }
            else
            {
                trans.Rollback();
            }
            arr.Add(result);
            arr.Add(entGuest.Customer_Code);
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectGuestDetails(string Guest_Code)
        {
            Ent_Guest entData = new Ent_Guest();
            entData = balGuestData.SelectGuestData(Guest_Code);

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
            }
            catch
            {

            }
            try { 
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
            }
            catch
            {

            }

            try { 
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
                }
            }
            catch
            {

            }
            return Json(entData, JsonRequestBehavior.AllowGet);
        }

      

        public JsonResult ConfirmWalkinDatas(string Customer_Code)
        {
            ArrayList arr = new ArrayList();
            Ent_Guest ent = new Ent_Guest();
            ent.Is_Active = 3;
            ent.Customer_Code = Customer_Code;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            var UserID = User_ID != null ? User_ID.Value.Split('=')[1] : "";
            ent.Modified_By = Convert.ToInt32(UserID);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            ent.Modified_Date = indiTime;        
            ent.Notification_Status = 1;
            ent.Confirmation_Code = "";
            SafeTransaction trans = new SafeTransaction();
            int i = balGuest.UpdateActiveStatus(ent, trans);
            if (i > 0)
            {
                trans.Commit();              
            }
            else
            {
                trans.Rollback();
            }
            arr.Add(i);
            HttpCookie User_Type = Request.Cookies["User_Type"];
            var UserType = User_Type != null ? User_Type.Value.Split('=')[1] : "";        
            arr.Add(Convert.ToString(UserType));
            return Json(arr, JsonRequestBehavior.AllowGet);
        }     
    
        #endregion
    }
}