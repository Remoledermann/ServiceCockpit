using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class WochenrapportDashboardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: WochenrapportDashboards
        public ActionResult Index()
        {
            var wochenrapport = db.Wochenrapport.Include(w => w.Mitarbeiter).Include(w=>w.WochenrapportZeitEintrag).Include(w => w.WochenrapportSpesenEintrag);
            List<Wochenrapport> listwochenrapport = wochenrapport.ToList();




            foreach (var l in listwochenrapport)
            {
                List<decimal> liststunden = new List<decimal>();

                if(l.WochenrapportZeitEintrag != null)
                {
                    foreach (var s in l.WochenrapportZeitEintrag)
                    {
                        if (s.Zeit != null)
                        {
                            liststunden.Add(s.Zeit.Value);
                        }
                    }

                    l.StundenTotal = liststunden.Sum();
                }
            }

            return View(wochenrapport.ToList());
        }
    }
}