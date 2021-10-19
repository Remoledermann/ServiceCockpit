using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class ModleViewStammdaten
    {
        public List<Ausführungsadresse> Ausführungsadresse { get; set; } 
            = new List<Ausführungsadresse>();
        public IList<Eigentuemeradresse> Eigentuemeradresse { get; set; } 
            = new List<Eigentuemeradresse>();
        public List<Rechnungsadresse> Rechnungsadresse { get; set; } 
            = new List<Rechnungsadresse>();
        public List<Material> Material { get; set; } 
            = new List<Material>();
        public List<Verrechnungsart> Verrechnungsart { get; set; } 
            = new List<Verrechnungsart>();
        public List<Mitarbeiter> Mitarbeiter { get; set; } 
            = new List<Mitarbeiter>();
        public List<Organisation> Organisation { get; set; } 
            = new List<Organisation>();
        public List<Projekt> Projekt { get; set; } 
            = new List<Projekt>();
        public List<ZeitKostenUeberzeitFaktor> ZeitKostenUeberzeitFaktor { get; set; }
            = new List<ZeitKostenUeberzeitFaktor>();

    }
}