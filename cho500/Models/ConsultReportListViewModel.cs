using cho500.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cho500.Models
{
    public class ConsultReportListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string HouseHoldNo { get; set; }
        public string Barangay { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfConsult { get; set; }
        public string AverageBP { get; set; }
        public int RespiratoryRate { get; set; }
        public decimal Temperature { get; set; }
        public BMIStatus BMIStatus { get; set; }
        public string CheifComplaint { get; set; }
        public string History { get; set; }
        public string Diagnostic { get; set; }
        public string Treatment { get; set; }
        public string Recommendation { get; set; }
        public string Pharmacy { get; set; }
        public string Physician { get; set; }


    }
}