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
    public class WochenrapportZeitEintragsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WochenrapportZeitEintrags
        public ActionResult Index()
        {
            var wochenrapportZeitEintrag = db.WochenrapportZeitEintrag.Include(w => w.Wochenrapport);
            return View(wochenrapportZeitEintrag.ToList());
        }

        // GET: WochenrapportZeitEintrags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportZeitEintrag wochenrapportZeitEintrag = db.WochenrapportZeitEintrag.Find(id);
            if (wochenrapportZeitEintrag == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapportZeitEintrag);
        }

        // GET: WochenrapportZeitEintrags/Create
        public ActionResult Create()
        {
            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Anzeige");
            return View();
        }

        // POST: WochenrapportZeitEintrags/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Datum,ProjektnNummer,ServicrapportNummer,Ausführungsadresse,Zeit,WochenrapportFK")] WochenrapportZeitEintrag wochenrapportZeitEintrag)
        {
            if (ModelState.IsValid)
            {

                db.WochenrapportZeitEintrag.Add(wochenrapportZeitEintrag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Anzeige", wochenrapportZeitEintrag.WochenrapportFK);
            return View(wochenrapportZeitEintrag);
        }

        // GET: WochenrapportZeitEintrags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportZeitEintrag wochenrapportZeitEintrag = db.WochenrapportZeitEintrag.Find(id);
            if (wochenrapportZeitEintrag == null)
            {
                return HttpNotFound();
            }
            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Anzeige", wochenrapportZeitEintrag.WochenrapportFK);
            return View(wochenrapportZeitEintrag);
        }

        // POST: WochenrapportZeitEintrags/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Datum,ProjektnNummer,ServicrapportNummer,Ausführungsadresse,Zeit,WochenrapportFK")] WochenrapportZeitEintrag wochenrapportZeitEintrag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wochenrapportZeitEintrag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Anzeige", wochenrapportZeitEintrag.WochenrapportFK);
            return View(wochenrapportZeitEintrag);
        }

        // GET: WochenrapportZeitEintrags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportZeitEintrag wochenrapportZeitEintrag = db.WochenrapportZeitEintrag.Find(id);
            if (wochenrapportZeitEintrag == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapportZeitEintrag);
        }

        // POST: WochenrapportZeitEintrags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WochenrapportZeitEintrag wochenrapportZeitEintrag = db.WochenrapportZeitEintrag.Find(id);
            db.WochenrapportZeitEintrag.Remove(wochenrapportZeitEintrag);
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
