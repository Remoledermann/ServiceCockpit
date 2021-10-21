using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ServiceCockpit.Migrations;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class StammDatenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StammDaten
        public ActionResult Index()
        {
            var stammdaten = new ModleViewStammdaten();
            //Stammdaten Laden
            stammdaten.Mitarbeiter = db.Mitarbeiter.ToList();
            stammdaten.Material = db.Material.ToList();
            stammdaten.Verrechnungsart = db.Verrechnungsart.ToList();
            stammdaten.ZeitKostenUeberzeitFaktor = db.ZeitKostenUeberzeitFaktor.ToList();
            stammdaten.Projekt = db.Projekt.ToList();
            stammdaten.Organisation = db.Organisation.ToList();
            stammdaten.Ausführungsadresse = db.Ausführungsadresse.ToList();
            stammdaten.Rechnungsadresse = db.Rechnungsadresse.ToList();
            stammdaten.Eigentuemeradresse = db.Eigentuemeradresse.ToList();


            return View(stammdaten);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Vornamne,Strasse,Plz,Ort,TelefonGeschäft,TelefonPrivat")] Ausführungsadresse ausführungsadresse)
        {
            if (ModelState.IsValid)
            {
                var eigentuemeradresse = new Eigentuemeradresse();
                var rechnungsadresse = new Rechnungsadresse();

                eigentuemeradresse.Name = ausführungsadresse.Name;
                eigentuemeradresse.Vornamne = ausführungsadresse.Vornamne;
                eigentuemeradresse.Ort = ausführungsadresse.Ort;
                eigentuemeradresse.Plz = ausführungsadresse.Plz;
                eigentuemeradresse.Strasse = ausführungsadresse.Strasse;
                eigentuemeradresse.TelefonGeschäft = ausführungsadresse.TelefonGeschäft;
                eigentuemeradresse.TelefonPrivat = ausführungsadresse.TelefonPrivat;

                rechnungsadresse.Name = ausführungsadresse.Name;
                rechnungsadresse.Vornamne = ausführungsadresse.Vornamne;
                rechnungsadresse.Ort = ausführungsadresse.Ort;
                rechnungsadresse.Plz = ausführungsadresse.Plz;
                rechnungsadresse.Strasse = ausführungsadresse.Strasse;
                rechnungsadresse.TelefonGeschäft = ausführungsadresse.TelefonGeschäft;
                rechnungsadresse.TelefonPrivat = ausführungsadresse.TelefonPrivat;





                db.Ausführungsadresse.Add(ausführungsadresse);
                db.SaveChanges();

                db.Eigentuemeradresse.Add(eigentuemeradresse);
                db.SaveChanges();

                db.Rechnungsadresse.Add(rechnungsadresse);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    
    }
}