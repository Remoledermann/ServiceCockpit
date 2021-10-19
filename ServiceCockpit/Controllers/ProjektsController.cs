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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekt projekt = db.Projekt.Find(id);
            if (projekt == null)
            {
                return HttpNotFound();
            }
            return View(projekt);
        }

        // GET: Projekts/Create
        public ActionResult Create()
        {
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName");
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
                db.Projekt.Add(projekt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", projekt.MitarbeiterId);
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
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", projekt.MitarbeiterId);
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
                return RedirectToAction("Index");
            }
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", projekt.MitarbeiterId);
            ViewBag.OrganisationFK = new SelectList(db.Organisation, "Id", "Name", projekt.OrganisationFK);
            return View(projekt);
        }

        // GET: Projekts/Delete/5
        public ActionResult Delete(int? id)
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
