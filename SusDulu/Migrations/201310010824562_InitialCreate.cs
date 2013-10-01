namespace SusDulu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        origin = c.String(),
                        destination = c.String(),
                        level = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Flights");
        }
    }
}
