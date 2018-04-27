using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeethCabinet.Models;


namespace TeethCabinet.Controllers
{
    [Authorize]
    public class PersonnelsController : Controller
    {
        private TeethCabEntities dbModel = new TeethCabEntities();
        // GET: Personnels

        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Fonction")
            {
                return View(dbModel.Personnels.Where(x => x.Fonction.StartsWith(search) || search == null).ToList());
            }
            else if (searchBy == "Login")
            {
                return View(dbModel.Personnels.Where(x => x.Login.StartsWith(search) || search == null).ToList());
            }

            else if (searchBy == "Email")
            {
                return View(dbModel.Personnels.Where(x => x.Email.StartsWith(search) || search == null).ToList());
            }

            else

            {
                return View(dbModel.Personnels.Where(x => x.Nom.StartsWith(search) || search == null).ToList());
            }
        }

        // GET: Personnels/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personnel personnel = dbModel.Personnels.Find(id);

            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // GET: Personnels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personnels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonnelID,Nom,Prenom,Fonction,Addresse,Email,NumTel,Login,Password,NiveauDroits")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                dbModel.Personnels.Add(personnel);
                dbModel.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = dbModel.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonnelID,Nom,Prenom,Fonction,Addresse,Email,NumTel,Login,Password,NiveauDroits")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                dbModel.Entry(personnel).State = EntityState.Modified;
                dbModel.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = dbModel.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Personnel personnel = dbModel.Personnels.Find(id);
            dbModel.Personnels.Remove(personnel);
            dbModel.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbModel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
