using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZS_SmartCheckIn.Models.BAL;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Controllers
{
    public class GuestDataController : Controller
    {
        // GET: GuestData
        #region Declaration
        public FileStream stream;
        private string AdharResult = "";
        TextBox tbResult = new TextBox();
        Ent_GuestData entGuestResult = new Ent_GuestData();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        Bal_Guest balGuest = new Bal_Guest();
        Bal_GuestData balGuestData = new Bal_GuestData();
        Bal_Master balMaster = new Bal_Master();
        int GotData = 0;
        string CardType = "";

        // The authentication key (API Key).
        // Get your own by registering at https://app.pdf.co/documentation/api
        const String API_KEY = "nirmal@intellilabs.co.in_40fd152db7426321";

        // Source file name
    
        // Comma-separated list of barcode types to search. 
        // See valid barcode types in the documentation https://app.pdf.co/documentation/api/1.0/barcode/read_from_url.html
        const string BarcodeTypes = "QRCode";
        // Comma-separated list of page indices (or ranges) to process. Leave empty for all pages. Example: '0,2-5,7-'.
        const string Pages = "";
        // (!) Make asynchronous job
        const bool Async = true;
        #endregion


        public ActionResult AddGuestNew()
        {
            HttpCookie Customer_Code = Request.Cookies["Customer_Code"];
            var CustomerCode = Customer_Code != null ? Customer_Code.Value.Split('=')[1] : "";
            if (CustomerCode == "")
            {
                return RedirectToAction("GuestLogin", "User");
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

                    listGuest = balGuestData.SelectGuestsList(entData.entGuestData.Customer_Code);
                    ViewBag.listGuest = listGuest;
                }
                else
                {
                  
                    //  Ent_Guest entGuest = balGuest.SelectGuest(Global.Customer_Code);
                    Ent_Guest entGuest = balGuest.SelectGuest(CustomerCode);
                    ViewBag.CustomerCode = entGuest.Customer_Code;
                    ViewBag.GuestID = entGuest.Guest_ID;
                    ViewBag.MobileNo = entGuest.Guest_MobileNo;
                    ViewBag.Email = entGuest.Guest_Email;
                    ViewBag.Guest_Accompany = entGuest.Guest_Accompany;
                    ViewBag.GroupLeader = 1;
                    entData = balGuest.SelectGroupLeader(CustomerCode);
                    
                    listGuest = balGuestData.SelectGuestsList(CustomerCode);
                    ViewBag.listGuest = listGuest;
                }

                return View(entData);
            }
        }
     

        public ActionResult History()
        {
            HttpCookie Login_Name = Request.Cookies["Login_Name"];
            List<Ent_Guest> Guestlist = new List<Ent_Guest>();
            Guestlist = balGuest.SelectGuestHistory(Login_Name.Value.Split('=')[1]);
            ViewBag.Guestlist = Guestlist;
            return View();
        }

        public ActionResult HistoryView()
        {
            HttpCookie Customer_Code = Request.Cookies["Customer_Code"];
            var CustomerCode = Customer_Code != null ? Customer_Code.Value.Split('=')[1] : "";
            if (CustomerCode == "")
            {
                return RedirectToAction("GuestLogin", "User");
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

                    listGuest = balGuestData.SelectGuestsList(entData.entGuestData.Customer_Code);
                    ViewBag.listGuest = listGuest;
                }
                else
                {

                    //  Ent_Guest entGuest = balGuest.SelectGuest(Global.Customer_Code);
                    Ent_Guest entGuest = balGuest.SelectGuest(CustomerCode);
                    ViewBag.CustomerCode = entGuest.Customer_Code;
                    ViewBag.GuestID = entGuest.Guest_ID;
                    ViewBag.MobileNo = entGuest.Guest_MobileNo;
                    ViewBag.Email = entGuest.Guest_Email;
                    ViewBag.Guest_Accompany = entGuest.Guest_Accompany;
                    ViewBag.GroupLeader = 1;
                    //  entData = balGuest.SelectGroupLeader(Global.Customer_Code);
                    entData = balGuest.SelectGroupLeader(CustomerCode);

                    // listGuest = balGuestData.SelectGuestsList(Global.Customer_Code);
                    listGuest = balGuestData.SelectGuestsList(CustomerCode);
                    ViewBag.listGuest = listGuest;
                }

                return View(entData);
            }
        }
        public int DeleteGuestData(string Guest_Code)
        {
            Ent_GuestData ent = new Ent_GuestData();

            ent.Guest_Code = Guest_Code;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            var UserID = User_ID != null ? User_ID.Value.Split('=')[1] : "";
            ent.Modified_By = Convert.ToInt32(UserID);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            ent.Modified_Date = indiTime;
            SafeTransaction trans = new SafeTransaction();
            int i = balGuestData.DeleteGuestData(ent, trans);
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

        public JsonResult SaveGuestData(Ent_GuestData entGuest)
        {
            bool IsEdit = false;
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
            //entGuest.Guest_DOB = Convert.ToDateTime(entGuest.Guest_DOB).ToString("dd/MM/yyyy");

          
            SafeTransaction trans = new SafeTransaction();
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            entGuest.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            entGuest.Created_By =Convert.ToInt32( User_ID.Value.Split('=')[1]);
            result = balGuestData.SaveGuestData(entGuest, trans, IsEdit);
            string result2 = "";
            if (result > 0)
            {
             
                if (IsEdit == false)
                {
                    HttpFileCollectionBase files = Request.Files;
                    if (files.Count > 0)
                    {

                        HttpPostedFileBase file = files["Guest_Document"]; 
                        if (file!= null)
                        {
                            string extension = Path.GetExtension(file.FileName);
                            string File_Name = "IDCard1-" + result + ".jpg";
                            file.SaveAs(Server.MapPath("~/CardImages/" + File_Name));
                            entGuest.Guest_Document = File_Name;
                        }

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
                            result2 = result + "," + entGuest.Guest_Code;
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                        }
                    }
                }
                else
                {
                    HttpFileCollectionBase files = Request.Files;


                    HttpPostedFileBase file = files["Guest_Document"];
                    if (file != null)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        string File_Name = "IDCard1-" + result + ".jpg";
                        file.SaveAs(Server.MapPath("~/CardImages/" + File_Name));
                        entGuest.Guest_Document = File_Name;
                    }

                    if (files["Guest_DocumentBack"] != null)
                        {
                            HttpPostedFileBase file2 = files["Guest_DocumentBack"];
                            string extension = Path.GetExtension(file2.FileName);
                            string File_Name = "IDCard2-" + result + ".jpg";
                            file2.SaveAs(Server.MapPath("~/CardImages/" + File_Name));
                            entGuest.Guest_DocumentBack = File_Name;
                        }
                    else { entGuest.Guest_DocumentBack = ""; }

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
                        result2 = result + "," + entGuest.Guest_Code;
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
            }
            else
            {
                trans.Rollback();
            }
            return Json(result2, JsonRequestBehavior.AllowGet);
            //return result;
        }

        public JsonResult GetGuestsList(string CustomerID)
        {
            List<Ent_GuestData> listGuest = new List<Ent_GuestData>();
            listGuest= balGuestData.SelectGuestsList(CustomerID);
            return Json(listGuest,JsonRequestBehavior.AllowGet);
        }

         public JsonResult SelectGuestData(string Guest_Code)
        {
            Ent_Guest entData = new Ent_Guest();
            entData = balGuestData.SelectGuestData(Guest_Code);
            return Json(entData, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult ScanAdharData(Ent_GuestData ent)
        {
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName);
                string File_Name = "scantest.jpg";
                Global.Image_Path = Server.MapPath("~/scanned/" + File_Name);
                file.SaveAs(Server.MapPath("~/scanned/" + File_Name));
            }

            if (GetOcrAdhar() == true)
            {
                CardType = "A";
                entGuestResult.Guest_CardType = "Adhar";
                GetAdharDatas();
            }
            return Json(entGuestResult);
        }

        public JsonResult ScanIDCard(Ent_GuestData ent)
        {
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName);
                string File_Name = "scantest.jpg";
                Global.Image_Path = Server.MapPath("~/scanned/" + File_Name);
                file.SaveAs(Server.MapPath("~/scanned/" + File_Name));
                //if (ent.Result != null)
                //{
                //    await OCRSpace();
                //}
            }
            if (ent.Result != null)
            {
                byte[] sResult = Encoding.ASCII.GetBytes(ent.Result);
                ReadAndExtractSpaceOcr(sResult,ent.Guest_CardType);
            }
            return Json(entGuestResult, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ScanIDCard1(Ent_GuestData ent)
        {
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName);
                string File_Name = "scantest.jpg";
                Global.Image_Path = Server.MapPath("~/scanned/" + File_Name);
                file.SaveAs(Server.MapPath("~/scanned/" + File_Name));
            }


            //byte[] sResult = Encoding.ASCII.GetBytes(ent.Result);
            //ReadAndExtractSpaceOcr(sResult);

            await OCRSpace(ent.Guest_CardType);
            return Json(entGuestResult);
        }

        public async Task<ActionResult> ScanVisaCard(Ent_GuestData ent)
        {
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName);
                string File_Name = "scantest.jpg";
                Global.Image_Path = Server.MapPath("~/scanned/" + File_Name);
                file.SaveAs(Server.MapPath("~/scanned/" + File_Name));
            }
            await OCRSpaceVisa();
            return Json(entGuestResult);
        }

        private string PopulateConfirmBody(Ent_Guest entguest)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlPage22.html")))
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
            body = body.Replace("{GuestName}", entguest.Guest_Firstname + " " + entguest.Guest_Lastname);
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            body = body.Replace("{datetime}", indiTime.ToString());
            return body;
        }


        public int SendConfirmMail(string customercode)
        {
            Ent_Guest entGuest = balGuest.SelectGuest(customercode);
            string Body = "";
            Body = PopulateConfirmBody(entGuest);


            Email em = new Email();
            int r = em.SendConfirmMail(entGuest.Guest_Firstname + " " + entGuest.Guest_Lastname, Body, "Guest Details Have Been Submitted");
          

            //int r = SendMail(entGuest.Guest_Firstname + " " + entGuest.Guest_Lastname,Body, entGuest.Guest_Email, "Guest Details Have Been Submitted");
            Bal_User balUser = new Bal_User();

            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime indiTime = Convert.ToDateTime(indianTime.ToString("yyyy-MM-dd h:m:s"));
            entGuest.Created_Date = indiTime;
            HttpCookie User_ID = Request.Cookies["User_ID"];
            entGuest.Created_By = Convert.ToInt32(User_ID.Value.Split('=')[1]); 
            int l = balUser.InsertLogActivity(entGuest, entGuest.Guest_ID, "Id Uploaded Mail");
            return r;
        }

        public int ConfirmDatas(string Customer_Code)
        {
            Ent_Guest ent = new Ent_Guest();
            ent.Is_Active = 2;
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
                SendConfirmMail(Customer_Code);
            }
            else
            {
                trans.Rollback();
            }

            return i;
        }

        #region OCRSpace

        public async Task<Ent_GuestData> OCRSpace(string cardType)
        {           
            string ImagePath = Global.Image_Path;
            string result = "";

            if (string.IsNullOrEmpty(ImagePath))
                result = "";

            try
            {
                //if (GetOcrAdhar() == true)
                if (CheckIfAdhar() == true)
                {                  
                    CardType = "A";                   
                    GetAdharDatas();                                      
                }
                else
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.Timeout = new TimeSpan(1, 1, 1);
                    
                    MultipartFormDataContent form = new MultipartFormDataContent();                   
                    // form.Add(new StringContent("ce4b4c527a88957"), "apikey"); //harithak456 SPACE OCR key 
                    //form.Add(new StringContent("2c096057dd88957"), "apikey"); //bharati SPACE OCR key 
                    form.Add(new StringContent("OCRK6454898A"), "apikey"); //new

                    form.Add(new StringContent("eng"), "language");
                    form.Add(new StringContent("true"), "isTable");
                    form.Add(new StringContent("true"), "scale");
                    if (cardType != "Adhar")
                    {
                        form.Add(new StringContent("true"), "detectOrientation");
                    }                    
                    form.Add(new StringContent("2"), "OCREngine");

                    if (string.IsNullOrEmpty(ImagePath) == false)
                    {
                        
                        byte[] imageData = System.IO.File.ReadAllBytes(ImagePath);
                        form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");
                    }
                 

                    //HttpResponseMessage response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);                    
                    HttpResponseMessage response = await httpClient.PostAsync("https://apipro3.ocr.space/parse/image", form);                    

                    string strContent = "";
                    using (HttpContent content = response.Content)
                    {
                        // strContent = await content.ReadAsStringAsync().ConfigureAwait(false);
                        strContent = content.ReadAsStringAsync().Result;
                    }
                    
                    Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);


                    if (ocrResult.OCRExitCode == 1)
                    {
                        for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                        {
                            result = result + ocrResult.ParsedResults[i].ParsedText;
                            byte[] sResult = Encoding.ASCII.GetBytes(result);
                            ReadAndExtractSpaceOcr(sResult, cardType);
                        }
                    }
                    else
                    {
                        result = "";
                        GotData = 0;
                    }
                }
            }
            catch (Exception e)
            {
                result = "";
                GotData = 0;
            }
            return entGuestResult;
        }

        public async Task<Ent_GuestData> OCRSpaceVisa()
        {
            string ImagePath = Global.Image_Path;
            string result = "";

            if (string.IsNullOrEmpty(ImagePath))
                result = "";

            try
            {               
                    HttpClient httpClient = new HttpClient();
                    httpClient.Timeout = new TimeSpan(1, 1, 1);

                    MultipartFormDataContent form = new MultipartFormDataContent();
                    // form.Add(new StringContent("ce4b4c527a88957"), "apikey"); //harithak456 SPACE OCR key 
                    //form.Add(new StringContent("2c096057dd88957"), "apikey"); //bharati SPACE OCR key 
                    form.Add(new StringContent("OCRK6454898A"), "apikey"); //new

                    form.Add(new StringContent("eng"), "language");
                    form.Add(new StringContent("true"), "isTable");
                    form.Add(new StringContent("true"), "scale");
                    form.Add(new StringContent("true"), "detectOrientation");
                    form.Add(new StringContent("2"), "OCREngine");

                    if (string.IsNullOrEmpty(ImagePath) == false)
                    {

                        byte[] imageData = System.IO.File.ReadAllBytes(ImagePath);
                        form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");
                    }


                    //HttpResponseMessage response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);
                    HttpResponseMessage response = await httpClient.PostAsync("https://apipro3.ocr.space/parse/image", form);

                    string strContent = "";
                    using (HttpContent content = response.Content)
                    {
                        // strContent = await content.ReadAsStringAsync().ConfigureAwait(false);
                        strContent = content.ReadAsStringAsync().Result;
                    }

                  
                    Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);


                    if (ocrResult.OCRExitCode == 1)
                    {
                        for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                        {
                            result = result + ocrResult.ParsedResults[i].ParsedText;
                            byte[] sResult = Encoding.ASCII.GetBytes(result);
                        ReadAndExtractVisa(sResult);
                        }
                    }
                    else
                    {
                        result = "";
                        GotData = 0;
                    }
                
            }
            catch (Exception e)
            {
                result = "";
                GotData = 0;
            }
            return entGuestResult;
        }

        private void ReadAndExtractVisa(byte[] sbytes)
        {          
            if (sbytes != null && sbytes.Length > 0)
            {
                string oneBigString = Encoding.ASCII.GetString(sbytes);
                string[] lines = oneBigString.Split('\n');
                lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                for (int i = lines.Length-1; i > 5; i--)
                {
                    if (lines[i].Contains("<<") )
                    {
                        GetVisaDatasbyOCR(lines);
                        break;
                    }
                }
            }
        }

        //Added by vineetha on 20-10-2020
        private void GetVisaDatasbyOCR(string[] lines)
        {
            try
            {
                string inp1 = "";
                string inp2 = "";
                string poiCountry = "";
                string poiCity = "";

                for (int i = 5; i <= lines.GetUpperBound(0); i++)
                {
                    if (lines[i].Contains("<<") && inp1 == "")
                    {
                        inp2 = Regex.Replace(lines[i], @"\s+", "");
                        if (lines[i].Contains('\t') && inp2.Count()>50)                        
                            inp1 = lines[i].Split('\t')[1];                        
                        else if(lines[i + 1].Count()>25)
                            inp1 = Regex.Replace(lines[i+1], @"\s+", "");
                        else
                            inp1 = Regex.Replace(lines[i + 2], @"\s+", "");
                        break;
                    }                   
                }                     

                string outputString1 = inp1;

                if (outputString1.Any(char.IsUpper))
                {
                    string string1 = "";
                    string string2 = "";
                    outputString1 = outputString1.Replace(" ", string.Empty);
                    outputString1 = Regex.Replace(outputString1, @"e", " ");
                    if (outputString1.Substring(0, 12).Contains('<'))
                    {
                        outputString1 = Regex.Replace(outputString1, "[<]", " ");
                        string[] splitString1 = outputString1.Split();
                        splitString1 = splitString1.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        string1 = splitString1[0];
                        string2 = splitString1[1];
                    }
                    else
                    {
                        string1 = outputString1.Substring(0, 9);
                        string2 = outputString1.Substring(9, 20); 
                    }                              
                        entGuestResult.Guest_VisaNo = string1;
                     
                      
                        poiCity = string2.Substring(1, 3);


                        try
                        {
                            string expirydate = "";
                            string exdate = string2.Substring(16, 2);
                            string exmonth = string2.Substring(14, 2);
                            string exyear = "20" + string2.Substring(12, 2);
                            expirydate = exdate + "/" + exmonth + "/" + exyear;
                            entGuestResult.Guest_ExpiryDate = expirydate;
                            DateTime convertedDate = GetDate(Convert.ToInt32(exdate), Convert.ToInt32(exmonth), Convert.ToInt32(exyear));
                            string asd = convertedDate.AddYears(-1).AddDays(+1).ToString("dd/MM/yyyy");
                            entGuestResult.Guest_VisaValidTill = asd.ToString().Substring(0, 10).Replace('-', '/');
                            entGuestResult.Guest_VisaDateofIssue = entGuestResult.Guest_VisaValidTill;
                            entGuestResult.Guest_VisaValidTill = expirydate;
                        }
                        catch
                        {

                        }

                        string outputString2;
                        if (inp2.Contains("<<<"))
                        {
                            int ind = inp2.IndexOf("<<<");

                            outputString2 = inp2.Substring(0, ind); //Changed by Vineetha on 09-12-2019
                            outputString2 = Regex.Replace(outputString2, "<K<", " ");
                        }
                        else
                        {
                            outputString2 = Regex.Replace(inp2, "<K<", " ");
                        }
                        outputString2 = Regex.Replace(outputString2, "<<<", " ");
                        outputString2 = Regex.Replace(outputString2, "<X<", " "); //Added by Vineetha on 09-12-2019
                        outputString2 = Regex.Replace(outputString2, " KK", " ");
                        outputString2 = Regex.Replace(outputString2, "KK ", " ");
                        outputString2 = Regex.Replace(outputString2, "<KK<", " ");
                        if (outputString2.Contains("<<"))
                            outputString2 = Regex.Replace(outputString2, "<<", "/");

                        outputString2 = Regex.Replace(outputString2, "<", " ");

                        poiCountry = outputString2.Substring(2, 3); // htDatas["txtCountryOfIssue"]                                            



                        if (poiCountry != null)
                        {
                            var query = from c in Global.glbCountryDT.AsEnumerable()
                                        where c.Field<string>("Country_iso3") == poiCountry
                                        select new
                                        {
                                            countryID = c.Field<string>("Country_iso3")
                                        };


                            foreach (var m in query)
                            {
                                entGuestResult.Guest_VisaPOICountry = m.countryID;
                            }

                            var query1 = from c in Global.glbCountryDT.AsEnumerable()
                                         where c.Field<string>("Country_iso3") == poiCountry
                                         select new
                                         {
                                             countryID = c.Field<string>("country_name")
                                         };


                            foreach (var m in query1)
                            {
                                entGuestResult.Guest_VisaPOICity = m.countryID;
                            }
                        }

                        if (string.IsNullOrEmpty(entGuestResult.Guest_VisaPOICountry))
                        {
                            poiCountry = outputString2.Substring(1, 3);
                            if (poiCountry != null)
                            {
                                var query = from c in Global.glbCountryDT.AsEnumerable()
                                            where c.Field<string>("Country_iso3") == poiCountry
                                            select new
                                            {
                                                countryID = c.Field<string>("Country_iso3")
                                            };


                                foreach (var m in query)
                                {
                                    entGuestResult.Guest_VisaPOICountry = m.countryID;
                                }

                                var query1 = from c in Global.glbCountryDT.AsEnumerable()
                                             where c.Field<string>("Country_iso3") == poiCountry
                                             select new
                                             {
                                                 countryID = c.Field<string>("country_name")
                                             };


                                foreach (var m in query1)
                                {
                                    entGuestResult.Guest_VisaPOICity = m.countryID;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(entGuestResult.Guest_VisaPOICountry))
                        {
                            if (poiCity != null)
                            {
                                var query = from c in Global.glbCountryDT.AsEnumerable()
                                            where c.Field<string>("Country_iso3") == poiCity
                                            select new
                                            {
                                                countryID = c.Field<string>("Country_iso3")
                                            };


                                foreach (var m in query)
                                {
                                    entGuestResult.Guest_VisaPOICountry = m.countryID;
                                }


                                var query1 = from c in Global.glbCountryDT.AsEnumerable()
                                             where c.Field<string>("Country_iso3") == poiCity
                                             select new
                                             {
                                                 countryID = c.Field<string>("country_name")
                                             };


                                foreach (var m in query1)
                                {
                                    entGuestResult.Guest_VisaPOICity = m.countryID;
                                }
                            }
                        }                    
                }
            }
            catch
            {

            }
        }


        private void ReadAndExtractSpaceOcr(byte[] sbytes, string cardType)
        {
            entGuestResult.Guest_Country = "IND";
            entGuestResult.Guest_Nationality = "IND";
            GotData = 0;
            if (sbytes != null && sbytes.Length > 0)
            {
                string oneBigString = Encoding.ASCII.GetString(sbytes);
                string[] lines = oneBigString.Split('\n');

                tbResult.Text = System.Text.Encoding.UTF8.GetString(sbytes);
                //string[] lines = tbResult.Lines;
                lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                //if (cardType == "Adhar")
                //{
                //    if (CheckIfAdhar() == true)
                //    {
                //        CardType = "A";
                //        entGuestResult.Guest_CardType = "Adhar";
                //        GetAdharDatas();
                //    }
                //    else
                //    {
                //        CardType = "A";
                //        entGuestResult.Guest_CardType = "Adhar";
                //        GetAdharBackByOCR(lines);                        
                //    }
                //}

                //if (GetOcrAdhar() == true)
                if (CheckIfAdhar() == true)
                {
                    CardType = "A";
                    entGuestResult.Guest_CardType = "Adhar";
                    GetAdharDatas();
                }
                else
                {
                    for (int i = 0; i <= lines.GetUpperBound(0); i++)
                    {
                        if (lines[i].Contains("KERALA") || lines[i].Contains("INDIA"))
                        {
                            entGuestResult.Guest_Country = "IND";
                        }

                        if (lines[i].Contains("REPUBLIC OF MAURITIUS") || lines[i].Contains("MAURITIUS"))
                        {
                            CardType = "V";
                            entGuestResult.Guest_CardType = "Voters ID";
                            GetMaurtiusId(lines);
                            break;
                        }

                        if (lines[i].Contains("DRIVIN") || lines[i].Contains("Transport"))
                        {
                            CardType = "DL";
                            entGuestResult.Guest_CardType = "Driving Licence";
                            GetDrivingLicence(lines);
                            break;
                        }
                        else if (lines[i].Contains("DL No") || lines[i].Contains("LMV") || lines[i].Contains("COV") || lines[i].Contains("Licenc"))
                        {
                            CardType = "DL";
                            entGuestResult.Guest_CardType = "Driving Licence";
                            if (tbResult.Text.Contains("Indian") || tbResult.Text.Contains("Union") || tbResult.Text.Contains("Organ") || tbResult.Text.Contains("Donor"))
                            {
                                GetDrivingLicenceCommon(lines);
                            }
                            else
                            {
                                GetDrivingLicenceKarnatakaSpaceOCR(lines);
                            }
                            break;
                        }
                        else if (lines[i].Contains("DL No") || lines[i].Contains("LMV") || lines[i].Contains("Licence"))
                        {
                            //newCard = "DF";
                            //txtCardType.Text = "Driving Licence";
                            //txtCardType.Tag = "DL";
                            //if (txtCardType.Tag.ToString() != "" && oldCard != "" || newCard == oldCard)
                            //{
                            //    ClearAllForNewcard();
                            //}
                            //GetDrivingLicenceCommon(lines);
                            //break;
                        }
                        else if (lines[i].Contains("Unique") || lines[i].Contains("UNIQUE"))
                        {
                            CardType = "A";
                            entGuestResult.Guest_CardType = "Adhar";
                            GetAdharBackByOCR(lines);
                            break;
                        }
                        else if (lines[i].Contains("IDENTITY") || lines[i].Contains("ELECT"))
                        {
                            CardType = "V";
                            entGuestResult.Guest_CardType = "Voters ID";
                            GetVoters(lines);
                            break;
                        }
                        else if (lines[i].Contains("Electoral") || lines[i].Contains("Registration Offic"))
                        {
                            CardType = "V";
                            entGuestResult.Guest_CardType = "Voters ID";
                            GetVotersBack(lines);
                            break;
                        }
                        else if (lines[i].Contains("Year of Birth") || lines[i].Contains("DOB"))
                        {
                            CardType = "A";
                            entGuestResult.Guest_CardType = "Adhar";
                            GetAdharDatasByOCR(lines, tbResult.Text);
                            break;
                        }
                        else if (lines[i].Contains("<<<<<<<<") || lines[i].Contains("<<<<<<") || lines[i].Contains("P<"))
                        {
                            CardType = "PF";
                            entGuestResult.Guest_CardType = "Passport";
                            GetPassportDatasBySpaceOCR(lines);
                            GetPassportDatasByOCR(lines);
                            break;
                        }
                        else if (lines[i].Contains("Old Passport No") || lines[i].Contains("Guardian") || lines[i].Contains("Mother") || lines[i].Contains("Name of") || lines[i].Contains("Spous") || lines[i].Contains("PIN:"))
                        {
                            //newCard = "PB";                               
                            //GetPassportBack(lines);
                            //break;
                        }
                        else if (lines[i].Contains("INCOME") || lines[i].Contains("TAX") || lines[i].Contains("Permanent Account Number") || lines[i].Contains("Account"))
                        {
                            entGuestResult.Guest_CardType = "PAN Card";
                            CardType = "PAN";

                            GetPANCard(lines);
                            break;
                        }
                    }
                    if (Global.glbCountryDT == null)
                    {
                        Global.glbCountryDT = balMaster.SelectCountryList();
                    }
                    if (!string.IsNullOrEmpty(entGuestResult.Guest_Country))
                    {
                        var query = from c in Global.glbCountryDT.AsEnumerable()
                                    where c.Field<string>("Country_iso3") == entGuestResult.Guest_Country
                                    select new
                                    {
                                        countryname = c.Field<string>("country_name")
                                    };

                        foreach (var m in query)
                        {
                            entGuestResult.Guest_State = m.countryname;
                            if (string.IsNullOrEmpty(entGuestResult.Guest_Address))
                            {
                                entGuestResult.Guest_Address = m.countryname;
                            }
                        }
                    }
                }
            }
        }
      
        private void GetAdharDatas()
        {
            try
            {
              
                string strdecoded = "";
                if (AdharResult != null)
                {
                    CardType= "A";

                    strdecoded = AdharResult;
                    string[] adharlines = strdecoded.Split('=');
                    int count = adharlines.Length;

                    entGuestResult.Guest_DocumentNo = Regex.Replace(adharlines[3], " name", "").ToString().Replace("\"", string.Empty);
                    GotData = 1;
                    string name = Regex.Replace(adharlines[4], " gender", "");
                    string[] splitNamefirstsec = name.Split(' ');
                    entGuestResult.Guest_Firstname = splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                    entGuestResult.Guest_Lastname= splitNamefirstsec[splitNamefirstsec.Length - 1].ToString().Replace("\"", string.Empty);
                    string gender = Regex.Replace(adharlines[5], " yob", "").ToString().Replace("\"", string.Empty);

                    if (gender == "M")
                        entGuestResult.Guest_Gender = "Male";
                    else if (gender == "F")
                        entGuestResult.Guest_Gender = "Female";

                    if (adharlines[6].Contains(" co"))
                    {
                        entGuestResult.Guest_DOB = Regex.Replace(adharlines[6], " co", "").ToString().Replace("\"", string.Empty);
                    }
                    if (adharlines[6].Contains(" gname"))
                    {
                        entGuestResult.Guest_DOB = Regex.Replace(adharlines[6], " gname", "").ToString().Replace("\"", string.Empty);
                        entGuestResult.Guest_Father = Regex.Replace(adharlines[7], " house", " ").ToString().Replace("\"", string.Empty);
                    }
                    string address = "";
                    for (int i = 7; i <= count - 2; i++)
                    {
                        if (adharlines[i].Contains(" house"))
                            address = Regex.Replace(adharlines[i], " house", " ,");
                        if (adharlines[i].Contains(" street"))
                            address = address + Regex.Replace(adharlines[i], " street", ",");
                        if (adharlines[i].Contains(" lm"))
                            address = address + Regex.Replace(adharlines[i], " lm", " ,");
                        if (adharlines[i].Contains(" loc"))
                            address = address + Regex.Replace(adharlines[i], " loc", ",");
                        if (adharlines[i].Contains(" vtc"))
                            address = address + Regex.Replace(adharlines[i], " vtc", " ,");
                        if (adharlines[i].Contains(" po"))
                            address = address + Regex.Replace(adharlines[i], " po", ",");
                        if (adharlines[i].Contains(" dist"))
                            address = address + Regex.Replace(adharlines[i], " dist", " ,");
                        if (adharlines[i].Contains(" subdist"))
                            address = address + Regex.Replace(adharlines[i], " subdist", ",");
                    }
                    address = address + Regex.Replace(adharlines[count - 3], " state", " ");
                    address = address.Replace("\"", string.Empty);
                    entGuestResult.Guest_Address  = Regex.Replace(address, ("^A-Za-z0-9 "), ",");
                    string addressfirst = Regex.Replace(adharlines[7], " house", "");
                    if (addressfirst.Contains("S/O"))
                    {
                        entGuestResult.Guest_Father = Regex.Match(adharlines[7], "S/O: (.*) house").Groups[1].Value.ToString().Replace("\"", string.Empty);
                    }
                    entGuestResult.Guest_City = Regex.Replace(adharlines[count - 3], "state", "").ToString().Replace("\"", string.Empty);
                    entGuestResult.Guest_State = Regex.Replace(adharlines[count - 2], "pc", "").ToString().Replace("\"", string.Empty);
                    entGuestResult.Guest_Country = Regex.Replace(adharlines[count - 1], "/", "").ToString().Replace("\"", string.Empty);
                     //entGuestResult.pin = Regex.Replace(adharlines[count - 1], "pc", "").ToString().Replace("\"", string.Empty).Substring(0, 6);
                    entGuestResult.Guest_Nationality = "IND";
                     entGuestResult.Guest_CountryofIssue = "INDIA";
                    entGuestResult.Guest_Country = "IND";                  
                }
                else
                {
                    stream.Dispose();
                    stream.Close();
                }
            }
            catch
            {
               
            }
        }

        private void GetMaurtiusId(string[] lines)
        {
            try
            {
                Regex date = new Regex(@"\d{2}/\d{2}/\d{4}");
                Regex regCharacter = new Regex("[*'\",_&:#^@]");
                Regex docNo1 = new Regex(@"\d{2}/\d{3,4}/\d{4}");
                Regex docNo2 = new Regex(@"\d{1}/\d{4,5}/\d{4}");
                entGuestResult.Guest_State = "Kerala";
                entGuestResult.Guest_Country = "IND";
                entGuestResult.Guest_CountryofIssue = "India";
                entGuestResult.Guest_Nationality = "IND";
                for (int i = 0; i < lines.Count(); i++)
                {
                    Match doc1 = docNo1.Match(lines[i]);
                    Match doc2 = docNo2.Match(lines[i]);
                    if (doc1.Success)
                    {
                        entGuestResult.Guest_DocumentNo = doc1.Value;
                    }
                    else if (doc2.Success)
                    {
                        entGuestResult.Guest_DocumentNo = doc2.Value;
                    }

                    if (lines[i].ToLower().Contains("surname"))
                    {
                        string stri = Regex.Replace(lines[i], "Surname", "");
                        string[] splitName = stri.ToString().Split(' ');
                        splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();                      
                        entGuestResult.Guest_Lastname = splitName[0];
                        GotData = 1;
                    }
                    if (lines[i].ToLower().Contains("other name"))
                    {
                        string[] splitName = lines[i].ToString().Split(' ');
                        splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        if (splitName[2] != null)
                            entGuestResult.Guest_Firstname = splitName[2];
                        else
                            entGuestResult.Guest_Firstname = splitName[1];
                    }
                    if (lines[i].ToLower().Contains("dob"))
                    {
                        string[] splitName = lines[i].ToString().Split(' ');
                        splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        string dob = splitName[1] + " " + splitName[2] + " " + splitName[3];
                        entGuestResult.Guest_DOB = dob;
                    }
                    if (lines[i].ToLower().Contains("female") )
                    {
                        entGuestResult.Guest_Gender = "Female";
                    }
                    else if (lines[i].ToLower().Contains("male") && !string.IsNullOrEmpty(entGuestResult.Guest_Gender))
                    {
                        entGuestResult.Guest_Gender = "Male";
                    }


                    //if (lines[i].Contains("Nationality"))
                    //{
                    //    string stri = Regex.Replace(lines[i], "[^A-Z]", " ");
                    //    string[] splitName = stri.ToString().Split(' ');
                    //    splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    //    entGuestResult.Guest_Firstname = splitName[1];
                    //    GotData = 1;
                    //    entGuestResult.Guest_Lastname = splitName[2];
                    //}

                    if (lines[i].Contains("Gender"))
                    {
                        string linGender = string.Concat(lines[i + 1].Where(x => x >= 'A' && x <= 'Z'));
                        string gender = linGender.Substring(linGender.Length - 1);
                        if (gender == "M")
                            entGuestResult.Guest_Gender = "Male";
                        else if (gender == "F")
                            entGuestResult.Guest_Gender = "Female";
                        else if (gender == "X")
                            entGuestResult.Guest_Gender = "Transgender";                                             
                    }
                }               
            }
            catch
            {

            }
        }

        private void GetDrivingLicence(string[] lines)
        {           
            try
            {
                Regex date = new Regex(@"\d{2}/\d{2}/\d{4}");
                Regex regCharacter = new Regex("[*'\",_&:#^@]");
                Regex docNo1 = new Regex(@"\d{2}/\d{3,4}/\d{4}");              
                Regex docNo2 = new Regex(@"\d{1}/\d{4,5}/\d{4}");              
               entGuestResult.Guest_State = "Kerala";
                entGuestResult.Guest_Country = "IND";
                entGuestResult.Guest_CountryofIssue = "India";
                entGuestResult.Guest_Nationality = "IND";              
                for (int i = 0; i < lines.Count(); i++)
                {
                    Match doc1 = docNo1.Match(lines[i]);
                    Match doc2 = docNo2.Match(lines[i]);
                    if (doc1.Success )
                    {
                        entGuestResult.Guest_DocumentNo = doc1.Value;
                    }
                    else if (doc2.Success)
                    {
                        entGuestResult.Guest_DocumentNo = doc2.Value;
                    }

                    if (lines[i].Contains("Name"))
                    {
                        string stri = Regex.Replace(lines[i], "[^A-Z]", " ");
                        string[] splitName = stri.ToString().Split(' ');
                        splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        entGuestResult.Guest_Firstname = splitName[1];
                        GotData = 1;
                        entGuestResult.Guest_Lastname = splitName[2];

                        //Get Fathers Name
                        i = i + 1;
                        entGuestResult.Guest_Father= Regex.Replace(lines[i].Split(new string[] { "of" }, StringSplitOptions.None)[1], @"[^a-zA-Z.\s]+", "");

                       
                    }

                    //Get Address
                    if (lines[i].Contains("Addres") && string.IsNullOrEmpty(entGuestResult.Guest_Address))
                    {
                        string tmpAdd = "";
                        try
                        {
                            tmpAdd = lines[i].Replace("Addres", "") + " , ";
                            tmpAdd = lines[i].Replace("Address", "") + " , ";
                            i = i + 1;
                            for (int j = i; j < i + 5; j++)
                            {
                                Match d = date.Match(lines[j]);
                                if (d.Success)
                                {
                                    entGuestResult.Guest_DOB = d.Value;
                                    i = j;
                                    entGuestResult.Guest_Address = tmpAdd;
                                    break;
                                }
                                {
                                    tmpAdd += lines[j].ToString() + ",";
                                }
                            }
                        }
                        catch 
                        {

                            entGuestResult.Guest_Address = tmpAdd;
                        }
                    }
                    if (lines[i].Contains("Valid"))
                    {
                        i++;
                        Match t = date.Match(lines[i]);
                        if (t.Success)
                        {
                            entGuestResult.Guest_DateOfIssue = t.Value.ToString();
                            entGuestResult.Guest_ExpiryDate= t.NextMatch().ToString();                            
                        }
                    }
                    if (lines[i].Contains("Non-Transport"))
                    {
                        Match t = date.Match(lines[i]);
                        if (t.Success)
                        {
                            entGuestResult.Guest_ExpiryDate = t.NextMatch().ToString();
                           
                        }
                    }
                    if (lines[i].Contains("Date of") || lines[i].Contains("Birth"))
                    {
                        Match t = date.Match(lines[i]);
                        if (t.Success)
                        {
                            entGuestResult.Guest_DOB = t.Value.ToString();
                           
                        }                       
                    }
                    if (string.IsNullOrEmpty(entGuestResult.Guest_Address))
                    {
                        if (lines[i].Contains("Date of") || lines[i].Contains("Birth"))
                        {
                            entGuestResult.Guest_Address = lines[i - 1].ToString() + "," + lines[i - 2].ToString() + "," + lines[i - 3].ToString();
                        }
                    }
                }

                entGuestResult.Guest_Address = regCharacter.Replace(entGuestResult.Guest_Address, string.Empty);
            }
            catch
            {               
                
            }
        }

        private void GetDrivingLicenceKarnatakaSpaceOCR(string[] lines)
        {           
            try
            {
                Regex dateFormat = new Regex(@"\d{2}/\d{2}/\d{4}");
                Regex pincodeFormat = new Regex(@"d{6}");
                Regex docFormat = new Regex(@"^[0-9]{11}$");
                entGuestResult.Guest_Country = "IND";
              entGuestResult.Guest_CountryofIssue = "India";
                entGuestResult.Guest_Nationality = "IND";
            
                int zipIndex = 0;
                int signofholderIndex = 0;
                int NameIndex = 0;
                int AddressIndex = 0;
                for (int i = 0; i < lines.Count(); i++)
                {
                    try
                    {
                        if (lines[i].Contains("DL No"))
                        {
                            string ldoc = Regex.Replace(lines[i].ToString(), "[^0-9/]+", " ");

                            string[] splitDoc = ldoc.Split(' ');
                            splitDoc = splitDoc.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            for (int k = 0; k < splitDoc.Length; k++)
                            {
                                Match doc = docFormat.Match(splitDoc[k]);
                                if (doc.Success)
                                    entGuestResult.Guest_DocumentNo = doc.Value;

                                Match doi = dateFormat.Match(splitDoc[k]);
                                if (doi.Success)
                                    entGuestResult.Guest_DateOfIssue = doi.Value;
                            }
                        }

                        //Get Name
                        if (lines[i].Contains("Name") || lines[i].Contains("NAME"))
                        {
                            string stri = "";
                            stri = Regex.Replace(lines[i], "[^A-Z]", " ");
                            string[] splitName = stri.ToString().Split(' ');
                            splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            if (splitName.Length > 1)
                            {
                                NameIndex = i;
                                entGuestResult.Guest_Firstname = splitName[1];
                                GotData = 1;
                                entGuestResult.Guest_Lastname = splitName[2];
                            }
                            else
                            {
                                Match date2 = dateFormat.Match(lines[i + 1]);
                                Match date3 = dateFormat.Match(lines[i - 1]);

                                bool isIntString = lines[i + 1].Any(char.IsDigit);
                                bool isIntString2 = lines[i - 1].All(char.IsDigit);

                                if (!date2.Success && isIntString == false)
                                {
                                    stri = "";
                                    stri = Regex.Replace(lines[i + 1], "[^A-Z]", " ");
                                    string[] splitName2 = stri.ToString().Split(' ');
                                    splitName2 = splitName2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                    if (splitName2.Length > 1)
                                    {
                                        NameIndex = i;
                                        entGuestResult.Guest_Firstname = splitName2[0];
                                        GotData = 1;
                                        entGuestResult .Guest_Lastname= splitName2[1];
                                    }
                                }
                                else if (!date3.Success && isIntString2 == false)
                                {
                                    stri = "";
                                    stri = Regex.Replace(lines[i - 1], "[^A-Z]", " ");
                                    string[] splitName2 = stri.ToString().Split(' ');
                                    splitName2 = splitName2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                    if (splitName2.Length > 1)
                                    {
                                        NameIndex = i;
                                        entGuestResult.Guest_Firstname = splitName2[0];
                                        GotData = 1;
                                        entGuestResult.Guest_Lastname= splitName2[1];
                                    }
                                }
                            }
                        }

                        if (lines[i].Contains("Sign. Of") || lines[i].Contains("Sign.") || lines[i].Contains("Licencing Au"))
                        {
                            signofholderIndex = i;
                        }

                        //Get Fathers Name
                        if (lines[i].Contains("S/o") || lines[i].Contains("S/D/W") || lines[i].Contains("D/W") || lines[i].Contains("Slo"))
                        {
                            string father = Regex.Replace(lines[i], "[^A-Z]", " ");
                            string[] splitFt = father.ToString().Split(' ');
                            splitFt = splitFt.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            if (splitFt.Length > 1)
                            {
                                entGuestResult.Guest_Father = splitFt[1];
                            }                          
                        }

                        Match date = dateFormat.Match(lines[i]);
                        if (date.Success)
                        {
                            if (lines[i].Contains("Issue"))
                           entGuestResult.Guest_DateOfIssue= date.Value;
                            else
                            if (lines[i].Contains("DOB") || lines[i].Contains("D.O.B") || lines[i].Contains("D.o."))
                                entGuestResult.Guest_DOB = date.Value;
                            else if (lines[i].Contains("VALID") || lines[i].Contains("TILL"))
                                entGuestResult.Guest_ExpiryDate= date.Value;
                        }

                        //Get Address                       
                        if (lines[i].Contains("Addres") || lines[i].Contains("ADDRE"))
                        {
                            AddressIndex = i;
                            if (lines[i].Contains("Address"))
                                entGuestResult.Guest_Address = lines[i].Replace("Address", "") + " , ";
                            if (lines[i].Contains("ADDRESS"))
                                entGuestResult.Guest_Address = lines[i].Replace("ADDRESS", "") + " , ";
                            i = i + 1;
                            for (int j = i; j < i + 2; j++)
                            {
                                Match d = pincodeFormat.Match(lines[j]);
                                if (d.Success)
                                {
                                    //htDatas["txtZip"] = d.Value;
                                    break;
                                }
                                {
                                    if (lines[j].Contains("Licenc") || lines[j].Contains("Holder") || lines[j].Contains("Authority"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        GotData = 1;
                                        entGuestResult.Guest_Address += lines[j].ToString() + " , ";
                                    }
                                }
                            }
                            entGuestResult.Guest_Address = Regex.Replace(entGuestResult.Guest_Address.ToString(), "[^A-Za-z0-9]", " ");
                            GotData = 1;
                        }

                        Match zi = pincodeFormat.Match(lines[i]);
                        if (zi.Success)
                        {
                            zipIndex = i;
                            //htDatas["txtZip"] = zi.Value;
                        }
                    }
                    catch (Exception)
                    {
                       
                    }
                }
                entGuestResult.Guest_Address = Regex.Replace(entGuestResult.Guest_Address.ToString(), "[^A-Za-z0-9]", " ");              
                if (entGuestResult.Guest_Address.ToString() == "")
                {
                    if (zipIndex != 0)
                    {
                        entGuestResult.Guest_Address = lines[zipIndex - 2].ToString() + " , " + lines[zipIndex - 1].ToString() + " , " + lines[zipIndex].ToString();
                    }

                    else if (signofholderIndex != 0)
                    {
                        entGuestResult.Guest_Address = lines[signofholderIndex - 2].ToString() + " , " + lines[signofholderIndex - 1].ToString() + " , " + lines[signofholderIndex].ToString();
                    }

                    if (entGuestResult.Guest_Address.ToString().Contains("Address"))
                        entGuestResult.Guest_Address = entGuestResult.Guest_Address.Replace("Address", "") + " , ";
                    if (entGuestResult.Guest_Address.ToString().Contains("ADDRESS"))
                        entGuestResult.Guest_Address = entGuestResult.Guest_Address.ToString().Replace("ADDRESS", "") + " , ";
                    entGuestResult.Guest_Address = Regex.Replace(entGuestResult.Guest_Address.ToString(), "[^A-Za-z0-9]", " ");
                    GotData = 1;
                 
                }


                if (entGuestResult.Guest_DOB.ToString() == "")
                {
                    Match dateDB = dateFormat.Match(lines[NameIndex + 1]);
                    if (dateDB.Success)
                    {
                        entGuestResult .Guest_DOB= dateDB.Value;
                    }
                }
                if (entGuestResult.Guest_Father.ToString() == "")
                {
                    string father = Regex.Replace(lines[AddressIndex - 1], "[^A-Z]", " ");
                    string[] splitFt = father.ToString().Split(' ');
                    splitFt = splitFt.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    entGuestResult.Guest_Father = splitFt[0];
                }
                if (entGuestResult.Guest_Firstname.ToString() == "")
                {
                    string stri = "";
                    stri = Regex.Replace(lines[1], "[^A-Z]", " ");
                    string[] splitName = stri.ToString().Split(' ');
                    splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    if (splitName.Length > 1)
                    {
                        entGuestResult.Guest_Firstname = splitName[0];
                        GotData = 1;
                        entGuestResult.Guest_Lastname = splitName[1];
                    }
                }
            }
            catch
            {
                
            }
        }

        private void GetDrivingLicenceCommon(string[] lines)
        {
            try
            {
                //Regex dateFormat = new Regex(@"\d{2}-d{2}-d{4}");
                Regex dateFormat = new Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
                Regex pincodeFormat = new Regex(@"d{6}");
                Regex docFormat1 = new Regex(@"^[A-Z]{2}[0-9]{2}$");
                Regex docFormat2 = new Regex(@"^[0-9]{10,11}$");
                entGuestResult.Guest_Country = "IND";
                entGuestResult.Guest_CountryofIssue = "India";
                entGuestResult.Guest_Nationality = "IND";

                int zipIndex = 0;
                int signofholderIndex = 0;
                int NameIndex = 0;
                int AddressIndex = 0;
                for (int i = 0; i < lines.Count(); i++)
                {
                    try
                    {
                        if (lines[i].Contains("DL No"))
                        {
                                          
                            string[] splitDoc = lines[i].ToString().Split(' ');
                            splitDoc = splitDoc.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            for (int k = 0; k < splitDoc.Length; k++)
                            {
                                Match doc1 = docFormat1.Match(splitDoc[k]);
                                if (doc1.Success)
                                {
                                    entGuestResult.Guest_DocumentNo = doc1.Value + " " +splitDoc[k+1];
                                }
                            }
                        }

                        try
                        {
                            if (lines[i].Contains("Issue") || lines[i].Contains("Valid"))
                            {
                                string[] splitDoc = lines[i + 1].ToString().Split(' ');
                                Match doi = dateFormat.Match(splitDoc[0]);
                                if (doi.Success)
                                {
                                    entGuestResult.Guest_DateOfIssue = doi.Value;
                                    entGuestResult.Guest_ExpiryDate = splitDoc[1];
                                }
                            }
                        }
                        catch
                        {

                        }
                        //Get Name
                        if (lines[i].Contains("Name") || lines[i].Contains("NAME"))
                        {
                            string stri = "";
                            stri = Regex.Replace(lines[i], "[^A-Z]", " ");
                            string[] splitName = stri.ToString().Split(' ');
                            splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            if (splitName.Length > 1)
                            {
                                NameIndex = i;
                                entGuestResult.Guest_Firstname = splitName[1];
                                GotData = 1;
                                entGuestResult.Guest_Lastname = splitName[2];
                            }
                            else
                            {
                                try
                                {


                                    Match date2 = dateFormat.Match(lines[i + 1]);
                                    Match date3 = dateFormat.Match(lines[i - 1]);

                                    bool isIntString = lines[i + 1].Any(char.IsDigit);
                                    bool isIntString2 = lines[i - 1].All(char.IsDigit);

                                    if (!date2.Success && isIntString == false)
                                    {
                                        stri = "";
                                        stri = Regex.Replace(lines[i + 1], "[^A-Z]", " ");
                                        string[] splitName2 = stri.ToString().Split(' ');
                                        splitName2 = splitName2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                        if (splitName2.Length > 1)
                                        {
                                            NameIndex = i;
                                            entGuestResult.Guest_Firstname = splitName2[0];
                                            GotData = 1;
                                            entGuestResult.Guest_Lastname = splitName2[1];
                                        }
                                    }
                                    else if (!date3.Success && isIntString2 == false)
                                    {
                                        stri = "";
                                        stri = Regex.Replace(lines[i - 1], "[^A-Z]", " ");
                                        string[] splitName2 = stri.ToString().Split(' ');
                                        splitName2 = splitName2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                        if (splitName2.Length > 1)
                                        {
                                            NameIndex = i;
                                            entGuestResult.Guest_Firstname = splitName2[0];
                                            GotData = 1;
                                            entGuestResult.Guest_Lastname = splitName2[1];
                                        }
                                    }
                                }
                                catch 
                                {
                                
                                }
                            }
                        }

                        if (lines[i].Contains("Sign. Of") || lines[i].Contains("Sign.") || lines[i].Contains("Licencing Au"))
                        {
                            signofholderIndex = i;
                        }
                        if (lines[i].Contains("Birth") || lines[i].Contains("Date Of"))
                        {
                            string[] splitDob = lines[i].ToString().Split(' ');
                            splitDob = splitDob.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            for (int k = 0; k < splitDob.Length; k++)
                            {
                                Match doc1 = dateFormat.Match(splitDob[k]);
                                if (doc1.Success)
                                {
                                    entGuestResult.Guest_DOB = doc1.Value ;
                                }                              
                            }
                        }

                        //Get Address                       
                        if (lines[i].Contains("Addres") || lines[i].Contains("ADDRE"))
                        {
                            AddressIndex = i;
                            if (lines[i].Contains("Address"))
                                entGuestResult.Guest_Address = lines[i].Replace("Address", "") + " , ";
                            if (lines[i].Contains("ADDRESS"))
                                entGuestResult.Guest_Address = lines[i].Replace("ADDRESS", "") + " , ";
                            i = i + 1;
                            for (int j = i; j < i + 2; j++)
                            {
                                Match d = pincodeFormat.Match(lines[j]);
                                if (d.Success)
                                {
                                    //htDatas["txtZip"] = d.Value;
                                    break;
                                }
                                {
                                    if (lines[j].Contains("Licenc") || lines[j].Contains("Holder") || lines[j].Contains("Authority"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        GotData = 1;
                                        entGuestResult.Guest_Address += lines[j].ToString() + " , ";
                                    }
                                }
                            }
                            entGuestResult.Guest_Address = Regex.Replace(entGuestResult.Guest_Address.ToString(), "[^A-Za-z0-9]", " ");
                            GotData = 1;
                        }

                        Match zi = pincodeFormat.Match(lines[i]);
                        if (zi.Success)
                        {
                            zipIndex = i;
                            //htDatas["txtZip"] = zi.Value;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                entGuestResult.Guest_Address = Regex.Replace(entGuestResult.Guest_Address.ToString(), "[^A-Za-z0-9]", " ");
                if (entGuestResult.Guest_Address.ToString() == "")
                {
                    if (zipIndex != 0)
                    {
                        entGuestResult.Guest_Address = lines[zipIndex - 2].ToString() + " , " + lines[zipIndex - 1].ToString() + " , " + lines[zipIndex].ToString();
                    }

                    else if (signofholderIndex != 0)
                    {
                        entGuestResult.Guest_Address = lines[signofholderIndex - 2].ToString() + " , " + lines[signofholderIndex - 1].ToString() + " , " + lines[signofholderIndex].ToString();
                    }

                    if (entGuestResult.Guest_Address.ToString().Contains("Address"))
                        entGuestResult.Guest_Address = entGuestResult.Guest_Address.Replace("Address", "") + " , ";
                    if (entGuestResult.Guest_Address.ToString().Contains("ADDRESS"))
                        entGuestResult.Guest_Address = entGuestResult.Guest_Address.ToString().Replace("ADDRESS", "") + " , ";
                    entGuestResult.Guest_Address = Regex.Replace(entGuestResult.Guest_Address.ToString(), "[^A-Za-z0-9]", " ");
                    GotData = 1;

                }


                if (entGuestResult.Guest_DOB.ToString() == "")
                {
                    Match dateDB = dateFormat.Match(lines[NameIndex + 1]);
                    if (dateDB.Success)
                    {
                        entGuestResult.Guest_DOB = dateDB.Value;
                    }
                }
                if (entGuestResult.Guest_Father.ToString() == "")
                {
                    string father = Regex.Replace(lines[AddressIndex - 1], "[^A-Z]", " ");
                    string[] splitFt = father.ToString().Split(' ');
                    splitFt = splitFt.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    entGuestResult.Guest_Father = splitFt[0];
                }
                if (entGuestResult.Guest_Firstname.ToString() == "")
                {
                    string stri = "";
                    stri = Regex.Replace(lines[1], "[^A-Z]", " ");
                    string[] splitName = stri.ToString().Split(' ');
                    splitName = splitName.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    if (splitName.Length > 1)
                    {
                        entGuestResult.Guest_Firstname = splitName[0];
                        GotData = 1;
                        entGuestResult.Guest_Lastname = splitName[1];
                    }
                }
            }
            catch
            {

            }
        }

        private void GetPANCard(string[] lines)
        {            
            try
            {
                Regex date = new Regex(@"\d{2}/\d{2}/\d{4}");
                entGuestResult.Guest_Country = "IND";
                entGuestResult.Guest_CountryofIssue = "India";
                entGuestResult .Guest_Nationality= "IND";
             

                string name = "";
                int dobIndex = 0;

                for (int i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Contains("INCOME") || lines[i].Contains("TAX") || lines[i].Contains("DEPARTMENT"))
                    {
                        name = Regex.Replace(lines[i + 1], @"[^A-Z]\s+", String.Empty);
                        entGuestResult.Guest_Father = lines[i + 2].ToString();
                        entGuestResult.Guest_DOB = lines[i + 3].ToString();
                    }

                    if (lines[i].Contains("Permanent") || lines[i].Contains("Account") || lines[i].Contains("Number"))
                    {
                        entGuestResult.Guest_DocumentNo = lines[i + 1].ToString();
                    }

                    Regex dob = new Regex(@"\d{2}/\d{2}/\d{4}");
                    Match d = dob.Match(lines[i].ToString());
                    if (d.Success)
                    {
                        dobIndex = i;
                        entGuestResult.Guest_DOB = d.Value;
                    }
                }

                for (int i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Contains("Name") && !lines[i].Contains("Father"))
                    {
                        name = Regex.Replace(lines[i + 1], @"[^A-Z]\s+", String.Empty);
                    }
                    if (lines[i].Contains("Father"))
                    {
                        entGuestResult.Guest_Father = lines[i + 1].ToString();
                    }
                    if (lines[i].Contains("Permanent") || lines[i].Contains("Account") || lines[i].Contains("Number"))
                    {
                        entGuestResult.Guest_DocumentNo = lines[i + 1].ToString();
                    }
                }

                if (name != "")
                {
                    GotData = 1;
                    name = Regex.Replace(name, @"[a-z]+", String.Empty);
                    string[] splitNamefirstsec = name.Split(' ');
                    entGuestResult .Guest_Firstname= splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                    entGuestResult.Guest_Lastname = splitNamefirstsec[1].ToString().Replace("\"", string.Empty);
                }


                if (entGuestResult.Guest_Firstname.ToString() == "")
                {
                    GotData = 1;
                    name = Regex.Replace(lines[dobIndex - 2], @"[^A-Z]\s+", String.Empty);
                    name = Regex.Replace(name, @"[a-z]+", String.Empty);
                    string[] splitNamefirstsec = name.Split(' ');
                    entGuestResult.Guest_Firstname = splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                    entGuestResult.Guest_Lastname = splitNamefirstsec[1].ToString().Replace("\"", string.Empty);

                }

                
                if (entGuestResult.Guest_Father.ToString() == "")
                    entGuestResult.Guest_Father = lines[dobIndex - 1].ToString();
            }
            catch
            {              
            }
        }

        private void GetPassportDatasBySpaceOCR(string[] lines)
        {         
            Regex dateFormat = new Regex(@"\d{2}/\d{2}/\d{4}");
            try
            {
                for (int i = 0; i < lines.Count(); i++)
                {

                    if (lines[i].Contains("Surname"))
                    {
                         entGuestResult.Guest_Lastname= lines[i + 1].ToString();
                        GotData = 1;
                        if (lines[i + 2].Contains("Given") || lines[i + 2].Contains("Name"))
                           entGuestResult.Guest_Firstname = lines[i + 3].ToString();
                    }
                    if (lines[i].Contains("Sex"))
                    {
                        string linGender = string.Concat(lines[i + 1].Where(x=>x>='A'&&x<='Z'));
                        string gender = linGender.Substring(linGender.Length-1);
                        if (gender=="M")
                            entGuestResult.Guest_Gender = "Male";
                        else if (gender=="F")
                            entGuestResult.Guest_Gender = "Female";
                        else if (gender == "X")
                            entGuestResult.Guest_Gender = "Transgender";

                        Match date = dateFormat.Match(lines[i + 1]);
                        if (date.Success && entGuestResult.Guest_DOB != "")
                        {
                            entGuestResult.Guest_DOB = date.Value;
                        }

                        string nationality = linGender.Substring(0,linGender.Length - 1);
                     

                        if (nationality != null)
                        {
                            var query = from c in Global.glbNationalityDT.AsEnumerable()
                                        where c.Field<string>("zs_nationality") == nationality.ToString()
                                        select new
                                        {
                                            countryID = c.Field<string>("zs_nationalitycode")
                                        };


                            foreach (var m in query)
                            {
                                entGuestResult.Guest_Nationality = m.countryID;
                                entGuestResult.Guest_Country = m.countryID;
                            }
                        }
                    }

                    if (lines[i].Contains("Place of Issue"))
                    {
                        entGuestResult.Guest_CountryofIssue = lines[i + 1].ToString();
                    }

                    if (lines[i].Contains("Expiry") || lines[i].Contains("Date of Issue"))
                    {
                        string[] splitString1 = lines[i + 1].ToString().Split();
                        splitString1 = splitString1.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        Match date1 = dateFormat.Match(splitString1[0]);
                        if (date1.Success)
                        {
                            entGuestResult.Guest_DateOfIssue = date1.Value;
                        }
                        Match date2 = dateFormat.Match(splitString1[1]);
                        if (date2.Success)
                        {
                            entGuestResult.Guest_ExpiryDate = date2.Value;
                        }
                    }
                    //Nationality
                    if (lines[i].Contains("Nationality") || lines[i].Contains("Nationaiity"))
                    {
                        if (entGuestResult.Guest_Nationality != "")
                        {
                            string[] splitString = lines[i + 1].ToString().Split('\t'); ;
                            splitString = splitString.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            string nationality = splitString[0];

                            if (nationality != null)
                            {
                                var query = from c in Global.glbNationalityDT.AsEnumerable()
                                            where c.Field<string>("zs_nationality") == nationality.ToString().ToUpper()
                                            select new
                                            {
                                                countryID = c.Field<string>("zs_nationalitycode")
                                            };


                                foreach (var m in query)
                                {
                                    entGuestResult.Guest_Nationality = m.countryID;
                                    entGuestResult.Guest_Country = m.countryID;
                                }
                            }
                        }
                    }
                    //Valid and Expiry Date
                    if (lines[i].Contains("<<<") )
                    {                      
                        Match date = dateFormat.Match(lines[i - 1]);
                        if (date.Success)
                        {
                            entGuestResult.Guest_DateOfIssue = date.Value;
                            entGuestResult.Guest_ExpiryDate = date.NextMatch().ToString();
                        }
                    }

                    //DOB
                    if (lines[i].Contains("Date of Birth"))
                    {
                        Match date = dateFormat.Match(lines[i + 1]);
                        if (date.Success && entGuestResult.Guest_DOB!="")
                        {
                            entGuestResult.Guest_DOB = date.Value;
                        }
                   }
                }


                for (int i = 0; i < lines.Count(); i++)
                {
                    string fname = "";
                    string lname = "";
                   
                    if (lines[i].Contains("Given"))
                    {
                        fname = lines[i + 1].ToString();
                        GotData = 1;
                        lname = lines[i - 1].ToString();
                        if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname) && lname != "")
                            entGuestResult.Guest_Lastname = lname;
                        if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname) && fname != "")
                            entGuestResult.Guest_Firstname = fname;
                        break;
                    }
                    else if (lines[i].Contains("Surn"))
                    {
                        lname = lines[i + 1].ToString();
                        GotData = 1;
                        fname = lines[i + 3].ToString();
                        if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname) && lname != "")                        
                            entGuestResult.Guest_Lastname = lname;                                                   
                        if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname) && fname != "")                        
                            entGuestResult.Guest_Firstname = fname;
                        break;
                    }
                   
                }

                if (string.IsNullOrEmpty(entGuestResult.Guest_ExpiryDate)) 
                {
                    int inde = 0;
                    Match date = dateFormat.Match(lines[lines.Length - 1]);
                    if (date.Success)
                    {
                        inde = lines.Length - 1;
                    }
                    Match date1 = dateFormat.Match(lines[lines.Length - 2]);
                    if (date1.Success)
                    {
                        inde = lines.Length - 2;
                    }
                    string[] splitString1 = lines[inde].ToString().Split();
                    splitString1 = splitString1.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    Match date3 = dateFormat.Match(splitString1[0]);
                    if (date3.Success)
                    {
                        entGuestResult.Guest_DateOfIssue= date3.Value;
                    }
                    Match date4 = dateFormat.Match(splitString1[1]);
                    if (date4.Success)
                    {
                        entGuestResult.Guest_ExpiryDate = date4.Value;
                    }

                }
            }
            catch
            {
               
            }
        }

        //added by vineetha on 20-01-2020  
        private void GetPassportDatasByOCR(string[] lines)
        {
            try
            {             
                string inp1 = "";
                string inp2 = "";

               
                for (int i = lines.Length-1; i > lines.Length-4; i--)
                {
                    if(lines[i].Contains("<") && inp1=="")
                    {
                        inp1 =Regex.Replace(lines[i],@"\s+","");
                    }
                    else if (lines[i].Contains("<") && inp2=="")
                    {
                        inp2 = Regex.Replace(lines[i], @"\s+", "");
                     
                        break;
                    }
                }
              
                string lng1 = "";//added by vineetha 17-01-2020
                lng1 = inp1.Substring(0, 10);
                if (lng1.Contains("<"))
                {
                    string outputString1 = Regex.Replace(inp1, "[<]", " ");
                    if (outputString1.Any(char.IsUpper))
                    {
                        string string2 = "";
                        string[] splitString1 = outputString1.Split();

                    entGuestResult.Guest_DocumentNo = splitString1[0];

                        string2 = splitString1[1];
                        if (string2.Length < 3)
                            string2 = splitString1[2];
                        try
                        {


                            if (string2.Substring(11, 1) != "M" || string2.Substring(11, 1) != "F" || string2.Substring(11, 1) != "X")
                            {
                                if (string2.Substring(12, 1) == "M" || string2.Substring(12, 1) == "F" || string2.Substring(12, 1) == "X")
                                {
                                    string2 = string2.Substring(1, splitString1[1].Length - 1);
                                }
                                else if (string2.Substring(10, 1) == "M" || string2.Substring(10, 1) == "F" || string2.Substring(10, 1) == "X")
                                {
                                    string2 = "P" + string2;
                                }
                            }
                            entGuestResult.Guest_Gender = string2.Substring(11, 1);
                            GotData = 1;
                            if (entGuestResult.Guest_Gender.ToString() == "M")
                                entGuestResult.Guest_Gender = "Male";
                            else if (entGuestResult.Guest_Gender.ToString() == "F")
                                entGuestResult.Guest_Gender = "Female";
                            else if (entGuestResult.Guest_Gender.ToString() == "X")
                                entGuestResult.Guest_Gender = "Transgender";
                        }
                        catch
                        {

                        }
                        try
                        {
                            string nationality = string2.Substring(1, 3).Replace('1', 'I');
                            nationality = nationality.Replace('0', 'O');
                            if (nationality != null)
                            {
                                //var query = from c in dtCountry.AsEnumerable()
                                var query = from c in Global.glbCountryDT.AsEnumerable()
                                            where c.Field<string>("Country_iso3") == nationality.ToString().ToUpper()
                                            select new
                                            {
                                                countryID = c.Field<string>("Country_iso3")
                                            };

                                foreach (var m in query)
                                {
                                    entGuestResult.Guest_Nationality = m.countryID;
                                    entGuestResult.Guest_Country = m.countryID;
                                }
                            }
                        }
                        catch
                        {
                        }

                        try
                        {


                            string dob = "";
                            string date = string2.Substring(8, 2);
                            string month = string2.Substring(6, 2);
                            string year = string2.Substring(4, 2);
                            if (25 >= Convert.ToInt32(year))
                                year = "20" + year;
                            else
                                year = "19" + year;
                            dob = date + "/" + month + "/" + year;
                            entGuestResult.Guest_DOB = dob;
                        }
                        catch
                        {

                        }

                        try
                        {
                            string expirydate = "";
                            string exdate = string2.Substring(16, 2);
                            string exmonth = string2.Substring(14, 2);
                            string exyear = "20" + string2.Substring(12, 2);
                            expirydate = exdate + "/" + exmonth + "/" + exyear;
                            entGuestResult.Guest_ExpiryDate = expirydate;
                            DateTime convertedDate = GetDate(Convert.ToInt32(exdate), Convert.ToInt32(exmonth), Convert.ToInt32(exyear));
                            string asd = convertedDate.AddYears(-10).AddDays(+1).ToString("dd/MM/yyyy");
                            entGuestResult.Guest_DateOfIssue = asd.ToString().Substring(0, 10).Replace('-', '/');
                        }
                        catch
                        {

                        }
                        string outputString2;
                        if (inp2.Contains("<<<"))
                        {
                            int ind = inp2.IndexOf("<<<");
                            outputString2 = inp2.Substring(0, ind); //Changed by Vineetha on 09-12-2019
                            outputString2 = Regex.Replace(outputString2, "<K<", " ");
                        }
                        else
                        {
                            outputString2 = Regex.Replace(inp2, "<K<", " ");
                        }
                        outputString2 = Regex.Replace(outputString2, "<<<", " ");
                        outputString2 = Regex.Replace(outputString2, "<X<", " "); //Added by Vineetha on 09-12-2019
                        outputString2 = Regex.Replace(outputString2, " KK", " ");
                        outputString2 = Regex.Replace(outputString2, "KK ", " ");
                        outputString2 = Regex.Replace(outputString2, "<KK<", " ");
                        if (outputString2.Contains("<<"))
                            outputString2 = Regex.Replace(outputString2, "<<", "/");

                        outputString2 = Regex.Replace(outputString2, "<", " ");

                        entGuestResult.Guest_CountryofIssue = outputString2.Substring(2, 3);
                        string name = outputString2.Substring(5, outputString2.Length - 5);
                        if (name.Contains("/"))
                        {
                            string[] namesss = name.Split('/');
                            if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname))
                                entGuestResult.Guest_Lastname = namesss[0];
                            if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname))
                                entGuestResult.Guest_Firstname = namesss[1];
                        }
                        else
                        {
                            string[] namesss = name.Split(' ');
                            if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname))
                                entGuestResult.Guest_Lastname = Regex.Replace(namesss[0], "[<]", " ");
                            if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname))
                                entGuestResult.Guest_Firstname = Regex.Replace(namesss[1], "[<]", " ");
                        }
                      
                    }
                }
                else
                {
                    string st1 = "";
                    string outputString11 = Regex.Replace(inp1, "[<]", " ");
                    string str1 = outputString11.Replace(" ", string.Empty);
                    if (str1.Length > 28)
                    {
                        st1 = str1.Substring(0, 28);
                    }
                    if (st1.Any(char.IsUpper))
                    {                     
                        string[] splitString1 = st1.Split();

                        string code = splitString1[0];
                        string orgcode = code.Substring(0, 9);
                        string remcode = code.Substring(9, 19);

                      entGuestResult.Guest_DocumentNo = orgcode;
                        string str2 = remcode;
                        try
                        {

                       
                        if (str2.Substring(11, 1) != "M" || str2.Substring(11, 1) != "F" || str2.Substring(11, 1) != "X")
                        {
                            if (str2.Substring(12, 1) == "M" || str2.Substring(12, 1) == "F" || str2.Substring(12, 1) == "X")
                            {
                                str2 = str2.Substring(1, str2.Length - 1);
                            }
                            else if (str2.Substring(10, 1) == "M" || str2.Substring(10, 1) == "F" || str2.Substring(10, 1) == "X")
                            {
                                str2 = "P" + str2;
                            }
                        }
                        entGuestResult.Guest_Gender = str2.Substring(11, 1);
                        GotData = 1;
                        if (entGuestResult.Guest_Gender.ToString() == "M")
                            entGuestResult.Guest_Gender = "Male";
                        else if (entGuestResult.Guest_Gender.ToString() == "F")
                            entGuestResult.Guest_Gender = "Female";
                        else if (entGuestResult.Guest_Gender.ToString() == "X")
                            entGuestResult.Guest_Gender = "Transgender";
                        }
                        catch 
                        {
                            
                        }
                        string nationality = str2.Substring(1, 3).Replace('1', 'I');
                        nationality = nationality.Replace('0', 'O');                                        

                        if (nationality != null)
                        {
                            var query = from c in Global.glbCountryDT.AsEnumerable()
                                        where c.Field<string>("Country_iso3") == nationality.ToString().ToUpper()
                                        select new
                                        {
                                            countryID = c.Field<string>("Country_iso3")
                                        };

                            foreach (var m in query)
                            {
                                entGuestResult.Guest_Nationality = m.countryID;
                                entGuestResult.Guest_Country = m.countryID;
                            }
                        }
                        try
                        {
                            string dob = "";
                            string date = str2.Substring(8, 2);
                            string month = str2.Substring(6, 2);
                            string year = str2.Substring(4, 2);
                            if (25 >= Convert.ToInt32(year))
                                year = "20" + year;
                            else
                                year = "19" + year;
                            dob = date + "/" + month + "/" + year;
                            entGuestResult.Guest_DOB = dob;
                        }
                        catch
                        {

                        }
                        if (entGuestResult.Guest_Nationality.ToString() == "IND")//edited by vineetha 15-01-2020
                        {
                            string expirydate = "";
                            string exdate = str2.Substring(16, 2);
                            string exmonth = str2.Substring(14, 2);
                            string exyear = "20" + str2.Substring(12, 2);
                            expirydate = exdate + "/" + exmonth + "/" + exyear;
                            entGuestResult.Guest_ExpiryDate = expirydate;
                            DateTime convertedDate = GetDate(Convert.ToInt32(exdate), Convert.ToInt32(exmonth), Convert.ToInt32(exyear));
                            string asd = convertedDate.AddYears(-10).AddDays(+1).ToString("dd/MM/yyyy");
                            entGuestResult.Guest_DateOfIssue = asd.ToString().Substring(0, 10).Replace('-', '/');
                        }
                        else
                        {
                            try
                            {
                                string expirydate = "";
                                string edate = "";
                                string emonth = "";
                                string eyr = "";

                                string exdate = str2.Substring(16, 2);
                                if (exdate.Contains("B"))//added by vineetha 17-01-2020
                                {
                                    edate = Regex.Replace(exdate, "B", "8");
                                    exdate = edate;
                                }

                                string exmonth = str2.Substring(14, 2);
                                if (exmonth.Contains("B"))
                                {
                                    emonth = Regex.Replace(exmonth, "B", "8");
                                    exmonth = emonth;
                                }
                                string exyear = "20" + str2.Substring(12, 2);
                                if (exyear.Contains("B"))
                                {
                                    eyr = Regex.Replace(exyear, "B", "8");
                                    exyear = eyr;
                                }
                                expirydate = exdate + "/" + exmonth + "/" + exyear;
                                entGuestResult.Guest_ExpiryDate = expirydate;

                                DateTime convertedDate = GetDate(Convert.ToInt32(exdate), Convert.ToInt32(exmonth), Convert.ToInt32(exyear));
                                string asd = convertedDate.AddYears(-10).ToString("dd/MM/yyyy");
                                entGuestResult.Guest_DateOfIssue = asd.ToString().Substring(0, 10).Replace('-', '/');
                            }
                            catch
                            {
                            }
                        }

                        string outputString22;
                        if (inp2.Contains("<<<"))
                        {
                            int ind = inp2.IndexOf("<<<");
                            outputString22 = inp2.Substring(0, ind); //Changed by Vineetha on 09-12-2019
                            outputString22 = Regex.Replace(outputString22, "<K<", " ");
                        }
                        else
                        {
                            outputString22 = Regex.Replace(inp2, "<K<", " ");
                        }
                        outputString22 = Regex.Replace(outputString22, "<<<", " ");
                        outputString22 = Regex.Replace(outputString22, "<X<", " "); //Added by Vineetha on 09-12-2019
                        outputString22 = Regex.Replace(outputString22, " KK", " ");
                        outputString22 = Regex.Replace(outputString22, "KK ", " ");
                        outputString22 = Regex.Replace(outputString22, "<KK<", " ");
                        if (outputString22.Contains("<<"))
                            outputString22 = Regex.Replace(outputString22, "<<", "/");

                        outputString22 = Regex.Replace(outputString22, "<", " ");
                        if (outputString22.Substring(1, 1) == "<" || outputString22.Substring(1, 1) == "K")
                        {
                            if (string.IsNullOrEmpty(entGuestResult.Guest_CountryofIssue))
                                entGuestResult.Guest_CountryofIssue = outputString22.Substring(2, 3);
                    
                          string name = outputString22.Substring(5, outputString22.Length - 5);
                            if (name.Contains("/"))
                            {
                                string[] namesss = name.Split('/');
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname))
                                    entGuestResult.Guest_Lastname = namesss[0];
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname))
                                    entGuestResult.Guest_Firstname = namesss[1];
                            }
                            else
                            {
                                string[] namesss = name.Split(' ');
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname))
                                    entGuestResult.Guest_Lastname = Regex.Replace(namesss[0], "[<]", " ");
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname))
                                    entGuestResult.Guest_Firstname = Regex.Replace(namesss[1], "[<]", " ");
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(entGuestResult.Guest_CountryofIssue))
                                entGuestResult.Guest_CountryofIssue = outputString22.Substring(1, 3);
                            string name = outputString22.Substring(5, outputString22.Length - 5);
                            if (name.Contains("/"))
                            {
                                string[] namesss = name.Split('/');
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname))
                                    entGuestResult.Guest_Lastname = namesss[0];
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname))
                                    entGuestResult.Guest_Firstname = namesss[1];
                            }
                            else
                            {
                                string[] namesss = name.Split(' ');
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Lastname))
                                    entGuestResult.Guest_Lastname = Regex.Replace(namesss[0], "[<]", " ");
                                if (string.IsNullOrEmpty(entGuestResult.Guest_Firstname))
                                    entGuestResult.Guest_Firstname = Regex.Replace(namesss[1], "[<]", " ");
                            }
                        }
                    }
                }
            }
            catch
            {
               
            }
        }

        public static DateTime GetDate(int dd, int MM, int yyyy)
        {
            string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames;
            string DateString = dd + monthNames[MM - 1] + yyyy;
            string SystemDateFormate = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string InputDate = Convert.ToDateTime(DateString).ToString(SystemDateFormate);
            DateTime ResultedDate = DateTime.Parse(InputDate);
            return ResultedDate;
        }

        private void GetAdharDatasByOCR(string[] lines, string text)
        {
            try
            {               
                Regex dateFormat = new Regex(@"\d{2}/\d{2}/\d{4}");
                Regex dateFor = new Regex(@"\d{2}-\d{2}-\d{4}");
                Regex docNo = new Regex(@"[0-9]{4}\s+[0-9]{4}\s+[0-9]{4}");

                int dobIndex = 0;
                for (int i = 1; i < lines.Count(); i++)
                {
                    Match dob = dateFormat.Match(lines[i]);
                    Match dobFor = dateFor.Match(lines[i]);

                    if (dob.Success)
                    {
                        entGuestResult.Guest_DOB= dob.Value;
                        string[] splitNamefirstsec = lines[i - 1].Replace("\t", string.Empty).Split(' ');
                        splitNamefirstsec = splitNamefirstsec.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        entGuestResult .Guest_Firstname= splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                        entGuestResult.Guest_Lastname = splitNamefirstsec[splitNamefirstsec.Length - 1].ToString().Replace("\"", string.Empty);
                        GotData = 1;
                        if (lines[i + 1].Contains("FEMALE") || lines[i + 1].Contains("Female"))
                        {
                           entGuestResult.Guest_Gender = "Female";
                        }
                        else if (lines[i + 1].Contains("MALE") || lines[i + 1].Contains("Male"))
                        {
                            entGuestResult.Guest_Gender = "Male";
                        }
                    }
                    else if (dobFor.Success)
                    {
                        entGuestResult.Guest_DOB = dobFor.Value;
                        string[] splitNamefirstsec = lines[i - 1].Replace("\t", string.Empty).Split(' ');
                        splitNamefirstsec = splitNamefirstsec.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        entGuestResult.Guest_Firstname = splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                        entGuestResult.Guest_Lastname = splitNamefirstsec[splitNamefirstsec.Length - 1].ToString().Replace("\"", string.Empty);
                        GotData = 1;
                        if (lines[i + 1].Contains("FEMALE") || lines[i + 1].Contains("Female"))
                        {
                            entGuestResult.Guest_Gender = "Female";
                        }
                        else if (lines[i + 1].Contains("MALE") || lines[i + 1].Contains("Male"))
                        {
                            entGuestResult.Guest_Gender = "Male";
                        }
                    }

                    Match doco = docNo.Match(lines[i]);
                    if (doco.Success && entGuestResult.Guest_DocumentNo.ToString() == string.Empty)
                    {
                       entGuestResult.Guest_DocumentNo = lines[i];
                    }

                    if (lines[i].Contains("FEMALE") || lines[i].Contains("Female"))
                    {
                        entGuestResult.Guest_Gender = "Female";
                    }
                    else if (lines[i].Contains("MALE") || lines[i].Contains("Male"))
                    {
                        entGuestResult.Guest_Gender = "Male";
                    }

                    if (lines[i].Contains("DOB"))
                    {
                        dobIndex = i;
                    }
                    entGuestResult.Guest_Nationality = "IND";
                    entGuestResult.Guest_Country = "IND";
                   entGuestResult.Guest_CountryofIssue = "INDIA";
                    //cmbNationality.SelectedValue = "IND";
                }


                if (entGuestResult.Guest_DOB.ToString() == "" || text.Contains("Father"))
                {
                    for (int i = 1; i < lines.Count(); i++)
                    {
                        if (lines[i].Contains("Father")) ///If adhar contains data of Father
                        {
                            entGuestResult.Guest_Father = lines[i].Split(':')[1].ToString();
                            string[] splitNamefirstsec = lines[i - 2].Replace("\t", string.Empty).Split(' ');
                            splitNamefirstsec = splitNamefirstsec.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            entGuestResult.Guest_Firstname = splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                            entGuestResult.Guest_Lastname = splitNamefirstsec[splitNamefirstsec.Length - 1].ToString().Replace("\"", string.Empty);
                            GotData = 1;
                            if (lines[i + 1].Contains("FEMALE") || lines[i + 1].Contains("Female"))
                            {
                                entGuestResult.Guest_Gender = "Female";
                            }
                            else if (lines[i + 1].Contains("MALE") || lines[i + 1].Contains("Male"))
                            {
                                entGuestResult.Guest_Gender = "Male";
                            }
                        }

                        if (lines[i].Contains("Year") || lines[i].Contains("Birth") || lines[i].Contains("of B"))  //If adhar contains only year of birth
                        {
                            entGuestResult.Guest_DOB = lines[i].Replace("[^0-9/]+", " ");
                            string[] splitNamefirstsec = lines[i - 1].Replace("\t", string.Empty).Split(' ');
                            splitNamefirstsec = splitNamefirstsec.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            entGuestResult.Guest_Firstname = splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                            entGuestResult.Guest_Lastname = splitNamefirstsec[splitNamefirstsec.Length - 1].ToString().Replace("\"", string.Empty);
                            GotData = 1;
                            if (lines[i + 1].Contains("FEMALE") || lines[i + 1].Contains("Female"))
                            {
                                entGuestResult.Guest_Gender = "Female";
                            }
                            else if (lines[i + 1].Contains("MALE") || lines[i + 1].Contains("Male"))
                            {
                                entGuestResult.Guest_Gender = "Male";
                            }
                            break;
                        }
                    }
                }


                if (entGuestResult.Guest_Firstname.ToString() == string.Empty && dobIndex != 0) //if adhar doesn't read DOB
                {
                    string[] splitNamefirstsec = lines[dobIndex - 1].Replace("\t", string.Empty).Split(' ');
                    splitNamefirstsec = splitNamefirstsec.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    entGuestResult.Guest_Firstname = splitNamefirstsec[0].ToString().Replace("\"", string.Empty);
                    entGuestResult.Guest_Lastname = splitNamefirstsec[splitNamefirstsec.Length - 1].ToString().Replace("\"", string.Empty);
                    GotData = 1;
                }
            }
            catch
            {
               
            }
        }

        ////Extract Datas from Voters ID
        private void GetVoters(string[] lines)
        {
            try
            {
                Regex regexdate = new Regex(@"\d{2}/\d{2}/\d{4}");
                for (int i = 0; i < lines.Count(); i++)
                {
                    string[] splitN = new string[5];
                    //Get Name
                    if (lines[i].Contains("Elector"))
                    {

                        if (lines[i].Contains(':'))
                            splitN = lines[i].Split(':');
                        else if (lines[i].Contains(';'))
                            splitN = lines[i].Split(';');

                        string[] splitName;
                        splitName = splitN[1].Split(' ');
                        if (splitName[0] != "")
                            entGuestResult.Guest_Firstname = splitName[0];
                        else
                            entGuestResult.Guest_Firstname = splitName[1];
                        entGuestResult.Guest_Lastname = splitName[splitName.Length - 1];
                        GotData = 1;
                    }

                    if (lines[i].Contains("Father"))
                    {
                        if (lines[i].Contains(':'))
                            splitN = lines[i].Split(':');
                        else if (lines[i].Contains(';'))
                            splitN = lines[i].Split(';');
                        entGuestResult.Guest_Father = splitN[1];
                    }
                    if ((lines[i].Contains("Sex") && lines[i].Contains("M /")) || (lines[i].Contains("Sex") && lines[i].Contains("/M")))
                        entGuestResult.Guest_Gender = "Male";
                    else if (lines[i].Contains("Sex") && lines[i].Contains("F /"))
                        entGuestResult.Guest_Gender = "Female";

                    Match m = regexdate.Match(lines[i]);
                    if (m.Success)
                    {
                        entGuestResult.Guest_DOB = m.Value;
                    }
                    entGuestResult.Guest_Country = "IND";
                    entGuestResult.Guest_Nationality = "IND";                 
                }
            }
            catch
            {                
            }
        }

        ////Extract Datas from Voters Back ID
        private void GetVotersBack(string[] lines)
        {
            try
            {
                int pinIndex = 0;
                int addIndex = 0;
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Contains("Address"))
                    {
                        addIndex = i;
                    }
                    if (lines[i].Contains("Pincode"))
                    {
                        string[] splitNa;
                        pinIndex = i;
                        if (lines[i].Contains(':'))
                        {
                            splitNa = lines[i].Split(':');
                            //txtZip.Text = lines[i];
                        }
                        //else
                        //    txtZip.Text = Regex.Replace(lines[i], "Pincode", "");

                        GotData = 1;
                    }

                    if (lines[i].Contains("Place"))
                    {
                        string[] splitNa = lines[i].Split(':');
                       entGuestResult.Guest_City= splitNa[1];                     
                    }
                }
                string add = "";
                if (pinIndex > 0)
                {
                    for (int k = addIndex; k < pinIndex; k++)
                    {
                        add = add + lines[k];
                        GotData = 1;
                    }
                }
                else
                {
                    for (int k = addIndex; k < addIndex + 5; k++)
                    {
                        add = add + lines[k];
                        GotData = 1;
                    }
                }
               entGuestResult.Guest_Address = Regex.Replace(add, "Address :", "");
               
            }
            catch
            {
            }
        }

        private void GetAdharBackByOCR(string[] lines)
        {            
            try
            {
                Regex pincodeFormat = new Regex(@"[0-9]{6}");
                for (int i = 1; i < lines.Count(); i++)
                {
                    if (lines[i].Contains("Address"))
                    {
                        string[] addsplit = lines[i].Split(':');
                        entGuestResult.Guest_Address= addsplit[addsplit.Length - 1].ToString() + ",";
                        i = i + 1;
                        for (int j = i; j < i + 5; j++)
                        {
                            Match d = pincodeFormat.Match(lines[j]);
                            if (d.Success)
                            {
                                i = j;
                                entGuestResult.Guest_Address += lines[j].ToString() + ",";
                                entGuestResult.Guest_Address = entGuestResult.Guest_Address;
                                //htDatas["txtZip"] = d.Value;
                                //txtZip.Text = d.Value;
                                break;
                            }
                            {
                                entGuestResult.Guest_Address += lines[j].ToString() + ",";
                            }
                        }                   
                        GotData = 1;
                    }
                }
            }
            catch
            {

            }
        }

        private bool CheckIfAdhar()
        {
            bool flag = false;             
            using (stream = new FileStream(Global.Image_Path, FileMode.Open, FileAccess.Read))
            {
                var bmp = new Bitmap(stream);
                using (bmp)
                {
                    try
                    {
                        string[] barcodes = KeepDynamic.BarcodeReader.BarcodeReader.read(bmp, KeepDynamic.BarcodeReader.Type.QRCode);
                        string result = "";
                        if (barcodes != null)
                        {
                            result = barcodes[0];
                        }

                        if (result != "")
                        {
                            string[] adharlines = result.Split('=');
                            int count = adharlines.Length;
                            string docno = "";
                            docno = Regex.Replace(adharlines[3], " name", "").ToString().Replace("\"", string.Empty);
                            if (docno != "")
                            {
                                AdharResult = result;
                                flag = true;
                            }
                        }
                    }
                    catch
                    {
                        stream.Dispose();
                        stream.Close();
                        return flag;
                    }
                }
            }
            stream.Dispose();
            stream.Close();
            return flag;
        }
        #endregion

    
        public bool GetOcrAdhar()
        {
            bool flag = false;
            // Create standard .NET web client instance
            WebClient webClient = new WebClient();

            // Set API Key
            webClient.Headers.Add("x-api-key", API_KEY);

            // 1. RETRIEVE THE PRESIGNED URL TO UPLOAD THE FILE.
            // * If you already have a direct file URL, skip to the step 3.

            // Prepare URL for `Get Presigned URL` API call
            string query = Uri.EscapeUriString(string.Format(
                "https://api.pdf.co/v1/file/upload/get-presigned-url?contenttype=application/octet-stream&name={0}",
                Path.GetFileName(Global.Image_Path)));

            try
            {
                // Execute request
                string response = webClient.DownloadString(query);

                // Parse JSON response
                JObject json = JObject.Parse(response);

                if (json["error"].ToObject<bool>() == false)
                {
                    // Get URL to use for the file upload
                    string uploadUrl = json["presignedUrl"].ToString();
                    // Get URL of uploaded file to use with later API calls
                    string uploadedFileUrl = json["url"].ToString();

                    // 2. UPLOAD THE FILE TO CLOUD.

                    webClient.Headers.Add("content-type", "application/octet-stream");
                    webClient.UploadFile(uploadUrl, "PUT", Global.Image_Path); // You can use UploadData() instead if your file is byte[] or Stream

                    // 3. READ BARCODES FROM UPLOADED FILE

                    // Prepare URL for `Barcode Reader` API call
                    query = Uri.EscapeUriString(string.Format("https://api.pdf.co/v1/barcode/read/from/url?types={0}&pages={1}&url={2}&async={3}",
                        BarcodeTypes,
                        Pages,
                        uploadedFileUrl,
                        Async));
                    //query = Uri.EscapeUriString(string.Format("https://api.pdf.co/v1/pdf/convert/to/text?x-api-key={0}&url={1}&inline=true",
                    //   BarcodeTypes,
                    //   Pages,
                    //   uploadedFileUrl,
                    //   Async));


                    try
                    {
                        // Execute request
                        response = webClient.DownloadString(query);

                        // Parse JSON response
                        json = JObject.Parse(response);

                        if (json["error"].ToObject<bool>() == false)
                        {
                            // Asynchronous job ID
                            string jobId = json["jobId"].ToString();
                            // URL of generated JSON file with decoded barcodes that will available after the job completion
                            string resultFileUrl = json["url"].ToString();

                            // Check the job status in a loop. 
                            // If you don't want to pause the main thread you can rework the code 
                            // to use a separate thread for the status checking and completion.
                            do
                            {
                                string status = CheckJobStatus(jobId); // Possible statuses: "working", "failed", "aborted", "success".

                                // Display timestamp and status (for demo purposes)
                             

                                if (status == "success")
                                {
                                    // Download JSON results file as string
                                    string jsonFileString = webClient.DownloadString(resultFileUrl);

                                    JArray jsonFoundBarcodes = JArray.Parse(jsonFileString);

                                    // Display found barcodes in console
                                    foreach (JToken token in jsonFoundBarcodes)
                                    {
                                        AdharResult = token["Value"].ToString();
                                        flag = true;
                                        //Console.WriteLine("Found barcode:");
                                        //Console.WriteLine("  Type: " + token["TypeName"]);
                                        //Console.WriteLine("  Value: " + token["Value"]);
                                        //Console.WriteLine("  Document Page Index: " + token["Page"]);
                                        //Console.WriteLine("  Rectangle: " + token["Rect"]);
                                        //Console.WriteLine("  Confidence: " + token["Confidence"]);
                                        //Console.WriteLine();
                                    }
                                    break;
                                }
                                else if (status == "working")
                                {
                                    // Pause for a few seconds
                                    Thread.Sleep(3000);
                                }
                                else
                                {
                                   Console.WriteLine(status);
                                    break;
                                }
                            }
                            while (true);
                        }
                        else
                        {
                            Console.WriteLine(json["message"].ToString());
                        }
                    }
                    catch (WebException e)
                    {
                        flag = false;
                        Console.WriteLine(e.ToString());
                    }

                }
                else
                {
                    flag = false;
                    Console.WriteLine(json["message"].ToString());
                }
            }
            catch(WebException e)
            {
                flag = false;
                 Console.WriteLine(e.ToString());
            }

            webClient.Dispose();

            return flag;         
        }


        /// <summary>
        /// Check Job Status
        /// </summary>
        static string CheckJobStatus(string jobId)
        {
            using (WebClient webClient = new WebClient())
            {
                // Set API Key
                webClient.Headers.Add("x-api-key", API_KEY);

                string url = "https://api.pdf.co/v1/job/check?jobid=" + jobId;

                string response = webClient.DownloadString(url);
                JObject json = JObject.Parse(response);

                return Convert.ToString(json["status"]);
            }
        }
    }
}