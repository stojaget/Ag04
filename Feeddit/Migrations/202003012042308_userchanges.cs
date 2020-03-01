namespace Feeddit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userchanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vote",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArticleID = c.Long(nullable: false),
                        UserID = c.Int(nullable: false),
                        VoteNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Article", "Vote_ID", c => c.Int());
            AddColumn("dbo.User", "Vote_ID", c => c.Int());
            CreateIndex("dbo.Article", "Vote_ID");
            CreateIndex("dbo.User", "Vote_ID");
            AddForeignKey("dbo.User", "Vote_ID", "dbo.Vote", "ID");
            AddForeignKey("dbo.Article", "Vote_ID", "dbo.Vote", "ID");
            DropColumn("dbo.User", "LoginErrorMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "LoginErrorMessage", c => c.String());
            DropForeignKey("dbo.Article", "Vote_ID", "dbo.Vote");
            DropForeignKey("dbo.User", "Vote_ID", "dbo.Vote");
            DropIndex("dbo.User", new[] { "Vote_ID" });
            DropIndex("dbo.Article", new[] { "Vote_ID" });
            DropColumn("dbo.User", "Vote_ID");
            DropColumn("dbo.Article", "Vote_ID");
            DropTable("dbo.Vote");
        }
    }
}
