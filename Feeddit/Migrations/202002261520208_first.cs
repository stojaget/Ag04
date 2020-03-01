namespace Feeddit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Articles", newName: "Article");
            RenameTable(name: "dbo.Users", newName: "User");
            AddColumn("dbo.User", "LoginErrorMessage", c => c.String());
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false));
            DropColumn("dbo.User", "FirstName");
            DropColumn("dbo.User", "LastName");
            DropColumn("dbo.User", "HCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "HCode", c => c.String());
            AddColumn("dbo.User", "LastName", c => c.String());
            AddColumn("dbo.User", "FirstName", c => c.String());
            AlterColumn("dbo.User", "Password", c => c.String());
            DropColumn("dbo.User", "LoginErrorMessage");
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.Article", newName: "Articles");
        }
    }
}
