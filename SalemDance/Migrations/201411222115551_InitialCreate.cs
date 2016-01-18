namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DanceMove",
                c => new
                    {
                        DanceMoveID = c.Int(nullable: false, identity: true),
                        DanceStyleID = c.Int(nullable: false),
                        DanceMoveName = c.String(),
                        Link = c.String(),
                        Venue_VenueID = c.Int(),
                    })
                .PrimaryKey(t => t.DanceMoveID)
                .ForeignKey("dbo.DanceStyle", t => t.DanceStyleID, cascadeDelete: true)
                .ForeignKey("dbo.Venue", t => t.Venue_VenueID)
                .Index(t => t.DanceStyleID)
                .Index(t => t.Venue_VenueID);
            
            CreateTable(
                "dbo.DanceStyle",
                c => new
                    {
                        DanceStyleID = c.Int(nullable: false, identity: true),
                        StyleName = c.String(),
                        MusicGenre = c.String(),
                        Event_EventID = c.Int(),
                    })
                .PrimaryKey(t => t.DanceStyleID)
                .ForeignKey("dbo.Event", t => t.Event_EventID)
                .Index(t => t.Event_EventID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        VenueID = c.Int(nullable: false),
                        EventName = c.String(maxLength: 100),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        Description = c.String(),
                        FoodServed = c.Boolean(nullable: false),
                        AgeRestriction = c.Boolean(nullable: false),
                        MinAge = c.Int(nullable: false),
                        MusicGenre = c.String(),
                        HasLessons = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Venue", t => t.VenueID, cascadeDelete: true)
                .Index(t => t.VenueID);
            
            CreateTable(
                "dbo.Venue",
                c => new
                    {
                        VenueID = c.Int(nullable: false, identity: true),
                        VenueName = c.String(),
                        Address = c.String(),
                        Website = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.VenueID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        RevierFirstName = c.String(maxLength: 50),
                        RevierLastName = c.String(maxLength: 50),
                        Stars = c.Int(nullable: false),
                        Comment = c.String(),
                        Title = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        VenueID = c.Int(),
                        EventID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .ForeignKey("dbo.Venue", t => t.VenueID)
                .Index(t => t.VenueID)
                .Index(t => t.EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "VenueID", "dbo.Venue");
            DropForeignKey("dbo.Review", "EventID", "dbo.Event");
            DropForeignKey("dbo.Event", "VenueID", "dbo.Venue");
            DropForeignKey("dbo.DanceMove", "Venue_VenueID", "dbo.Venue");
            DropForeignKey("dbo.DanceStyle", "Event_EventID", "dbo.Event");
            DropForeignKey("dbo.DanceMove", "DanceStyleID", "dbo.DanceStyle");
            DropIndex("dbo.Review", new[] { "EventID" });
            DropIndex("dbo.Review", new[] { "VenueID" });
            DropIndex("dbo.Event", new[] { "VenueID" });
            DropIndex("dbo.DanceStyle", new[] { "Event_EventID" });
            DropIndex("dbo.DanceMove", new[] { "Venue_VenueID" });
            DropIndex("dbo.DanceMove", new[] { "DanceStyleID" });
            DropTable("dbo.Review");
            DropTable("dbo.Venue");
            DropTable("dbo.Event");
            DropTable("dbo.DanceStyle");
            DropTable("dbo.DanceMove");
        }
    }
}
