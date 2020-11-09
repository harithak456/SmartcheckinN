using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZS_SmartCheckIn.Models.Entity
{
    public class Ent_NearbyPlaces
    {
        public int NearbyPlacesId { get; set; }
        public string NearbyPlacesName { get; set; }
        public string Description { get; set; }
        public string Distance { get; set; }
        public string LocationMap { get; set; }
        public int UploadFileID { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Branch_ID { get; set; }
        public string CuisineTypeName { get; set; }
        public string DocName { get; set; }
        public string DocNameOrig { get; set; }
        public HttpPostedFileBase UploadImg { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int imageCount { get; set; }
    }

    public class Ent_FileUpload
    {
        public int DocId { get; set; }
        public int UploadID { get; set; }
        public string DocName { get; set; }
        public string DocNameOrig { get; set; }
        public int Branch_ID { get; set; }
        public int DocType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
    }
}