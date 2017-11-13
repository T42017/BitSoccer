namespace bitsoccer.net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teams", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "UserId", c => c.String());
        }
    }
}
