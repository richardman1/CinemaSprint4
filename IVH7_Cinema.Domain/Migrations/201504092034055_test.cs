namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LostObject", "FinderName", c => c.String());
            AlterColumn("dbo.LostObject", "FinderEmail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LostObject", "FinderEmail", c => c.String(nullable: false));
            AlterColumn("dbo.LostObject", "FinderName", c => c.String(nullable: false));
        }
    }
}
