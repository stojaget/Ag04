namespace Feeddit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleanup_Vote : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "Vote_ID", "dbo.Vote");
            DropForeignKey("dbo.Article", "Vote_ID", "dbo.Vote");
            DropIndex("dbo.Article", new[] { "Vote_ID" });
            DropIndex("dbo.User", new[] { "Vote_ID" });
            CreateIndex("dbo.Vote", "ArticleID");
            CreateIndex("dbo.Vote", "UserID");
            AddForeignKey("dbo.Vote", "ArticleID", "dbo.Article", "ID", cascadeDelete: false);
            AddForeignKey("dbo.Vote", "UserID", "dbo.User", "ID", cascadeDelete: false);
            DropColumn("dbo.Article", "Vote_ID");
            DropColumn("dbo.User", "Vote_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Vote_ID", c => c.Int());
            AddColumn("dbo.Article", "Vote_ID", c => c.Int());
            DropForeignKey("dbo.Vote", "UserID", "dbo.User");
            DropForeignKey("dbo.Vote", "ArticleID", "dbo.Article");
            DropIndex("dbo.Vote", new[] { "UserID" });
            DropIndex("dbo.Vote", new[] { "ArticleID" });
            CreateIndex("dbo.User", "Vote_ID");
            CreateIndex("dbo.Article", "Vote_ID");
            AddForeignKey("dbo.Article", "Vote_ID", "dbo.Vote", "ID");
            AddForeignKey("dbo.User", "Vote_ID", "dbo.Vote", "ID");
        }
    }
}
