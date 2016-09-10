using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace cho500.Entity
{
    public enum PlaceOfPreviousConsult
    {
        Government=1, Private=2
    }

    public enum BMIStatus
    {
        Obese=1,Overweight,Normal,Underweight
    }

    public class Consultation
    {
        [Key]
        public int ConsultationID { get; set; }

        public string AdmittedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfConsult { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PreviousConsultDate { get; set; }
        public PlaceOfPreviousConsult PreviousConsult { get; set; }
        public string ChiefComplaint { get; set; }
        public int Age { get; set; }

        public string BPFirstReading { get; set; }
        public string BPSecondReading { get; set; }
        public string BPAverage { get; set; }
        public Boolean RaisedBP { get; set; }
        public int PulseRate { get; set; }
        public int RR { get; set; }
        public decimal Temperature { get; set; }

        public decimal WeightInKgms { get; set; }
        public decimal HeightInCm { get; set; }
        public decimal WaistCircumferenceInCm { get; set; }
        public bool CentralAdiposity { get; set; }
        public decimal BMI { get; set; }
        public BMIStatus BMIStatus { get; set; }

        public string History { get; set; }
        public string PhysicalExam { get; set; }
        public string DiagnosisLabResult { get; set; }
        public string ManagementTreatment { get; set; }
        public string Recommendation { get; set; }
        public string Pharmacy { get; set; }
        //public string Physician { get; set; }
        public int PhysicianID { get; set; }
        public virtual Physician Physician { get; set; }
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

    }
}