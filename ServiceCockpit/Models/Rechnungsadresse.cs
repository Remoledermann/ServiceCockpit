using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Rechnungsadresse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Vornamne { get; set; }

        public string Strasse { get; set; }

        public string Plz { get; set; }

        public string Ort { get; set; }

        [DisplayName("Telefon Geschäft")]
        public string TelefonGeschäft { get; set; }
        [DisplayName("Telefon Privat")]
        public string TelefonPrivat { get; set; }

        [NotMapped]
        public string Anzeigeadresse
        {
            get { return Name + ", " + Strasse + ", " + Plz + ", " + Ort; }
        }
    }
}