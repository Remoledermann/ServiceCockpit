namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjektNummernichtmehrerforderlich : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projekts", "Nummer", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projekts", "Nummer", c => c.Int(nullable: false));
        }
    }
}
