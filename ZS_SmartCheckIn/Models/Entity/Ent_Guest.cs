using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Entity
{
    public class Ent_Guest
    {
        public int Guest_ID{ get; set; }        
        public string Customer_Code { get; set; }
        public string Guest_Username { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Password { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserToken { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Firstname { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Lastname { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Email { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_PhoneNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_MobileNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Booking_Portal { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_Accompany { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Notification_Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Confirmation_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime Arrival_Date { get; set; }
        
             [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Arrival_Time { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime Checkin_Date { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Branch_ID { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Modified_By { get; set; }
        public int Is_Active { get; set; } // 0- deleted , 1- not deleted , 2- id verified
        public int IsWalking { get; set; }
        public int IsFrequent { get; set; }
        public int FrequentVisitor { get; set; }
        public Ent_GuestData entGuestData = new Ent_GuestData();

        public bool sendmail { get; set; }
        public bool sendsms { get; set; }

        public string createduser { get; set; }
        public string logaction { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Signature { get; set; }

    }
}