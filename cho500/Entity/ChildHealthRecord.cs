using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cho500.Entity
{
    public class ChildHealthRecord
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonID { get; set; }

        public int Months { get; set; }
        public int Weeks { get; set; }
        public int Days { get; set; }
        public string TypeOfDelivery { get; set; }
        public decimal BirthWeightInPounds { get; set; }
        public decimal BirthLength { get; set; }
        public decimal HeadCircumference { get; set; }
        public decimal ChestCircumference { get; set; }
        public decimal AbdominalCircumference { get; set; }

        //public int? BloodTypeID { get; set; }
        //public virtual BloodType BloodType { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<ChildImmunizationRecord> ChildImmunizationRecords { get; set; }
        public virtual ICollection<ChildBirthFollowUpVisit> ChildBirthFollowUpVisits { get; set; }

    }
}