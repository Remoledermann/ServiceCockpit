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
    public class WochenrapportSpesenEintragsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WochenrapportSpesenEintrags
        public ActionResult Index()
        {
            var wochenrapportSpesenEintrag = db.WochenrapportSpesenEintrag.Include(w => w.Wochenrapport);
            return View(wochenrapportSpesenEintrag.ToList());
        }

        // GET: WochenrapportSpesenEintrags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportSpesenEintrag wochenrapportSpesenEintrag = db.WochenrapportSpesenEintrag.Find(id);
            if (wochenrapportSpesenEintrag == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapportSpesenEintrag);
        }

        // GET: WochenrapportSpesenEintrags/Create
        public ActionResult Create()
        {
            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Kalenderwoche");
            return View();
        }

        // POST: WochenrapportSpesenEintrags/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Beschreibung,Kosten,WochenrapportFK")] WochenrapportSpesenEintrag wochenrapportSpesenEintrag)
        {
            if (ModelState.IsValid)
            {
                db.WochenrapportSpesenEintrag.Add(wochenrapportSpesenEintrag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Kalenderwoche", wochenrapportSpesenEintrag.WochenrapportFK);
            return View(wochenrapportSpesenEintrag);
        }

        // GET: WochenrapportSpesenEintrags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportSpesenEintrag wochenrapportSpesenEintrag = db.WochenrapportSpesenEintrag.Find(id);
            if (wochenrapportSpesenEintrag == null)
            {
                return HttpNotFound();
            }
            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Kalenderwoche", wochenrapportSpesenEintrag.WochenrapportFK);
            return View(wochenrapportSpesenEintrag);
        }

        // POST: WochenrapportSpesenEintrags/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Beschreibung,Kosten,WochenrapportFK")] WochenrapportSpesenEintrag wochenrapportSpesenEintrag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wochenrapportSpesenEintrag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WochenrapportFK = new SelectList(db.Wochenrapport, "Id", "Kalenderwoche", wochenrapportSpesenEintrag.WochenrapportFK);
            return View(wochenrapportSpesenEintrag);
        }

        // GET: WochenrapportSpesenEintrags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WochenrapportSpesenEintrag wochenrapportSpesenEintrag = db.WochenrapportSpesenEintrag.Find(id);
            if (wochenrapportSpesenEintrag == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapportSpesenEintrag);
        }

        // POST: WochenrapportSpesenEintrags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WochenrapportSpesenEintrag wochenrapportSpesenEintrag = db.WochenrapportSpesenEintrag.Find(id);
            db.WochenrapportSpesenEintrag.Remove(wochenrapportSpesenEintrag);
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
