using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace ServiceCockpit.Models
{
    public class Mitarbeiter
    {
        public int Id { get; set; }

        [DisplayName("Vorname")]
        public string VorName { get; set; }
        [DisplayName("Nachname")]
        public string NachName { get; set; }
        [DisplayName("Telefon Geschäft")]
        public string TelefonGeschäft { get; set; }
        [DisplayName("Telefon Privat")]
        public string TelefonPrivat { get; set; }
        public string Kürzel { get; set; }
        [DisplayName("GFE Nr.")]
        public string GfeNummer { get; set; }
        [DisplayName("E-Mail Adresse")]
        public string Email { get; set; }

        [NotMapped]
        public string VollerName
        {
            get { return VorName + " " + NachName ; }
        }

    }
}