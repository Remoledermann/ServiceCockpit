namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatetimeServicrapportEintragGeändert : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ZeitKostens", "Eintragsdatum", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZeitKostens", "Eintragsdatum", c => c.DateTime());
        }
    }
}
