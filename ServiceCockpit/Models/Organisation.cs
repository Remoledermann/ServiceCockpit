using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Organisation
    {
        public int Id { get; set; }
        [DisplayName("Organisatios Bezeichnung")]
        public string Name { get; set; }

        [DisplayName("Kosten der Arbeitszeiten")]
        public decimal? KostenZeit { get; set; }

        [DisplayName("Kosten des Material")]
        public decimal? KostenMaterial { get; set; }

        [DisplayName("Kosten Total")]
        public decimal? KostenTotal { get; set; }

        public string Status { get; set; }

        [ForeignKey("OrganisationFK")]
        public List<Projekt> Projekte { get; set; }
    }
}