namespace PBL3_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BusStation", "AccountID");
            DropColumn("dbo.Customer", "AccountID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "AccountID", c => c.Int(nullable: false));
            AddColumn("dbo.BusStation", "AccountID", c => c.Int(nullable: false));
        }
    }
}
