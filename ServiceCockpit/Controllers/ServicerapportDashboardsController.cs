using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class ServicerapportDashboardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServicerapportDashboards
        public ActionResult Index()
        {
            var Rapportee = new ModelViewServiceCockpit();

            var servicerapport = db.Servicerapport.Include(s => s.Ausführungsadresse).Include(s => s.Eigentuemeradresse).Include(s => s.Mitarbeiter).Include(s => s.Projekt).Include(s => s.Rechnungsadresse);

            Rapportee.Servicerapports = servicerapport.ToList();
            return View(Rapportee);
        }


    }
}