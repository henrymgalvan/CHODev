﻿using System;
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
    public class VaccinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vaccines
        public ActionResult Index()
        {
            return View(db.Vaccines.ToList());
        }

        // GET: Vaccines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccine vaccine = db.Vaccines.Find(id);
            if (vaccine == null)
            {
                return HttpNotFound();
            }
            return View(vaccine);
        }

        // GET: Vaccines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VaccineID,Name,Notes")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                db.Vaccines.Add(vaccine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaccine);
        }

        // GET: Vaccines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccine vaccine = db.Vaccines.Find(id);
            if (vaccine == null)
            {
                return HttpNotFound();
            }
            return View(vaccine);
        }

        // POST: Vaccines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VaccineID,Name,Notes")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaccine);
        }

        // GET: Vaccines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccine vaccine = db.Vaccines.Find(id);
            if (vaccine == null)
            {
                return HttpNotFound();
            }
            return View(vaccine);
        }

        // POST: Vaccines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vaccine vaccine = db.Vaccines.Find(id);
            db.Vaccines.Remove(vaccine);
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
