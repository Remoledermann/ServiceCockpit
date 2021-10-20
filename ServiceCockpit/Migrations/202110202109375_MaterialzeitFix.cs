namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaterialzeitFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MaterialKostens", name: "VerrechnungsartId", newName: "MaterialId");
            RenameIndex(table: "dbo.MaterialKostens", name: "IX_VerrechnungsartId", newName: "IX_MaterialId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MaterialKostens", name: "IX_MaterialId", newName: "IX_VerrechnungsartId");
            RenameColumn(table: "dbo.MaterialKostens", name: "MaterialId", newName: "VerrechnungsartId");
        }
    }
}
