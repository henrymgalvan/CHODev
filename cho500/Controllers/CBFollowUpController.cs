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
using cho500.Models.ChildRecords;

namespace cho500.Controllers
{
    public class CBFollowUpController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CBFollowUp
        public ActionResult Index()
        {
            List<ChildBirthFollowUpVisitIndexViewModel> childBirthFollowUpVisitList = new List<ChildBirthFollowUpVisitIndexViewModel>();
            var childBirthFollowUpVisits = db.ChildBirthFollowUpVisits.Include(c => c.ChildHealthRecord).ToList();

            if (childBirthFollowUpVisits.Any())
            {
                foreach (var cv in childBirthFollowUpVisits)
                {
                    childBirthFollowUpVisitList.Add(new ChildBirthFollowUpVisitIndexViewModel()
                    {
                        Id = cv.Id,
                        Name = db.Patient.Find(cv.PersonID).FullName,
                        AgeInWeeks = cv.AgeInWeeks,
                        DateOfFollowup = cv.DateOfFollowup.HasValue ? cv.DateOfFollowup.Value.ToString("yyyy-mm-dd") : "NoDate",
                        Weight = cv.Weight,
                        Height = cv.Height,
                        Diagnosis = cv.Diagnosis,
                        Physician=db.Physicians.Find(cv.PhysicianID).Name,
                        Notes=cv.Notes,
                        PersonID=cv.PersonID
                    });
                }
            }
            return View(childBirthFollowUpVisitList.ToList());
        }

        // GET: CBFollowUp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildBirthFollowUpVisit childBirthFollowUpVisit = db.ChildBirthFollowUpVisits.Find(id);
            if (childBirthFollowUpVisit == null)
            {
                return HttpNotFound();
            }
            return View(childBirthFollowUpVisit);
        }

        // GET: CBFollowUp/Create
        public ActionResult Create(int? patientID)
        {
            if (patientID == null||patientID==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var childBirthFollowUpVisitCreateModel = new ChildBirthFollowUpVisitCreateViewModel();
            childBirthFollowUpVisitCreateModel.PersonID = (int)patientID;
            childBirthFollowUpVisitCreateModel.Name = db.Patient.Find(patientID).FullName;
            childBirthFollowUpVisitCreateModel.DateOfFollowup = DateTime.Now;
            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery");
            PopulatePhysicianDropDownList();
            return View(childBirthFollowUpVisitCreateModel);
        }

        // POST: CBFollowUp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AgeInWeeks,DateOfFollowup,Weight,Height,PhysicianID,Diagnosis,Notes,PersonID")] ChildBirthFollowUpVisitCreateViewModel childBirthFollowUpVisitCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var childBirthFollowUpVisits = new ChildBirthFollowUpVisit();
                childBirthFollowUpVisits.AgeInWeeks = childBirthFollowUpVisitCreateViewModel.AgeInWeeks;
                childBirthFollowUpVisits.DateOfFollowup = childBirthFollowUpVisitCreateViewModel.DateOfFollowup;
                childBirthFollowUpVisits.Weight = childBirthFollowUpVisitCreateViewModel.Weight;
                childBirthFollowUpVisits.Height = childBirthFollowUpVisitCreateViewModel.Height;
                childBirthFollowUpVisits.PhysicianID = childBirthFollowUpVisitCreateViewModel.PhysicianID;
                childBirthFollowUpVisits.Diagnosis = childBirthFollowUpVisitCreateViewModel.Diagnosis;
                childBirthFollowUpVisits.Notes = childBirthFollowUpVisitCreateViewModel.Notes;
                childBirthFollowUpVisits.PersonID = childBirthFollowUpVisitCreateViewModel.PersonID;

                db.ChildBirthFollowUpVisits.Add(childBirthFollowUpVisits);
                db.SaveChanges();
                return RedirectToAction("Details","ChildRecord", new { id = childBirthFollowUpVisitCreateViewModel.PersonID });
            }

            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", childBirthFollowUpVisit.PersonID);
            return View(childBirthFollowUpVisitCreateViewModel);
        }

        // GET: CBFollowUp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildBirthFollowUpVisit childBirthFollowUpVisit = db.ChildBirthFollowUpVisits.Find(id);
            if (childBirthFollowUpVisit == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", childBirthFollowUpVisit.PersonID);
            //ViewBag.PhysicianID = new SelectList(db.Physicians, "PhysicianID", "Name", childBirthFollowUpVisit.PhysicianID);
            PopulatePhysicianDropDownList(childBirthFollowUpVisit.PhysicianID);
            return View(childBirthFollowUpVisit);
        }

        // POST: CBFollowUp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AgeInWeeks,DateOfFollowup,Weight,Height,PhysicianID,Diagnosis,Notes,PersonID")] ChildBirthFollowUpVisit childBirthFollowUpVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childBirthFollowUpVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", childBirthFollowUpVisit.PersonID);
            return View(childBirthFollowUpVisit);
        }

        // GET: CBFollowUp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildBirthFollowUpVisit childBirthFollowUpVisit = db.ChildBirthFollowUpVisits.Find(id);
            if (childBirthFollowUpVisit == null)
            {
                return HttpNotFound();
            }
            return View(childBirthFollowUpVisit);
        }

        // POST: CBFollowUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChildBirthFollowUpVisit childBirthFollowUpVisit = db.ChildBirthFollowUpVisits.Find(id);
            db.ChildBirthFollowUpVisits.Remove(childBirthFollowUpVisit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void PopulatePhysicianDropDownList(object selectedPhysician = null)
        {
            var PhysiciansQuery = from p in db.Physicians
                                 orderby p.Name
                                 select p;
            ViewBag.PhysicianID = new SelectList(PhysiciansQuery, "PhysicianID", "Name", selectedPhysician);
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
