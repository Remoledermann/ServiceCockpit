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
    public class AusführungsadressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ausführungsadresses
        public ActionResult Index()
        {
            return View(db.Ausführungsadresse.ToList());
        }

        // GET: Ausführungsadresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ausführungsadresse ausführungsadresse = db.Ausführungsadresse.Find(id);
            if (ausführungsadresse == null)
            {
                return HttpNotFound();
            }
            return View(ausführungsadresse);
        }

        // GET: Ausführungsadresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ausführungsadresses/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Vornamne,Strasse,Plz,Ort,TelefonGeschäft,TelefonPrivat")] Ausführungsadresse ausführungsadresse)
        {
            if (ModelState.IsValid)
            {
                db.Ausführungsadresse.Add(ausführungsadresse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ausführungsadresse);
        }

        // GET: Ausführungsadresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ausführungsadresse ausführungsadresse = db.Ausführungsadresse.Find(id);
            if (ausführungsadresse == null)
            {
                return HttpNotFound();
            }
            return View(ausführungsadresse);
        }

        // POST: Ausführungsadresses/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Vornamne,Strasse,Plz,Ort,TelefonGeschäft,TelefonPrivat")] Ausführungsadresse ausführungsadresse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ausführungsadresse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ausführungsadresse);
        }

        // GET: Ausführungsadresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ausführungsadresse ausführungsadresse = db.Ausführungsadresse.Find(id);
            if (ausführungsadresse == null)
            {
                return HttpNotFound();
            }
            return View(ausführungsadresse);
        }

        // POST: Ausführungsadresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ausführungsadresse ausführungsadresse = db.Ausführungsadresse.Find(id);
            db.Ausführungsadresse.Remove(ausführungsadresse);
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
