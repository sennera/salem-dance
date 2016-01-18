namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayOfWeekEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "EntryCost", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "EntryCost", c => c.Single());
        }
    }
}
