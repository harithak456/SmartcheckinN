using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Common
{
    public class Global
    {
       
        //public static int glbUserID { get; set; }
        //public static int glbBranchID { get; set; }
        public static string Image_Path { get; set; }
        private static Random random = new Random();

        //public static string Customer_Code { get; set; }

      //  public static int OrgID { get; set; }
      //  public static string OrgName { get; set; }
        //public static string OrgMail { get; set; }
        //public static string OrgPhone { get; set; }
        //public static string OrgAddress { get; set; }
        //public static string BranchMail { get; set; }
        //public static string BranchPhone { get; set; }
        //public static string BranchAddress { get; set; }
        //public static string Branch_ContactPerson { get; set; }

        public static DataTable glbCountryDT { get; set; }
        public static DataTable glbNationalityDT { get; set; }
        public static DataTable glbPurposeDT { get; set; }
        public static DataTable glbVisaTypeDT { get; set; }

        public static string GetCustomerCode()
        {
            int length = 3;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetGuestCode()
        {
            int length = 5;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetConfirmationCode()
        {
            int length = 6;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}