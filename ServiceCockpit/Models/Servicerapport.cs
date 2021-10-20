using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Servicerapport
    {
        
        public int Id { get; set; }

        public DateTime? KundenTerminZeit { get; set; }

        public DateTime? RapportAbgechlossenZeit { get; set; }

        public string VoranmeldungName { get; set; }

        public string VoranmeldungNummer { get; set; }

        public string Status { get; set; }

        public string Beschreibung { get; set; }

        public string Bemerkung { get; set; }

        public string Unterschrift { get; set; }

        public decimal? KostenZeit { get; set; }

        public decimal? KostenMaterial { get; set; }

        public decimal? KostenTotal { get; set; }



        //Referenz
        public Eigentuemeradresse Eigentuemeradresse { get; set; }
        public int? EigentuemeradresseId { get; set; }
        //Referenz
        public Ausführungsadresse Ausführungsadresse { get; set; }
        public int? AusführungsadresseId { get; set; }
        //Referenz
        public Rechnungsadresse Rechnungsadresse { get; set; }
        public int? RechnungsadresseId { get; set; }
        // Referenz
        public Mitarbeiter Mitarbeiter { get; set; }
        public int? MitarbeiterId { get; set; }


        //Foreignkey Zeit ZeitKosten
        [ForeignKey("ServicerapportFK")]
        public List<ZeitKosten> ZeitKosten { get; set; }

        //Foreignkey Zeit MaterialKosten
        [ForeignKey("ServicerapportFK")]
        public List<MaterialKosten> MaterialKosten { get; set; }

        //FK Projekt
        [ForeignKey("ProjektFK")]
        public Projekt Projekt { get; set; }
        public int? ProjektFK { get; set; }

    }
}