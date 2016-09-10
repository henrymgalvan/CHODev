using System;
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
    public class ChildRecordController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChildRecord
        public ActionResult Index()
        {
            var childHealthRecords = db.ChildHealthRecord.ToList();
            List<ChildBirthHistoryIndexViewModel> childBirthHistoryIndexViewModels = new List<ChildBirthHistoryIndexViewModel>();
            if (childHealthRecords.Any())
            {
                foreach (var c in childHealthRecords)
                {
                    if (c != null)
                    {
                        var BDate = db.Patient.First(x => x.PersonID == c.PersonID).DateOfBirth;
                        childBirthHistoryIndexViewModels.Add(new ChildBirthHistoryIndexViewModel()
                        {
                            PatientId = c.PersonID,
                            PatientName = db.Patient.First(x => x.PersonID == c.PersonID).FullName,
                            BirthDate = BDate.HasValue ? BDate.Value.ToString("yyyy-MM-dd") : "NoDate",
                            //BloodType = c.BloodType.Type,
                            Months = c.Months,
                            Weeks = c.Weeks,
                            Days = c.Days,
                            TypeOfDelivery = c.TypeOfDelivery,
                            BirthWeightInPounds = c.BirthWeightInPounds,
                            BirthLength = c.BirthLength,
                            HeadCircumference = c.HeadCircumference,
                            ChestCircumference = c.ChestCircumference,
                            AbdominalCircumference = c.AbdominalCircumference
                        });
                    }
                }
            }
            return View(childBirthHistoryIndexViewModels);
        }

        // GET: ChildRecord/Details/5
        public ActionResult Details(int? id)
        {
            ChildHealthRecord childRecordDetail = db.ChildHealthRecord.Find(id);
            if (childRecordDetail == null)
            {
                return RedirectToAction("Create", new { PatientID = id });
            }
            else
            {
                var BDate = db.Patient.FirstOrDefault(x => x.PersonID == id).DateOfBirth;
                ChildBirthHistoryRecordDetailsViewModel childBirthHistoryRecordDetailsViewModel = new ChildBirthHistoryRecordDetailsViewModel();
                childBirthHistoryRecordDetailsViewModel.PatientId = childRecordDetail.PersonID;
                childBirthHistoryRecordDetailsViewModel.PatientName = db.Patient.First(x => x.PersonID == childRecordDetail.PersonID).FullName;
                childBirthHistoryRecordDetailsViewModel.BirthDate = BDate.HasValue ? BDate.Value.ToString("yyyy-MM-dd") : "NoDate";
                //childBirthHistoryRecordDetailsViewModel.BloodType = db.BloodType.Find(childRecordDetail.BloodTypeID).Type;
                childBirthHistoryRecordDetailsViewModel.Months = childRecordDetail.Months;
                childBirthHistoryRecordDetailsViewModel.Weeks = childRecordDetail.Weeks;
                childBirthHistoryRecordDetailsViewModel.Days = childRecordDetail.Days;
                childBirthHistoryRecordDetailsViewModel.TypeOfDelivery = childRecordDetail.TypeOfDelivery;
                childBirthHistoryRecordDetailsViewModel.BirthWeightInPounds = childRecordDetail.BirthWeightInPounds;
                childBirthHistoryRecordDetailsViewModel.BirthLength = childRecordDetail.BirthLength;
                childBirthHistoryRecordDetailsViewModel.HeadCircumference = childRecordDetail.HeadCircumference;
                childBirthHistoryRecordDetailsViewModel.ChestCircumference = childRecordDetail.ChestCircumference;
                childBirthHistoryRecordDetailsViewModel.AbdominalCircumference = childRecordDetail.AbdominalCircumference;

                //childBirthHistoryRecordDetailsViewModel.ChildImmunizationRecordsViewModel = new List<ChildImmunizationRecordViewModel>();
                List<ChildBirthFollowUpVisit> cbFollowUpVisitList = db.ChildBirthFollowUpVisits.Where(v => v.PersonID == id).ToList();
                var cbFollowUpVisitViewModelList = new List<ChildBirthFollowUpVisitViewModel>();
                if (cbFollowUpVisitList.Any())
                {
                    foreach (var v in cbFollowUpVisitList)
                    {
                        cbFollowUpVisitViewModelList.Add(new ChildBirthFollowUpVisitViewModel()
                        {
                            Id = v.Id,
                            AgeInWeeks = v.AgeInWeeks,
                            DateOfFollowUp = v.DateOfFollowup.HasValue ? v.DateOfFollowup.Value.ToString("MM-dd-yyyy") : "No Date",
                            Weight = v.Weight,
                            Height = v.Height,
                            Physician = db.Physicians.Find(v.PhysicianID).Name,
                            Diagnosis = v.Diagnosis,
                            Notes = v.Notes
                        });
                    }
                }
                childBirthHistoryRecordDetailsViewModel.ChildBirthFollowUpVisitsViewModel = cbFollowUpVisitViewModelList;

                List<ChildImmunizationRecord> cImmunizationRecord = db.ChildImmunizationRecords.Where(v => v.PersonID == id).ToList();
                var cImmunizationRecordViewModelList = new List<ChildImmunizationRecordViewModel>();
                if (cImmunizationRecord.Any())
                {
                    foreach (var i in cImmunizationRecord)
                        cImmunizationRecordViewModelList.Add(new ChildImmunizationRecordViewModel()
                        {
                            Id = i.Id,
                            VaccineName = i.Vaccine.Name,
                            First = i.First.HasValue ? i.First.Value.ToString("MM-dd-yyyy") : "No Date",
                            Second = i.Second.HasValue ? i.Second.Value.ToString("MM-dd-yyyy") : "No Date",
                            Third = i.Third.HasValue ? i.Third.Value.ToString("MM-dd-yyyy") : "No Date",
                            Booster1 = i.Booster1.HasValue ? i.Booster1.Value.ToString("MM-dd-yyyy") : "No Date",
                            Booster2 = i.Booster2.HasValue ? i.Booster2.Value.ToString("MM-dd-yyyy") : "No Date",
                            Booster3 = i.Booster3.HasValue ? i.Booster3.Value.ToString("MM-dd-yyyy") : "No Date",
                            Reaction=i.Reaction
                        });
                }
                childBirthHistoryRecordDetailsViewModel.ChildImmunizationRecordsViewModel = cImmunizationRecordViewModelList;
                return View(childBirthHistoryRecordDetailsViewModel);
            }
        }

        // GET: ChildRecord/Create
        public ActionResult Create(int PatientID)
        {
            PopulateBloodTypeDropDownList();
            var childRecord = db.ChildHealthRecord.Find(PatientID);
            if (childRecord != null)
            {
                return RedirectToAction("Edit", new { id = childRecord.PersonID });
            }
            ChildHealthRecordCreateViewModel childHealthRecordCreateViewModel = new ChildHealthRecordCreateViewModel();
            var patient = db.Patient.Find(PatientID);
            childHealthRecordCreateViewModel.PatientID = PatientID;
            childHealthRecordCreateViewModel.PatientName = patient.FullName;
            childHealthRecordCreateViewModel.BirthDate = (DateTime)patient.DateOfBirth;
            return View(childHealthRecordCreateViewModel);
        }

        // POST: ChildRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public ActionResult Create([Bind(Include = "PatientID,PatientName,BirthDate,Months,Weeks,Days,TypeOfDelivery,BirthWeightInPounds,BirthLength,HeadCircumference,ChestCircumference,AbdominalCircumference,BloodType")] ChildHealthRecordCreateViewModel childHealthRecordCreateViewModel)
        //public ActionResult Create([Bind(Include = "PatientID,PatientName,BirthDate")] ChildHealthRecordCreateViewModel childHealthRecordCreateViewModel)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientID,PatientName,BirthDate,Months,Weeks,Days,TypeOfDelivery,BirthWeightInPounds,BirthLength,HeadCircumference,ChestCircumference,AbdominalCircumference,BloodTypeID")] ChildHealthRecordCreateViewModel childHealthRecordCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                ChildHealthRecord childHealthRecord = new ChildHealthRecord();
                childHealthRecord.PersonID = childHealthRecordCreateViewModel.PatientID;
                childHealthRecord.Months = childHealthRecordCreateViewModel.Months;
                childHealthRecord.Weeks = childHealthRecordCreateViewModel.Weeks;
                childHealthRecord.Days = childHealthRecordCreateViewModel.Days;
                childHealthRecord.TypeOfDelivery = childHealthRecordCreateViewModel.TypeOfDelivery;
                childHealthRecord.BirthWeightInPounds = childHealthRecordCreateViewModel.BirthWeightInPounds;
                childHealthRecord.BirthLength = childHealthRecordCreateViewModel.BirthLength;
                childHealthRecord.HeadCircumference = childHealthRecordCreateViewModel.HeadCircumference;
                childHealthRecord.ChestCircumference = childHealthRecordCreateViewModel.ChestCircumference;
                childHealthRecord.AbdominalCircumference = childHealthRecordCreateViewModel.AbdominalCircumference;
                //childHealthRecord.BloodTypeID = childHealthRecordCreateViewModel.BloodTypeID;

                db.ChildHealthRecord.Add(childHealthRecord);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = childHealthRecordCreateViewModel.PatientID });
            }
            return View(childHealthRecordCreateViewModel);
        }

        // GET: ChildRecord/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildHealthRecord childHealthRecord = db.ChildHealthRecord.Find(id);
            if (childHealthRecord == null)
            {
                return HttpNotFound();
            }
            ChildBirthHistoryEditViewModel childBirthHistoryEditViewModel = new ChildBirthHistoryEditViewModel();
            childBirthHistoryEditViewModel.PatientID = childHealthRecord.PersonID;
            childBirthHistoryEditViewModel.PatientName = db.Patient.Find(childHealthRecord.PersonID).FullName;
            childBirthHistoryEditViewModel.BirthDate = (DateTime)db.Patient.Find(childHealthRecord.PersonID).DateOfBirth;
            //childBirthHistoryEditViewModel.BloodTypeID = (int)childHealthRecord.BloodTypeID;
            childBirthHistoryEditViewModel.Months = childHealthRecord.Months;
            childBirthHistoryEditViewModel.Weeks = childHealthRecord.Weeks;
            childBirthHistoryEditViewModel.Days = childHealthRecord.Days;
            childBirthHistoryEditViewModel.TypeOfDelivery = childHealthRecord.TypeOfDelivery;
            childBirthHistoryEditViewModel.BirthWeightInPounds = childHealthRecord.BirthWeightInPounds;
            childBirthHistoryEditViewModel.BirthLength = childHealthRecord.BirthLength;
            childBirthHistoryEditViewModel.HeadCircumference = childHealthRecord.HeadCircumference;
            childBirthHistoryEditViewModel.ChestCircumference = childHealthRecord.ChestCircumference;
            childBirthHistoryEditViewModel.AbdominalCircumference = childHealthRecord.AbdominalCircumference;

            //PopulateBloodTypeDropDownList(childHealthRecord.BloodTypeID);
            return View(childBirthHistoryEditViewModel);
            // ViewBag.ChildBirthHistoryID = new SelectList(db.ChildBirthHistories, "Id", "TypeOfDelivery", childHealthRecord.ChildBirthHistoryId);
            //return View(childHealthRecord);
        }

        // POST: ChildRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientID,PatientName,BirthDate,BloodTypeID,Months,Weeks,Days,TypeOfDelivery,BirthWeightInPounds,BirthLength,HeadCircumference,ChestCircumference,AbdominalCircumference")] ChildBirthHistoryEditViewModel childBirthHistoryEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var childHealthRecord = new ChildHealthRecord();
                childHealthRecord.PersonID = childBirthHistoryEditViewModel.PatientID;
                childHealthRecord.Months = childBirthHistoryEditViewModel.Months;
                childHealthRecord.Weeks = childBirthHistoryEditViewModel.Weeks;
                childHealthRecord.Days = childBirthHistoryEditViewModel.Days;
                childHealthRecord.TypeOfDelivery = childBirthHistoryEditViewModel.TypeOfDelivery;
                childHealthRecord.BirthWeightInPounds = childBirthHistoryEditViewModel.BirthWeightInPounds;
                childHealthRecord.BirthLength = childBirthHistoryEditViewModel.BirthLength;
                childHealthRecord.HeadCircumference = childBirthHistoryEditViewModel.HeadCircumference;
                childHealthRecord.ChestCircumference = childBirthHistoryEditViewModel.ChestCircumference;
                childHealthRecord.AbdominalCircumference = childBirthHistoryEditViewModel.AbdominalCircumference;
                //childHealthRecord.BloodTypeID = childBirthHistoryEditViewModel.BloodTypeID;

                db.Entry(childHealthRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = childBirthHistoryEditViewModel.PatientID });
            }
            // ViewBag.ChildBirthHistoryID = new SelectList(db.ChildBirthHistories, "Id", "TypeOfDelivery", childHealthRecord.ChildBirthHistoryId);
            return View(childBirthHistoryEditViewModel);
        }

        // GET: ChildRecord/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildHealthRecord childHealthRecord = db.ChildHealthRecord.Find(id);
            if (childHealthRecord == null)
            {
                return HttpNotFound();
            }
            return View(childHealthRecord);
        }

        // POST: ChildRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChildHealthRecord childHealthRecord = db.ChildHealthRecord.Find(id);
            db.ChildHealthRecord.Remove(childHealthRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateBloodTypeDropDownList(object selectedBloodType = null)
        {
            var BloodTypeQuery = from bt in db.BloodType
                                 orderby bt.Type
                                 select bt;
            ViewBag.BloodTypeID = new SelectList(BloodTypeQuery, "BloodTypeID", "Type", selectedBloodType);
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
