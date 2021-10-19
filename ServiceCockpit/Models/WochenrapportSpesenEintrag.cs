using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class WochenrapportSpesenEintrag
    {
        public int Id { get; set; }

        public string Beschreibung { get; set; }

        public decimal? Kosten { get; set; }


        [ForeignKey("WochenrapportFK")]
        public Wochenrapport Wochenrapport { get; set; }
        public int? WochenrapportFK { get; set; }
    }
}