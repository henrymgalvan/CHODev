using cho500.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cho500.Models
{ 
    public class CreatePersonViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 1,ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Middle name cannot be longer than 50 characters.")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Ext. Name")]
        [StringLength(3)]
        public string ExtensionName { get; set; }

        [Display(Name = "Tittle")]
        [StringLength(25)]
        public string NameTittle { get; set; }

        public Person.Education HighestEducationalAttainment { get; set; }
        public Person.EducationStatus EducationStats { get; set; }
        public string Degree { get; set; }

        [Display(Name = "Religion")]
        [StringLength(50)]
        public string Religion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        [Display(Name = "Gender")]
        public Person.Gender Sex { get; set; }

        [Display(Name = "Civil Status")]
        public Person.State CivilStatus { get; set; }
        [Display(Name = "Work")]
        public string WorkSkills { get; set; }
        public Person.Employed EmploymentAgency { get; set; }

        //public int FatherPersonId { get; set; }
        //public int MotherPersonId { get; set; }

        [Display(Name = "Blood Type")]
        public int? BloodTypeID { get; set; }

        //public string HouseholdProfileID { get; set; }
        [Display(Name = "PhilHealth ID")]        
        public string PhilHealthNo { get; set; }
        public Person.PHICMembership PhicMembership { get; set; }
        public Person.Sponsored SponsoredMembership { get; set; }
        public Person.IPP IndividuallyPayingProgram { get; set; }
        public Person.MembershipCategory MembershipCategoryGroup { get; set; }
        public Person.MembershipStatus CurrentMembershipStatus { get; set; }

        [Phone]
        public string ContactNumber { get; set; }
        [Phone]
        public string LandlineNumber { get; set; }

        [Display(Name = "Contact Person Name")]
        [StringLength(50)]
        public string EmergencyContactName { get; set; }
        [Phone]
        [Display(Name = "Contact CP#")]
        public string EmergencyContactNumberCP { get; set; }
        [Phone]
        [Display(Name = "Contact LandLine#")]
        public string EmergencyContactNumberLL { get; set; }
        [Display(Name = "Relation to Patient")]
        public string Relation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Contact Person BirthDate")]
        public DateTime? EmergencyContactBirthday { get; set; }

        public string Encoder { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime? LastDateUpdated { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

    }
}