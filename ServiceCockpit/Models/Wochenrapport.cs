using System;
using System.Collections.Generic;
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

        public string Kalenderwoche { get; set; }
        
        public DateTime? StartDatum { get; set; }
       
        public DateTime? EndDate { get; set; }
        
        public string Status { get; set; }

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

            get { return Id + "  /  " + StartDatum.Value.ToString("M") + "  /  "+Kalenderwoche; }
        }
    }
}