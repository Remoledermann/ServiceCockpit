namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WechselnDerStatusIntZuString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Servicerapports", "Status", c => c.String());
            AlterColumn("dbo.Projekts", "Status", c => c.String());
            AlterColumn("dbo.Wochenrapports", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wochenrapports", "Status", c => c.Int());
            AlterColumn("dbo.Projekts", "Status", c => c.Int());
            AlterColumn("dbo.Servicerapports", "Status", c => c.Int());
        }
    }
}
