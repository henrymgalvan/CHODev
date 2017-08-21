using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cho500.Models.Patient
{
    public class ConsultationSummaryViewModel
    {
        public int Id { get; set; }
        public string Admittedby { get; set; }
        public string DateOfConsult { get; set; }
        public string ChiefComplaint { get; set; }
        public string Physician { get; set; }
    }
}
