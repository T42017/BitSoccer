namespace bitsoccer.net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CompletedIntroduction", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CompletedIntroduction");
        }
    }
}
