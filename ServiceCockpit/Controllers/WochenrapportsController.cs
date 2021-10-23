using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using ServiceCockpit.Models;

namespace ServiceCockpit.Controllers
{
    public class WochenrapportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wochenrapports
        public ActionResult Index()
        {
            var wochenrapport = db.Wochenrapport.Include(w => w.Mitarbeiter);
            return View(wochenrapport.ToList());
        }

        // GET: Wochenrapports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);
            if (wochenrapport == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapport);
        }

        // GET: Wochenrapports/Create
        public ActionResult Create()
        {
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName");
            return View();
        }

        // POST: Wochenrapports/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Kalenderwoche,StartDatum,EndDate,Status,StundenTotal,MitarbeiterId")] Wochenrapport wochenrapport)
        {
            
            
                int kalenderwoche = Convert.ToInt32(wochenrapport.Kalenderwoche);

                int GetIso8601WeekOfYear(DateTime time)
                {
                    DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
                    if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                    {
                        time = time.AddDays(3);
                    }

                    return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                }



                 DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo us)
                 {
                    

                     DateTime jan1 = new DateTime(year, 1, 1);
                     int daysOffset = (int)us.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
                     DateTime firstWeekDay = jan1.AddDays(daysOffset);
                     int firstWeek = us.Calendar.GetWeekOfYear(jan1, us.DateTimeFormat.CalendarWeekRule, us.DateTimeFormat.FirstDayOfWeek);
                     if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
                     {
                         weekOfYear -= 1;
                     }

                     
                     return firstWeekDay.AddDays(weekOfYear * 7);
                 }


                 int Jahr = DateTime.Now.Year;


                 DateTime firstDayOfWeek = FirstDateOfWeek(Jahr, kalenderwoche, CultureInfo.CurrentCulture);
                DateTime lastDayofWeek = firstDayOfWeek.AddDays(6);

                string sdDate = firstDayOfWeek.ToString("dd/MM/yyyy");
                DateTime ddDate;
                string fnewDateFormat = String.Empty;
                if (DateTime.TryParseExact(sdDate, "dd/MM/yyyy", null, DateTimeStyles.None, out ddDate))
                {
                    fnewDateFormat = string.Format("{0:MM/dd/yyyy}", ddDate);
                }
                string sDate = lastDayofWeek.ToString("dd/MM/yyyy");
                DateTime dDate;
                string lnewDateFormat = String.Empty;
                if (DateTime.TryParseExact(sDate, "dd/MM/yyyy", null, DateTimeStyles.None, out dDate))
                {
                    lnewDateFormat = string.Format("{0:MM/dd/yyyy}", dDate);
                }




                System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

                customCulture.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy";

                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

                DateTime newfirstDateTime = System.Convert.ToDateTime(firstDayOfWeek.ToString("MM/dd/yyyy"));


                wochenrapport.StartDatum = System.Convert.ToDateTime(firstDayOfWeek.ToString("MM/dd/yyyy"));
                wochenrapport.EndDate = System.Convert.ToDateTime(lastDayofWeek.ToString("MM/dd/yyyy"));
                wochenrapport.Status = "Offen";

                db.Wochenrapport.Add(wochenrapport);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", wochenrapport.MitarbeiterId);
            return View(wochenrapport);
        }

        // GET: Wochenrapports/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);

            if (wochenrapport == null)
            {
                return HttpNotFound();
            }

            Wochenrapport wrapport = new Wochenrapport();
            wrapport = db.Wochenrapport.SingleOrDefault(c => c.Id == id);
            var  zeiteinträge =
                db.WochenrapportZeitEintrag.Where(c => c.WochenrapportFK == wrapport.Id);
            var speseneinträge = db.WochenrapportSpesenEintrag.Where(c => c.WochenrapportFK == wrapport.Id);

            wrapport.WochenrapportZeitEintrag = zeiteinträge.ToList();
            wrapport.WochenrapportSpesenEintrag = speseneinträge.ToList();

            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", wochenrapport.MitarbeiterId);

            List<decimal> liststunden = new List<decimal>();
            foreach (var VARIABLE in wrapport.WochenrapportZeitEintrag)
            {
                if (VARIABLE.Zeit != null)
                {
                    liststunden.Add(VARIABLE.Zeit.Value);
                }
            }

            wrapport.StundenTotal = liststunden.Sum();
            db.SaveChanges();

            return View(wrapport);
        }

        // POST: Wochenrapports/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "Id,Kalenderwoche,StartDatum,EndDate,Status,StundenTotal,MitarbeiterId")] Wochenrapport wochenrapport)
        {
           
                db.Entry(wochenrapport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            ViewBag.MitarbeiterId = new SelectList(db.Mitarbeiter, "Id", "VollerName", wochenrapport.MitarbeiterId);
            return View(wochenrapport);
        }

        // GET: Wochenrapports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);
            if (wochenrapport == null)
            {
                return HttpNotFound();
            }
            return View(wochenrapport);
        }

        // POST: Wochenrapports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wochenrapport wochenrapport = db.Wochenrapport.Find(id);
            db.Wochenrapport.Remove(wochenrapport);
            db.SaveChanges();
            return RedirectToAction("Index");
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
