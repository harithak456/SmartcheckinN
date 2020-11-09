﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Entity
{
    public class Ent_User
    {
        public int User_ID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Name { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Username { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Password { get; set; }

        public int Branch_ID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string User_Type { get; set; }
                        
        public int Is_Active { get; set; }
        public int Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FRRO_Username { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FRRO_Password { get; set; }
    }
}