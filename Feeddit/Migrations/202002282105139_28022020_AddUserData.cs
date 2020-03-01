namespace Feeddit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28022020_AddUserData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "UserName", c => c.String());
        }
    }
}
