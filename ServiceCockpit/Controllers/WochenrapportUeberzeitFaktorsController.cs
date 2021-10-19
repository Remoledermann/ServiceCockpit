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
    public class WochenrapportUeberzeitFaktorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WochenrapportUeberzeitFaktors
        public ActionResult Index()
        {
            return View(db.WochenrapportUeberzeitFaktor.ToList());
        }

        // GET: WochenrapportUeberzeitFaktors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportUeberzeitFaktor wochenrapportUeberzeitFaktor = db.WochenrapportUeberzeitFaktor.Find(id);
            if (wochenrapportUeberzeitFaktor == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapportUeberzeitFaktor);
        }

        // GET: WochenrapportUeberzeitFaktors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WochenrapportUeberzeitFaktors/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Faktor")] WochenrapportUeberzeitFaktor wochenrapportUeberzeitFaktor)
        {
            if (ModelState.IsValid)
            {
                db.WochenrapportUeberzeitFaktor.Add(wochenrapportUeberzeitFaktor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wochenrapportUeberzeitFaktor);
        }

        // GET: WochenrapportUeberzeitFaktors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportUeberzeitFaktor wochenrapportUeberzeitFaktor = db.WochenrapportUeberzeitFaktor.Find(id);
            if (wochenrapportUeberzeitFaktor == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapportUeberzeitFaktor);
        }

        // POST: WochenrapportUeberzeitFaktors/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Faktor")] WochenrapportUeberzeitFaktor wochenrapportUeberzeitFaktor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wochenrapportUeberzeitFaktor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wochenrapportUeberzeitFaktor);
        }

        // GET: WochenrapportUeberzeitFaktors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportUeberzeitFaktor wochenrapportUeberzeitFaktor = db.WochenrapportUeberzeitFaktor.Find(id);
            if (wochenrapportUeberzeitFaktor == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapportUeberzeitFaktor);
        }

        // POST: WochenrapportUeberzeitFaktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WochenrapportUeberzeitFaktor wochenrapportUeberzeitFaktor = db.WochenrapportUeberzeitFaktor.Find(id);
            db.WochenrapportUeberzeitFaktor.Remove(wochenrapportUeberzeitFaktor);
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
