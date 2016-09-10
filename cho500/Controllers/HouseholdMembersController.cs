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
    public class HouseholdMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HouseholdMembers
        public ActionResult Index()
        {
            var householdMembers = db.HouseholdMembers.Include(h => h.Member);
            return View(householdMembers.ToList());
        }

        // GET: HouseholdMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdMember householdMember = db.HouseholdMembers.Find(id);
            if (householdMember == null)
            {
                return HttpNotFound();
            }
            return View(householdMember);
        }

        // GET: HouseholdMembers/Create
        public ActionResult Create(string houseHoldId)
        {
            var householdProfile = db.HouseholdProfiles.First(y => y.HouseholdProfileID == houseHoldId);
            var householdMember = new HouseholdMember();
            householdMember.HouseholdProfileID = houseHoldId;
            var NonHouseholdMemberPatients = db.Patient.Where(y => y.HouseholdProfileID == null || y.HouseholdProfileID == "");
            ViewBag.PersonID = new SelectList(NonHouseholdMemberPatients, "PersonID", "FullName");
            ViewBag.Respondent = householdProfile.Respondent.FullName;
            return View(householdMember);
        }

        // POST: HouseholdMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdProfileID,PersonID,RelationToHead")] HouseholdMember householdMember)
        {
            if (ModelState.IsValid)
            {
                if (householdMember.PersonID <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var patient = db.Patient.First(p=>p.PersonID==householdMember.PersonID);
                if (patient!= null)
                {
                    db.Patient.Find(patient.PersonID).HouseholdProfileID = householdMember.HouseholdProfileID;
                }

                db.HouseholdMembers.Add(householdMember);
                db.SaveChanges();
                //return RedirectToAction("Index");
                int householdId = db.HouseholdProfiles.First(y => y.HouseholdProfileID == householdMember.HouseholdProfileID).Id;
                return RedirectToAction("Details", "HouseholdProfiles", new { id = householdId });
            }
            var NonHouseholdMemberPatients = db.Patient.Where(y => y.HouseholdProfileID == null || y.HouseholdProfileID == "");
            ViewBag.PersonID = new SelectList(NonHouseholdMemberPatients, "PersonID", "FullName", householdMember.PersonID);
            return View(householdMember);
        }

        // GET: HouseholdMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdMember householdMember = db.HouseholdMembers.Find(id);
            if (householdMember == null)
            {
                return HttpNotFound();
            }
            var member = db.Patient.Find(householdMember.PersonID);
            householdMember.Member = member;

            return View(householdMember);
        }

        // POST: HouseholdMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdProfileID,PersonID,RelationToHead")] HouseholdMember householdMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(householdMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //           ViewBag.PersonID = new SelectList(db.Patient, "PersonID", "FullName", householdMember.PersonID);
            var member = db.Patient.Find(householdMember.PersonID);
            householdMember.Member = member;
            return View(householdMember);
        }

        // GET: HouseholdMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdMember householdMember = db.HouseholdMembers.Find(id);
            if (householdMember == null)
            {
                return HttpNotFound();
            }
            return View(householdMember);
        }

        // POST: HouseholdMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseholdMember householdMember = db.HouseholdMembers.Find(id);
            householdMember.Member.HouseholdProfileID = null;
            db.HouseholdMembers.Remove(householdMember);
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
