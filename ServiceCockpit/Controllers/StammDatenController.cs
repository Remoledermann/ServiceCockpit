using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
    }
}