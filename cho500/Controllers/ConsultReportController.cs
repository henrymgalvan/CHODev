using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cho500.Entity;
using cho500.Models;

namespace cho500.Controllers
{
    public class ConsultReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ConsultReport
        public ActionResult Index()
        {
            var peopleconsults = (db.Patient.Include(X => X.Consultations).ToList());
            List<ConsultReportListViewModel> consults = new List<ConsultReportListViewModel>();
            if (peopleconsults.Any())
            {
                foreach (Person patient in peopleconsults)
                {
                    foreach (Consultation cons in patient.Consultations)
                    {
                        if (cons != null)
                        {
                            consults.Add(new ConsultReportListViewModel()
                            {
                                Id=cons.ConsultationID,
                                FullName = patient.FullName,
                                HouseHoldNo = patient.HouseholdProfileID,
                                //Barangay = patient.Barangay.Name,
                                DateOfConsult = cons.DateOfConsult,
                                AverageBP = cons.BPAverage,
                                RespiratoryRate = cons.RR,
                                Temperature = cons.Temperature,
                                BMIStatus = cons.BMIStatus,
                                CheifComplaint = cons.ChiefComplaint,
                                History = cons.History,
                                Diagnostic = cons.History,
                                Treatment = cons.ManagementTreatment,
                                Recommendation = cons.Recommendation,
                                Pharmacy = cons.Pharmacy,
                                Physician = db.Physicians.Find(cons.PhysicianID).Name
                            });
                        }
                    }
                }
            }
            return View(consults);
        }
    }
}