using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class MaterialKosten
    {
        public int Id { get; set; }
        [DisplayName("Anzahl des Materials")]
        public decimal? AnzahlMaterial { get; set; }
        [DisplayName("Kosten Total")]
        public decimal? KostenTotal { get; set; }


        // Referenz 
        public Material Material { get; set; }
        public int? MaterialId { get; set; }


        //FK Rapport
        [ForeignKey("ServicerapportFK")]
        public Servicerapport Servicerapport { get; set; }
        public int? ServicerapportFK { get; set; }

    }
}