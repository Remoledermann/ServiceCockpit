using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace ServiceCockpit.Models
{
    public class Mitarbeiter
    {
        public int Id { get; set; }

        public string VorName { get; set; }

        public string NachName { get; set; }

        public string TelefonGeschäft { get; set; }

        public string TelefonPrivat { get; set; }

        public string Kürzel { get; set; }

        public string GfeNummer { get; set; }

        public string Email { get; set; }
    }
}