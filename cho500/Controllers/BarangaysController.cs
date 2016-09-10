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
    public class BarangaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Barangays
        public ActionResult Index()
        {
            return View(db.Barangays.ToList());
        }

        // GET: Barangays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barangay barangay = db.Barangays.Find(id);
            if (barangay == null)
            {
                return HttpNotFound();
            }
            return View(barangay);
        }

        // GET: Barangays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Barangays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarangayID,Name")] Barangay barangay)
        {
            if (ModelState.IsValid)
            {
                db.Barangays.Add(barangay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(barangay);
        }

        // GET: Barangays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barangay barangay = db.Barangays.Find(id);
            if (barangay == null)
            {
                return HttpNotFound();
            }
            return View(barangay);
        }

        // POST: Barangays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarangayID,Name")] Barangay barangay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barangay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(barangay);
        }

        // GET: Barangays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barangay barangay = db.Barangays.Find(id);
            if (barangay == null)
            {
                return HttpNotFound();
            }
            return View(barangay);
        }

        // POST: Barangays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Barangay barangay = db.Barangays.Find(id);
            db.Barangays.Remove(barangay);
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
