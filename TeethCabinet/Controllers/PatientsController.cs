using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeethCabinet.Models;
using System.Data;
using System.Data.Entity;
using System.Net;



namespace TeethCabinet.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private TeethCabEntities dbMod = new TeethCabEntities();
        // GET: Patients
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Sexe")
            {
                return View(dbMod.Patients.Where(x => x.Sexe.StartsWith(search) || search == null).ToList());
            }
            else if (searchBy == "NumSS")
            {
                return View(dbMod.Patients.Where(x => x.NumSS.StartsWith(search) || search == null).ToList());
            }

            else if (searchBy == "Addresse")
            {
                return View(dbMod.Patients.Where(x => x.Addresse.StartsWith(search) || search == null).ToList());
            }

            else

            {
                return View(dbMod.Patients.Where(x => x.Nom.StartsWith(search) || search == null).ToList());
            }
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = dbMod.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientID,Nom,Prenom,Addresse,Email,NumTel,DateNaissance,Sexe,NumSS")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                dbMod.Patients.Add(patient);
                dbMod.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = dbMod.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);

        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientID,Nom,Prenom,Addresse,Email,NumTel,DateNaissance,Sexe,NumSS")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                dbMod.Entry(patient).State = EntityState.Modified;
                dbMod.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = dbMod.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = dbMod.Patients.Find(id);
            dbMod.Patients.Remove(patient);
            dbMod.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbMod.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
