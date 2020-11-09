using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Entity
{
    public class Ent_Branch
    {
        public int Organization_ID { get; set; }
        public string Organization_Name { get; set; }
        public int Branch_ID { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Code { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Address { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_City { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_State { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Country { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_PinCode { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_ContactPerson { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_Phone { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch_email { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FRRO_Username { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FRRO_Password { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FRRO_SystemName { get; set; }

        public int Is_Active { get; set; }
        public int Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
    }
}