namespace SusDulu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDistanceSchedulePrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "distance", c => c.Int(nullable: false));
            AddColumn("dbo.Flights", "schedule", c => c.DateTime(nullable: false));
            AddColumn("dbo.Flights", "price", c => c.Long(nullable: false));
            AlterColumn("dbo.Flights", "origin", c => c.String(nullable: false));
            AlterColumn("dbo.Flights", "destination", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flights", "destination", c => c.String());
            AlterColumn("dbo.Flights", "origin", c => c.String());
            DropColumn("dbo.Flights", "price");
            DropColumn("dbo.Flights", "schedule");
            DropColumn("dbo.Flights", "distance");
        }
    }
}
