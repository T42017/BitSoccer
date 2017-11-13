namespace bitsoccer.net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version9 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Teams");
            AddColumn("dbo.Teams", "TeamsId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Teams", "TeamsId");
            DropColumn("dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Id", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Teams");
            DropColumn("dbo.Teams", "TeamsId");
            AddPrimaryKey("dbo.Teams", "Id");
        }
    }
}
