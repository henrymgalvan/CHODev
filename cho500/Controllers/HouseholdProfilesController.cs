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
using cho500.Models.Households;
using AutoMapper;
using PagedList;
using Microsoft.Reporting.WebForms;

namespace cho500.Controllers
{
    public class HouseholdProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HouseholdProfiles
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RespondentSortParm = String.IsNullOrEmpty(sortOrder) ? "respondent_desc" : "";
            ViewBag.BarangaySortParm = sortOrder == "barangay" ? "barangay_desc" : "barangay";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var householdProfileWithMembers = db.HouseholdProfiles.Include(h => h.HouseholdMembers).ToList();
            List<HouseholdIndexViewModel> householdProfiles = new List<HouseholdIndexViewModel>();

            if (householdProfileWithMembers.Any())
            {
                householdProfiles = Mapper.Map<List<HouseholdProfile>, List<HouseholdIndexViewModel>>(householdProfileWithMembers);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                householdProfiles = householdProfiles.Where(p => p.Respondent.ToUpper().Contains(searchString.ToUpper())| p.Barangay.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            switch (sortOrder)
            {
                case "respondent_desc":
                    householdProfiles = householdProfiles.OrderByDescending(s => s.Respondent).ToList();
                    break;
                case "barangay":
                    householdProfiles = householdProfiles.OrderBy(s => s.Barangay).ToList();
                    break;
                case "barangay_desc":
                    householdProfiles = householdProfiles.OrderByDescending(s => s.Barangay).ToList();
                    break;
                default:
                    householdProfiles = householdProfiles.OrderBy(s => s.Respondent).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(householdProfiles.ToPagedList(pageNumber, pageSize));
            //return View(householdProfiles);
        }

        // GET: HouseholdProfiles/Details/5
        public ActionResult Details(int? id, string householdProfileID = "Temp")
        {
            if ((id == null) & (householdProfileID == "Temp"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdProfile householdProfile;
            if (id > 0)
            {
                householdProfile = db.HouseholdProfiles.Find(id);
            }
            else
            {
                householdProfile = db.HouseholdProfiles.First(y => y.HouseholdProfileID == householdProfileID);
            }

            var householdMembers = db.HouseholdMembers.Where(y => y.HouseholdProfileID == householdProfile.HouseholdProfileID).ToList();
            if (householdProfile == null)
            {
                return HttpNotFound();
            }
            foreach (HouseholdMember member in householdMembers)
            {
                householdProfile.HouseholdMembers.Add(member);
            }
            return View(householdProfile);
        }

        // GET: HouseholdProfiles/Create
        public ActionResult Create()
        {
            var NonHouseholdMemberPatients = db.Patient.Where(y => y.HouseholdProfileID == null || y.HouseholdProfileID == "").ToList();
            ViewBag.PersonID = new SelectList(NonHouseholdMemberPatients, "PersonID", "FullName");
            ViewBag.BarangayId = new SelectList(db.Barangays, "BarangayID", "Name");
            return View();
        }

        // POST: HouseholdProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdProfileID,FourPsCCTBeneficiary,Address,BarangayID,PersonId,Note")] HouseholdProfile householdProfile)
        {
            if (ModelState.IsValid)
            {
                db.HouseholdProfiles.Add(householdProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var NonHouseholdMemberPatients = db.Patient.Where(y => y.HouseholdProfileID == null || y.HouseholdProfileID == "").ToList();
            ViewBag.PersonID = new SelectList(NonHouseholdMemberPatients, "PersonID", "FullName", householdProfile.PersonID);
            ViewBag.BarangayId = new SelectList(db.Barangays, "BarangayID", "Name", householdProfile.BarangayID);
            return View(householdProfile);
        }

        // GET: HouseholdProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdProfile householdProfile = db.HouseholdProfiles.Find(id);
            if (householdProfile == null)
            {
                return HttpNotFound();
            }
            var NonHouseholdMemberPatients = db.Patient.Where(y => y.HouseholdProfileID == null || y.HouseholdProfileID == "" || y.HouseholdProfileID == householdProfile.HouseholdProfileID);
            ViewBag.PersonID = new SelectList(NonHouseholdMemberPatients, "PersonID", "FullName", householdProfile.PersonID);
            ViewBag.BarangayId = new SelectList(db.Barangays, "BarangayID", "Name", householdProfile.BarangayID);

            return View(householdProfile);
        }

        // POST: HouseholdProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdProfileID,FourPsCCTBeneficiary,Address,BarangayID,PersonId,Note")] HouseholdProfile householdProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(householdProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var NonHouseholdMemberPatients = db.Patient.Where(y => y.HouseholdProfileID == null || y.HouseholdProfileID == "" || y.HouseholdProfileID == householdProfile.HouseholdProfileID);
            ViewBag.PersonID = new SelectList(NonHouseholdMemberPatients, "PersonID", "FullName", householdProfile.PersonID);
            ViewBag.BarangayId = new SelectList(db.Barangays, "BarangayID", "Name", householdProfile.BarangayID);
            return View(householdProfile);
        }

        // GET: HouseholdProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdProfile householdProfile = db.HouseholdProfiles.Find(id);
            if (householdProfile == null)
            {
                return HttpNotFound();
            }
            return View(householdProfile);
        }

        // POST: HouseholdProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseholdProfile householdProfile = db.HouseholdProfiles.Find(id);
            var householdMembers = db.Patient.Where(y => y.HouseholdProfileID == householdProfile.HouseholdProfileID).ToList();
            foreach (Person patient in householdMembers)
            {
                patient.HouseholdProfileID = null;
                db.Entry(patient).State = EntityState.Modified;
            }
            db.HouseholdProfiles.Remove(householdProfile);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult ExportTo(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/HouseholdProfile.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = db.HouseholdProfiles.ToList();

            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : (ReportType == "Word") ? "docx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding,
                                            out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=HouseholdProfile." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
