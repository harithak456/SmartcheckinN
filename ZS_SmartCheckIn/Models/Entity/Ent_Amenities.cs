using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Entity
{
    public class Ent_Amenities
    {
        public int AmenitiesId { get; set; }
        public string AmenitiesName { get; set; }
        public string Description { get; set; }
        public int UploadFileID { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Branch_ID { get; set; }
        public string DocName { get; set; }
        public string DocNameOrig { get; set; }
        public HttpPostedFileBase UploadImg { get; set; }
    }
}