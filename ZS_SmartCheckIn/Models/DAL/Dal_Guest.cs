
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Models.DAL
{
    public class Dal_Guest
    {
        SqlConnection con = null;
        DBConnection dbcon = new DBConnection();
        public Dal_Guest()
        {
            con = dbcon.DatabaseConnection;
        }
        public int SaveNewGuest(Ent_Guest ent, SafeTransaction trans,string mode)
        {
            int dataresult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertGuest", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_ID", ent.Guest_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", ent.Customer_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Firstname", ent.Guest_Firstname));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Lastname", ent.Guest_Lastname));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Username", ent.Guest_Username));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Password", ent.Guest_Password));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Email", ent.Guest_Email));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_PhoneNo", ent.Guest_PhoneNo));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_MobileNo", ent.Guest_MobileNo));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Accompany", ent.Guest_Accompany));
                    cmd.Parameters.Add(new SqlParameter("@p_Booking_Portal", ent.Booking_Portal));
                    cmd.Parameters.Add(new SqlParameter("@p_FrequentVisitor", ent.FrequentVisitor));
                    cmd.Parameters.Add(new SqlParameter("@p_Arrival_Date", ent.Arrival_Date));
                    cmd.Parameters.Add(new SqlParameter("@p_Arrival_Time", ent.Arrival_Time));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", ent.Branch_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Created_Date", ent.Created_Date));
                    cmd.Parameters.Add(new SqlParameter("@p_Created_By", ent.Created_By));
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
                        string exception = "insert into ZS_ExceptionLog(Exception,ExceptionFrom,ExceptionID,CreatedDate)values('" + ex.Message + "','SaveNewGuest','" + ent.Guest_ID + "','"+DateTime.Now+"')";
                        using (SqlCommand cmd1 = new SqlCommand(exception,con))
                        {
                            cmd1.ExecuteNonQuery();
                            cmd1.Dispose();
                        }
                    }                  
                }

                if (dataresult > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("zs_insertlog", trans.DatabaseConnection, trans.Transaction))
                    {                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                        cmd.Parameters.Add(new SqlParameter("@p_branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                        cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Created_By));
                        cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Created_Date));
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", dataresult));
                        cmd.Parameters.Add(new SqlParameter("@p_logaction", mode));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ent.Customer_Code));
                        cmd.Parameters.Add(new SqlParameter("@p_guest_name", ent.Guest_Firstname));
                        try
                        {
                            dataresult1 = Convert.ToInt32(cmd.ExecuteScalar());
                            if (dataresult1 > 0)
                            {
                                cmd.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            dataresult1 = -2;
                        }                      
                    }
                }
            }
            catch (Exception ex)
            {
                dataresult = -2;
            }
            finally { con.Close(); }
            return dataresult;
        }

        public int UpdateProfile(Ent_GuestData ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_UpdateProfile", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Code", ent.Guest_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_ProfileImage", ent.Guest_ProfileImage));
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
                        string exception = "insert into ZS_ExceptionLog(Exception,ExceptionFrom,ExceptionID,CreatedDate)values('" + ex.Message + "','UpdateProfile','" + ent.Guest_Code + "','"+DateTime.Now+"')";
                        using (SqlCommand cmd1 = new SqlCommand(exception, con))
                        {
                            cmd1.ExecuteNonQuery();
                            cmd1.Dispose();
                        }
                    }
                }        
            }
            catch (Exception ex)
            {
                dataresult = -2;
                string exception = "insert into ZS_ExceptionLog(Exception,ExceptionFrom,ExceptionID,CreatedDate)values('" + ex.Message + "','UpdateProfile','" + ent.Guest_Code + "','" + DateTime.Now + "')";
                using (SqlCommand cmd1 = new SqlCommand(exception, con))
                {
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();
                }
            }
            finally { con.Close(); }
            return dataresult;
        }

        //SELECT Guest Data by code
        public Ent_Guest SelectGuest(string Customer_Code)
        {
            Ent_Guest ent = new Ent_Guest();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuest", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", Customer_Code));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.Customer_Code = Convert.ToString(dr["Customer_Code"]);
                        ent.Guest_Firstname= Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Guest_MobileNo= Convert.ToString(dr["Guest_MobileNo"]);
                        ent.Guest_PhoneNo= Convert.ToString(dr["Guest_PhoneNo"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.Guest_Accompany = Convert.ToInt32(dr["Guest_Accompany"]);                      
                        ent.Guest_Username = Convert.ToString(dr["Guest_Username"]);                      
                        ent.Guest_Password = Convert.ToString(dr["Guest_Password"]);                      
                        ent.Booking_Portal = Convert.ToString(dr["Booking_Portal"]);                      
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);                      
                        ent.Arrival_Date = Convert.ToDateTime(dr["Arrival_Date"]);                      
                        ent.Arrival_Time = Convert.ToString(dr["Arrival_Time"]);                      
                        ent.Confirmation_Code = Convert.ToString(dr["Confirmation_Code"]);                      
                        ent.IsFrequent = Convert.ToInt32(dr["IsFrequent"]);                      
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

        public Ent_Guest SelectGroupLeader(string Customer_Code)
        {
            Ent_Guest ent = new Ent_Guest();
            try
            {
                using (SqlCommand cmd = new SqlCommand("zs_selectgroupleader", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", Customer_Code));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.Customer_Code = Convert.ToString(dr["Customer_Code"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        ent.IsWalking = Convert.ToInt32(dr["IsWalking"]);                   
                        ent.Guest_Accompany = Convert.ToInt32(dr["Guest_Accompany"]);
                        ent.Confirmation_Code = Convert.ToString(dr["Confirmation_Code"]);
                        ent.entGuestData.Guestdata_id = Convert.ToInt32(dr["Guestdata_id"]);
                        ent.entGuestData.Guest_Code = Convert.ToString(dr["Guest_Code"]);
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
                        ent.entGuestData.Guest_AdultCount = Convert.ToInt32(dr["Guest_AdultCount"]);
                        ent.entGuestData.Guest_ChildCount = Convert.ToInt32(dr["Guest_ChildCount"]);
                        ent.entGuestData.Guest_NightCount = Convert.ToInt32(dr["Guest_NightCount"]);
                        ent.entGuestData.Guest_DayCount = Convert.ToInt32(dr["Guest_DayCount"]);
                        ent.entGuestData.Guest_CountryofIssue = Convert.ToString(dr["Guest_CountryofIssue"]);
                        ent.entGuestData.Guest_DateOfIssue = Convert.ToString(dr["Guest_DateOfIssue"]);
                        ent.entGuestData.Guest_ExpiryDate = Convert.ToString(dr["Guest_ExpiryDate"]);
                        ent.entGuestData.Guest_Document = Convert.ToString(dr["Guest_Document"]);
                        ent.entGuestData.Guest_DocumentBack = Convert.ToString(dr["Guest_DocumentBack"]);
                        ent.entGuestData.Guest_DocumentVisa= Convert.ToString(dr["Guest_DocumentVisa"]);
                        ent.entGuestData.Guest_ProfileImage= Convert.ToString(dr["Guest_ProfileImage"]);
                        ent.entGuestData.Guest_VisaNo= Convert.ToString(dr["Guest_VisaNo"]);
                        ent.entGuestData.Guest_VisaDateofIssue= Convert.ToString(dr["Guest_VisaDateofIssue"]);
                        ent.entGuestData.Guest_VisaPOICity= Convert.ToString(dr["Guest_VisaPOICity"]);
                        ent.entGuestData.Guest_VisaPOICountry= Convert.ToString(dr["Guest_VisaPOICountry"]);
                        ent.entGuestData.Guest_VisaType= Convert.ToString(dr["Guest_VisaType"]);
                        ent.entGuestData.Guest_VisaValidTill= Convert.ToString(dr["Guest_VisaValidTill"]);
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


        public Ent_Guest SelectGuestSearch(string Guest_Email)
        {
            Ent_Guest ent = new Ent_Guest();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuestSearch", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Guest_Email", Guest_Email));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();                     
                        ent.Guest_Username = Convert.ToString(dr["Guest_Username"]);                     
                        ent.Guest_Password = Convert.ToString(dr["Guest_Password"]);                     
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);                     
                        ent.Guest_Lastname= Convert.ToString(dr["Guest_Lastname"]);                     
                        ent.Guest_PhoneNo = Convert.ToString(dr["Guest_PhoneNo"]);                     
                        ent.Guest_MobileNo = Convert.ToString(dr["Guest_MobileNo"]);                                                 
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

        public List<Ent_Guest> SelectGuestList()
        {
            List<Ent_Guest> list = new List<Ent_Guest>();
            Ent_Guest ent = new Ent_Guest();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuest", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code",""));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.Customer_Code = Convert.ToString(dr["Customer_Code"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Guest_MobileNo = Convert.ToString(dr["Guest_MobileNo"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.Guest_Accompany = Convert.ToInt32(dr["Guest_Accompany"]);
                        ent.Arrival_Date = Convert.ToDateTime(dr["Arrival_Date"]);
                        ent.Checkin_Date = Convert.ToDateTime(dr["Checkin_Date"]);                        
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        ent.entGuestData.Guest_ProfileImage = Convert.ToString(dr["Guest_ProfileImage"]);
                        ent.IsWalking = Convert.ToInt32(dr["IsWalking"]);
                        if (ent.Is_Active == 4)
                        {
                            ent.Arrival_Time = Convert.ToDateTime(dr["Checkin_Date"]).ToString("HH:mm");
                        }
                        else
                        { ent.Arrival_Time = Convert.ToString(dr["Arrival_Time"]); }
                            list.Add(ent);
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
            return list;
        }

        public List<Ent_Guest> GuestReportSearch(DateTime FromDate, DateTime ToDate, int flag)
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            Ent_Guest ent = new Ent_Guest();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuestReport", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", FromDate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", ToDate));
                    cmd.Parameters.Add(new SqlParameter("@flag", flag));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.Customer_Code = Convert.ToString(dr["Customer_Code"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Guest_MobileNo = Convert.ToString(dr["Guest_MobileNo"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.Guest_Accompany = Convert.ToInt32(dr["Guest_Accompany"]);
                        ent.Arrival_Date = Convert.ToDateTime(dr["Arrival_Date"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
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


        public int DeleteGuest(Ent_Guest ent, SafeTransaction trans)
        {
            int dataResult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("zs_deleteguest", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", ent.Customer_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_Modified_By", ent.Modified_By));
                    cmd.Parameters.Add(new SqlParameter("@p_Modified_Date", ent.Modified_Date));
                    try
                    {
                        dataResult = Convert.ToInt32(cmd.ExecuteScalar());
                        if (dataResult > 0)
                        {
                            cmd.Dispose();
                        }
                    }
                    catch (Exception e)
                    {
                        dataResult = -1;
                    }                   
                }

                if (dataResult > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("zs_insertlog", trans.DatabaseConnection, trans.Transaction))
                    {                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                        cmd.Parameters.Add(new SqlParameter("@p_branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                        cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Created_By));
                        cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Created_Date));
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", dataResult));
                        cmd.Parameters.Add(new SqlParameter("@p_logaction", "Delete Guest"));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ent.Customer_Code));
                        cmd.Parameters.Add(new SqlParameter("@p_guest_name", ""));
                        try
                        {
                            dataresult1 = Convert.ToInt32(cmd.ExecuteScalar());
                            if (dataresult1 > 0)
                            {
                                cmd.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            dataresult1 = -2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dataResult = -1; 
            }
            finally
            {
                con.Close();
            }
            return dataResult;
        }

        public int UpdateActiveStatus(Ent_Guest ent, SafeTransaction trans)
        {
            int dataResult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd1 = new SqlCommand("zs_updategueststatus", trans.DatabaseConnection, trans.Transaction))
                {

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@p_Customer_Code", ent.Customer_Code));
                    cmd1.Parameters.Add(new SqlParameter("@p_flag", ent.Is_Active));
                    cmd1.Parameters.Add(new SqlParameter("@p_Notification_Status", ent.Notification_Status));
                    cmd1.Parameters.Add(new SqlParameter("@p_Confirmation_Code", ent.Confirmation_Code));
                    cmd1.Parameters.Add(new SqlParameter("@p_Modified_Date", ent.Modified_Date));
                    cmd1.Parameters.Add(new SqlParameter("@p_Modified_By", ent.Modified_By));
                    try
                    {
                        dataResult = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (dataResult > 0)
                        {
                            dataResult = 1;
                            cmd1.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        dataResult = -2;
                    }
                }

                if (dataResult > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("zs_insertlog", trans.DatabaseConnection, trans.Transaction))
                    {                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                        cmd.Parameters.Add(new SqlParameter("@p_branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                        cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Modified_By));
                        cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Modified_Date));
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", dataResult));
                        if (ent.Is_Active == 2)
                        {
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Id Uploaded"));
                        }
                        else if (ent.Is_Active == 3)
                        {
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Id Verified"));
                        }
                        else if (ent.Is_Active == 4)
                        {
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "CheckIn"));
                        }

                      
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ent.Customer_Code));
                        cmd.Parameters.Add(new SqlParameter("@p_guest_name", ""));                       
                        try
                        {
                            dataresult1 = Convert.ToInt32(cmd.ExecuteScalar());
                            if (dataresult1 > 0)
                            {
                                cmd.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            dataresult1 = -2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dataResult = -2;
            }
            finally
            { con.Close(); }
            return dataResult;
        }

        public List<Ent_Guest> SelectGuestHistory(string User_Name)
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            Ent_Guest ent = new Ent_Guest();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectGuestHistory", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Username", User_Name));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();                     
                        ent.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.Customer_Code = Convert.ToString(dr["Customer_Code"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Guest_MobileNo = Convert.ToString(dr["Guest_MobileNo"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.Guest_Accompany = Convert.ToInt32(dr["Guest_Accompany"]);
                        ent.Arrival_Date = Convert.ToDateTime(dr["Arrival_Date"]);
                        ent.Checkin_Date = Convert.ToDateTime(dr["Checkin_Date"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);                     
                        if (ent.Is_Active == 4)
                        {
                            ent.Arrival_Time = Convert.ToDateTime(dr["Checkin_Date"]).ToString("HH:mm");
                        }
                        else
                        { ent.Arrival_Time = Convert.ToString(dr["Arrival_Time"]); }
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

        public int GetNotification()
        {
            int count=0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                string query = "select count(*) as count from zs_guests where Notification_Status=1 and Is_Active=2 and Branch_ID=" + Convert.ToInt32(BranchID.Value.Split('=')[1]);
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        count = Convert.ToInt32(dr["count"]);
                    }
                }
            }
            catch (Exception)
            {
                count = 0;
            }
            finally { con.Close(); }
            return count;
        }


        public int UpdateNotification()
        {
            int dataResult = 0;          
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];                       
                string query = "update zs_guests set Notification_Status=0 where Branch_ID=" + Convert.ToInt32(BranchID.Value.Split('=')[1]);
                using (SqlCommand cmd = new SqlCommand(query, con))
                {                                
                    try
                    {
                        dataResult= cmd.ExecuteNonQuery();                        
                        if (dataResult > 0)
                        {                           
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        dataResult =0;
                    }
                }                
            }
            catch (Exception ex)
            {
                dataResult = 0;
            }
            finally
            { con.Close(); }
            return dataResult;
        }

        public int SaveGuestSignature(Ent_Guest ent, SafeTransaction trans)
        {
            int dataresult = 0;
           int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertSignature", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    cmd.Parameters.Add(new SqlParameter("@p_Customer_Code", ent.Customer_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Signature", ent.Guest_Signature));                
                    cmd.Parameters.Add(new SqlParameter("@p_Created_Date", ent.Created_Date));
                    cmd.Parameters.Add(new SqlParameter("@p_Created_By", ent.Created_By));
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

                if (dataresult > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("zs_insertlog", trans.DatabaseConnection, trans.Transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                        cmd.Parameters.Add(new SqlParameter("@p_branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                        cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Created_By));
                        cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Created_Date));
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", dataresult));
                        cmd.Parameters.Add(new SqlParameter("@p_logaction", "Save Signature"));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ent.Customer_Code));
                        cmd.Parameters.Add(new SqlParameter("@p_guest_name", ent.Guest_Firstname));
                        try
                        {
                            dataresult1 = Convert.ToInt32(cmd.ExecuteScalar());
                            if (dataresult1 > 0)
                            {
                                cmd.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            dataresult1 = -2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dataresult = -2;
            }
            finally { con.Close(); }
            return dataresult;
        }

        public DataTable SelectYesterdayCount()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectYesterdayCount", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception e)
            {

            }
            finally { con.Close(); }
            return dt;
        }
    }
}