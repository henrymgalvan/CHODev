using System;
using System.ComponentModel.DataAnnotations;
using cho500.Entity;

namespace cho500.Models
{
    public class IndexPatientViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string BloodType { get; set; }
        public Person.Gender Sex { get; set; }
        public Person.State CivilStatus { get; set; }
        public string Address { get; set; }
        public string Barangay { get; set; }
        public string HouseholdProfileID { get; set; }
        public string PhilHealthNo { get; set; }   
        public string ContactNumber { get; set; }
        public string Notes { get; set; }
    }
}