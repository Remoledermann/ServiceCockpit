namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Verrechungsartenanspassen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Verrechnungsarts", "KostenProStunde", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Verrechnungsarts", "KostenProStunde", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
