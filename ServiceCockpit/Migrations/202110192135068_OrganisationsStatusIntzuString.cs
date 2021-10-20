namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganisationsStatusIntzuString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organisations", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organisations", "Status", c => c.Int());
        }
    }
}
