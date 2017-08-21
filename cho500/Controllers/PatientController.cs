using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cho500.Entity;
using cho500.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using AutoMapper;
using cho500.Models.Patient;

namespace cho500.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Patient
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1; 
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var people = db.Patient.ToList();
            List<IndexPatientViewModel> patients = new List<IndexPatientViewModel>();
            if (people.Any())
            {
                patients = Mapper.Map<List<Person>, List<IndexPatientViewModel>>(people);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.FullName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    patients = patients.OrderByDescending(s => s.FullName).ToList();
                    break;

                default:
                    patients = patients.OrderBy(s => s.FullName).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(patients.ToPagedList(pageNumber, pageSize));
        }

        // GET: Patient/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.Patient.FindAsync(Id);
            if (person == null)
            {
                return HttpNotFound();
            }
            DetailsPatientViewModel model = new DetailsPatientViewModel();
            model = Mapper.Map<Person, DetailsPatientViewModel>(person);
            model.Father = (person.FatherPersonId > 0 ) ? db.Patient.Find(person.FatherPersonId).FullName.ToString() : "";
            model.Mother = (person.MotherPersonId > 0 ) ? db.Patient.Find(person.MotherPersonId).FullName.ToString() : "";
            model.BloodType = (person.BloodTypeID > 0 ) ? db.BloodType.Find(person.BloodTypeID).Type.ToString() : "";

            //model.ConsultationSummary.Id = person.


            return View(model);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            //PopulateBarangaysDropDownList();
            PopulateBloodTypeDropDownList();
            CreatePersonViewModel Model = new CreatePersonViewModel();
            return View(Model);
        }

        // POST: Patient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,FirstName,MiddleName,LastName,ExtensionName,NameTittle,Religion,DateOfBirth,Sex,CivilStatus,WorkSkills,FatherPersonId,MotherPersonId,HouseholdProfileID,PhilHealthNo,ContactNumber,LandlineNumber,EmergencyContactName,EmergencyContactNumberCP,EmergencyContactNumberLL,Relation,EmergencyContactBirthday,Encoder,Notes,BloodTypeID")] CreatePersonViewModel createPersonViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patient = new Person();
                    patient = Mapper.Map<CreatePersonViewModel, Person>(createPersonViewModel);
                    patient.DateCreated = DateTime.Now;
                    db.Patient.Add(patient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            //PopulateBarangaysDropDownList(createPersonViewModel.BarangayID);
            PopulateBloodTypeDropDownList(createPersonViewModel.BloodTypeID);
            return View(createPersonViewModel);
        }

        // GET: Patient/Edit/5
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
            EditPersonViewModel model = new EditPersonViewModel();
            model = Mapper.Map<Person, EditPersonViewModel>(person);
            //PopulateBarangaysDropDownList(person.BarangayID);
            PopulateBloodTypeDropDownList(person.BloodTypeID);
            return View(model);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,FirstName,MiddleName,LastName,DateOfBirth,Sex,CivilStatus,BloodTypeID,PhilHealthNo,ContactNumber,Notes")] EditPersonViewModel editPersonViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var person = Mapper.Map<EditPersonViewModel, Person>(editPersonViewModel);
                    person.DateCreated = DateTime.Now;
                    db.Entry(person).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            //PopulateBarangaysDropDownList(editPersonViewModel.BarangayID);
            PopulateBloodTypeDropDownList(editPersonViewModel.BloodTypeID);
            return View(editPersonViewModel);
        }

        // GET: Patient/Delete/5
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

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Patient.Find(id);
            db.Patient.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateBarangaysDropDownList(object selectedBarangay = null)
        {
            var barangaysQuery = from b in db.Barangays
                                 orderby b.Name
                                 select b;
            ViewBag.BarangayID = new SelectList(barangaysQuery, "BarangayID", "Name", selectedBarangay);
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
