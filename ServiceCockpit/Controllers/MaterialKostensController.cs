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
    public class MaterialKostensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MaterialKostens
        public ActionResult Index()
        {
            var materialKosten = db.MaterialKosten.Include(m => m.Material).Include(m => m.Servicerapport);
            return View(materialKosten.ToList());
        }

        // GET: MaterialKostens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialKosten materialKosten = db.MaterialKosten.Find(id);
            if (materialKosten == null)
            {
                return HttpNotFound();
            }
            return View(materialKosten);
        }

        // GET: MaterialKostens/Create
        public ActionResult Create()
        {
            List<Servicerapport> Servicer = db.Servicerapport.Where(c => c.Status == "Bearbeiten").ToList();
            ViewBag.ServicerapportFK = new SelectList(Servicer, "Id", "Id");

            ViewBag.MaterialId = new SelectList(db.Material, "Id", "NameUndNummer");
            return View();
        }

        // POST: MaterialKostens/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AnzahlMaterial,KostenTotal,MaterialId,ServicerapportFK")] MaterialKosten materialKosten)
        {
            if (ModelState.IsValid)
            {
                var material = db.Material.SingleOrDefault(c =>
                    c.Id == materialKosten.MaterialId);
                materialKosten.Material = material;

                materialKosten.KostenTotal = materialKosten.Material.KostenProMaterial * materialKosten.AnzahlMaterial;


                db.MaterialKosten.Add(materialKosten);
                db.SaveChanges();
                return RedirectToAction("Edit", "Servicerapports", new { id = materialKosten.ServicerapportFK });
            }

            ViewBag.MaterialId = new SelectList(db.Material, "Id", "Name", materialKosten.MaterialId);
            ViewBag.ServicerapportFK = new SelectList(db.Servicerapport, "Id", "Id", materialKosten.ServicerapportFK);
            return View(materialKosten);
        }

        // GET: MaterialKostens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialKosten materialKosten = db.MaterialKosten.Find(id);
            if (materialKosten == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialId = new SelectList(db.Material, "Id", "Name", materialKosten.MaterialId);
            List<Servicerapport> Servicer = db.Servicerapport.Where(c => c.Status == "Bearbeiten").ToList();
            ViewBag.ServicerapportFK = new SelectList(Servicer, "Id", "Id");
            return View(materialKosten);
        }

        // POST: MaterialKostens/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AnzahlMaterial,KostenTotal,MaterialId,ServicerapportFK")] MaterialKosten materialKosten)
        {
            if (ModelState.IsValid)
            {
                var material = db.Material.SingleOrDefault(c =>
                    c.Id == materialKosten.MaterialId);
                materialKosten.Material = material;

                materialKosten.KostenTotal = materialKosten.Material.KostenProMaterial * materialKosten.AnzahlMaterial;

                db.Entry(materialKosten).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Servicerapports", new { id = materialKosten.ServicerapportFK });
            }
            ViewBag.MaterialId = new SelectList(db.Material, "Id", "Name", materialKosten.MaterialId);
            List<Servicerapport> Servicer = db.Servicerapport.Where(c => c.Status == "Bearbeiten").ToList();
            ViewBag.ServicerapportFK = new SelectList(Servicer, "Id", "Id");
            return View(materialKosten);
        }

        // GET: MaterialKostens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialKosten materialKosten = db.MaterialKosten.Find(id);
            if (materialKosten == null)
            {
                return HttpNotFound();
            }
            return View(materialKosten);
        }

        // POST: MaterialKostens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaterialKosten materialKosten = db.MaterialKosten.Find(id);
            db.MaterialKosten.Remove(materialKosten);
            db.SaveChanges();
            return RedirectToAction("Edit", "Servicerapports", new { id = materialKosten.ServicerapportFK });
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
