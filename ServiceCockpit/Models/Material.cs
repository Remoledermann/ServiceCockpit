using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Material
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nummer { get; set; }

        public decimal? KostenProMaterial { get; set; }

        [NotMapped]
        public string NameUndNummer
        {
            get { return Name + " " + Nummer; }
        }
    }
}