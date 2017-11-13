namespace bitsoccer.net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version7 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Teams");
            AddColumn("dbo.Teams", "Draws", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Teams");
            AlterColumn("dbo.Teams", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Teams", "Draws");
            AddPrimaryKey("dbo.Teams", "Id");
        }
    }
}
