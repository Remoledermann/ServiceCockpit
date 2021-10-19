using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class WochenrapportZeitEintrag
    {
        public int Id { get; set; }

        public DateTime? Datum { get; set; }

        public int? ProjektnNummer { get; set; }

        public int? ServicrapportNummer { get; set; }

        public string Ausführungsadresse { get; set; }

        public decimal? Zeit { get; set; }

        [ForeignKey("WochenrapportFK")]
        public Wochenrapport Wochenrapport { get; set; }
        public int? WochenrapportFK { get; set; }
    }
}