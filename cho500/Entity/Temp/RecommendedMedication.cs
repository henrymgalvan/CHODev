using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cho500.Entity
{
    public class RecommendedMedication
    {
        public int Id { get; set; }
        public string Medicine { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string PharmacyReleasedBy { get; set; }
        public DateTime DateReleased { get; set; }
        public string Notes { get; set; }

        public int DiagnosisId { get; set; }
        //public virtual Diagnosis Diagnosis { get; set; }
    }
}