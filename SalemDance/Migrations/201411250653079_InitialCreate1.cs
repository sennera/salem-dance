namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Review", name: "RevierLastName", newName: "LastName");
            AddColumn("dbo.Review", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.DanceMove", "DanceMoveName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.DanceStyle", "StyleName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.DanceStyle", "MusicGenre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Event", "EventName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Venue", "VenueName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Review", "Comment", c => c.String(nullable: false));
            DropColumn("dbo.Review", "RevierFirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Review", "RevierFirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Review", "Comment", c => c.String());
            AlterColumn("dbo.Venue", "VenueName", c => c.String());
            AlterColumn("dbo.Event", "EventName", c => c.String(maxLength: 100));
            AlterColumn("dbo.DanceStyle", "MusicGenre", c => c.String());
            AlterColumn("dbo.DanceStyle", "StyleName", c => c.String());
            AlterColumn("dbo.DanceMove", "DanceMoveName", c => c.String());
            DropColumn("dbo.Review", "FirstName");
            RenameColumn(table: "dbo.Review", name: "LastName", newName: "RevierLastName");
        }
    }
}
