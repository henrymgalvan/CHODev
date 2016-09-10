using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using cho500.Entity;
using cho500.Models;
using cho500.Models.ChildRecords;

namespace cho500.Controllers
{
    public class ChildImmunizationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChildImmunizaton
        public ActionResult Index()
        {
            List<ChildImmunizationRecordViewModel> childImmunizationRecordList = new List<ChildImmunizationRecordViewModel>();
            var childImmunizationRecords = db.ChildImmunizationRecords.Include(c => c.ChildHealthRecord).Include(v => v.Vaccine).ToList();

            if (childImmunizationRecords.Any())
            {
                childImmunizationRecordList = AutoMapper.Mapper.Map<List<ChildImmunizationRecord>, List<ChildImmunizationRecordViewModel>>(childImmunizationRecords);               
            }

            return View(childImmunizationRecordList);
        }

        // GET: ChildImmunizaton/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildImmunizationRecord childImmunizationRecord = db.ChildImmunizationRecords.Find(id);
            if (childImmunizationRecord == null)
            {
                return HttpNotFound();
            }
            var childImmunizationRecordViewModel = new ChildImmunizationRecordViewModel();
            childImmunizationRecordViewModel = AutoMapper.Mapper.Map<ChildImmunizationRecord, ChildImmunizationRecordViewModel>(childImmunizationRecord);
            return View(childImmunizationRecordViewModel);
        }

        // GET: ChildImmunizaton/Create
        public ActionResult Create(int? patientID)
        {
            if (patientID == null || patientID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ChildBirthCreateImmunizationVM = new ChildImmunizationRecordCreateViewModel();
            ChildBirthCreateImmunizationVM.PersonID = (int)patientID;
            ChildBirthCreateImmunizationVM.ChildHealthRecord = db.ChildHealthRecord.Find(patientID);
            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery");
            PopulateVaccinesDropDownList();
            return View(ChildBirthCreateImmunizationVM);
        }

        // POST: ChildImmunizaton/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VaccineID,First,Second,Third,Booster1,Booster2,Booster3,Reaction,PersonID")] ChildImmunizationRecordCreateViewModel childImmunizationRecordCreateVM)
        {
            if (ModelState.IsValid)
            {
                ChildImmunizationRecord childImmunizationRecord = new ChildImmunizationRecord();
                childImmunizationRecord = AutoMapper.Mapper.Map<ChildImmunizationRecordCreateViewModel, ChildImmunizationRecord>(childImmunizationRecordCreateVM);
                db.ChildImmunizationRecords.Add(childImmunizationRecord);
                db.SaveChanges();
                return RedirectToAction("Details", "ChildRecord", new { id = childImmunizationRecord.PersonID });
            }

            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", childImmunizationRecordCreateVM.PersonID);
            return View(childImmunizationRecordCreateVM);
        }

        // GET: ChildImmunizaton/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildImmunizationRecord childImmunizationRecord = db.ChildImmunizationRecords.Find(id);
            if (childImmunizationRecord == null)
            {
                return HttpNotFound();
            }
            var childImmunizationRecordEditViewModel = new ChildImmunizationRecordEditViewModel();
            childImmunizationRecordEditViewModel = AutoMapper.Mapper.Map<ChildImmunizationRecord, ChildImmunizationRecordEditViewModel>(childImmunizationRecord);

            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", childImmunizationRecord.PersonID);
            PopulateVaccinesDropDownList(childImmunizationRecord.VaccineID);
            return View(childImmunizationRecordEditViewModel);
        }

        // POST: ChildImmunizaton/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VaccineID,First,Second,Third,Booster1,Booster2,Booster3,Reaction,PersonID")] ChildImmunizationRecordEditViewModel childImmunizationRecordEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var childImmunizationRecord = AutoMapper.Mapper.Map<ChildImmunizationRecordEditViewModel, ChildImmunizationRecord >(childImmunizationRecordEditViewModel);
                db.Entry(childImmunizationRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","ChildRecord", new { id=childImmunizationRecord.PersonID});
            }
            //ViewBag.PersonID = new SelectList(db.ChildHealthRecord, "PersonID", "TypeOfDelivery", childImmunizatonRecord.PersonID);
            return View(childImmunizationRecordEditViewModel);
        }

        // GET: ChildImmunizaton/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildImmunizationRecord childImmunizationRecord = db.ChildImmunizationRecords.Find(id);
            if (childImmunizationRecord == null)
            {
                return HttpNotFound();
            }
            return View(childImmunizationRecord);
        }

        // POST: ChildImmunizaton/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChildImmunizationRecord childImmunizatonRecord = db.ChildImmunizationRecords.Find(id);
            db.ChildImmunizationRecords.Remove(childImmunizatonRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateVaccinesDropDownList(object selectedVaccine = null)
        {
            var VaccinesQuery = from v in db.Vaccines
                                  orderby v.Name
                                  select v;
            ViewBag.VaccineID = new SelectList(VaccinesQuery, "VaccineID", "Name", selectedVaccine);
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
