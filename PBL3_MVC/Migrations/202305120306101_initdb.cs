namespace PBL3_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50, fixedLength: true),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.BusStation",
                c => new
                    {
                        BusStationID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.BusStationID)
                .ForeignKey("dbo.Account", t => t.BusStationID)
                .Index(t => t.BusStationID);
            
            CreateTable(
                "dbo.Bus",
                c => new
                    {
                        BusID = c.Int(nullable: false, identity: true),
                        BusStationID = c.Int(nullable: false),
                        BusName = c.String(nullable: false, maxLength: 50),
                        NumberOfSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BusID)
                .ForeignKey("dbo.BusStation", t => t.BusStationID)
                .Index(t => t.BusStationID);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        ScheduleID = c.Int(nullable: false, identity: true),
                        BusID = c.Int(nullable: false),
                        RouteID = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        DestinationTime = c.DateTime(nullable: false),
                        Weekday = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleID)
                .ForeignKey("dbo.Route", t => t.RouteID)
                .ForeignKey("dbo.Bus", t => t.BusID)
                .Index(t => t.BusID)
                .Index(t => t.RouteID);
            
            CreateTable(
                "dbo.Route",
                c => new
                    {
                        RouteID = c.Int(nullable: false, identity: true),
                        RouteName = c.String(nullable: false, maxLength: 50),
                        Departure = c.String(nullable: false, maxLength: 50),
                        Destination = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RouteID);
            
            CreateTable(
                "dbo.Seat",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        ScheduleID = c.Int(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        BillID = c.Int(),
                    })
                .PrimaryKey(t => t.SeatID)
                .ForeignKey("dbo.Bill", t => t.BillID)
                .ForeignKey("dbo.Schedule", t => t.ScheduleID)
                .Index(t => t.ScheduleID)
                .Index(t => t.BillID);
            
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BillID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Account", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Customer", "CustomerID", "dbo.Account");
            DropForeignKey("dbo.BusStation", "BusStationID", "dbo.Account");
            DropForeignKey("dbo.Bus", "BusStationID", "dbo.BusStation");
            DropForeignKey("dbo.Schedule", "BusID", "dbo.Bus");
            DropForeignKey("dbo.Seat", "ScheduleID", "dbo.Schedule");
            DropForeignKey("dbo.Seat", "BillID", "dbo.Bill");
            DropForeignKey("dbo.Bill", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Schedule", "RouteID", "dbo.Route");
            DropIndex("dbo.Customer", new[] { "CustomerID" });
            DropIndex("dbo.Bill", new[] { "CustomerID" });
            DropIndex("dbo.Seat", new[] { "BillID" });
            DropIndex("dbo.Seat", new[] { "ScheduleID" });
            DropIndex("dbo.Schedule", new[] { "RouteID" });
            DropIndex("dbo.Schedule", new[] { "BusID" });
            DropIndex("dbo.Bus", new[] { "BusStationID" });
            DropIndex("dbo.BusStation", new[] { "BusStationID" });
            DropIndex("dbo.Account", new[] { "RoleID" });
            DropTable("dbo.Role");
            DropTable("dbo.Customer");
            DropTable("dbo.Bill");
            DropTable("dbo.Seat");
            DropTable("dbo.Route");
            DropTable("dbo.Schedule");
            DropTable("dbo.Bus");
            DropTable("dbo.BusStation");
            DropTable("dbo.Account");
        }
    }
}
