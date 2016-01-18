namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "EntryCost", c => c.Single());
            AlterColumn("dbo.Event", "StartTime", c => c.String());
            AlterColumn("dbo.Event", "EndTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "EndTime", c => c.Int());
            AlterColumn("dbo.Event", "StartTime", c => c.Int());
            DropColumn("dbo.Event", "EntryCost");
        }
    }
}
