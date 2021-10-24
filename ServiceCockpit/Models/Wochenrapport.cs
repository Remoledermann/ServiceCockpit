using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace ServiceCockpit.Models
{
    public class Wochenrapport
    {
        public int Id { get; set; }
        [DisplayName("Kalenderwoche")]
        public string Kalenderwoche { get; set; }
        [DisplayName("Start-Datum")]
        public DateTime? StartDatum { get; set; }
        [DisplayName("End-Datum")]
        public DateTime? EndDate { get; set; }
        
        public string Status { get; set; }
        [DisplayName("Stunden")]
        public decimal? StundenTotal { get; set; }

        public Mitarbeiter Mitarbeiter { get; set; }
        public int? MitarbeiterId { get; set; }

        [ForeignKey("WochenrapportFK")]
        public List<WochenrapportZeitEintrag> WochenrapportZeitEintrag { get; set; }

        [ForeignKey("WochenrapportFK")]
        public List<WochenrapportSpesenEintrag> WochenrapportSpesenEintrag { get; set; }

        [NotMapped]
        public string Anzeige
        {

            get { return "Wochenrapport Nr."+ Id.ToString() + " /  "+"Kalenderwoche Nr." + Kalenderwoche ; }
        }
    }
}