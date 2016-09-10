using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cho500.Entity
{
    public enum RelationToHouseholdHead
    {
        Head = 1, Spouse = 2, Son = 3, Daughter = 4, GrandSon = 5, GrandDaughter = 6, SonInLaw = 7, DaughterInLaw = 8
    }

    public class HouseholdMember
    {
        public int Id { get; set; }
        [Required]
        public string HouseholdProfileID { get; set; }
        public int PersonID { get; set; }
        public RelationToHouseholdHead RelationToHead { get; set; }

        public virtual Person Member { get; set; }
    }
}