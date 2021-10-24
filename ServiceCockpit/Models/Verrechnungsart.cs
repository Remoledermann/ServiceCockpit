using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Verrechnungsart
    {
        public int Id { get; set; }
        [DisplayName("Beschreibung")]
        public string Name { get; set; }
        [DisplayName("Verrechungs Nr.")]
        public string Nummer { get; set; }
        [DisplayName("Kosten Pro Stunde")]
        public decimal? KostenProStunde { get; set; }
    }
}