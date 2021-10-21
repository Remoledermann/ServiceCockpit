namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Verrechungsartenanspassen1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servicerapports", "EmailAdresse", c => c.String());
            DropColumn("dbo.Servicerapports", "Bemerkung");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servicerapports", "Bemerkung", c => c.String());
            DropColumn("dbo.Servicerapports", "EmailAdresse");
        }
    }
}
