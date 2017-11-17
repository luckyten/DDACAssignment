namespace DDACAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        row = c.Int(nullable: false),
                        col = c.Int(nullable: false),
                        Booking_BookingId = c.Int(),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingId)
                .Index(t => t.Booking_BookingId);
            
            DropColumn("dbo.Bookings", "SeatNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "SeatNo", c => c.Int(nullable: false));
            DropForeignKey("dbo.Seats", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.Seats", new[] { "Booking_BookingId" });
            DropTable("dbo.Seats");
        }
    }
}
