namespace Feeddit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28022020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Name", c => c.String());
            AlterColumn("dbo.Article", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Article", "ArticleUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Article", "Author", c => c.String(nullable: false));
            CreateIndex("dbo.Article", "UserID");
            AddForeignKey("dbo.Article", "UserID", "dbo.User", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "UserID", "dbo.User");
            DropIndex("dbo.Article", new[] { "UserID" });
            AlterColumn("dbo.Article", "Author", c => c.String());
            AlterColumn("dbo.Article", "ArticleUrl", c => c.String());
            AlterColumn("dbo.Article", "Title", c => c.String());
            DropColumn("dbo.User", "Name");
        }
    }
}
