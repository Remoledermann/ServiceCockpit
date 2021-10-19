using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class StammDatenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StammDaten
        public ActionResult Index()
        {
            
            return View();
        }
    }
}