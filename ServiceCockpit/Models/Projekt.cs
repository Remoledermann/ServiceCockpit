using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Projekt
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Nummer { get; set; }

        public string Status { get; set; }

        public decimal? KostenZeit { get; set; }

        public decimal? KostenMaterial { get; set; }

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