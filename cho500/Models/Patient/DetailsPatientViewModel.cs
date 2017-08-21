using System.ComponentModel.DataAnnotations;

namespace cho500.Models.Patient
{
    public class DetailsPatientViewModel
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Ext. Name")]
        public string ExtensionName { get; set; }

        [Display(Name = "Tittle")]
        public string NameTittle { get; set; }

        //Education
        public string HighestEducationalAttainment { get; set; }
        //Education Status
        public string EducationStats { get; set; }
        //Educational Degree
        public string Degree { get; set; }

        //Address
        public string Address { get; set; }
        //Barangay
        public string Barangay { get; set; }
        //City
        public string City { get; set; }
        //Province
        public string Province { get; set; }

        [Display(Name = "Religion")]
        public string Religion { get; set; }

        //Age
        public string Age { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        //Place of Birth, City Province
        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Gender")]
        public string Sex { get; set; }

        [Display(Name = "Civil Status")]
        public string CivilStatus { get; set; }

        [Display(Name = "Occupation")]
        public string WorkSkills { get; set; }

        //Employed private,government
        public string EmploymentAgency { get; set; }

        public string Father { get; set; }
        //Fathers BirthDate
        [Display(Name = "Father's Birthdate")]
        public string FathersBirthdate { get; set; }

        public string Mother { get; set; }
        //Mothers BirthDate
        [Display(Name = "Mother's Birthdate")]
        public string MothersBirthdate { get; set; }

        [Display(Name = "Household ID")]
        public string HouseholdProfileID { get; set; }

        [Display(Name = "PhilHealth ID")]
        public string PhilHealthNo { get; set; }

        //Membership Type
        public string PhicMembership { get; set; }
        //Individually Paying Program
        public string IndividuallyPayingProgram { get; set; }
        //Membership Category
        public string MembershipCategoryGroup { get; set; }
        //Membership Status
        public string CurrentMembershipStatus { get; set; }

        [Display(Name = "Mobile No.")]
        public string ContactNumber { get; set; }

        [Display(Name = "Landline")]
        public string LandlineNumber { get; set; }

        [Display(Name = "Emergency Contact Person")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Mobile No.")]
        public string EmergencyContactNumberCP { get; set; }

        [Display(Name = "LandLine")]
        public string EmergencyContactNumberLL { get; set; }

        [Display(Name = "Relation to Patient")]
        public string Relation { get; set; }

        [Display(Name = "Contact Person BirthDate")]
        public string EmergencyContactBirthday { get; set; }

        public string Encoder { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated { get; set; }

        //Last Date Updated
        public string LastDateUpdated { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }


        public string FullName
        {
            get
            {
                return LastName + (ExtensionName != null ? (ExtensionName.Length > 0 ? (" " + ExtensionName) : "") : "") + ", " + FirstName + " " + MiddleName;
            }
        }

        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }

        public ConsultationSummaryViewModel ConsultationSummary { get; set; }

    }
}
