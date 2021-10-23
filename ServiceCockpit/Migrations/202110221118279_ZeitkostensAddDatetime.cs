namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZeitkostensAddDatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ZeitKostens", "Eintragsdatum", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ZeitKostens", "Eintragsdatum");
        }
    }
}
