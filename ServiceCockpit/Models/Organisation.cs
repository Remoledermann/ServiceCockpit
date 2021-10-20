using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Organisation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? KostenZeit { get; set; }

        public decimal? KostenMaterial { get; set; }

        public decimal? KostenTotal { get; set; }

        public string Status { get; set; }

        [ForeignKey("OrganisationFK")]
        public List<Projekt> Projekte { get; set; }
    }
}