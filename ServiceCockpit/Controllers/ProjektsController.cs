using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceCockpit.Migrations;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class ProjektsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projekts
        public ActionResult Index()
        {
            var projekt = db.Projekt.Include(p => p.Mitarbeiter).Include(p => p.Organisation);
            return View(projekt.ToList());
        }

        // GET: Projekts/Details/5
        public ActionResult Details(int? id)
        {



            List<decimal> listStunden = new List<decimal>();
            List<decimal> listeMaterial = new List<decimal>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekt projekt = db.Projekt.Find(id);
            if (projekt == null)
            {
                return HttpNotFound();
            }
            else
            {
               


                var rapport = db.Servicerapport.ToList().Where(s => s.ProjektFK == id);

                foreach (var VARIABLE in rapport)
                {
                    if (VARIABLE.KostenZeit == null)
                    {
                        
                    }
                    else
                    {
                        listStunden.Add(VARIABLE.KostenZeit.Value);
                        
                        
                    }
                    if(VARIABLE.KostenMaterial == null)
                    {
                        
                    }
                    else
                    {
                        listeMaterial.Add(VARIABLE.KostenMaterial.Value);
                    }
                        
                   
                }
            }

            projekt.KostenZeit = listStunden.Sum();
            projekt.KostenMaterial = listeMaterial.Sum();
            projekt.KostenTotal = projekt.KostenMaterial + projekt.KostenZeit;

            db.SaveChanges();

            return View(projekt);
        }

        // GET: Projekts/Create
        public ActionResult Create(bool IsFromProjekts = true)
        {
            if (IsFromProjekts)
            {
                ViewBag.ControllerNameBack = "Projekts";
            }
            else
            {
                ViewBag.ControllerNameBack = "StammDaten";
            }

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName");
            ViewBag.OrganisationFK = new SelectList(db.Organisation, "Id", "Name");
            return View();
        }

        // POST: Projekts/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Nummer,Status,KostenZeit,KostenMaterial,KostenTotal,MitarbeiterId,OrganisationFK")] Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                if (projekt.Status == null || projekt.Status.Length < 1)
                    projekt.Status = "Offen";
                db.Projekt.Add(projekt);
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", projekt.MitarbeiterId);
            ViewBag.OrganisationFK = new SelectList(db.Organisation, "Id", "Name", projekt.OrganisationFK);
            return View(projekt);
        }

        // GET: Projekts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekt projekt = db.Projekt.Find(id);
            if (projekt == null)
            {
                return HttpNotFound();
            }

            List<decimal> listStunden = new List<decimal>();
            List<decimal> listeMaterial = new List<decimal>();


                var rapport = db.Servicerapport.ToList().Where(s => s.ProjektFK == id);

                foreach (var VARIABLE in rapport)
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
            

            projekt.KostenZeit = listStunden.Sum();
            projekt.KostenMaterial = listeMaterial.Sum();
            projekt.KostenTotal = projekt.KostenMaterial + projekt.KostenZeit;

            db.SaveChanges();
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", projekt.MitarbeiterId);
            ViewBag.OrganisationFK = new SelectList(db.Organisation, "Id", "Name", projekt.OrganisationFK);


            return View(projekt);
        }

        // POST: Projekts/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Nummer,Status,KostenZeit,KostenMaterial,KostenTotal,MitarbeiterId,OrganisationFK")] Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projekt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", projekt.MitarbeiterId);
            ViewBag.OrganisationFK = new SelectList(db.Organisation, "Id", "Name", projekt.OrganisationFK);
            return View(projekt);
        }

        // GET: Projekts/Delete/5
        public ActionResult Delete(int? id)
        {
            List<decimal> listStunden = new List<decimal>();
            List<decimal> listeMaterial = new List<decimal>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekt projekt = db.Projekt.Find(id);
            if (projekt == null)
            {
                return HttpNotFound();
            }
            else
            {



                var rapport = db.Servicerapport.ToList().Where(s => s.ProjektFK == id);

                foreach (var VARIABLE in rapport)
                {
                    listStunden.Add(VARIABLE.KostenZeit.Value);
                    listeMaterial.Add(VARIABLE.KostenMaterial.Value);
                }
            }

            projekt.KostenZeit = listStunden.Sum();
            projekt.KostenMaterial = listeMaterial.Sum();
            projekt.KostenTotal = projekt.KostenMaterial + projekt.KostenZeit;

            db.SaveChanges();

            return View(projekt);
        }

        // POST: Projekts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projekt projekt = db.Projekt.Find(id);
            db.Projekt.Remove(projekt);
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
