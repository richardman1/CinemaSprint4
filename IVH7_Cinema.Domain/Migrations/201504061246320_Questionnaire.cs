namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Questionnaire : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questionnaire",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeneralRating = c.Int(nullable: false),
                        EmployeeRating = c.Int(nullable: false),
                        FilmsRating = c.Int(nullable: false),
                        HygieneRating = c.Int(nullable: false),
                        ScreenRating = c.Int(nullable: false),
                        ParkingRating = c.Int(nullable: false),
                        SiteRating = c.Int(nullable: false),
                        FoodRating = c.Int(nullable: false),
                        PriceRating = c.Int(nullable: false),
                        BuildingRating = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Questionnaire");
        }
    }
}
