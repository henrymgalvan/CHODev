using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cho500.Models.Households
{
    public class HouseholdIndexViewModel
    {
        public int Id { get; set; }
        public string HouseholdProfileID { get; set; }
        public bool FourPsCCTBeneficiary { get; set; }
        public string Address { get; set; }
        public string Barangay { get; set; }
        public int PersonID { get; set; }
        public string Respondent { get; set; }
        public string Note { get; set; }

    }
}