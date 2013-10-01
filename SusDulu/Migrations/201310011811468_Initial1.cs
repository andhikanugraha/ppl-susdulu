namespace SusDulu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Flights", "departure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "departure", c => c.DateTime(nullable: false));
        }
    }
}
