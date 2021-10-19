using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class WochenrapportUeberzeitFaktor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Faktor { get; set; }
    }
}