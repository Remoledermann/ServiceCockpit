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
    public class MitarbeitersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mitarbeiters
        public ActionResult Index()
        {
            return View(db.Mitarbeiter.ToList());
        }

        // GET: Mitarbeiters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mitarbeiters/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VorName,NachName,TelefonGeschäft,TelefonPrivat,Kürzel,GfeNummer,Email")] Mitarbeiter mitarbeiter)
        {
            if (ModelState.IsValid)
            {
                db.Mitarbeiter.Add(mitarbeiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VorName,NachName,TelefonGeschäft,TelefonPrivat,Kürzel,GfeNummer,Email")] Mitarbeiter mitarbeiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mitarbeiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mitarbeiter mitarbeiter = db.Mitarbeiter.Find(id);
            db.Mitarbeiter.Remove(mitarbeiter);
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
