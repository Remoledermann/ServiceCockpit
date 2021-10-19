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
    public class ZeitKostensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ZeitKostens
        public ActionResult Index()
        {
            var zeitKosten = db.ZeitKosten.Include(z => z.Mitarbeiter).Include(z => z.Servicerapport).Include(z => z.Verrechnungsart).Include(z => z.ZeitKostenUeberzeitFaktor);
            return View(zeitKosten.ToList());
        }

        // GET: ZeitKostens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZeitKosten zeitKosten = db.ZeitKosten.Find(id);
            if (zeitKosten == null)
            {
                return HttpNotFound();
            }
            return View(zeitKosten);
        }

        // GET: ZeitKostens/Create
        public ActionResult Create()
        {
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName");
            ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "VoranmeldungName");
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name");
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name");
            return View();
        }

        // POST: ZeitKostens/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AnzahlStunden,AnzahlStundenTotal,KostenTotal,VerrechnungsartId,MitarbeiterId,ZeitKostenUeberzeitFaktorId,ServicerapportFK")] ZeitKosten zeitKosten)
        {
            if (ModelState.IsValid)
            {
                db.ZeitKosten.Add(zeitKosten);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", zeitKosten.MitarbeiterId);
            ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "VoranmeldungName", zeitKosten.ServicerapportFK);
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name", zeitKosten.VerrechnungsartId);
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name", zeitKosten.ZeitKostenUeberzeitFaktorId);
            return View(zeitKosten);
        }

        // GET: ZeitKostens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZeitKosten zeitKosten = db.ZeitKosten.Find(id);
            if (zeitKosten == null)
            {
                return HttpNotFound();
            }
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", zeitKosten.MitarbeiterId);
            ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "VoranmeldungName", zeitKosten.ServicerapportFK);
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name", zeitKosten.VerrechnungsartId);
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name", zeitKosten.ZeitKostenUeberzeitFaktorId);
            return View(zeitKosten);
        }

        // POST: ZeitKostens/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AnzahlStunden,AnzahlStundenTotal,KostenTotal,VerrechnungsartId,MitarbeiterId,ZeitKostenUeberzeitFaktorId,ServicerapportFK")] ZeitKosten zeitKosten)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zeitKosten).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", zeitKosten.MitarbeiterId);
            ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "VoranmeldungName", zeitKosten.ServicerapportFK);
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name", zeitKosten.VerrechnungsartId);
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name", zeitKosten.ZeitKostenUeberzeitFaktorId);
            return View(zeitKosten);
        }

        // GET: ZeitKostens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZeitKosten zeitKosten = db.ZeitKosten.Find(id);
            if (zeitKosten == null)
            {
                return HttpNotFound();
            }
            return View(zeitKosten);
        }

        // POST: ZeitKostens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZeitKosten zeitKosten = db.ZeitKosten.Find(id);
            db.ZeitKosten.Remove(zeitKosten);
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
