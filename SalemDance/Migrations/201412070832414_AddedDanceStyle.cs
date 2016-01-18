namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDanceStyle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DanceStyle", "Event_EventID", "dbo.Event");
            DropIndex("dbo.DanceStyle", new[] { "Event_EventID" });
            AddColumn("dbo.Event", "DanceStyleID", c => c.Int());
            CreateIndex("dbo.Event", "DanceStyleID");
            AddForeignKey("dbo.Event", "DanceStyleID", "dbo.DanceStyle", "DanceStyleID");
            DropColumn("dbo.DanceStyle", "Event_EventID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DanceStyle", "Event_EventID", c => c.Int());
            DropForeignKey("dbo.Event", "DanceStyleID", "dbo.DanceStyle");
            DropIndex("dbo.Event", new[] { "DanceStyleID" });
            DropColumn("dbo.Event", "DanceStyleID");
            CreateIndex("dbo.DanceStyle", "Event_EventID");
            AddForeignKey("dbo.DanceStyle", "Event_EventID", "dbo.Event", "EventID");
        }
    }
}
