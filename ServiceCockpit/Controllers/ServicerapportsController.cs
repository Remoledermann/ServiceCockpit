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
    public class ServicerapportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Servicerapports
        public ActionResult Index()
        {
            var servicerapport = db.Servicerapport.Include(s => s.Ausführungsadresse).Include(s => s.Eigentuemeradresse).Include(s => s.Mitarbeiter).Include(s => s.Projekt).Include(s => s.Rechnungsadresse);
            return View(servicerapport.ToList());
        }

        // GET: Servicerapports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicerapport servicerapport = db.Servicerapport.Find(id);
            if (servicerapport == null)
            {
                return HttpNotFound();
            }
            return View(servicerapport);
        }

        // GET: Servicerapports/Create
        public ActionResult Create()
        {
            ViewBag.AusführungsadresseId = new SelectList(db.Ausführungsadresse, "Id", "Anzeigeadresse");
            ViewBag.EigentuemeradresseId = new SelectList(db.Eigentuemeradresse, "Id", "Anzeigeadresse");
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName");
            ViewBag.ProjektFK = new SelectList(db.Projekt, "Id", "Nummer");
            ViewBag.RechnungsadresseId = new SelectList(db.Rechnungsadresse, "Id", "Anzeigeadresse");
            return View();
        }

        // POST: Servicerapports/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KundenTerminZeit,RapportAbgechlossenZeit,VoranmeldungName,VoranmeldungNummer,Status,Beschreibung,Bemerkung,Unterschrift,KostenZeit,KostenMaterial,KostenTotal,EigentuemeradresseId,AusführungsadresseId,RechnungsadresseId,MitarbeiterId,ProjektFK")] Servicerapport servicerapport)
        {
            if (ModelState.IsValid)
            {
                if (servicerapport.MitarbeiterId == 3)
                {
                    servicerapport.Status = "Offen";
                }
                else
                {
                    servicerapport.Status = "Bearbeiten";
                }
                db.Servicerapport.Add(servicerapport);
                db.SaveChanges();
                return RedirectToAction("Index", "ServicerapportDashboards");
            }

            ViewBag.AusführungsadresseId = new SelectList(db.Ausführungsadresse, "Id", "Name", servicerapport.AusführungsadresseId);
            ViewBag.EigentuemeradresseId = new SelectList(db.Eigentuemeradresse, "Id", "Name", servicerapport.EigentuemeradresseId);
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", servicerapport.MitarbeiterId);
            ViewBag.ProjektFK = new SelectList(db.Projekt, "Id", "Name", servicerapport.ProjektFK);
            ViewBag.RechnungsadresseId = new SelectList(db.Rechnungsadresse, "Id", "Name", servicerapport.RechnungsadresseId);
            return View(servicerapport);
        }

        // GET: Servicerapports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicerapport servicerapport = db.Servicerapport.Find(id);
            if (servicerapport == null)
            {
                return HttpNotFound();
            }
            ViewBag.AusführungsadresseId = new SelectList(db.Ausführungsadresse, "Id", "Name", servicerapport.AusführungsadresseId);
            ViewBag.EigentuemeradresseId = new SelectList(db.Eigentuemeradresse, "Id", "Name", servicerapport.EigentuemeradresseId);
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", servicerapport.MitarbeiterId);
            ViewBag.ProjektFK = new SelectList(db.Projekt, "Id", "Name", servicerapport.ProjektFK);
            ViewBag.RechnungsadresseId = new SelectList(db.Rechnungsadresse, "Id", "Name", servicerapport.RechnungsadresseId);

            var zeitKosten = db.ZeitKosten.Include(z => z.Mitarbeiter).Include(z => z.Servicerapport).Include(z => z.Verrechnungsart).Include(z => z.ZeitKostenUeberzeitFaktor).Where(z=>servicerapport.Id == id);
            servicerapport.ZeitKosten = zeitKosten.ToList();

            var materialKosten = db.MaterialKosten.Include(m => m.Servicerapport).Include(m => m.Material);
            servicerapport.MaterialKosten = materialKosten.ToList();

            return View(servicerapport);
        }

        // POST: Servicerapports/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KundenTerminZeit,RapportAbgechlossenZeit,VoranmeldungName,VoranmeldungNummer,Status,Beschreibung,Bemerkung,Unterschrift,KostenZeit,KostenMaterial,KostenTotal,EigentuemeradresseId,AusführungsadresseId,RechnungsadresseId,MitarbeiterId,ProjektFK")] Servicerapport servicerapport)
        {
            if (ModelState.IsValid)
            {
                if (servicerapport.MitarbeiterId == 3)
                {
                    servicerapport.Status = "Offen";
                }
                else
                {
                    servicerapport.Status = "Bearbeiten";
                }

                db.Entry(servicerapport).State = EntityState.Modified;
             
                db.SaveChanges();
                return RedirectToAction("Index", "ServicerapportDashboards");
            }
            ViewBag.AusführungsadresseId = new SelectList(db.Ausführungsadresse, "Id", "Name", servicerapport.AusführungsadresseId);
            ViewBag.EigentuemeradresseId = new SelectList(db.Eigentuemeradresse, "Id", "Name", servicerapport.EigentuemeradresseId);
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VorName", servicerapport.MitarbeiterId);
            ViewBag.ProjektFK = new SelectList(db.Projekt, "Id", "Name", servicerapport.ProjektFK);
            ViewBag.RechnungsadresseId = new SelectList(db.Rechnungsadresse, "Id", "Name", servicerapport.RechnungsadresseId);
            return View(servicerapport);
        }

        // GET: Servicerapports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicerapport servicerapport = db.Servicerapport.Find(id);
            if (servicerapport == null)
            {
                return HttpNotFound();
            }
            return View(servicerapport);
        }

        // POST: Servicerapports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servicerapport servicerapport = db.Servicerapport.Find(id);
            db.Servicerapport.Remove(servicerapport);
            db.SaveChanges();
            return RedirectToAction("Index", "ServicerapportDashboards");
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
