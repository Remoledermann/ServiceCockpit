using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class Servicerapport
    {
        
        public int Id { get; set; }
        [DisplayName("Termin am")]
        public DateTime? KundenTerminZeit { get; set; }
        [DisplayName("Rapport Abgeschlossen am")]
        public DateTime? RapportAbgechlossenZeit { get; set; }
        [DisplayName("Voranmeldung bei")]
        public string VoranmeldungName { get; set; }
        [DisplayName("Voranmeldung Telefon Nr.")]
        public string VoranmeldungNummer { get; set; }

        public string Status { get; set; }

        public string Beschreibung { get; set; }
        [DisplayName("E-Mail Adresse")]
        public string EmailAdresse { get; set; }
        [DisplayName("Unterschrift Kunde")]
        public string Unterschrift { get; set; }
        [DisplayName("Kosten der Arbeitszeiten")]
        public decimal? KostenZeit { get; set; }
        [DisplayName("Kosten des Material")]
        public decimal? KostenMaterial { get; set; }
        [DisplayName("Kosten Total")]
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