namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDayEventJoinTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DayOpen", "EventID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DayOpen", "EventID", c => c.Int(nullable: false));
        }
    }
}
