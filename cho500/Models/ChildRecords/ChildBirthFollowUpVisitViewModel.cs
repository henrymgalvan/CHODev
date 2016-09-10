namespace cho500.Models.ChildRecords
{
    public class ChildBirthFollowUpVisitViewModel
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgeInWeeks { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateOfFollowUp { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Physician { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }

        public int PersonID { get; set; }
    }
}