
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Models.DAL
{
    public class Dal_GuestData
    {
        SqlConnection con = null;
        DBConnection dbcon = new DBConnection();
        public Dal_GuestData()
        {
            con = dbcon.DatabaseConnection;
        }
        public int SaveGuestData(Ent_GuestData ent, SafeTransaction trans,bool IsEdit)
        {
            int dataresult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertGuestData", trans.DatabaseConnection, trans.Transaction))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_guestdata_id", ent.Guestdata_id));
                    cmd.Parameters.Add(new SqlParameter("@p_customer_code", ent.Customer_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_id", ent.Guest_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_code", ent.Guest_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_firstname", ent.Guest_Firstname));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_lastname", ent.Guest_Lastname));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_father", ent.Guest_Father));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_email", ent.Guest_Email));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_phoneno", ent.Guest_PhoneNo));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_gender", ent.Guest_Gender));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_nationality", ent.Guest_Nationality));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_dob", ent.Guest_DOB));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_address", ent.Guest_Address));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_country", ent.Guest_Country));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_state", ent.Guest_State));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_city", ent.Guest_City));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_purposeofvisit", ent.Guest_PurposeofVisit));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_cardtype", ent.Guest_CardType));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_documentno", ent.Guest_DocumentNo));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_CountryofIssue", ent.Guest_CountryofIssue));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_DateOfIssue", ent.Guest_DateOfIssue));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_ExpiryDate", ent.Guest_ExpiryDate));

                    cmd.Parameters.Add(new SqlParameter("@p_guest_adultcount", ent.Guest_AdultCount));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_childcount", ent.Guest_ChildCount));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_daycount", ent.Guest_DayCount));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_nightcount", ent.Guest_NightCount));
                    cmd.Parameters.Add(new SqlParameter("@p_is_groupleader", ent.Is_GroupLeader));
                    cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Created_Date));
                    cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Created_By));

                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaNo", ent.Guest_VisaNo));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaDateofIssue", ent.Guest_VisaDateofIssue));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaPOICity", ent.Guest_VisaPOICity));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaPOICountry", ent.Guest_VisaPOICountry));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaType", ent.Guest_VisaType));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaValidTill", ent.Guest_VisaValidTill));
                    try
                    {
                        dataresult = Convert.ToInt32(cmd.ExecuteScalar());
                        if (dataresult > 0)
                        {
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        dataresult = -2;
                    }
                }

                //if (dataresult > 0 && IsEdit == false)
                //{
                //    using (SqlCommand cmd1 = new SqlCommand("zs_updategueststatus", trans.DatabaseConnection, trans.Transaction))
                //    {

                //        cmd1.CommandType = CommandType.StoredProcedure;                      
                //        cmd1.Parameters.Add(new SqlParameter("@p_Customer_Code", ent.Customer_Code));                        
                //        cmd1.Parameters.Add(new SqlParameter("@p_flag", 2));                        
                //        try
                //        {
                //            dataresult1 = Convert.ToInt32(cmd1.ExecuteScalar());
                //            if (dataresult1 > 0)
                //            {
                //                cmd1.Dispose();
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            dataresult1 = -2;
                //        }
                //    }
                //}              
            }
            catch (Exception ex)
            {

            }
            finally { con.Close(); }
            
            return dataresult;
        }

        public int SaveGuestDocuments(Ent_GuestData ent,int id, SafeTransaction trans)
        {
            int dataresult = 0;
            using (SqlCommand cmd = new SqlCommand("ZS_InsertGuestDocument", trans.DatabaseConnection, trans.Transaction))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", ent.Customer_Code));
                cmd.Parameters.Add(new SqlParameter("@p_Guestdata_id", id));
                cmd.Parameters.Add(new SqlParameter("@p_Guest_Document", ent.Guest_Document));
                cmd.Parameters.Add(new SqlParameter("@p_Guest_DocumentBack", ent.Guest_DocumentBack));
                cmd.Parameters.Add(new SqlParameter("@p_Guest_DocumentVisa", ent.Guest_DocumentVisa));
                cmd.Parameters.Add(new SqlParameter("@p_Guest_Code", ent.Guest_Code));
                cmd.Parameters.Add(new SqlParameter("@p_Created_Date", ent.Created_Date));

                try
                {
                    dataresult = Convert.ToInt32(cmd.ExecuteScalar());
                    if (dataresult > 0)
                    {
                        cmd.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    dataresult = -2;
                }
                finally { con.Close(); }
            }
            return dataresult;
        }

        public List<Ent_GuestData> SelectGuestsList(string Customer_Code)
        {
            List<Ent_GuestData> result = new List<Ent_GuestData>();
            Ent_GuestData ent = new Ent_GuestData();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuestsData", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", Customer_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Code", ""));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_GuestData();
                        ent.guestdoc_id = Convert.ToInt32(dr["guestdoc_id"]);
                        ent.Guestdata_id = Convert.ToInt32(dr["Guestdata_id"]);
                        ent.Guest_Document = Convert.ToString(dr["Guest_Document"]);
                        ent.Guest_DocumentVisa = Convert.ToString(dr["Guest_DocumentVisa"]);
                        ent.Guest_ProfileImage = Convert.ToString(dr["Guest_ProfileImage"]);
                        ent.Guest_Code = Convert.ToString(dr["Guest_Code"]);                      
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        ent.Guest_PhoneNo = Convert.ToString(dr["Guest_PhoneNo"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        result.Add(ent);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
            return result;
        }

      

        public Ent_Guest SelectGuestData(string Guest_Code)
        {
            Ent_Guest ent = new Ent_Guest();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuestsData", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", ""));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Code", Guest_Code));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.entGuestData.Guestdata_id= Convert.ToInt32(dr["Guestdata_id"]);
                        ent.entGuestData.Guest_Code = Convert.ToString(dr["Guest_Code"]);
                        ent.entGuestData.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.entGuestData.Customer_Code = Convert.ToString(dr["Customer_Code"]);
                        ent.entGuestData.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.entGuestData.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.entGuestData.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.entGuestData.Guest_PhoneNo = Convert.ToString(dr["Guest_PhoneNo"]);
                        ent.entGuestData.Guest_Gender = Convert.ToString(dr["Guest_Gender"]);
                        ent.entGuestData.Guest_Father = Convert.ToString(dr["Guest_Father"]);
                        ent.entGuestData.Guest_Nationality = Convert.ToString(dr["Guest_Nationality"]);
                        ent.entGuestData.Guest_DOB = Convert.ToString(dr["Guest_DOB"]);
                        ent.entGuestData.Guest_Address = Convert.ToString(dr["Guest_Address"]);
                        ent.entGuestData.Guest_Country = Convert.ToString(dr["Guest_Country"]);
                        ent.entGuestData.Guest_State = Convert.ToString(dr["Guest_State"]);
                        ent.entGuestData.Guest_City = Convert.ToString(dr["Guest_City"]);
                        ent.entGuestData.Guest_PurposeofVisit = Convert.ToString(dr["Guest_PurposeofVisit"]);
                        ent.entGuestData.Guest_CardType = Convert.ToString(dr["Guest_CardType"]);
                        ent.entGuestData.Guest_DocumentNo = Convert.ToString(dr["Guest_DocumentNo"]);
                        ent.entGuestData.Guest_AdultCount = Convert.ToInt32(dr["Guest_AdultCount"]);
                        ent.entGuestData.Guest_ChildCount = Convert.ToInt32(dr["Guest_ChildCount"]);
                        ent.entGuestData.Guest_NightCount = Convert.ToInt32(dr["Guest_NightCount"]);
                        ent.entGuestData.Guest_DayCount = Convert.ToInt32(dr["Guest_DayCount"]);
                        ent.entGuestData.Guest_CountryofIssue = Convert.ToString(dr["Guest_CountryofIssue"]);
                        ent.entGuestData.Guest_DateOfIssue= Convert.ToString(dr["Guest_DateOfIssue"]);
                        ent.entGuestData.Guest_ExpiryDate= Convert.ToString(dr["Guest_ExpiryDate"]);
                        ent.entGuestData.Guest_VisaNo = Convert.ToString(dr["Guest_VisaNo"]);
                        ent.entGuestData.Guest_VisaDateofIssue = Convert.ToString(dr["Guest_VisaDateofIssue"]);
                        ent.entGuestData.Guest_VisaPOICity = Convert.ToString(dr["Guest_VisaPOICity"]);
                        ent.entGuestData.Guest_VisaPOICountry = Convert.ToString(dr["Guest_VisaPOICountry"]);
                        ent.entGuestData.Guest_VisaType = Convert.ToString(dr["Guest_VisaType"]);
                        ent.entGuestData.Guest_VisaValidTill = Convert.ToString(dr["Guest_VisaValidTill"]);
                        ent.entGuestData.Guest_Document = Convert.ToString(dr["Guest_Document"]);
                        ent.entGuestData.Guest_DocumentBack = Convert.ToString(dr["Guest_DocumentBack"]);
                        ent.entGuestData.Guest_DocumentVisa = Convert.ToString(dr["Guest_DocumentVisa"]);
                        ent.entGuestData.Guest_ProfileImage = Convert.ToString(dr["Guest_ProfileImage"]);
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
            return ent;
        }

        public int DeleteGuestData(Ent_GuestData ent, SafeTransaction trans)
        {
            int dataResult = 0;
            using (SqlCommand cmd = new SqlCommand("zs_deleteguestdata", trans.DatabaseConnection, trans.Transaction))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_Guest_Code", ent.Guest_Code));
                cmd.Parameters.Add(new SqlParameter("@p_Modified_By", ent.Modified_By));
                cmd.Parameters.Add(new SqlParameter("@p_Modified_Date", ent.Modified_Date));
                try
                {
                    dataResult = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception e)
                {
                    dataResult = -1;
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }
            }
            return dataResult;
        }

        public int SaveWalkingGuestData(Ent_GuestData ent, SafeTransaction trans, bool IsEdit)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertWalkingGuest", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@p_guestdata_id", ent.Guestdata_id));
                    cmd.Parameters.Add(new SqlParameter("@p_customer_code", ent.Customer_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_id", ent.Guest_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_code", ent.Guest_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_firstname", ent.Guest_Firstname));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_lastname", ent.Guest_Lastname));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_father", ent.Guest_Father));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_email", ent.Guest_Email));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_phoneno", ent.Guest_PhoneNo));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_gender", ent.Guest_Gender));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_nationality", ent.Guest_Nationality));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_dob", ent.Guest_DOB));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_address", ent.Guest_Address));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_country", ent.Guest_Country));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_state", ent.Guest_State));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_city", ent.Guest_City));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_purposeofvisit", ent.Guest_PurposeofVisit));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_cardtype", ent.Guest_CardType));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_documentno", ent.Guest_DocumentNo));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_adultcount", ent.Guest_AdultCount));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_childcount", ent.Guest_ChildCount));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_daycount", ent.Guest_DayCount));
                    cmd.Parameters.Add(new SqlParameter("@p_guest_nightcount", ent.Guest_NightCount));
                    cmd.Parameters.Add(new SqlParameter("@p_is_groupleader", ent.Is_GroupLeader));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_CountryofIssue", ent.Guest_CountryofIssue));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_DateOfIssue", ent.Guest_DateOfIssue));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_ExpiryDate", ent.Guest_ExpiryDate));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaNo", ent.Guest_VisaNo));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaDateofIssue", ent.Guest_VisaDateofIssue));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaPOICity", ent.Guest_VisaPOICity));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaPOICountry", ent.Guest_VisaPOICountry));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaType", ent.Guest_VisaType));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_VisaValidTill", ent.Guest_VisaValidTill));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Accompany", ent.Guest_Accompany));
                    cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Created_Date));
                    cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Created_By));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", ent.Branch_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Arrival_Time", ent.Arrival_Time));

                 
                    cmd.Parameters.Add("@Result", SqlDbType.Int);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        dataresult = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                        if (dataresult > 0)
                        {
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        dataresult = -2;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

            return dataresult;
        }

        #region FRRO
        //Added by Arun
        public List<Ent_GuestData> SelectGuestsListChromeExt(int Guestdata_id)
        {
            List<Ent_GuestData> result = new List<Ent_GuestData>();
            Ent_GuestData ent = new Ent_GuestData();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuestsDataForChromeExt", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Guestdata_id", Guestdata_id));
                    //cmd.Parameters.Add(new SqlParameter("@p_Guest_Code", ""));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_GuestData();
                        //ent.guestdoc_id = Convert.ToInt32(dr["guestdoc_id"]);
                        //ent.Guest_Document = Convert.ToString(dr["Guest_Document"]);
                        ent.Guestdata_id = Convert.ToInt32(dr["Guestdata_id"]);
                        ent.Guest_Code = Convert.ToString(dr["Guest_Code"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        ent.Guest_PhoneNo = Convert.ToString(dr["Guest_PhoneNo"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.Guest_Gender = Convert.ToString(dr["Guest_Gender"]);
                        ent.Guest_DOB = Convert.ToString(dr["Guest_DOB"]);
                        ent.Guest_Nationality = Convert.ToString(dr["Guest_Nationality"]);
                        ent.Guest_Address = Convert.ToString(dr["Guest_Address"]);
                        ent.Guest_City = Convert.ToString(dr["Guest_City"]);
                        ent.Guest_Country = Convert.ToString(dr["Guest_Country"]);
                        ent.Guest_VisaNo = Convert.ToString(dr["Guest_VisaNo"]);
                        ent.Guest_VisaPOICity = Convert.ToString(dr["Guest_VisaPOICity"]);
                        ent.Guest_VisaPOICountry = Convert.ToString(dr["Guest_VisaPOICountry"]);
                        ent.Guest_VisaDateofIssue = Convert.ToString(dr["Guest_VisaDateofIssue"]);
                        ent.Guest_VisaValidTill = Convert.ToString(dr["Guest_VisaValidTill"]);
                        ent.Guest_VisaType = Convert.ToString(dr["Guest_VisaType"]);
                        ent.Arrival_Time = Convert.ToString(dr["ArrivalTime"]);
                        ent.Arrival_Date = Convert.ToString(dr["ArrivalDt"]);
                        ent.Guest_PurposeofVisit = Convert.ToString(dr["Guest_PurposeofVisit"]);
                        ent.Guest_DocumentNo = Convert.ToString(dr["Guest_DocumentNo"]);
                        ent.Guest_CountryofIssue = Convert.ToString(dr["Guest_CountryofIssue"]);
                        ent.Guest_DateOfIssue = Convert.ToString(dr["Guest_DateOfIssue"]);
                        ent.Guest_ExpiryDate = Convert.ToString(dr["Guest_ExpiryDate"]);
                        ent.Guest_ProfilePic = ImageToByteArray(Convert.ToString(dr["Guest_ProfileImage"]));
                        result.Add(ent);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public byte[] ImageToByteArray(string ProImg)
        {
            try
            {
                byte[] imgdata = null;
                //string path = HttpContext.Current.Server.MapPath("~/CardImages/IDCard-2.jpg");
                //byte[] imgdata = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/CardImages/IDCard-4.jpg"));
                if (!string.IsNullOrEmpty(ProImg))
                {
                    imgdata = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/ProfileImages/" + ProImg));
                }
                return imgdata;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public int UpdateFRROStatus(Ent_GuestData ent)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_UpdateFRROStatusForChromeExt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Guestdata_id", ent.Guestdata_id));
                    cmd.Parameters.Add(new SqlParameter("@guest_PassToFRRO", ent.Guest_PassToFRRO));
                    cmd.Parameters.Add(new SqlParameter("@guest_FrroChellan", ent.guest_FrroChellan));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", ent.Guest_FrroEntryUser));
                    cmd.Parameters.Add("@Result", SqlDbType.Int);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        dataresult = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                        if (dataresult > 0)
                        {
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        dataresult = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

            return dataresult;
        }

        public int UpdateFRROCheckOutStatus(Ent_GuestData ent)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_UpdateFRROCheckOutStatusChrome", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Guestdata_id", ent.Guestdata_id));
                    cmd.Parameters.Add(new SqlParameter("@guest_PassToFRRO", ent.Guest_PassToFRRO));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", ent.Guest_FrroCheckOutUser));
                    cmd.Parameters.Add("@Result", SqlDbType.Int);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        dataresult = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                        if (dataresult > 0)
                        {
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        dataresult = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

            return dataresult;
        }

        public List<Ent_GuestData> SelectGuestsListChromeExt(int Guestdata_id, int branch_ID)
        {
            List<Ent_GuestData> result = new List<Ent_GuestData>();
            Ent_GuestData ent = new Ent_GuestData();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuestsDataForChromeExt", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Guestdata_id", Guestdata_id));
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", branch_ID));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_GuestData();
                        //ent.guestdoc_id = Convert.ToInt32(dr["guestdoc_id"]);
                        //ent.Guest_Document = Convert.ToString(dr["Guest_Document"]);
                        ent.Guestdata_id = Convert.ToInt32(dr["Guestdata_id"]);
                        ent.Guest_Code = Convert.ToString(dr["Guest_Code"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        ent.Guest_PhoneNo = Convert.ToString(dr["Guest_PhoneNo"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.Guest_Gender = Convert.ToString(dr["Guest_Gender"]);
                        ent.Guest_DOB = Convert.ToString(dr["Guest_DOB"]);
                        ent.Guest_Nationality = Convert.ToString(dr["Guest_Nationality"]);
                        ent.Guest_Address = Convert.ToString(dr["Guest_Address"]);
                        ent.Guest_City = Convert.ToString(dr["Guest_City"]);
                        ent.Guest_Country = Convert.ToString(dr["Guest_Country"]);
                        ent.Guest_VisaNo = Convert.ToString(dr["Guest_VisaNo"]);
                        ent.Guest_VisaPOICity = Convert.ToString(dr["Guest_VisaPOICity"]);
                        ent.Guest_VisaPOICountry = Convert.ToString(dr["Guest_VisaPOICountry"]);
                        ent.Guest_VisaDateofIssue = Convert.ToString(dr["Guest_VisaDateofIssue"]);
                        ent.Guest_VisaValidTill = Convert.ToString(dr["Guest_VisaValidTill"]);
                        ent.Guest_VisaType = Convert.ToString(dr["Guest_VisaType"]);
                        ent.Arrival_Time = Convert.ToString(dr["ArrivalTime"]);
                        ent.Arrival_Date = Convert.ToString(dr["ArrivalDt"]);
                        ent.Guest_PurposeofVisit = Convert.ToString(dr["Guest_PurposeofVisit"]);
                        ent.Guest_DocumentNo = Convert.ToString(dr["Guest_DocumentNo"]);
                        ent.Guest_CountryofIssue = Convert.ToString(dr["Guest_CountryofIssue"]);
                        ent.Guest_DateOfIssue = Convert.ToString(dr["Guest_DateOfIssue"]);
                        ent.Guest_ExpiryDate = Convert.ToString(dr["Guest_ExpiryDate"]);
                        ent.Guest_PassToFRRO = Convert.ToInt32(dr["Guest_PassToFRRO"]);
                        ent.guest_FrroChellan = Convert.ToString(dr["guest_FrroChellan"]);
                        ent.Guest_FrroCheckOutStatus = Convert.ToInt32(dr["Guest_FrroCheckOutStatus"]);
                        ent.Guest_ProfilePic = ImageToByteArray(Convert.ToString(dr["Guest_ProfileImage"]));

                        ent.Organization.Organization_Address = Convert.ToString(dr["Organization_Address"]);
                        ent.Organization.Organization_State = Convert.ToString(dr["Organization_State"]);
                        ent.Organization.Organization_Country = Convert.ToString(dr["Organization_Country"]);
                        ent.Organization.Organization_PinCode = Convert.ToString(dr["Organization_PinCode"]);

                        result.Add(ent);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public int UpdateFRROCheckOutFlag(Ent_GuestData ent)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_UpdateFRROCheckOutFlagChrome", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Guestdata_id", ent.Guestdata_id));
                    cmd.Parameters.Add(new SqlParameter("@Guest_FrroCheckOutStatus", ent.Guest_FrroCheckOutStatus));
                    cmd.Parameters.Add("@Result", SqlDbType.Int);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        dataresult = Convert.ToInt32(cmd.Parameters["@Result"].Value);
                        if (dataresult > 0)
                        {
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        dataresult = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

            return dataresult;
        }
        #endregion
    }
}