namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class laatste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieReview", "Movie_MovieID", "dbo.Movie");
            DropIndex("dbo.MovieReview", new[] { "Movie_MovieID" });
            AlterColumn("dbo.MovieReview", "Movie_MovieID", c => c.Long(nullable: false));
            CreateIndex("dbo.MovieReview", "Movie_MovieID");
            AddForeignKey("dbo.MovieReview", "Movie_MovieID", "dbo.Movie", "MovieID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieReview", "Movie_MovieID", "dbo.Movie");
            DropIndex("dbo.MovieReview", new[] { "Movie_MovieID" });
            AlterColumn("dbo.MovieReview", "Movie_MovieID", c => c.Long());
            CreateIndex("dbo.MovieReview", "Movie_MovieID");
            AddForeignKey("dbo.MovieReview", "Movie_MovieID", "dbo.Movie", "MovieID");
        }
    }
}
