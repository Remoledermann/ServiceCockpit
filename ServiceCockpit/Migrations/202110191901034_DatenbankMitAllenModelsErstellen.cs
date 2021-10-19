namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatenbankMitAllenModelsErstellen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ausführungsadresse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Vornamne = c.String(),
                        Strasse = c.String(),
                        Plz = c.String(),
                        Ort = c.String(),
                        TelefonGeschäft = c.String(),
                        TelefonPrivat = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Eigentuemeradresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Vornamne = c.String(),
                        Strasse = c.String(),
                        Plz = c.String(),
                        Ort = c.String(),
                        TelefonGeschäft = c.String(),
                        TelefonPrivat = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nummer = c.String(),
                        KostenProMaterial = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialKostens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnzahlMaterial = c.Decimal(precision: 18, scale: 2),
                        KostenTotal = c.Decimal(precision: 18, scale: 2),
                        VerrechnungsartId = c.Int(),
                        ServicerapportFK = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servicerapports", t => t.ServicerapportFK)
                .ForeignKey("dbo.Materials", t => t.VerrechnungsartId)
                .Index(t => t.VerrechnungsartId)
                .Index(t => t.ServicerapportFK);
            
            CreateTable(
                "dbo.Servicerapports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KundenTerminZeit = c.DateTime(),
                        RapportAbgechlossenZeit = c.DateTime(),
                        VoranmeldungName = c.String(),
                        VoranmeldungNummer = c.Int(),
                        Status = c.Int(),
                        Beschreibung = c.String(),
                        Bemerkung = c.String(),
                        Unterschrift = c.String(),
                        KostenZeit = c.Decimal(precision: 18, scale: 2),
                        KostenMaterial = c.Decimal(precision: 18, scale: 2),
                        KostenTotal = c.Decimal(precision: 18, scale: 2),
                        EigentuemeradresseId = c.Int(),
                        AusführungsadresseId = c.Int(),
                        RechnungsadresseId = c.Int(),
                        MitarbeiterId = c.Int(),
                        ProjektFK = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ausführungsadresse", t => t.AusführungsadresseId)
                .ForeignKey("dbo.Eigentuemeradresses", t => t.EigentuemeradresseId)
                .ForeignKey("dbo.Mitarbeiters", t => t.MitarbeiterId)
                .ForeignKey("dbo.Projekts", t => t.ProjektFK)
                .ForeignKey("dbo.Rechnungsadresses", t => t.RechnungsadresseId)
                .Index(t => t.EigentuemeradresseId)
                .Index(t => t.AusführungsadresseId)
                .Index(t => t.RechnungsadresseId)
                .Index(t => t.MitarbeiterId)
                .Index(t => t.ProjektFK);
            
            CreateTable(
                "dbo.Mitarbeiters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VorName = c.String(),
                        NachName = c.String(),
                        TelefonGeschäft = c.String(),
                        TelefonPrivat = c.String(),
                        Kürzel = c.String(),
                        GfeNummer = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projekts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nummer = c.Int(nullable: false),
                        Status = c.Int(),
                        KostenZeit = c.Decimal(precision: 18, scale: 2),
                        KostenMaterial = c.Decimal(precision: 18, scale: 2),
                        KostenTotal = c.Decimal(precision: 18, scale: 2),
                        MitarbeiterId = c.Int(),
                        OrganisationFK = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mitarbeiters", t => t.MitarbeiterId)
                .ForeignKey("dbo.Organisations", t => t.OrganisationFK)
                .Index(t => t.MitarbeiterId)
                .Index(t => t.OrganisationFK);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        KostenZeit = c.Decimal(precision: 18, scale: 2),
                        KostenMaterial = c.Decimal(precision: 18, scale: 2),
                        KostenTotal = c.Decimal(precision: 18, scale: 2),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rechnungsadresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Vornamne = c.String(),
                        Strasse = c.String(),
                        Plz = c.String(),
                        Ort = c.String(),
                        TelefonGeschäft = c.String(),
                        TelefonPrivat = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZeitKostens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnzahlStunden = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KostenTotal = c.Decimal(precision: 18, scale: 2),
                        VerrechnungsartId = c.Int(),
                        MitarbeiterId = c.Int(),
                        ServicerapportFK = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mitarbeiters", t => t.MitarbeiterId)
                .ForeignKey("dbo.Servicerapports", t => t.ServicerapportFK)
                .ForeignKey("dbo.Verrechnungsarts", t => t.VerrechnungsartId)
                .Index(t => t.VerrechnungsartId)
                .Index(t => t.MitarbeiterId)
                .Index(t => t.ServicerapportFK);
            
            CreateTable(
                "dbo.Verrechnungsarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nummer = c.String(),
                        KostenProStunde = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Wochenrapports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kalenderwoche = c.String(),
                        StartDatum = c.DateTime(),
                        EndDate = c.DateTime(),
                        Status = c.Int(),
                        StundenTotal = c.Decimal(precision: 18, scale: 2),
                        MitarbeiterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mitarbeiters", t => t.MitarbeiterId)
                .Index(t => t.MitarbeiterId);
            
            CreateTable(
                "dbo.WochenrapportSpesenEintrags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Beschreibung = c.String(),
                        Kosten = c.Decimal(precision: 18, scale: 2),
                        WochenrapportFK = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wochenrapports", t => t.WochenrapportFK)
                .Index(t => t.WochenrapportFK);
            
            CreateTable(
                "dbo.WochenrapportZeitEintrags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(),
                        ProjektnNummer = c.Int(),
                        ServicrapportNummer = c.Int(),
                        Ausführungsadresse = c.String(),
                        Zeit = c.Decimal(precision: 18, scale: 2),
                        WochenrapportUeberzeitFaktorId = c.Int(),
                        WochenrapportFK = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wochenrapports", t => t.WochenrapportFK)
                .ForeignKey("dbo.WochenrapportUeberzeitFaktors", t => t.WochenrapportUeberzeitFaktorId)
                .Index(t => t.WochenrapportUeberzeitFaktorId)
                .Index(t => t.WochenrapportFK);
            
            CreateTable(
                "dbo.WochenrapportUeberzeitFaktors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Faktor = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WochenrapportZeitEintrags", "WochenrapportUeberzeitFaktorId", "dbo.WochenrapportUeberzeitFaktors");
            DropForeignKey("dbo.WochenrapportZeitEintrags", "WochenrapportFK", "dbo.Wochenrapports");
            DropForeignKey("dbo.WochenrapportSpesenEintrags", "WochenrapportFK", "dbo.Wochenrapports");
            DropForeignKey("dbo.Wochenrapports", "MitarbeiterId", "dbo.Mitarbeiters");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MaterialKostens", "VerrechnungsartId", "dbo.Materials");
            DropForeignKey("dbo.ZeitKostens", "VerrechnungsartId", "dbo.Verrechnungsarts");
            DropForeignKey("dbo.ZeitKostens", "ServicerapportFK", "dbo.Servicerapports");
            DropForeignKey("dbo.ZeitKostens", "MitarbeiterId", "dbo.Mitarbeiters");
            DropForeignKey("dbo.Servicerapports", "RechnungsadresseId", "dbo.Rechnungsadresses");
            DropForeignKey("dbo.Servicerapports", "ProjektFK", "dbo.Projekts");
            DropForeignKey("dbo.Projekts", "OrganisationFK", "dbo.Organisations");
            DropForeignKey("dbo.Projekts", "MitarbeiterId", "dbo.Mitarbeiters");
            DropForeignKey("dbo.Servicerapports", "MitarbeiterId", "dbo.Mitarbeiters");
            DropForeignKey("dbo.MaterialKostens", "ServicerapportFK", "dbo.Servicerapports");
            DropForeignKey("dbo.Servicerapports", "EigentuemeradresseId", "dbo.Eigentuemeradresses");
            DropForeignKey("dbo.Servicerapports", "AusführungsadresseId", "dbo.Ausführungsadresse");
            DropIndex("dbo.WochenrapportZeitEintrags", new[] { "WochenrapportFK" });
            DropIndex("dbo.WochenrapportZeitEintrags", new[] { "WochenrapportUeberzeitFaktorId" });
            DropIndex("dbo.WochenrapportSpesenEintrags", new[] { "WochenrapportFK" });
            DropIndex("dbo.Wochenrapports", new[] { "MitarbeiterId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ZeitKostens", new[] { "ServicerapportFK" });
            DropIndex("dbo.ZeitKostens", new[] { "MitarbeiterId" });
            DropIndex("dbo.ZeitKostens", new[] { "VerrechnungsartId" });
            DropIndex("dbo.Projekts", new[] { "OrganisationFK" });
            DropIndex("dbo.Projekts", new[] { "MitarbeiterId" });
            DropIndex("dbo.Servicerapports", new[] { "ProjektFK" });
            DropIndex("dbo.Servicerapports", new[] { "MitarbeiterId" });
            DropIndex("dbo.Servicerapports", new[] { "RechnungsadresseId" });
            DropIndex("dbo.Servicerapports", new[] { "AusführungsadresseId" });
            DropIndex("dbo.Servicerapports", new[] { "EigentuemeradresseId" });
            DropIndex("dbo.MaterialKostens", new[] { "ServicerapportFK" });
            DropIndex("dbo.MaterialKostens", new[] { "VerrechnungsartId" });
            DropTable("dbo.WochenrapportUeberzeitFaktors");
            DropTable("dbo.WochenrapportZeitEintrags");
            DropTable("dbo.WochenrapportSpesenEintrags");
            DropTable("dbo.Wochenrapports");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Verrechnungsarts");
            DropTable("dbo.ZeitKostens");
            DropTable("dbo.Rechnungsadresses");
            DropTable("dbo.Organisations");
            DropTable("dbo.Projekts");
            DropTable("dbo.Mitarbeiters");
            DropTable("dbo.Servicerapports");
            DropTable("dbo.MaterialKostens");
            DropTable("dbo.Materials");
            DropTable("dbo.Eigentuemeradresses");
            DropTable("dbo.Ausführungsadresse");
        }
    }
}
