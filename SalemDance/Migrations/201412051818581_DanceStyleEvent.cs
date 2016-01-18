namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DanceStyleEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "StyleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "StyleName");
        }
    }
}
