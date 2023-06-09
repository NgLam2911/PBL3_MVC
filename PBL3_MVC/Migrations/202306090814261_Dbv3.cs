namespace PBL3_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dbv3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Route", "DepartureID");
            DropColumn("dbo.Route", "DestinationID");
            RenameColumn(table: "dbo.Route", name: "Departure_LocationID", newName: "DepartureID");
            RenameColumn(table: "dbo.Route", name: "Destination_LocationID", newName: "DestinationID");
            RenameIndex(table: "dbo.Route", name: "IX_Departure_LocationID", newName: "IX_DepartureID");
            RenameIndex(table: "dbo.Route", name: "IX_Destination_LocationID", newName: "IX_DestinationID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Route", name: "IX_DestinationID", newName: "IX_Destination_LocationID");
            RenameIndex(table: "dbo.Route", name: "IX_DepartureID", newName: "IX_Departure_LocationID");
            RenameColumn(table: "dbo.Route", name: "DestinationID", newName: "Destination_LocationID");
            RenameColumn(table: "dbo.Route", name: "DepartureID", newName: "Departure_LocationID");
            AddColumn("dbo.Route", "DestinationID", c => c.Int(nullable: false));
            AddColumn("dbo.Route", "DepartureID", c => c.Int(nullable: false));
        }
    }
}
