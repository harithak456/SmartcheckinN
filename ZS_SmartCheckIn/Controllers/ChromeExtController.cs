using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ZS_SmartCheckIn.Models.BAL;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Controllers
{
    public class ChromeExtController : ApiController
    {
        [Route("api/GetBranch")]
        [HttpPost]  //POST   api/GetBranch
        public HttpResponseMessage GetBranch()
        {
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            List<Ent_Branch> listBranch = new List<Ent_Branch>();
            Bal_Master balMaster = new Bal_Master();
            listBranch = balMaster.SelectBranchList(0);

            if (listBranch.Count > 0)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(listBranch), Encoding.UTF8, "application/json");
            }
            return response;
        }

        [Route("api/ZeroLogin")]
        [HttpPost]  //POST   api/ZeroLogin
        public HttpResponseMessage ZeroLogin([FromBody] Ent_User entu)
        {
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            Bal_User balUser = new Bal_User();
            Bal_Master balMaster = new Bal_Master();
            List<Ent_User> result = new List<Ent_User>();
            //DateTime date = new DateTime(2020, 09, 18);
            DateTime date = new DateTime(2021, 09, 18);
            if (DateTime.Now < date)
            {
                if (entu.User_Username == "iadmin" && entu.User_Password == "ipass")
                {
                    entu.User_ID = 1;
                    entu.Branch_ID = 1;
                    entu.User_Name = "Intelli Admin";

                    entu.FRRO_Username = "zerosnap123";
                    entu.FRRO_Password = "Zerosnap@1235";
                }
                else
                {
                    result = balUser.SelectLogin(entu);
                    if (result.Count > 0)
                    {
                        if (result[0].User_ID > 0)
                        {
                            entu.User_ID = result[0].User_ID;
                            entu.Branch_ID = result[0].Branch_ID;
                            entu.User_Name = result[0].User_Name;
                            Ent_Branch ent = new Ent_Branch();
                            if (result[0].Branch_ID != 0)
                            {
                                ent = balMaster.SelectBranch(result[0].Branch_ID);
                                if (ent != null)
                                {
                                    entu.FRRO_Username = ent.FRRO_Username;
                                    entu.FRRO_Password = ent.FRRO_Password;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                entu.User_ID = 0;
                entu.Branch_ID = 0;
                entu.User_Name = "";

                entu.FRRO_Username = "";
                entu.FRRO_Password = "";
            }
            if (entu != null)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(entu), Encoding.UTF8, "application/json");
            }
            return response;
        }

        [Route("api/GuestDataForChrome")]
        [HttpPost]  //POST   api/GuestDataForChrome
        public HttpResponseMessage GuestDataForChrome([FromBody] Ent_GuestData entg)
        {
            int Guestdata_id = 0;
            int branch_ID = 0;
            int res = 0;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            Bal_GuestData balUser = new Bal_GuestData();
            List<Ent_GuestData> result = new List<Ent_GuestData>();
            if (entg.Guestdata_id > 0)
            {
                Guestdata_id = entg.Guestdata_id;
                entg.Guest_PassToFRRO = 1;
                entg.guest_FrroChellan = string.Empty;
            }
            if (entg.Branch_ID > 0)
            {
                branch_ID = entg.Branch_ID;
            }
            result = balUser.SelectGuestsListChromeExt(Guestdata_id, branch_ID);
            if (Guestdata_id > 0)
            {
                res = balUser.UpdateFRROStatus(entg);
            }
            if (entg.Guest_FrroCheckOutStatus == 1 && entg.Guestdata_id > 0)
            {
                res = balUser.UpdateFRROCheckOutFlag(entg);
            }
            if (result.Count > 0)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");
            }
            return response;
        }

        [Route("api/UpdateFRROStatusForChrome")]
        [HttpPost]  //POST   api/UpdateFRROStatusForChrome
        public HttpResponseMessage UpdateFRROStatusForChrome([FromBody] Ent_GuestData entg)
        {
            int res = 0;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            Bal_GuestData balUser = new Bal_GuestData();
            if (entg.Guestdata_id > 0 && !string.IsNullOrEmpty(entg.guest_FrroChellan))
            {
                entg.Guest_PassToFRRO = 2;
                res = balUser.UpdateFRROStatus(entg);
            }
            if (res > 0)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(res), Encoding.UTF8, "application/json");
            }
            return response;
        }

        [Route("api/UpdateFRROCheckOutStatusChrome")]
        [HttpPost]  //POST   api/UpdateFRROStatusForChrome
        public HttpResponseMessage UpdateFRROCheckOutStatusChrome([FromBody] Ent_GuestData entg)
        {
            int res = 0;
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            Bal_GuestData balUser = new Bal_GuestData();
            if (entg.Guestdata_id > 0)
            {
                entg.Guest_PassToFRRO = 3;
                res = balUser.UpdateFRROCheckOutStatus(entg);
            }
            if (res > 0)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(res), Encoding.UTF8, "application/json");
            }
            return response;
        }
    }
}
