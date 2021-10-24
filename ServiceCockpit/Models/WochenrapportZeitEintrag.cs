using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class WochenrapportZeitEintrag
    {
        public int Id { get; set; }
        [DisplayName("Eintragsdatum")]
        public DateTime? Datum { get; set; }
        [DisplayName("Projekt Nr.")]
        public int? ProjektnNummer { get; set; }
        [DisplayName("Servicerapport Nr.")]
        public int? ServicrapportNummer { get; set; }

        public string Ausführungsadresse { get; set; }
        [DisplayName("Stunden")]
        public decimal? Zeit { get; set; }

        [ForeignKey("WochenrapportFK")]
        public Wochenrapport Wochenrapport { get; set; }
        public int? WochenrapportFK { get; set; }
    }
}