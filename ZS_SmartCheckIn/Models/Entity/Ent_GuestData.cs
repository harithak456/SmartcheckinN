using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZS_SmartCheckIn.Models.Entity
{
    public class Ent_GuestData
    {

        [AllowHtml]
        public string Result { get; set; }

        public int Guest_ID { get; set; }
        public int Guestdata_id { get; set; }
        public string Customer_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Firstname { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Lastname { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Father { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_PhoneNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Email { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Gender { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Nationality { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_DOB { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Address { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Country { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_State { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_City { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_PurposeofVisit { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_DocumentNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_CountryofIssue { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_DateOfIssue { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_ExpiryDate { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_CardType { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_AdultCount { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_ChildCount { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_DayCount { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_NightCount { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Modified_By { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_VisaNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_VisaPOICity { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_VisaPOICountry { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_VisaDateofIssue { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_VisaValidTill { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_VisaType { get; set; }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_Document { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_DocumentBack { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_DocumentVisa { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_ProfileImage { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int guestdoc_id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Is_Active { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Is_GroupLeader { get; set; }

        public int PersonID { get; set; }
        public int PersonDetailID { get; set; }
        public int Photo { get; set; }
        public int CropPointX { get; set; }
        public int CropPointY { get; set; }
        public int CropPortionWidth { get; set; }
        public int CropPortionHeight { get; set; }
        public int PreviewImageWidth { get; set; }
        public int PreviewImageHeight { get; set; }
        public int IsCropped { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_Accompany { get; set; }

        public int Branch_ID { get; set; }

        public string Arrival_Date { get; set; }
        public string Arrival_Time { get; set; }
        public byte[] Guest_ProfilePic { get; set; }

        public int Guest_PassToFRRO { get; set; }

        public string guest_FrroChellan { get; set; }
        public string Guest_FrroReferenceId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_FrroEntryDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_FrroEntryUser { get; set; }
        public string Guest_FrroEntryUserName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Guest_FrroCheckOutDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Guest_FrroCheckOutUser { get; set; }
        public string Guest_FrroCheckOutUserName { get; set; }

        public Ent_Organization Organization = new Ent_Organization();
        public int Guest_FrroCheckOutStatus { get; set; }
    }
}