namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUeberzeitDemServicRapport : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WochenrapportUeberzeitFaktors", newName: "ZeitKostenUeberzeitFaktors");
            DropForeignKey("dbo.WochenrapportZeitEintrags", "WochenrapportUeberzeitFaktorId", "dbo.WochenrapportUeberzeitFaktors");
            DropIndex("dbo.WochenrapportZeitEintrags", new[] { "WochenrapportUeberzeitFaktorId" });
            AddColumn("dbo.ZeitKostens", "AnzahlStundenTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ZeitKostens", "ZeitKostenUeberzeitFaktorId", c => c.Int());
            AlterColumn("dbo.ZeitKostens", "AnzahlStunden", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.ZeitKostens", "ZeitKostenUeberzeitFaktorId");
            AddForeignKey("dbo.ZeitKostens", "ZeitKostenUeberzeitFaktorId", "dbo.ZeitKostenUeberzeitFaktors", "Id");
            DropColumn("dbo.WochenrapportZeitEintrags", "WochenrapportUeberzeitFaktorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WochenrapportZeitEintrags", "WochenrapportUeberzeitFaktorId", c => c.Int());
            DropForeignKey("dbo.ZeitKostens", "ZeitKostenUeberzeitFaktorId", "dbo.ZeitKostenUeberzeitFaktors");
            DropIndex("dbo.ZeitKostens", new[] { "ZeitKostenUeberzeitFaktorId" });
            AlterColumn("dbo.ZeitKostens", "AnzahlStunden", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ZeitKostens", "ZeitKostenUeberzeitFaktorId");
            DropColumn("dbo.ZeitKostens", "AnzahlStundenTotal");
            CreateIndex("dbo.WochenrapportZeitEintrags", "WochenrapportUeberzeitFaktorId");
            AddForeignKey("dbo.WochenrapportZeitEintrags", "WochenrapportUeberzeitFaktorId", "dbo.WochenrapportUeberzeitFaktors", "Id");
            RenameTable(name: "dbo.ZeitKostenUeberzeitFaktors", newName: "WochenrapportUeberzeitFaktors");
        }
    }
}
