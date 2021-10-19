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
    public class WochenrapportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wochenrapports
        public ActionResult Index()
        {
            var wochenrapport = db.Wochenrapport.Include(w => w.Mitarbeiter);
            return View(wochenrapport.ToList());
        }

        // GET: Wochenrapports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);
            if (wochenrapport == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapport);
        }

        // GET: Wochenrapports/Create
        public ActionResult Create()
        {
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName");
            return View();
        }

        // POST: Wochenrapports/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Kalenderwoche,StartDatum,EndDate,Status,StundenTotal,MitarbeiterId")] Wochenrapport wochenrapport)
        {
            if (ModelState.IsValid)
            {
                db.Wochenrapport.Add(wochenrapport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", wochenrapport.MitarbeiterId);
            return View(wochenrapport);
        }

        // GET: Wochenrapports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);
            if (wochenrapport == null)
            {
                return HttpNotFound();
            }
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", wochenrapport.MitarbeiterId);
            return View(wochenrapport);
        }

        // POST: Wochenrapports/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Kalenderwoche,StartDatum,EndDate,Status,StundenTotal,MitarbeiterId")] Wochenrapport wochenrapport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wochenrapport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", wochenrapport.MitarbeiterId);
            return View(wochenrapport);
        }

        // GET: Wochenrapports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);
            if (wochenrapport == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapport);
        }

        // POST: Wochenrapports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);
            db.Wochenrapport.Remove(wochenrapport);
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
