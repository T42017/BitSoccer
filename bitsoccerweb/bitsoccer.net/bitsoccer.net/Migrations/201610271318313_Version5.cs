namespace bitsoccer.net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        UserId = c.String(),
                        FilePath = c.String(),
                        Private = c.Boolean(nullable: false),
                        Matches = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Teams");
        }
    }
}
