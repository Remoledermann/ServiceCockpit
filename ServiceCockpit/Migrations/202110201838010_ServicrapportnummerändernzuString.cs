namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServicrapportnummerÃ¤ndernzuString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Servicerapports", "VoranmeldungNummer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Servicerapports", "VoranmeldungNummer", c => c.Int());
        }
    }
}
