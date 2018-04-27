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
    public class ConsultationsController : Controller
    {
        
        private TeethCabEntities db = new TeethCabEntities();

        // GET: Consultations
        public ActionResult Index(string searchBy, string search)
        {
            {
                if (searchBy == "patient")
                {
                    return View(db.Consultations.Where(x => x.Patient.Nom.StartsWith(search) || search == null).ToList());
                }
                else if (searchBy == "personnel")
                {
                    return View(db.Consultations.Where(x => x.Personnel.Nom.StartsWith(search) || search == null).ToList());
                }
                else

                {
                    return View(db.Consultations.Where(x => x.Type.StartsWith(search) || search == null).ToList());
                }

            }
            var consultations = db.Consultations.Include(c => c.Patient).Include(c => c.Personnel);
            return View(consultations.ToList());
        }

        // GET: Consultations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // GET: Consultations/Create
        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom");
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom");
            return View();
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultationID,PatientID,PersonnelID,Type,Observation,DateConsultation")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Consultations.Add(consultation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom", consultation.PatientID);
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom", consultation.PersonnelID);
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom", consultation.PatientID);
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom", consultation.PersonnelID);
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsultationID,PatientID,PersonnelID,Type,Observation,DateConsultation")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Nom", consultation.PatientID);
            ViewBag.PersonnelID = new SelectList(db.Personnels, "PersonnelID", "Nom", consultation.PersonnelID);
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultation consultation = db.Consultations.Find(id);
            db.Consultations.Remove(consultation);
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
