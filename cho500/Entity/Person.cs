using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cho500.Entity
{
    public class Person
    {
        public enum Gender
        {
            Male = 1, Female = 2
        }
        public enum State
        {
            Dependent = 1, Single, Married, Separated, Annuled, Widowed
        }
        public enum Education
        {
          Nursery=0, Preparatory=1, Elementary=2, HighSchool=3, Vocational=4, College=5, PostGraduate=6, HomeStudy=7, NoSchooling=8
        }
        public enum EducationStatus
        {
          Current=0, Graduate=1, Stopped=3
        }
        public enum PHICMembership
        {
          Member=0, NonMember=1, Dependent=3
        }
        //MembershipType
        public enum Sponsored
        {
          NHTS=1, LGU=2, NGA=3, Private=4
        }
        public enum IPP   //Individually Paying Program
        {
          OrganizedGroup=1, OFW=2
        }
        public enum Employed
        {
          Unemployed=0, Government=1, Private=3
        }
        public enum MembershipCategory
        {
          Government=1, Private=2,MigrantWorker=3, InformalSector=4, NHTSPR=5
        }
        public enum MembershipStatus
        {
          Lifetime=1, Eligible=2, NonEligible=2, Revoked=3
        }

        [Key]
        public int PersonID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]    
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(3)]
        public string ExtensionName { get; set; }

        [StringLength(25)]
        public string NameTittle { get; set; }

        public Education HighestEducationalAttainment { get; set; }
        public EducationStatus EducationStats { get; set; }
        public string Degree { get; set; }

        [StringLength(50)]
        public string Religion { get; set; }

        [DataType(DataType.Date)]
         public DateTime? DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public Gender Sex { get; set; }

        public State CivilStatus { get; set; }
        public string WorkSkills { get; set; }
        public Employed EmploymentAgency { get; set; }

        public int FatherPersonId { get; set; }
        public int MotherPersonId { get; set; }

        public string HouseholdProfileID { get; set; }

        public string PhilHealthNo { get; set; }  
        public PHICMembership PhicMembership { get; set; }
        public Sponsored SponsoredMembership { get; set; }
        public IPP IndividuallyPayingProgram { get; set; }
        public MembershipCategory MembershipCategoryGroup { get; set; }
        public MembershipStatus CurrentMembershipStatus { get; set; }

        [Phone]
        public string ContactNumber { get; set; }
        [Phone]
        public string LandlineNumber { get; set; }

        [StringLength(50)]
        public string EmergencyContactName { get; set; }
        [Phone]
        public string EmergencyContactNumberCP { get; set; }
        [Phone]
        public string EmergencyContactNumberLL { get; set; }
        public string Relation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EmergencyContactBirthday { get; set; }


        public string Encoder { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastDateUpdated { get; set; }

        public string Notes { get; set; }

        public string FullName
        {
            get
            {
                return LastName + (ExtensionName!=null?(ExtensionName.Length>0 ? (" " + ExtensionName): ""): "") + ", " + FirstName + " " + MiddleName;
            }
        }

        public virtual ICollection<Consultation> Consultations { get; set; }

        public int? BloodTypeID { get; set; }
        public virtual BloodType BloodType { get; set; }

        public virtual ChildHealthRecord ChildHealthRecords { get; set; }
    }
}