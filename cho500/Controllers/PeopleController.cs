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
    public class PeopleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: People
        public ActionResult Index()
        {
            var patient = db.Patient.Include(p => p.BloodType).Include(p => p.ChildHealthRecords);
            return View(patient.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Patient.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.BloodTypeID = new SelectList(db.BloodType, "BloodTypeID", "Type");
            ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,FirstName,MiddleName,LastName,ExtensionName,NameTittle,Religion,DateOfBirth,Sex,CivilStatus,WorkSkills,FatherPersonId,MotherPersonId,HouseholdProfileID,PhilHealthNo,ContactNumber,LandlineNumber,EmergencyContactName,EmergencyContactNumberCP,EmergencyContactNumberLL,Relation,EmergencyContactBirthday,Encoder,DateCreated,Notes,BloodTypeID")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Patient.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BloodTypeID = new SelectList(db.BloodType, "BloodTypeID", "Type", person.BloodTypeID);
            ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", person.PersonID);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Patient.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.BloodTypeID = new SelectList(db.BloodType, "BloodTypeID", "Type", person.BloodTypeID);
            ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", person.PersonID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,FirstName,MiddleName,LastName,ExtensionName,NameTittle,Religion,DateOfBirth,Sex,CivilStatus,WorkSkills,FatherPersonId,MotherPersonId,HouseholdProfileID,PhilHealthNo,ContactNumber,LandlineNumber,EmergencyContactName,EmergencyContactNumberCP,EmergencyContactNumberLL,Relation,EmergencyContactBirthday,Encoder,DateCreated,Notes,BloodTypeID")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BloodTypeID = new SelectList(db.BloodType, "BloodTypeID", "Type", person.BloodTypeID);
            ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", person.PersonID);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Patient.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Patient.Find(id);
            db.Patient.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
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
