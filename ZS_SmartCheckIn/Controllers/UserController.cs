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
    public class UserController : Controller
    {
        #region Declarations
        Bal_User balUser = new Bal_User();
        Bal_Master balMaster = new Bal_Master();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        #endregion

        // GET: User
        public ActionResult Login()
        {
            List<Ent_Branch> listBranch = new List<Ent_Branch>();         
            listBranch = balMaster.SelectBranchList(0);
            ViewBag.listBranch = listBranch;
            return View();
        }

        public ActionResult GuestLogin()
        {
            return View();
        }

        //Login
        public int CreateLogin(string Username, string Password,int Branch_ID)
        {
            int i = 0;
            if (Username != "" && Password != "")
            {
                if (Username == "iadmin" && Password == "ipass")
                {              
                    //Global.glbBranchID = 1;
                    //Global.Customer_Code = "";                  

                    if (Branch_ID == 0)
                        Branch_ID = 1;

                    //Login branch
                    HttpCookie BranchID = new HttpCookie("Branch_ID");
                    BranchID.Values["Branch_ID"] = Convert.ToString(Branch_ID);
                    BranchID.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(BranchID);

                    //Login Customer Code
                    HttpCookie Customer_Code = new HttpCookie("Customer_Code");
                    Customer_Code.Values["Customer_Code"] = "";
                    Customer_Code.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(Customer_Code);

                    HttpCookie User_ID = new HttpCookie("User_ID");
                    User_ID.Values["User_ID"] = Convert.ToString(1);
                    User_ID.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(User_ID);

                    HttpCookie User_Name = new HttpCookie("User_Name");
                    User_Name.Values["User_Name"] = Convert.ToString("Intelli Admin");
                    User_Name.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(User_Name);

                    HttpCookie User_Type = new HttpCookie("User_Type");
                    User_Type.Values["User_Type"] = Convert.ToString("SuperAdmin");
                    User_Type.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(User_Type);
                  
                    i = 1;
                }
                else
                {
                    List<Ent_User> result = new List<Ent_User>();
                    Ent_User entu = new Ent_User();
                    entu.User_Username = Username;
                    entu.User_Password = Password;
                    entu.Branch_ID = Branch_ID;
                    result = balUser.SelectLogin(entu);
                    if (result.Count > 0)
                    {
                        if (result[0].User_ID > 0)
                        {                         
                            //Login branch
                            HttpCookie BranchID = new HttpCookie("Branch_ID");
                            BranchID.Values["Branch_ID"] = Convert.ToString(result[0].Branch_ID);
                            BranchID.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(BranchID);

                            //Login Customer Code
                            HttpCookie Customer_Code = new HttpCookie("Customer_Code");
                            Customer_Code.Values["Customer_Code"] ="";
                            Customer_Code.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(Customer_Code);

                            //Login User ID
                            HttpCookie User_ID = new HttpCookie("User_ID");
                            User_ID.Values["User_ID"] = Convert.ToString(result[0].User_ID);
                            User_ID.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(User_ID);

                            //Login User Name
                            HttpCookie User_Name = new HttpCookie("User_Name");
                            User_Name.Values["User_Name"] = Convert.ToString(result[0].User_Name);
                            User_Name.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(User_Name);

                            HttpCookie Login_Name = new HttpCookie("Login_Name");
                            Login_Name.Values["Login_Name"] = Convert.ToString(result[0].User_Username);
                            Login_Name.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(Login_Name);

                            //Login User Type
                            HttpCookie User_Type = new HttpCookie("User_Type");
                            User_Type.Values["User_Type"] = Convert.ToString(result[0].User_Type);
                            User_Type.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(User_Type);
                         
                            i = 1;
                        }
                        else
                        {
                            //  Global.glbUserID = 0;
                            HttpCookie User_ID = new HttpCookie("User_ID");
                            User_ID.Values["User_ID"] = "";
                            User_ID.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(User_ID);

                            i = -1;
                        }
                    }
                    else { i = -1; }
                }

                Ent_Organization ent = balMaster.SelectOrganization();
                if (ent.Organization_ID > 0)
                {
                    HttpCookie OrgID = new HttpCookie("OrgID");
                    OrgID.Values["OrgID"] = Convert.ToString(ent.Organization_ID);
                    OrgID.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(OrgID);

                    HttpCookie OrgName = new HttpCookie("OrgName");
                    OrgName.Values["OrgName"] = Convert.ToString(ent.Organization_Name);
                    OrgName.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(OrgName);

                    Ent_Branch entB = balMaster.SelectBranch(Branch_ID);

                    HttpCookie BranchMail = new HttpCookie("BranchMail");
                    BranchMail.Values["BranchMail"] = Convert.ToString(entB.Branch_email);
                    BranchMail.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(BranchMail);

                    HttpCookie BranchPhone = new HttpCookie("BranchPhone");
                    BranchPhone.Values["BranchPhone"] = Convert.ToString(entB.Branch_Phone);
                    BranchPhone.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(BranchPhone);

                    HttpCookie BranchAddress = new HttpCookie("BranchAddress");
                    BranchAddress.Values["BranchAddress"] = Convert.ToString(entB.Branch_Address);
                    BranchAddress.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(BranchAddress);

                    HttpCookie Branch_ContactPerson = new HttpCookie("Branch_ContactPerson");
                    Branch_ContactPerson.Values["Branch_ContactPerson"] = Convert.ToString(entB.Branch_ContactPerson);
                    Branch_ContactPerson.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(Branch_ContactPerson);

                }
                Global.glbCountryDT = balMaster.SelectCountryList();
                Global.glbPurposeDT = balMaster.SelectPurposeList();
                Global.glbVisaTypeDT = balMaster.SelectVisaType("");
                Global.glbNationalityDT = balMaster.SelectNationalityList();
            }
            else { i = 0; }
            return i;
        }

        public int CreateGuestLogin(string Username, string Password)
        {
            int i = 0;
            if (Username != "" && Password != "")
            {
                List<Ent_Guest> result = new List<Ent_Guest>();
                Ent_Guest entG = new Ent_Guest();
                entG.Guest_Username = Username;
                entG.Guest_Password = Password;
                result = balUser.SelectGuestLogin(entG);
                if (result.Count > 0)
                {
                    if (result[0].Guest_ID > 0)
                    {                       

                        //Login branch
                        HttpCookie Branch_ID = new HttpCookie("Branch_ID");
                        Branch_ID.Values["Branch_ID"] = Convert.ToString(result[0].Branch_ID);
                        Branch_ID.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(Branch_ID);

                        //Login Customer Code
                        HttpCookie Customer_Code = new HttpCookie("Customer_Code");
                        Customer_Code.Values["Customer_Code"] = Convert.ToString(result[0].Customer_Code);
                        Customer_Code.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(Customer_Code);


                        HttpCookie User_ID = new HttpCookie("User_ID");
                        User_ID.Values["User_ID"] = Convert.ToString(result[0].Guest_ID);
                        User_ID.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(User_ID);

                        HttpCookie User_Name = new HttpCookie("User_Name");
                        User_Name.Values["User_Name"] = Convert.ToString(result[0].Guest_Firstname);
                        User_Name.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(User_Name);

                        HttpCookie Login_Name = new HttpCookie("Login_Name");
                        Login_Name.Values["Login_Name"] = Convert.ToString(Username);
                        Login_Name.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(Login_Name);

                        HttpCookie User_Type = new HttpCookie("User_Type");
                        User_Type.Values["User_Type"] = Convert.ToString("Guest");
                        User_Type.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(User_Type);
                       
                        Ent_Organization ent = balMaster.SelectOrganization();
                        if (ent.Organization_ID > 0)
                        {
                            HttpCookie OrgID = new HttpCookie("OrgID");
                            OrgID.Values["OrgID"] = Convert.ToString(ent.Organization_ID);
                            OrgID.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(OrgID);

                            HttpCookie OrgName = new HttpCookie("OrgName");
                            OrgName.Values["OrgName"] = Convert.ToString(ent.Organization_Name);
                            OrgName.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(OrgName);

                            Ent_Branch entB = balMaster.SelectBranch(result[0].Branch_ID);

                            HttpCookie BranchMail = new HttpCookie("BranchMail");
                            BranchMail.Values["BranchMail"] = Convert.ToString(entB.Branch_email);
                            BranchMail.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(BranchMail);

                            HttpCookie BranchPhone = new HttpCookie("BranchPhone");
                            BranchPhone.Values["BranchPhone"] = Convert.ToString(entB.Branch_Phone);
                            BranchPhone.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(BranchPhone);

                            HttpCookie BranchAddress = new HttpCookie("BranchAddress");
                            BranchAddress.Values["BranchAddress"] = Convert.ToString(entB.Branch_Address);
                            BranchAddress.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(BranchAddress);

                            HttpCookie Branch_ContactPerson = new HttpCookie("Branch_ContactPerson");
                            Branch_ContactPerson.Values["Branch_ContactPerson"] = Convert.ToString(entB.Branch_ContactPerson);
                            Branch_ContactPerson.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(Branch_ContactPerson);

                        }                     
                        i = 1;
                    }
                    else
                    {                     
                        HttpCookie User_ID = new HttpCookie("User_ID");
                        User_ID.Values["User_ID"] = "";
                        User_ID.Expires = DateTime.Now.AddHours(-1);
                        Response.Cookies.Add(User_ID);

                        HttpCookie Customer_Code = new HttpCookie("Customer_Code");
                        Customer_Code.Values["Customer_Code"] = "";
                        Customer_Code.Expires = DateTime.Now.AddHours(-1);
                        Response.Cookies.Add(Customer_Code);

                        i = -1;
                    }
                }
                else { i = -1; }
            }
            else { i = 0; }
            return i;
        }

        public int CreateLogout()
        {
            if (Request.Cookies["User_ID"] != null)
            {
                Response.Cookies["User_ID"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["User_Name"] != null)
            {
                Response.Cookies["User_Name"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["User_Type"] != null)
            {
                Response.Cookies["User_Type"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["Customer_Code"] != null)
            {
                Response.Cookies["Customer_Code"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["OrgID"] != null)
            {
                Response.Cookies["OrgID"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["OrgName"] != null)
            {
                Response.Cookies["OrgName"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["BranchMail"] != null)
            {
                Response.Cookies["BranchMail"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["Branch_ID"] != null)
            {
                Response.Cookies["Branch_ID"].Expires = DateTime.Now.AddDays(-1);
            }
          
            return 1;
        }

        public ActionResult ForgotGuestPassword(string username)
        {
            string i = "";
            List<Ent_Guest> result = new List<Ent_Guest>();
            Ent_Guest entu = new Ent_Guest();
            entu.Guest_Username = username;
            result = balUser.SelectGuestLoginMail(entu);
            if (result.Count > 0)
            {
                string emaill = result[0].Guest_Email;
                if (!string.IsNullOrEmpty(emaill))
                {
                    if (ModelState.IsValid)
                    {
                        string token = Guid.NewGuid().ToString();
                        if (token == null)
                        {
                            i = null;
                        }
                        else
                        {                           
                            int j = balUser.UpdateToken(username, emaill, token);
                            if (j > 0)
                            {
                                //var lnkHref = "<a href='https://zscheckin.atintellilabs.live/" + @Url.Action("ResetCustPassword", "User", new { email = emaill, code = token }) + "'>Click here to reset your password</a>";
                                var lnkHref = "<a href='https://smartcheckin.atintellilabs.live/" + @Url.Action("ResetCustPassword", "User", new { email = emaill, code = token }) + "'>Click here to reset your password</a>";
                              
                            
                                Ent_Branch entB = balMaster.SelectBranch(result[0].Branch_ID);
                        
                        
                                string body = string.Empty;
                                using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlPage5.html")))
                                {
                                    body = reader.ReadToEnd();
                                }
                                body = body.Replace("{Url}", lnkHref);
                                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                                DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
                                body = body.Replace("{datetime}", indiTime.ToString());
                                Ent_Organization ent = balMaster.SelectOrganization();
                                if (ent.Organization_ID > 0)
                                {
                                    body = body.Replace("{orgname}", ent.Organization_Name);
                                    body = body.Replace("{branchmail}", entB.Branch_email);
                                    body = body.Replace("{branchphone}", entB.Branch_Phone);
                                    body = body.Replace("{branchaddress}", entB.Branch_Address);
                                    body = body.Replace("{LoginUser}", entB.Branch_ContactPerson);
                                    body = body.Replace("{GuestName}", result[0].Guest_Firstname + " " + result[0].Guest_Lastname);
                                    body = body.Replace("{datetime}", indiTime.ToString());
                                }                             


                                Email em = new Email();
                                int c = em.SendMail(body, emaill, "Your Password Reset Link");
                                if (c > 0)
                                    i = emaill;
                                else
                                    i = null;
                            }
                            else
                            {
                                i = null;
                            }
                        }
                    }
                }
            }
            return Json(i, JsonRequestBehavior.AllowGet);
        }


        private string PopulatePasswordBody(string url)
        {
           
                string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlPage5.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Url}", url);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            body = body.Replace("{datetime}", indiTime.ToString());
            Ent_Organization ent = balMaster.SelectOrganization();
            if (ent.Organization_ID > 0)
            {
                body = body.Replace("{orgname}", ent.Organization_Name);

                HttpCookie BranchAddress =Request.Cookies["BranchAddress"];
                body = body.Replace("{branchaddress}", BranchAddress.Value.Split('=')[1]);

                //body = body.Replace("{branchaddress}", Global.BranchAddress); 

                HttpCookie OrgID = Request.Cookies["OrgID"];
                OrgID.Values["OrgID"] = Convert.ToString(ent.Organization_ID);
                OrgID.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(OrgID);

                HttpCookie OrgName = Request.Cookies["OrgName"];
                OrgName.Values["OrgName"] = Convert.ToString(ent.Organization_Name);
                OrgName.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(OrgName);


                //Global.OrgID = ent.Organization_ID;
                //Global.OrgName = ent.Organization_Name;
            }
            return body;
        }

        public ActionResult ResetCustPassword(string code, string email)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.Token = code;
            return View(model);
        }
    }
}