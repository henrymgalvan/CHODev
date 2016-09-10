using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cho500.Entity
{
    public class HouseholdProfile
    {
        public int Id { get; set; }
        [Required]
        public string HouseholdProfileID{ get; set; }
        public bool FourPsCCTBeneficiary { get; set; }
        public string Address { get; set; }
        public int BarangayID { get; set; }
        public int PersonID { get; set; } //Name of Respondent
        public string Note { get; set; }

        public virtual Person Respondent { get; set; }
        public virtual Barangay Barangay { get; set; }

        public virtual ICollection<HouseholdMember> HouseholdMembers { get; set; }
    }
}