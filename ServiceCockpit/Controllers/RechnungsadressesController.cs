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
    public class RechnungsadressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rechnungsadresses
        public ActionResult Index()
        {
            return View(db.Rechnungsadresse.ToList());
        }

        // GET: Rechnungsadresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechnungsadresse rechnungsadresse = db.Rechnungsadresse.Find(id);
            if (rechnungsadresse == null)
            {
                return HttpNotFound();
            }
            return View(rechnungsadresse);
        }

        // GET: Rechnungsadresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rechnungsadresses/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Vornamne,Strasse,Plz,Ort,TelefonGeschäft,TelefonPrivat")] Rechnungsadresse rechnungsadresse)
        {
            if (ModelState.IsValid)
            {
                db.Rechnungsadresse.Add(rechnungsadresse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rechnungsadresse);
        }

        // GET: Rechnungsadresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechnungsadresse rechnungsadresse = db.Rechnungsadresse.Find(id);
            if (rechnungsadresse == null)
            {
                return HttpNotFound();
            }
            return View(rechnungsadresse);
        }

        // POST: Rechnungsadresses/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Vornamne,Strasse,Plz,Ort,TelefonGeschäft,TelefonPrivat")] Rechnungsadresse rechnungsadresse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rechnungsadresse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rechnungsadresse);
        }

        // GET: Rechnungsadresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechnungsadresse rechnungsadresse = db.Rechnungsadresse.Find(id);
            if (rechnungsadresse == null)
            {
                return HttpNotFound();
            }
            return View(rechnungsadresse);
        }

        // POST: Rechnungsadresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rechnungsadresse rechnungsadresse = db.Rechnungsadresse.Find(id);
            db.Rechnungsadresse.Remove(rechnungsadresse);
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
