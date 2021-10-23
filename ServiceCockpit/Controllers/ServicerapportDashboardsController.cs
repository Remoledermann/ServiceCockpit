using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceCockpit.Migrations;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class ServicerapportDashboardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServicerapportDashboards
        public ActionResult Index()
        {
            var servicrapporte = new ModelViewServiceCockpit();

            AddiereServicrapportDaten();

            var servicerapport = db.Servicerapport.
                Include(s => s.Ausführungsadresse).
                Include(s => s.Eigentuemeradresse).
                Include(s => s.Mitarbeiter).
                Include(s => s.Projekt).
                Include(s => s.Rechnungsadresse).ToList();
            servicrapporte.Servicerapports = servicerapport;
            return View(servicrapporte);
        }


        public void AddiereServicrapportDaten()
        {
            var rapport = db.Servicerapport.
                Include(s => s.Ausführungsadresse).
                Include(s => s.Eigentuemeradresse)
                .Include(s => s.Mitarbeiter).
                Include(s => s.Projekt).
                Include(s => s.Rechnungsadresse)
                .ToList();


            foreach (var v in rapport)
            {

                int id = v.Id;

                var zeiteinträge = db.ZeitKosten.Include(s => s.ZeitKostenUeberzeitFaktor)
                    .Include(s => s.Verrechnungsart).Include(s => s.Mitarbeiter).Where(s => s.ServicerapportFK == id);
                v.ZeitKosten = zeiteinträge.ToList();

                var materialeinträge = db.MaterialKosten.Include(s => s.Material).Where(s => s.ServicerapportFK == id);
                v.MaterialKosten = materialeinträge.ToList();

                // Additon kosten
                List<decimal> listAnzahlStunden = new List<decimal>();
                foreach (var VARIABLE in v.ZeitKosten)
                {
                    if (VARIABLE.KostenTotal == null)
                    {

                    }
                    else
                    {
                        listAnzahlStunden.Add(VARIABLE.KostenTotal.Value);
                    }

                }

                v.KostenZeit = listAnzahlStunden.Sum();
                List<decimal> listAnzahlMaterial = new List<decimal>();
                foreach (var VARIABLE in v.MaterialKosten)
                {
                    if (VARIABLE.KostenTotal == null)
                    {

                    }
                    else
                    {
                        listAnzahlMaterial.Add(VARIABLE.KostenTotal.Value);
                    }
                }

                v.KostenMaterial = listAnzahlMaterial.Sum();
                v.KostenTotal = v.KostenMaterial + v.KostenZeit;
                db.SaveChanges();
            }
        }

    }
}