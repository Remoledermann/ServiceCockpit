using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class OrganisationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Organisations
        public ActionResult Index()
        {
            return View(db.Organisation.ToList());
        }

        // GET: Organisations/Details/5
        public ActionResult Details(int? id)
        {
            List<decimal> listStunden = new List<decimal>();
            List<decimal> listeMaterial = new List<decimal>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisation.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }


            var projekt = db.Projekt.ToList().Where(s => s.OrganisationFK == id);

            foreach (var VARIABLE in projekt)
            {
                if (VARIABLE.KostenZeit == null)
                {

                }
                else
                {
                    listStunden.Add(VARIABLE.KostenZeit.Value);


                }
                if (VARIABLE.KostenMaterial == null)
                {

                }
                else
                {
                    listeMaterial.Add(VARIABLE.KostenMaterial.Value);
                }


            }
            organisation.KostenZeit = listStunden.Sum();
            organisation.KostenMaterial = listeMaterial.Sum();
            organisation.KostenTotal = organisation.KostenMaterial + organisation.KostenZeit;
            db.SaveChanges();

            return View(organisation);
        }

        // GET: Organisations/Create
        public ActionResult Create(bool IsFromOrganisations = true)
        {
            if (IsFromOrganisations)
            {
                ViewBag.ControllerNameBack = "Organisation";
            }
            else
            {
                ViewBag.ControllerNameBack = "StammDaten";
            }
            return View();
        }

        // POST: Organisations/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,KostenZeit,KostenMaterial,KostenTotal,Status")] Organisation organisation)
        {
            if (ModelState.IsValid)
            {
                if (organisation.Status == null || organisation.Status.Length < 1)
                    organisation.Status = "Offen";
                
                    
                
                db.Organisation.Add(organisation);
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }

            return View(organisation);
        }

        // GET: Organisations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisation.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }

            

            return View(organisation);
        }

        // POST: Organisations/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,KostenZeit,KostenMaterial,KostenTotal,Status")] Organisation organisation)
        {
            List<decimal> listStunden = new List<decimal>();
            List<decimal> listeMaterial = new List<decimal>();


           

            if (ModelState.IsValid)
            {
                db.Entry(organisation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }
            return View(organisation);
        }

        // GET: Organisations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisation.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }
            return View(organisation);
        }

        // POST: Organisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organisation organisation = db.Organisation.Find(id);
            db.Organisation.Remove(organisation);
            db.SaveChanges();
            return RedirectToAction("Index", "StammDaten");
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
