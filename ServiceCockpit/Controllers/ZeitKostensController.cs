﻿using System;
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

        // GET: ZeitKostens1
        public ActionResult Index()
        {
            var zeitKosten = db.ZeitKosten.Include(z => z.Mitarbeiter).Include(z => z.Servicerapport).Include(z => z.Verrechnungsart).Include(z => z.ZeitKostenUeberzeitFaktor);
            return View(zeitKosten.ToList());
        }

        // GET: ZeitKostens1/Details/5
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

        // GET: ZeitKostens1/Create
        public ActionResult Create()
        {

            List<Servicerapport> Servicer = db.Servicerapport.Where(c => c.Status == "Bearbeiten").ToList();
            ViewBag.ServicerapportFK = new SelectList(Servicer, "Id", "Id");




            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName");
            //ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "Id");
            
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name");
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name");
            return View();
        }

        // POST: ZeitKostens1/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AnzahlStunden,AnzahlStundenTotal,KostenTotal,Eintragsdatum,VerrechnungsartId,MitarbeiterId,ZeitKostenUeberzeitFaktorId,ServicerapportFK")] ZeitKosten zeitKosten)
        {
            if (ModelState.IsValid)
            {

                var überzeit = db.ZeitKostenUeberzeitFaktor.SingleOrDefault(c =>
                    c.Id == zeitKosten.ZeitKostenUeberzeitFaktorId);
                zeitKosten.ZeitKostenUeberzeitFaktor = überzeit;

                var verrechnungsart = db.Verrechnungsart.SingleOrDefault(c => c.Id == zeitKosten.VerrechnungsartId);
                zeitKosten.Verrechnungsart = verrechnungsart;

                zeitKosten.AnzahlStundenTotal = zeitKosten.AnzahlStunden * zeitKosten.ZeitKostenUeberzeitFaktor.Faktor;
                zeitKosten.KostenTotal = zeitKosten.AnzahlStundenTotal * zeitKosten.Verrechnungsart.KostenProStunde;


                db.ZeitKosten.Add(zeitKosten);
                db.SaveChanges();
                return RedirectToAction("Edit", "Servicerapports",new  {id = zeitKosten.ServicerapportFK});
            }

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", zeitKosten.MitarbeiterId);
            //ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "Id", zeitKosten.ServicerapportFK);
            List<Servicerapport> Servicer = db.Servicerapport.Where(c => c.Status == "Bearbeiten").ToList();
            ViewBag.ServicerapportFK = new SelectList(Servicer, "Id", "Id", zeitKosten.ServicerapportFK);
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name", zeitKosten.VerrechnungsartId);
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name", zeitKosten.ZeitKostenUeberzeitFaktorId);
            return View(zeitKosten);
        }

        // GET: ZeitKostens1/Edit/5
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
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", zeitKosten.MitarbeiterId);
            //ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "Id", zeitKosten.ServicerapportFK);
            List<Servicerapport> Servicer = db.Servicerapport.Where(c => c.Status == "Bearbeiten").ToList();
            ViewBag.ServicerapportFK = new SelectList(Servicer, "Id", "Id", zeitKosten.ServicerapportFK);
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name", zeitKosten.VerrechnungsartId);
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name", zeitKosten.ZeitKostenUeberzeitFaktorId);
            return View(zeitKosten);
        }

        // POST: ZeitKostens1/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AnzahlStunden,AnzahlStundenTotal,KostenTotal,Eintragsdatum,VerrechnungsartId,MitarbeiterId,ZeitKostenUeberzeitFaktorId,ServicerapportFK")] ZeitKosten zeitKosten)
        {
            if (ModelState.IsValid)
            {
                var überzeit = db.ZeitKostenUeberzeitFaktor.SingleOrDefault(c =>
                    c.Id == zeitKosten.ZeitKostenUeberzeitFaktorId);
                zeitKosten.ZeitKostenUeberzeitFaktor = überzeit;

                var verrechnungsart = db.Verrechnungsart.SingleOrDefault(c => c.Id == zeitKosten.VerrechnungsartId);
                zeitKosten.Verrechnungsart = verrechnungsart;
                zeitKosten.AnzahlStundenTotal = zeitKosten.AnzahlStunden * zeitKosten.ZeitKostenUeberzeitFaktor.Faktor;
                zeitKosten.KostenTotal = zeitKosten.AnzahlStundenTotal * zeitKosten.Verrechnungsart.KostenProStunde;

                db.Entry(zeitKosten).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Servicerapports", new { id = zeitKosten.ServicerapportFK });
            }
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", zeitKosten.MitarbeiterId);
            //ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "Id", zeitKosten.ServicerapportFK);
            List<Servicerapport> Servicer = db.Servicerapport.Where(c => c.Status == "Bearbeiten").ToList();
            ViewBag.ServicerapportFK = new SelectList(Servicer, "Id", "Id", zeitKosten.ServicerapportFK);
            ViewBag.VerrechnungsartId = new SelectList(db.Verrechnungsart, "Id", "Name", zeitKosten.VerrechnungsartId);
            ViewBag.ZeitKostenUeberzeitFaktorId = new SelectList(db.ZeitKostenUeberzeitFaktor, "Id", "Name", zeitKosten.ZeitKostenUeberzeitFaktorId);
            return View(zeitKosten);
        }

        // GET: ZeitKostens1/Delete/5
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

        // POST: ZeitKostens1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZeitKosten zeitKosten = db.ZeitKosten.Find(id);
            db.ZeitKosten.Remove(zeitKosten);
            db.SaveChanges();
            return RedirectToAction("Edit", "Servicerapports", new { id = zeitKosten.ServicerapportFK });
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
