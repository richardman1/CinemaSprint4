namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lostobject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LostObject",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                        Foundplace = c.String(),
                        FinderName = c.String(nullable: false),
                        FinderAddress = c.String(),
                        FinderEmail = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        PickedUp = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LostObject");
        }
    }
}
