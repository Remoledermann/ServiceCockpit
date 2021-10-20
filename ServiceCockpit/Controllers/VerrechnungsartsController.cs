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
    public class VerrechnungsartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Verrechnungsarts
        public ActionResult Index()
        {
            return View(db.Verrechnungsart.ToList());
        }

        // GET: Verrechnungsarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verrechnungsart verrechnungsart = db.Verrechnungsart.Find(id);
            if (verrechnungsart == null)
            {
                return HttpNotFound();
            }
            return View(verrechnungsart);
        }

        // GET: Verrechnungsarts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Verrechnungsarts/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Nummer,KostenProStunde")] Verrechnungsart verrechnungsart)
        {
            if (ModelState.IsValid)
            {
                db.Verrechnungsart.Add(verrechnungsart);
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }

            return View(verrechnungsart);
        }

        // GET: Verrechnungsarts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verrechnungsart verrechnungsart = db.Verrechnungsart.Find(id);
            if (verrechnungsart == null)
            {
                return HttpNotFound();
            }
            return View(verrechnungsart);
        }

        // POST: Verrechnungsarts/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Nummer,KostenProStunde")] Verrechnungsart verrechnungsart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verrechnungsart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }
            return View(verrechnungsart);
        }

        // GET: Verrechnungsarts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verrechnungsart verrechnungsart = db.Verrechnungsart.Find(id);
            if (verrechnungsart == null)
            {
                return HttpNotFound();
            }
            return View(verrechnungsart);
        }

        // POST: Verrechnungsarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verrechnungsart verrechnungsart = db.Verrechnungsart.Find(id);
            db.Verrechnungsart.Remove(verrechnungsart);
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
