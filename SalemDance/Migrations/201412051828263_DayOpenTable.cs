namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayOpenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayOpen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventDayOpen",
                c => new
                    {
                        Event_EventID = c.Int(nullable: false),
                        DayOpen_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_EventID, t.DayOpen_ID })
                .ForeignKey("dbo.Event", t => t.Event_EventID, cascadeDelete: true)
                .ForeignKey("dbo.DayOpen", t => t.DayOpen_ID, cascadeDelete: true)
                .Index(t => t.Event_EventID)
                .Index(t => t.DayOpen_ID);
            
        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.EventDayOpen", "DayOpen_ID", "dbo.DayOpen");
            DropForeignKey("dbo.EventDayOpen", "Event_EventID", "dbo.Event");
            DropIndex("dbo.EventDayOpen", new[] { "DayOpen_ID" });
            DropIndex("dbo.EventDayOpen", new[] { "Event_EventID" });
            DropTable("dbo.EventDayOpen");
            DropTable("dbo.DayOpen");
        }
    }
}
