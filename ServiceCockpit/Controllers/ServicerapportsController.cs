﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
            Servicerapport servicerapport, string rapportSpeichern, string mailSenden, string rapportabschliessen)
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
                        db.Entry(servicerapport).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    SaveServicrapportEinträgeInWochenrapport(servicerapport);


                    return RedirectToAction("Index", "ServicerapportDashboards");
                }
            }

            if (rapportSpeichern == "RapportSpeichern")
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

        public void SaveServicrapportEinträgeInWochenrapport(Servicerapport servicerapport)
        {
            servicerapport.RapportAbgechlossenZeit = DateTime.Now;



            Projekt projekt = db.Projekt.SingleOrDefault(c => c.Id == servicerapport.ProjektFK);


            Ausführungsadresse ausführungsadresse =
                db.Ausführungsadresse.SingleOrDefault(a => a.Id == servicerapport.AusführungsadresseId);

            var zeitkostenvonDb = db.ZeitKosten.Include(z => z.Mitarbeiter).Where(z => z.ServicerapportFK == servicerapport.Id);

            servicerapport.ZeitKosten = zeitkostenvonDb.ToList();

            if (servicerapport.ZeitKosten != null)
            {
                foreach (var s in servicerapport.ZeitKosten)
                {
                    List<Wochenrapport> listWochenrapport = db.Wochenrapport
                        .Include(sw => sw.WochenrapportSpesenEintrag)
                        .Include(sw => sw.WochenrapportZeitEintrag)
                        .Include(sw => sw.Mitarbeiter).ToList();

                    List<Wochenrapport> gefundeneWr = listWochenrapport.Where(w => 
                        s.Eintragsdatum >= w.StartDatum &&
                        s.Eintragsdatum <= w.EndDate &&
                        s.MitarbeiterId == w.MitarbeiterId).ToList();
                    
                    if (gefundeneWr.Count() == 1)
                    {
                        var w = gefundeneWr.First();
                        WochenrapportZeitEintrag eintrag = new WochenrapportZeitEintrag();
                        eintrag.WochenrapportFK = w.Id;
                        eintrag.Zeit = s.AnzahlStundenTotal;
                        eintrag.Ausführungsadresse = ausführungsadresse.Anzeigeadresse;
                        eintrag.Datum = s.Eintragsdatum;
                        eintrag.ProjektnNummer = projekt.Nummer;
                        eintrag.ServicrapportNummer = servicerapport.Id;
                        w.Status = "Offen";

                        db.WochenrapportZeitEintrag.Add(eintrag);
                        servicerapport.Status = "Abgeschlossen";

                        db.SaveChanges();
                    }
                    else if (gefundeneWr.Count() > 1)
                    {
                        throw new Exception("Es kann nur ein Wochenrapport mit dem ZeitKosten Eintrag geben");
                    }
                    else
                    {
                        Wochenrapport neuerWochenrapport = new Wochenrapport();

                        CultureInfo ciCurr = CultureInfo.CurrentCulture;

                        int weekNum = ciCurr.Calendar.GetWeekOfYear(s.Eintragsdatum,
                            CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                        neuerWochenrapport.Kalenderwoche = weekNum.ToString();
                        neuerWochenrapport.Mitarbeiter = s.Mitarbeiter;

                        int GetIso8601WeekOfYear(DateTime time)
                        {
                            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
                            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                            {
                                time = time.AddDays(3);
                            }

                            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time,
                                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                        }



                        DateTime FirstDateOfWeek(int year, int weekOfYear,
                            System.Globalization.CultureInfo us)
                        {


                            DateTime jan1 = new DateTime(year, 1, 1);
                            int daysOffset = (int)us.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
                            DateTime firstWeekDay = jan1.AddDays(daysOffset);
                            int firstWeek = us.Calendar.GetWeekOfYear(jan1,
                                us.DateTimeFormat.CalendarWeekRule,
                                us.DateTimeFormat.FirstDayOfWeek);
                            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
                            {
                                weekOfYear -= 1;
                            }


                            return firstWeekDay.AddDays(weekOfYear * 7);
                        }


                        int Jahr = DateTime.Now.Year;


                        DateTime firstDayOfWeek = FirstDateOfWeek(s.Eintragsdatum.Year, weekNum,
                            CultureInfo.CurrentCulture);
                        DateTime lastDayofWeek = firstDayOfWeek.AddDays(6);

                        string sdDate = firstDayOfWeek.ToString("dd/MM/yyyy");
                        DateTime ddDate;
                        string fnewDateFormat = String.Empty;
                        if (DateTime.TryParseExact(sdDate, "dd/MM/yyyy", null, DateTimeStyles.None,
                            out ddDate))
                        {
                            fnewDateFormat = string.Format("{0:MM/dd/yyyy}", ddDate);
                        }

                        string sDate = lastDayofWeek.ToString("dd/MM/yyyy");
                        DateTime dDate;
                        string lnewDateFormat = String.Empty;
                        if (DateTime.TryParseExact(sDate, "dd/MM/yyyy", null, DateTimeStyles.None,
                            out dDate))
                        {
                            lnewDateFormat = string.Format("{0:MM/dd/yyyy}", dDate);
                        }




                        System.Globalization.CultureInfo customCulture =
                            new System.Globalization.CultureInfo("en-US", true);

                        customCulture.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy";

                        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                        System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

                        DateTime newfirstDateTime =
                            System.Convert.ToDateTime(firstDayOfWeek.ToString("MM/dd/yyyy"));


                        neuerWochenrapport.StartDatum =
                            System.Convert.ToDateTime(firstDayOfWeek.ToString("MM/dd/yyyy"));
                        neuerWochenrapport.EndDate =
                            System.Convert.ToDateTime(lastDayofWeek.ToString("MM/dd/yyyy"));

                        const string IdGenerierenStatus = "ID_GENERIEREN";
                        neuerWochenrapport.Status = IdGenerierenStatus;


                        db.Wochenrapport.Add(neuerWochenrapport);
                        db.SaveChanges();

                        // Die DB generiert eine ID, welche wir brauchen
                        neuerWochenrapport =
                            db.Wochenrapport.SingleOrDefault(wr => wr.Status == IdGenerierenStatus);
                        int neueWochenrapportId = neuerWochenrapport.Id;

                        neuerWochenrapport.Status = "Offen";

                        WochenrapportZeitEintrag eintrag = new WochenrapportZeitEintrag();
                        eintrag.WochenrapportFK = neuerWochenrapport.Id;
                        eintrag.Zeit = s.AnzahlStundenTotal;
                        eintrag.Ausführungsadresse = ausführungsadresse.Anzeigeadresse;
                        eintrag.Datum = s.Eintragsdatum;
                        eintrag.ProjektnNummer = projekt.Nummer;
                        eintrag.ServicrapportNummer = servicerapport.Id;

                        db.WochenrapportZeitEintrag.Add(eintrag);

                        servicerapport.Status = "Abgeschlossen";

                        db.SaveChanges();
                    }

                    //foreach (var w in listWochenrapport)
                    //{
                    //    if (s.Servicerapport.RapportAbgechlossenZeit >= w.StartDatum &&
                    //        s.Servicerapport.RapportAbgechlossenZeit <= w.EndDate &&
                    //        s.MitarbeiterId == w.MitarbeiterId)
                    //    {
                    //        WochenrapportZeitEintrag eintrag = new WochenrapportZeitEintrag();
                    //        eintrag.WochenrapportFK = w.Id;
                    //        eintrag.Zeit = s.AnzahlStundenTotal;
                    //        eintrag.Ausführungsadresse = ausführungsadresse.Anzeigeadresse;
                    //        eintrag.Datum = s.Eintragsdatum;
                    //        eintrag.ProjektnNummer = projekt.Nummer;
                    //        eintrag.ServicrapportNummer = servicerapport.Id;
                    //        w.Status = "Bearbeitet";

                    //        db.WochenrapportZeitEintrag.Add(eintrag);
                    //        servicerapport.Status = "Abgeschlossen";

                    //        db.SaveChanges();

                    //        break;
                    //    }
                    //    else
                    //    {
                    //            Wochenrapport neuerWochenrapport = new Wochenrapport();

                    //            CultureInfo ciCurr = CultureInfo.CurrentCulture;

                    //            int weekNum = ciCurr.Calendar.GetWeekOfYear(s.Eintragsdatum,
                    //                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);


                    //            neuerWochenrapport.Mitarbeiter = s.Mitarbeiter;
                    //            neuerWochenrapport.Status = "Offen";

                    //            int kalenderwoche = weekNum;

                    //            int GetIso8601WeekOfYear(DateTime time)
                    //            {
                    //                DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
                    //                if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                    //                {
                    //                    time = time.AddDays(3);
                    //                }

                    //                return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time,
                    //                    CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    //            }



                    //            DateTime FirstDateOfWeek(int year, int weekOfYear,
                    //                System.Globalization.CultureInfo us)
                    //            {


                    //                DateTime jan1 = new DateTime(year, 1, 1);
                    //                int daysOffset = (int)us.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
                    //                DateTime firstWeekDay = jan1.AddDays(daysOffset);
                    //                int firstWeek = us.Calendar.GetWeekOfYear(jan1,
                    //                    us.DateTimeFormat.CalendarWeekRule,
                    //                    us.DateTimeFormat.FirstDayOfWeek);
                    //                if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
                    //                {
                    //                    weekOfYear -= 1;
                    //                }


                    //                return firstWeekDay.AddDays(weekOfYear * 7);
                    //            }


                    //            int Jahr = DateTime.Now.Year;


                    //            DateTime firstDayOfWeek = FirstDateOfWeek(s.Eintragsdatum.Year, kalenderwoche,
                    //                CultureInfo.CurrentCulture);
                    //            DateTime lastDayofWeek = firstDayOfWeek.AddDays(6);

                    //            string sdDate = firstDayOfWeek.ToString("dd/MM/yyyy");
                    //            DateTime ddDate;
                    //            string fnewDateFormat = String.Empty;
                    //            if (DateTime.TryParseExact(sdDate, "dd/MM/yyyy", null, DateTimeStyles.None,
                    //                out ddDate))
                    //            {
                    //                fnewDateFormat = string.Format("{0:MM/dd/yyyy}", ddDate);
                    //            }

                    //            string sDate = lastDayofWeek.ToString("dd/MM/yyyy");
                    //            DateTime dDate;
                    //            string lnewDateFormat = String.Empty;
                    //            if (DateTime.TryParseExact(sDate, "dd/MM/yyyy", null, DateTimeStyles.None,
                    //                out dDate))
                    //            {
                    //                lnewDateFormat = string.Format("{0:MM/dd/yyyy}", dDate);
                    //            }




                    //            System.Globalization.CultureInfo customCulture =
                    //                new System.Globalization.CultureInfo("en-US", true);

                    //            customCulture.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy";

                    //            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                    //            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

                    //            DateTime newfirstDateTime =
                    //                System.Convert.ToDateTime(firstDayOfWeek.ToString("MM/dd/yyyy"));


                    //            neuerWochenrapport.StartDatum =
                    //                System.Convert.ToDateTime(firstDayOfWeek.ToString("MM/dd/yyyy"));
                    //            neuerWochenrapport.EndDate =
                    //                System.Convert.ToDateTime(lastDayofWeek.ToString("MM/dd/yyyy"));
                    //            neuerWochenrapport.Status = "Offen";

                    //            db.Wochenrapport.Add(neuerWochenrapport);
                    //            db.SaveChanges();

                    //            WochenrapportZeitEintrag eintrag = new WochenrapportZeitEintrag();
                    //            eintrag.WochenrapportFK = w.Id;
                    //            eintrag.Zeit = s.AnzahlStundenTotal;
                    //            eintrag.Ausführungsadresse = ausführungsadresse.Anzeigeadresse;
                    //            eintrag.Datum = s.Eintragsdatum;
                    //            eintrag.ProjektnNummer = projekt.Nummer;
                    //            eintrag.ServicrapportNummer = servicerapport.Id;

                    //            db.WochenrapportZeitEintrag.Add(eintrag);

                    //            servicerapport.Status = "Abgeschlossen";

                    //            db.SaveChanges();

                    //        }
                    //}
                }



            }
        }
    }
}

