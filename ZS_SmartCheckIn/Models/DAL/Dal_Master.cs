
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using ZS_SmartCheckIn.Models.Common;
using ZS_SmartCheckIn.Models.Entity;

namespace ZS_SmartCheckIn.Models.DAL
{
    public class Dal_Master
    {
        SqlConnection con = null;
        DBConnection dbcon = new DBConnection();
        public Dal_Master()
        {
            con = dbcon.DatabaseConnection;
        }

        #region Organization
        public int SaveOrganization(Ent_Organization ent, SafeTransaction trans)
        {
            int dataresult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertOrganization", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_ID", ent.Organization_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_Name", ent.Organization_Name));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_Address", ent.Organization_Address));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_ContactPerson", ent.Organization_ContactPerson));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_City", ent.Organization_City));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_State", ent.Organization_State));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_PinCode", ent.Organization_PinCode));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_Country", ent.Organization_Country));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_Phone", ent.Organization_Phone));
                    cmd.Parameters.Add(new SqlParameter("@p_organization_email", ent.organization_email));
                    cmd.Parameters.Add(new SqlParameter("@p_organization_web", ent.organization_web));
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
                        cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Created_By));
                        cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Created_Date));
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", dataresult));
                        if (ent.Organization_ID>0)
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Update Organization"));
                        else
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Insert Organization"));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ""));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally { con.Close(); }                                       
            return dataresult;
        }

        public Ent_Organization SelectOrganization()
        {
            Ent_Organization ent = new Ent_Organization();
            try
            {
                string query = "select * from zs_organization where Is_Active=1;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Organization();
                        ent.Organization_ID = Convert.ToInt32(dr["Organization_ID"]);
                        ent.Organization_Name = Convert.ToString(dr["Organization_Name"]);
                        ent.Organization_Address = Convert.ToString(dr["Organization_Address"]);
                        ent.Organization_ContactPerson = Convert.ToString(dr["Organization_ContactPerson"]);
                        ent.Organization_Phone = Convert.ToString(dr["Organization_Phone"]);
                        ent.Organization_City = Convert.ToString(dr["Organization_City"]);
                        ent.Organization_State = Convert.ToString(dr["Organization_State"]);
                        ent.Organization_PinCode = Convert.ToString(dr["Organization_PinCode"]);
                        ent.Organization_Country = Convert.ToString(dr["Organization_Country"]);
                        ent.organization_email = Convert.ToString(dr["organization_email"]);
                        ent.organization_web = Convert.ToString(dr["organization_web"]);
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally { con.Close(); }
            return ent;
        }
        #endregion

        #region Branch
        public int SaveBranch(Ent_Branch ent, SafeTransaction trans)
        {
            int dataresult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertBranch", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", ent.Branch_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Organization_ID", ent.Organization_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_Name", ent.Branch_Name));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_Code", ent.Branch_Code));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ContactPerson", ent.Branch_ContactPerson));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_Address", ent.Branch_Address));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_City", ent.Branch_City));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_State", ent.Branch_State));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_PinCode", ent.Branch_PinCode));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_Country", ent.Branch_Country));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_Phone", ent.Branch_Phone));
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_email", ent.Branch_email));
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
                        if (ent.Branch_ID > 0)
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Update Branch"));
                        else
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Insert Branch"));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ""));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally { con.Close(); }
            return dataresult;           
        }      

        public List<Ent_Branch> SelectBranchList(int branch_id)
        {
            List<Ent_Branch> list = new List<Ent_Branch>();
            Ent_Branch ent = new Ent_Branch();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectBranch", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", branch_id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Branch();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.Branch_Name = Convert.ToString(dr["Branch_Name"]);
                        ent.Branch_Code = Convert.ToString(dr["Branch_Code"]);               
                        ent.Branch_ContactPerson = Convert.ToString(dr["Branch_ContactPerson"]);
                        ent.Branch_City = Convert.ToString(dr["Branch_City"]);
                        ent.Branch_Phone = Convert.ToString(dr["Branch_Phone"]);
                        ent.Branch_email = Convert.ToString(dr["Branch_email"]);
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

        public Ent_Branch SelectBranch(int branch_id)
        {           
            Ent_Branch ent = new Ent_Branch();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectBranch", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", branch_id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Branch();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);                        
                        ent.Branch_Name = Convert.ToString(dr["Branch_Name"]);
                        ent.Branch_Code = Convert.ToString(dr["Branch_Code"]);
                        ent.Branch_ContactPerson = Convert.ToString(dr["Branch_ContactPerson"]);
                        ent.Branch_City = Convert.ToString(dr["Branch_City"]);
                        ent.Branch_Phone = Convert.ToString(dr["Branch_Phone"]);
                        ent.Branch_email = Convert.ToString(dr["Branch_email"]);                      
                        ent.Branch_State = Convert.ToString(dr["Branch_State"]);                      
                        ent.Branch_Country = Convert.ToString(dr["Branch_Country"]);                      
                        ent.Branch_PinCode = Convert.ToString(dr["Branch_PinCode"]);                      
                        ent.Branch_Address = Convert.ToString(dr["Branch_Address"]);
                        ent.FRRO_Password = Convert.ToString(dr["FRRO_Password"]);
                        ent.FRRO_Username = Convert.ToString(dr["FRRO_Username"]);
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

        public int DeleteBranch(Ent_Branch ent, SafeTransaction trans)
        {
            int dataResult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("zs_deletebranch", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", ent.Branch_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Modified_By", ent.Modified_By));
                    cmd.Parameters.Add(new SqlParameter("@p_Modified_Date", ent.Modified_Date));
                    try
                    {
                        dataResult = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.Dispose();
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
                        cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Modified_By));
                        cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Modified_Date));
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", ent.Branch_ID));                      
                        cmd.Parameters.Add(new SqlParameter("@p_logaction", "Delete Branch"));                       
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ""));
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
            catch (Exception)
            {
                dataResult = -1;
            }
            finally
            {

                con.Close();
            }
            return dataResult;
        }
        #endregion

        #region ServerMaster
        public int UpdateOCRServer(int OcrServer, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_UpdateOcrServer", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];
                    cmd.Parameters.Add(new SqlParameter("@branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    cmd.Parameters.Add(new SqlParameter("@OcrServer", OcrServer));
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

            }
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally { con.Close(); }
            return dataresult;
        }
        #endregion

        #region FRROMaster
        public int SaveFRRO(Ent_Branch ent, SafeTransaction trans)
        {
            int dataresult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_UpdateFrroCredentials", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];
                    cmd.Parameters.Add(new SqlParameter("@branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    cmd.Parameters.Add(new SqlParameter("@FRRO_Username", ent.FRRO_Username));
                    cmd.Parameters.Add(new SqlParameter("@FRRO_Password", ent.FRRO_Password));
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
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                        cmd.Parameters.Add(new SqlParameter("@p_logaction", "Update FRRO"));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ""));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally { con.Close(); }
            return dataresult;
        }
        #endregion

        #region User
        public int SaveUser(Ent_User ent, SafeTransaction trans)
        {
            int dataresult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertUser", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Branch_ID", ent.Branch_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_User_ID", ent.User_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_User_Name", ent.User_Name));
                    cmd.Parameters.Add(new SqlParameter("@p_User_Type", ent.User_Type));
                    cmd.Parameters.Add(new SqlParameter("@p_User_Username", ent.User_Username));
                    cmd.Parameters.Add(new SqlParameter("@p_User_Password", ent.User_Password));
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
                        if (ent.User_ID > 0)
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Update User"));
                        else
                            cmd.Parameters.Add(new SqlParameter("@p_logaction", "Insert User"));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ""));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally { con.Close(); }
            return dataresult;           
        }

        public List<Ent_User> SelectUserList(int user_id)
        {
            List<Ent_User> list = new List<Ent_User>();
            Ent_User ent = new Ent_User();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectUser", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_user_id", user_id));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@p_branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_User();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.User_ID = Convert.ToInt32(dr["User_ID"]);
                        ent.User_Name = Convert.ToString(dr["User_Name"]);
                        ent.User_Type = Convert.ToString(dr["User_Type"]);
                        ent.User_Username = Convert.ToString(dr["User_Username"]);
                        ent.User_Password = Convert.ToString(dr["User_Password"]);
                        ent.User_Type = Convert.ToString(dr["User_Type"]);
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

        public Ent_User SelectUser(int user_id)
        {
            Ent_User ent = new Ent_User();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectUser", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_user_id", user_id));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@p_branch_id", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_User();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.User_ID = Convert.ToInt32(dr["User_ID"]);
                        ent.User_Name = Convert.ToString(dr["User_Name"]);
                        ent.User_Type = Convert.ToString(dr["User_Type"]);
                        ent.User_Username = Convert.ToString(dr["User_Username"]);
                        ent.User_Password = Convert.ToString(dr["User_Password"]);
                        ent.User_Type = Convert.ToString(dr["User_Type"]);
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

        public int DeleteUser(Ent_User ent, SafeTransaction trans)
        {
            int dataResult = 0;
            int dataresult1 = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("zs_deleteuser", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_User_ID", ent.User_ID));
                    cmd.Parameters.Add(new SqlParameter("@p_Modified_By", ent.Modified_By));
                    cmd.Parameters.Add(new SqlParameter("@p_Modified_Date", ent.Modified_Date));
                    try
                    {
                        dataResult = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.Dispose();
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
                        cmd.Parameters.Add(new SqlParameter("@p_created_by", ent.Modified_By));
                        cmd.Parameters.Add(new SqlParameter("@p_created_date", ent.Modified_Date));
                        cmd.Parameters.Add(new SqlParameter("@p_primary_id", ent.User_ID));
                        cmd.Parameters.Add(new SqlParameter("@p_logaction", "Delete User"));
                        cmd.Parameters.Add(new SqlParameter("@p_customer_code", ""));
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
            catch (Exception)
            {
                dataResult = -1;
            }
            finally
            {

                con.Close();
            }
            return dataResult;           
        }
        #endregion

        public DataTable SelectCountryList( )
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectCountryList", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;  
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            catch { }
            finally { con.Close(); }
            return dt;
        }

        public DataTable SelectNationalityList()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("zs_selectnationalitylist", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            catch { }
            finally { con.Close(); }
            return dt;
        }

        public DataTable SelectPurposeList()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("zs_selectpurposeofvisit", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            catch { }
            finally { con.Close(); }
            return dt;
        }

        public DataTable SelectVisaType(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectVisaType", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_VisaID", id));
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            catch (Exception e)
            {
            }
            finally { con.Close(); }
            return dt;

        }

        public List<Ent_GuestData> SelectFRROList(DateTime FromDate, DateTime ToDate, int flag)
        {
            List<Ent_GuestData> list = new List<Ent_GuestData>();
            Ent_GuestData ent = new Ent_GuestData();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectFRROList", con))
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
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_GuestData();
                        ent.Guest_Code = Convert.ToString(dr["Guest_Code"]);
                        ent.Guest_Firstname = Convert.ToString(dr["Guest_Firstname"]);
                        ent.Guest_Lastname = Convert.ToString(dr["Guest_Lastname"]);
                        ent.Guest_FrroReferenceId = Convert.ToString(dr["Guest_FrroReferenceId"]);
                        ent.guest_FrroChellan = Convert.ToString(dr["guest_FrroChellan"]);
                        ent.Guest_FrroEntryDate = Convert.ToString(dr["Guest_FrroEntryDate"]);
                        ent.Guest_FrroEntryUserName = Convert.ToString(dr["Guest_FrroEntryUser"]);
                        ent.Guest_FrroCheckOutDate = Convert.ToString(dr["Guest_FrroCheckOutDate"]);
                        ent.Guest_FrroCheckOutUserName = Convert.ToString(dr["Guest_FrroCheckOutUser"]);
                        ent.Guest_PassToFRRO = Convert.ToInt32(dr["Guest_PassToFRRO"]);
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

        #region Cuisine
        public List<Ent_Cuisine> SelectCuisine(int cuisineId)
        {
            List<Ent_Cuisine> list = new List<Ent_Cuisine>();
            Ent_Cuisine ent = new Ent_Cuisine();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectCuisine", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@cuisineId", cuisineId));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.Cuisine));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Cuisine();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.CuisineId = Convert.ToInt32(dr["CuisineId"]);
                        ent.CuisineName = Convert.ToString(dr["CuisineName"]);
                        ent.Description = Convert.ToString(dr["Description"]);
                        ent.CuisineType = Convert.ToInt32(dr["CuisineType"]);
                        ent.CuisineTypeName = Convert.ToString(dr["CuisineTypeName"]);
                        ent.UploadFileID = Convert.ToInt32(dr["UploadFileID"]);
                        ent.DocNameOrig = Convert.ToString(dr["DocNameOrig"]);
                        ent.DocName = Convert.ToString(dr["DocName"]);
                        ent.Status = Convert.ToInt32(dr["Status"]);
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

        public List<Ent_CuisineType> SelectCuisineType()
        {
            List<Ent_CuisineType> list = new List<Ent_CuisineType>();
            Ent_CuisineType ent = new Ent_CuisineType();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectCuisineType", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));                    
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_CuisineType();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.CuisineTypeId = Convert.ToInt32(dr["CuisineTypeId"]);
                        ent.CuisineTypeName = Convert.ToString(dr["CuisineTypeName"]);

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

        public int SaveCuisine(Ent_Cuisine ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertCuisine", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@cuisineId", ent.CuisineId));
                    cmd.Parameters.Add(new SqlParameter("@cuisineName", ent.CuisineName));
                    cmd.Parameters.Add(new SqlParameter("@description", ent.Description));
                    cmd.Parameters.Add(new SqlParameter("@cuisineType", ent.CuisineType));
                    cmd.Parameters.Add(new SqlParameter("@uploadFileID", ent.UploadFileID));
                    cmd.Parameters.Add(new SqlParameter("@DocName", ent.DocName));
                    cmd.Parameters.Add(new SqlParameter("@status", ent.Status));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", ent.CreatedBy));
                    cmd.Parameters.Add(new SqlParameter("@CreatedOn", ent.CreatedOn));
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.Cuisine));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally
            {
                con.Close();
            }
            return dataresult;
        }

        public int DeleteCuisine(Ent_Cuisine ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_DeleteCuisine", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@cuisineId", ent.CuisineId));
                    cmd.Parameters.Add(new SqlParameter("@uploadFileID", ent.UploadFileID));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.Cuisine));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally
            {
                con.Close();
            }
            return dataresult;
        }



        #endregion

        #region Amenities
        public List<Ent_Amenities> SelectAmenities(int amenitiesId)
        {
            List<Ent_Amenities> list = new List<Ent_Amenities>();
            Ent_Amenities ent = new Ent_Amenities();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectAmenities", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@amenitiesId", amenitiesId));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));                    
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.Amities));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_Amenities();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.AmenitiesId = Convert.ToInt32(dr["AmenitiesId"]);
                        ent.AmenitiesName = Convert.ToString(dr["AmenitiesName"]);
                        ent.Description = Convert.ToString(dr["Description"]);
                        ent.UploadFileID = Convert.ToInt32(dr["UploadFileID"]);
                        ent.DocNameOrig = Convert.ToString(dr["DocNameOrig"]);
                        ent.DocName = Convert.ToString(dr["DocName"]);
                        ent.Status = Convert.ToInt32(dr["Status"]);
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

        public int SaveAmenities(Ent_Amenities ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_InsertAmenities", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@amenitiesId", ent.AmenitiesId));
                    cmd.Parameters.Add(new SqlParameter("@amenitiesName", ent.AmenitiesName));
                    cmd.Parameters.Add(new SqlParameter("@description", ent.Description));
                    cmd.Parameters.Add(new SqlParameter("@uploadFileID", ent.UploadFileID));
                    cmd.Parameters.Add(new SqlParameter("@DocName", ent.DocName));
                    cmd.Parameters.Add(new SqlParameter("@status", ent.Status));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));                    
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", ent.CreatedBy));
                    cmd.Parameters.Add(new SqlParameter("@CreatedOn", ent.CreatedOn));
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.Amities));
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
            catch (Exception e)
            {
                dataresult = 0;
            }
            finally
            {
                con.Close();
            }
            return dataresult;
        }

        public int DeleteAmenities(Ent_Amenities ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_DeleteAmenities", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@amenitiesId", ent.AmenitiesId));
                    cmd.Parameters.Add(new SqlParameter("@uploadFileID", ent.UploadFileID));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));                    
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.Amities));
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
            catch (Exception e)
            {
                dataresult = 0;
            }
            finally
            {
                con.Close();
            }
            return dataresult;
        }

        #endregion

        #region NearbyPlaces
        public List<Ent_NearbyPlaces> SelectNearbyPlaces(int nearbyPlacesId)
        {
            List<Ent_NearbyPlaces> list = new List<Ent_NearbyPlaces>();
            Ent_NearbyPlaces ent = new Ent_NearbyPlaces();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectNearbyPlaces", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@nearbyPlacesId", nearbyPlacesId));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.NearbyPlaces));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_NearbyPlaces();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.NearbyPlacesId = Convert.ToInt32(dr["NearbyPlacesId"]);
                        ent.NearbyPlacesName = Convert.ToString(dr["NearbyPlacesName"]);
                        ent.Description = Convert.ToString(dr["Description"]);
                        ent.Distance = Convert.ToString(dr["Distance"]);
                        ent.LocationMap = WebUtility.HtmlDecode(Convert.ToString(dr["LocationMap"]));
                        ent.UploadFileID = Convert.ToInt32(dr["UploadFileID"]);
                        ent.DocNameOrig = Convert.ToString(dr["DocNameOrig"]);
                        ent.DocName = Convert.ToString(dr["DocName"]);
                        ent.Status = Convert.ToInt32(dr["Status"]);                       
                            ent.imageCount = Convert.ToInt32(dr["imageCount"]);                        
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

        public int SaveNearbyPlaces(Ent_NearbyPlaces ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("ZS_InsertNearbyPlaces", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@nearbyPlacesId", ent.NearbyPlacesId));
                    cmd.Parameters.Add(new SqlParameter("@nearbyPlacesName", ent.NearbyPlacesName));
                    cmd.Parameters.Add(new SqlParameter("@description", ent.Description));
                    cmd.Parameters.Add(new SqlParameter("@distance", ent.Distance));
                    cmd.Parameters.Add(new SqlParameter("@locationMap", WebUtility.HtmlEncode(ent.LocationMap)));
                    cmd.Parameters.Add(new SqlParameter("@uploadFileID", ent.UploadFileID));
                    cmd.Parameters.Add(new SqlParameter("@DocName", ent.DocName));
                    cmd.Parameters.Add(new SqlParameter("@status", ent.Status));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));                    
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", ent.CreatedBy));
                    cmd.Parameters.Add(new SqlParameter("@CreatedOn", ent.CreatedOn));
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.NearbyPlaces));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally
            {
                con.Close();
            }
            return dataresult;
        }

        public int DeleteNearbyPlaces(Ent_NearbyPlaces ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_DeleteNearbyPlaces", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@nearbyPlacesId", ent.NearbyPlacesId));
                    cmd.Parameters.Add(new SqlParameter("@uploadFileID", ent.UploadFileID));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));                    
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.NearbyPlaces));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally
            {
                con.Close();
            }
            return dataresult;
        }

        public int DeleteUploadDetails(Ent_FileUpload ent, SafeTransaction trans)
        {
            int dataresult = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand("ZS_DeleteUploadDetails", trans.DatabaseConnection, trans.Transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", ent.DocId));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));
                    cmd.Parameters.Add(new SqlParameter("@docType", ent.DocType));
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
            catch (Exception e)
            {
                dataresult = -2;
            }
            finally
            {
                con.Close();
            }
            return dataresult;
        }

        public List<Ent_FileUpload> SelectUploadDetails(int Id)
        {
            List<Ent_FileUpload> list = new List<Ent_FileUpload>();
            Ent_FileUpload ent = new Ent_FileUpload();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ZS_SelectUploadDetails", con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    HttpCookie BranchID = HttpContext.Current.Request.Cookies["Branch_ID"];        
                    cmd.Parameters.Add(new SqlParameter("@branch_ID", Convert.ToInt32(BranchID.Value.Split('=')[1])));                    
                    cmd.Parameters.Add(new SqlParameter("@docType", General.DocumentUploadType.NearbyPlaces));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ent = new Ent_FileUpload();
                        ent.Branch_ID = Convert.ToInt32(dr["Branch_ID"]);
                        ent.DocId = Convert.ToInt32(dr["DocId"]);
                        ent.UploadID = Convert.ToInt32(dr["UploadID"]);
                        ent.DocName = Convert.ToString(dr["DocName"]);
                        ent.DocNameOrig = Convert.ToString(dr["DocNameOrig"]);
                        ent.DocType = Convert.ToInt32(dr["DocType"]);
                        ent.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        ent.Status = Convert.ToInt32(dr["Status"]);
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


        #endregion
    }
}