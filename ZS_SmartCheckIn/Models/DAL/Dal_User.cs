
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
    public class Dal_User
    {
        SqlConnection con = null;
        DBConnection dbcon = new DBConnection();
        public Dal_User()
        {
            con = dbcon.DatabaseConnection;
        }


        public List<Ent_User> SelectLogin(Ent_User entu)
        {
            List<Ent_User> result = new List<Ent_User>();
            Ent_User ent = new Ent_User();
            try
            {
                string query = "select * from zs_users where User_Username = '" + entu.User_Username + "'" +
                               "and User_Password = '" + entu.User_Password + "' and Branch_ID = " + entu.Branch_ID + " and Is_Active!=0 ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_User();
                        ent.User_ID = Convert.ToInt32(dr["User_ID"]);
                        ent.User_Name = Convert.ToString(dr["User_Name"]);
                        ent.User_Type = Convert.ToString(dr["User_Type"]);
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        result.Add(ent);
                    }
                }
            }
            catch
            {
                ent.User_ID = -2;
                result.Add(ent);
            }
            finally { con.Close(); }
            return result;
        }

        public List<Ent_Guest> SelectGuestLogin(Ent_Guest entu)
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            Ent_Guest ent = new Ent_Guest();
            try
            {
                string query = "select * from zs_guests where Guest_Username = '" + entu.Guest_Username + "'" +
                               "and Guest_Password = '" + entu.Guest_Password + "' and Is_Active =1 ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Customer_Code = Convert.ToString(dr["Customer_Code"]);
                        ent.Is_Active = Convert.ToInt32(dr["Is_Active"]);
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.Booking_Portal = Convert.ToString(dr["Booking_Portal"]);
                        result.Add(ent);
                    }
                }
            }
            catch
            {
                ent.Guest_ID = -2;
                result.Add(ent);
            }
            finally { con.Close(); }
            return result;
        }

        public List<Ent_Guest> SelectGuestLoginMail(Ent_Guest entu)
        {
            List<Ent_Guest> result = new List<Ent_Guest>();
            Ent_Guest ent = new Ent_Guest();
            try
            {
                string query = "select Guest_ID,Customer_Code,Guest_Email,Guest_Firstname,Guest_Lastname from zs_guests where Guest_Username = '" + entu.Guest_Username + "'" +
                               " and Is_Active not in (0,4) ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.Guest_ID = Convert.ToInt32(dr["Guest_ID"]);
                        ent.Guest_Email = Convert.ToString(dr["Guest_Email"]);
                        ent.Customer_Code = Convert.ToString(dr["Customer_Code"]);                    
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);                    
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);                    
                        result.Add(ent);
                    }
                }
            }
            catch
            {
                ent.Guest_ID = -2;
                result.Add(ent);
            }
            finally { con.Close(); }
            return result;
        }

        public DataTable SelectOrganization()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from zs_organization where Is_Active=1;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch(Exception e)
            {

            }
            finally { con.Close(); }
            return dt;
        }

        public int UpdatePassword(Ent_Guest entu, SafeTransaction trans)
        {
            
            int result = 0;
            try
            {                
                using (SqlCommand cmd = new SqlCommand("zs_updatepassword", trans.DatabaseConnection, trans.Transaction))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Username", entu.Guest_Username));
                    cmd.Parameters.Add(new SqlParameter("@p_Guest_Password", entu.Guest_Password));
                    cmd.Parameters.Add(new SqlParameter("@p_UserToken", entu.UserToken));
                    try
                    {
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        if (result > 0)
                        {
                            cmd.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        result = 0;
                    }
                }
            }
            catch
            {
                result = 0;
            }
            finally { con.Close(); }
            return result;
        }

        public int UpdateToken(string username, string email, string token)
        {
            string query = "update  zs_guests set UserToken='" + token + "' where Guest_Username='" + username + "' and Guest_Email='" + email + "'";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }

        public int InsertLogActivity(Ent_Guest ent, int primaryid,string logaction)
        {
            int dataresult = 0;
            using (SqlCommand cmd = new SqlCommand("zs_insertlog", con))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.CommandType = CommandType.StoredProcedure;
                HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];            
                cmd.Parameters.Add(new SqlParameter("@p_branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Created_By));
                cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Created_Date));
                cmd.Parameters.Add(new SqlParameter("@p_primary_id", primaryid));
                cmd.Parameters.Add(new SqlParameter("@p_logaction", logaction));
                cmd.Parameters.Add(new SqlParameter("@p_customer_code", ent.Customer_Code));
                cmd.Parameters.Add(new SqlParameter("@p_guest_name", ""));
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

        public List<Ent_Guest> SelectLogActivity()
        {
            List<Ent_Guest> list = new List<Ent_Guest>();
            Ent_Guest ent = new Ent_Guest();    
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectLogActivity", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Guest();
                        ent.Created_Date = Convert.ToDateTime(dr["created_date"]);
                        ent.createduser = Convert.ToString(dr["createduser"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.logaction = Convert.ToString(dr["logaction"]);                       
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
    }
}