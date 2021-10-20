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
    public class ZeitKostenUeberzeitFaktorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ZeitKostenUeberzeitFaktors
        public ActionResult Index()
        {
            return View(db.ZeitKostenUeberzeitFaktor.ToList());
        }

        // GET: ZeitKostenUeberzeitFaktors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZeitKostenUeberzeitFaktor zeitKostenUeberzeitFaktor = db.ZeitKostenUeberzeitFaktor.Find(id);
            if (zeitKostenUeberzeitFaktor == null)
            {
                return HttpNotFound();
            }
            return View(zeitKostenUeberzeitFaktor);
        }

        // GET: ZeitKostenUeberzeitFaktors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZeitKostenUeberzeitFaktors/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Faktor")] ZeitKostenUeberzeitFaktor zeitKostenUeberzeitFaktor)
        {
            if (ModelState.IsValid)
            {
                db.ZeitKostenUeberzeitFaktor.Add(zeitKostenUeberzeitFaktor);
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }

            return View(zeitKostenUeberzeitFaktor);
        }

        // GET: ZeitKostenUeberzeitFaktors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZeitKostenUeberzeitFaktor zeitKostenUeberzeitFaktor = db.ZeitKostenUeberzeitFaktor.Find(id);
            if (zeitKostenUeberzeitFaktor == null)
            {
                return HttpNotFound();
            }
            return View(zeitKostenUeberzeitFaktor);
        }

        // POST: ZeitKostenUeberzeitFaktors/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Faktor")] ZeitKostenUeberzeitFaktor zeitKostenUeberzeitFaktor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zeitKostenUeberzeitFaktor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }
            return View(zeitKostenUeberzeitFaktor);
        }

        // GET: ZeitKostenUeberzeitFaktors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZeitKostenUeberzeitFaktor zeitKostenUeberzeitFaktor = db.ZeitKostenUeberzeitFaktor.Find(id);
            if (zeitKostenUeberzeitFaktor == null)
            {
                return HttpNotFound();
            }
            return View(zeitKostenUeberzeitFaktor);
        }

        // POST: ZeitKostenUeberzeitFaktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZeitKostenUeberzeitFaktor zeitKostenUeberzeitFaktor = db.ZeitKostenUeberzeitFaktor.Find(id);
            db.ZeitKostenUeberzeitFaktor.Remove(zeitKostenUeberzeitFaktor);
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
