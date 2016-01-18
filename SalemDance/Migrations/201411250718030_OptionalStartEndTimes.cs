namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalStartEndTimes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "StartTime", c => c.Int());
            AlterColumn("dbo.Event", "EndTime", c => c.Int());
            AlterColumn("dbo.Event", "MinAge", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "MinAge", c => c.Int(nullable: false));
            AlterColumn("dbo.Event", "EndTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Event", "StartTime", c => c.Int(nullable: false));
        }
    }
}
