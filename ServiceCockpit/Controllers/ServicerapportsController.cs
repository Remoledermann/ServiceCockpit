using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
    public class ServicerapportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Servicerapports
        public ActionResult Index()
        {
            var servicerapport = db.Servicerapport.Include(s => s.Ausführungsadresse).Include(s => s.Eigentuemeradresse)
                .Include(s => s.Mitarbeiter).Include(s => s.Projekt).Include(s => s.Rechnungsadresse);

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
        public ActionResult Create(
            [Bind(Include =
                "Id,KundenTerminZeit,RapportAbgechlossenZeit,VoranmeldungName,VoranmeldungNummer,Status,Beschreibung,EmailAdresse,Unterschrift,KostenZeit,KostenMaterial,KostenTotal,EigentuemeradresseId,AusführungsadresseId,RechnungsadresseId,MitarbeiterId,ProjektFK")]
            Servicerapport servicerapport)
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

            ViewBag.AusführungsadresseId = new SelectList(db.Ausführungsadresse, "Id", "Anzeigeadresse",
                servicerapport.AusführungsadresseId);
            ViewBag.EigentuemeradresseId = new SelectList(db.Eigentuemeradresse, "Id", "Anzeigeadresse",
                servicerapport.EigentuemeradresseId);
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", servicerapport.MitarbeiterId);
            ViewBag.ProjektFK = new SelectList(db.Projekt, "Id", "Nummer", servicerapport.ProjektFK);
            ViewBag.RechnungsadresseId = new SelectList(db.Rechnungsadresse, "Id", "Anzeigeadresse",
                servicerapport.RechnungsadresseId);


            return View(servicerapport);
        }

        // GET: Servicerapports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var rapport = db.Servicerapport.SingleOrDefault(s => s.Id == id);

                var zeiteinträge = db.ZeitKosten.Include(s => s.ZeitKostenUeberzeitFaktor)
                    .Include(s => s.Verrechnungsart).Include(s => s.Mitarbeiter).Where(s => s.ServicerapportFK == id);
                rapport.ZeitKosten = zeiteinträge.ToList();

                var materialeinträge = db.MaterialKosten.Include(s => s.Material).Where(s => s.ServicerapportFK == id);
                rapport.MaterialKosten = materialeinträge.ToList();

                // Additon kosten
                List<decimal> listAnzahlStunden = new List<decimal>();
                foreach (var VARIABLE in rapport.ZeitKosten)
                {
                    if (VARIABLE.KostenTotal == null)
                    {

                    }
                    else
                    {
                        listAnzahlStunden.Add(VARIABLE.KostenTotal.Value);
                    }

                }

                rapport.KostenZeit = listAnzahlStunden.Sum();
                List<decimal> listAnzahlMaterial = new List<decimal>();
                foreach (var VARIABLE in rapport.MaterialKosten)
                {
                    if (VARIABLE.KostenTotal == null)
                    {

                    }
                    else
                    {
                        listAnzahlMaterial.Add(VARIABLE.KostenTotal.Value);
                    }
                }

                rapport.KostenMaterial = listAnzahlMaterial.Sum();
                rapport.KostenTotal = rapport.KostenMaterial + rapport.KostenZeit;
                db.SaveChanges();


            }

            Servicerapport servicerapport = db.Servicerapport.Find(id);
            if (servicerapport == null)
            {
                return HttpNotFound();
            }

            ViewBag.AusführungsadresseId = new SelectList(db.Ausführungsadresse, "Id", "Anzeigeadresse",
                servicerapport.AusführungsadresseId);
            ViewBag.EigentuemeradresseId = new SelectList(db.Eigentuemeradresse, "Id", "Anzeigeadresse",
                servicerapport.EigentuemeradresseId);
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", servicerapport.MitarbeiterId);
            ViewBag.ProjektFK = new SelectList(db.Projekt, "Id", "Nummer", servicerapport.ProjektFK);
            ViewBag.RechnungsadresseId = new SelectList(db.Rechnungsadresse, "Id", "Anzeigeadresse",
                servicerapport.RechnungsadresseId);

            var zeitKosten = db.ZeitKosten.Include(z => z.Mitarbeiter).Include(z => z.Servicerapport)
                .Include(z => z.Verrechnungsart).Include(z => z.ZeitKostenUeberzeitFaktor)
                .Where(z => z.ServicerapportFK == id);
            servicerapport.ZeitKosten = zeitKosten.ToList();

            var materialKosten = db.MaterialKosten.Include(m => m.Servicerapport).Include(m => m.Material)
                .Where(z => z.ServicerapportFK == id);
            servicerapport.MaterialKosten = materialKosten.ToList();

            return View(servicerapport);
        }

        // POST: Servicerapports/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include =
                "Id,KundenTerminZeit,RapportAbgechlossenZeit,VoranmeldungName,VoranmeldungNummer,Status,Beschreibung,EmailAdresse,Unterschrift,KostenZeit,KostenMaterial,KostenTotal,EigentuemeradresseId,AusführungsadresseId,RechnungsadresseId,MitarbeiterId,ProjektFK")]
            Servicerapport servicerapport, string rapportSpeichern, string mailSenden)
        {

            if (mailSenden == "MailSenden")
            {

                if (servicerapport.EmailAdresse != null && servicerapport.Unterschrift != null)
                {
                    servicerapport.Mitarbeiter =
                        db.Mitarbeiter.SingleOrDefault(s => servicerapport.MitarbeiterId == s.Id);

                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Gfeller Elektro", "gfellerelektroservicrapport@gmail.com"));
                    message.To.Add(MailboxAddress.Parse(servicerapport.EmailAdresse));

                    message.Subject = "Rapport Nr." + servicerapport.Id.ToString();
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = $@"Wir bedanken uns bei Ihnen für den Aufrag.


Die Materialkosten belaufen sich auf: {servicerapport.KostenMaterial.ToString()} 
Die Arbeitskosten belaufen sich auf : {servicerapport.KostenMaterial.ToString()}

Bei Fragen zum Rapport stehen wir Ihnen jederzeit zurverfügung"
                    };

                    SmtpClient client = new SmtpClient();
                    try
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate("gfellerelektroservicrapport@gmail.com", "Gfeller_1234");
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        throw;
                    }
                    finally
                    {
                        client.Disconnect(true);
                        client.Dispose();
                        servicerapport.Status = "Abgeschlossen";
                        servicerapport.KundenTerminZeit = DateTime.Now;
                        db.Entry(servicerapport).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "ServicerapportDashboards");
                }
                else
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

                    ViewBag.AusführungsadresseId = new SelectList(db.Ausführungsadresse, "Id", "Anzeigeadresse",
                        servicerapport.AusführungsadresseId);
                    ViewBag.EigentuemeradresseId = new SelectList(db.Eigentuemeradresse, "Id", "Anzeigeadresse",
                        servicerapport.EigentuemeradresseId);
                    ViewBag.MitarbeiterId =
                        new SelectList(db.Mitarbeiter, "Id", "VollerName", servicerapport.MitarbeiterId);
                    ViewBag.ProjektFK = new SelectList(db.Projekt, "Id", "Nummer", servicerapport.ProjektFK);
                    ViewBag.RechnungsadresseId = new SelectList(db.Rechnungsadresse, "Id", "Anzeigeadresse",
                        servicerapport.RechnungsadresseId);


                    return View(servicerapport);
                }

            }

            return RedirectToAction("Index", "ServicerapportDashboards");

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
            return RedirectToAction("Index", "Servicerapports");
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
