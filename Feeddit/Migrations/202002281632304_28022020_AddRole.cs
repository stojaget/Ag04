namespace Feeddit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28022020_AddRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Role");
        }
    }
}
