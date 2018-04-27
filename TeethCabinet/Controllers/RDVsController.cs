using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeethCabinet.Models;

namespace TeethCabinet.Controllers
{
    [Authorize]
    public class RDVsController : Controller
    {
        private TeethCabEntities db = new TeethCabEntities();

        // GET: RDVs               

        public ActionResult Index(string searchBy, string search)
        {
            var rDVs = db.RDVs.Include(r => r.Patient).Include(r => r.Personnel);

            if (searchBy == "patient")
            {
                return View(db.RDVs.Where(x => x.Patient.Nom.StartsWith(search) || search == null).ToList());
            }
            else if (searchBy == "personnel")
            {
                return View(db.RDVs.Where(x => x.Personnel.Nom.StartsWith(search) || search == null).ToList());
            }
            else

            {
                return View(db.RDVs.Where(x => x.Motif.StartsWith(search) || search == null).ToList());
            }

            //return View(rDVs.ToList());
        }

        // GET: RDVs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RDV rDV = db.RDVs.Find(id);
            if (rDV == null)
            {
                return HttpNotFound();
            }
            return View(rDV);
        }

        // GET: RDVs/Create
        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom");
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom");
            return View();
        }

        // POST: RDVs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RdvID,PatientID,PersonnelID,DateHeureRdv,Motif")] RDV rDV)
        {
            if (ModelState.IsValid)
            {
                db.RDVs.Add(rDV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom", rDV.PatientID);
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom", rDV.PersonnelID);
            return View(rDV);
        }

        // GET: RDVs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RDV rDV = db.RDVs.Find(id);
            if (rDV == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom", rDV.PatientID);
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom", rDV.PersonnelID);
            return View(rDV);
        }

        // POST: RDVs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RdvID,PatientID,PersonnelID,DateHeureRdv,Motif")] RDV rDV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rDV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom", rDV.PatientID);
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom", rDV.PersonnelID);
            return View(rDV);
        }

        // GET: RDVs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RDV rDV = db.RDVs.Find(id);
            if (rDV == null)
            {
                return HttpNotFound();
            }
            return View(rDV);
        }

        // POST: RDVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RDV rDV = db.RDVs.Find(id);
            db.RDVs.Remove(rDV);
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
