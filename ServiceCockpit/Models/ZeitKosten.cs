using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class ZeitKosten
    {
        public int Id { get; set; }

        public decimal AnzahlStunden { get; set; }

        public decimal? KostenTotal { get; set; }

        //Referenz
        public Verrechnungsart Verrechnungsart { get; set; }
        public int? VerrechnungsartId { get; set; }


        //Referenz
        public Mitarbeiter Mitarbeiter { get; set; }
        public int? MitarbeiterId { get; set; }


        //FK Rapport
        [ForeignKey("ServicerapportFK")]
        public Servicerapport Servicerapport { get; set; }
        public int? ServicerapportFK { get; set; }
    }
}