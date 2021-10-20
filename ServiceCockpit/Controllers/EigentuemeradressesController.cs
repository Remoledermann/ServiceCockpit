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
    public class EigentuemeradressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Eigentuemeradresses
        public ActionResult Index()
        {
            return View(db.Eigentuemeradresse.ToList());
        }

        // GET: Eigentuemeradresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eigentuemeradresse eigentuemeradresse = db.Eigentuemeradresse.Find(id);
            if (eigentuemeradresse == null)
            {
                return HttpNotFound();
            }
            return View(eigentuemeradresse);
        }

        // GET: Eigentuemeradresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eigentuemeradresses/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Vornamne,Strasse,Plz,Ort,TelefonGeschäft,TelefonPrivat")] Eigentuemeradresse eigentuemeradresse)
        {
            if (ModelState.IsValid)
            {
                db.Eigentuemeradresse.Add(eigentuemeradresse);
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }

            return View(eigentuemeradresse);
        }

        // GET: Eigentuemeradresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eigentuemeradresse eigentuemeradresse = db.Eigentuemeradresse.Find(id);
            if (eigentuemeradresse == null)
            {
                return HttpNotFound();
            }
            return View(eigentuemeradresse);
        }

        // POST: Eigentuemeradresses/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Vornamne,Strasse,Plz,Ort,TelefonGeschäft,TelefonPrivat")] Eigentuemeradresse eigentuemeradresse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eigentuemeradresse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "StammDaten");
            }
            return View(eigentuemeradresse);
        }

        // GET: Eigentuemeradresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eigentuemeradresse eigentuemeradresse = db.Eigentuemeradresse.Find(id);
            if (eigentuemeradresse == null)
            {
                return HttpNotFound();
            }
            return View(eigentuemeradresse);
        }

        // POST: Eigentuemeradresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eigentuemeradresse eigentuemeradresse = db.Eigentuemeradresse.Find(id);
            db.Eigentuemeradresse.Remove(eigentuemeradresse);
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
