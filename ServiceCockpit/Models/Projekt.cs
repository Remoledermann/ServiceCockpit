using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Projekt
    {
        public int Id { get; set; }

        [DisplayName("Projekt Bezeichnung")]
        public string Name { get; set; }

        [DisplayName("Projekt Nr.")]
        public int? Nummer { get; set; }

        public string Status { get; set; }

        [DisplayName("Kosten der Arbeitszeiten")]
        public decimal? KostenZeit { get; set; }

        [DisplayName("Kosten des Material")]
        public decimal? KostenMaterial { get; set; }

        [DisplayName("Kosten Total")]
        public decimal? KostenTotal { get; set; }


        //Referenz
        public Mitarbeiter Mitarbeiter { get; set; }
        public int? MitarbeiterId { get; set; }


        //FK Projekt
        [ForeignKey("ProjektFK")]
        public List<Servicerapport> Servicerapports { get; set; }


        //FK Organisation

        
        [ForeignKey("OrganisationFK")]
        public Organisation Organisation { get; set; }
        public int? OrganisationFK { get; set; }
    }
}