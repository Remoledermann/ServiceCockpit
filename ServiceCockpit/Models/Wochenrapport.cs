using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Wochenrapport
    {
        public int Id { get; set; }

        public string Kalenderwoche { get; set; }
        
        public DateTime? StartDatum { get; set; }
       
        public DateTime? EndDate { get; set; }
        
        public int? Status { get; set; }

        public decimal? StundenTotal { get; set; }

        public Mitarbeiter Mitarbeiter { get; set; }
        public int? MitarbeiterId { get; set; }

        [ForeignKey("WochenrapportFK")]
        public List<WochenrapportZeitEintrag> WochenrapportZeitEintrag { get; set; }

        [ForeignKey("WochenrapportFK")]
        public List<WochenrapportSpesenEintrag> WochenrapportSpesenEintrag { get; set; }

    }
}