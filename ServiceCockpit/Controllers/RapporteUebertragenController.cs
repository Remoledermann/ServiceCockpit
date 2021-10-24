using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using ServiceCockpit.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Text;
using ServiceCockpit.Migrations;

namespace ServiceCockpit.Controllers
{
    public class RapporteUebertragenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Servicerapports
        public ActionResult Index()
        {
            ModelViewAlleRapporte alleRapporte = new ModelViewAlleRapporte();

           alleRapporte.Servicerapports = db.Servicerapport.Include(s => s.Ausführungsadresse)
                .Include(s => s.Eigentuemeradresse)
                .Include(s => s.Projekt).Include(s => s.Rechnungsadresse).ToList();
                alleRapporte.Wochenrapports = db.Wochenrapport.Include(s => s.WochenrapportZeitEintrag)
               .Include(s => s.WochenrapportZeitEintrag).ToList();



                foreach (var VARIABLE in alleRapporte.Wochenrapports)
                {
                    VARIABLE.Mitarbeiter = db.Mitarbeiter.SingleOrDefault(s => s.Id == VARIABLE.MitarbeiterId);
                }
           
            return View(alleRapporte);
        }
    }
}