namespace Feeddit.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Feeddit.DAL.FeedditContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Feeddit.DAL.FeedditContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.Users.AddOrUpdate(i => i.UserName,
     new User
     {
         UserName = "stojaget",
         Password = "Darkobosak84",  
         Name = "Darko Bosak",
         Role = "Admin",
         ID = 1
     },
      new User
      {
          UserName = "testUser",
          Password = "Biljeznica384",
          Name = "Test User",
          Role = "Admin",
          ID = 2
      },
      new User
      {
          UserName = "testUser2",
          Password = "Mobitel384",
          Name = "Test User Jr",
          Role = "User",
          ID = 3
      }
      ,
      new User
      {
          UserName = "testUser3",
          Password = "Mobitel3842",
          Name = "Test User Sr",
          Role = "User",
          ID = 4
      }
     );
        

          context.Articles.AddOrUpdate(i => i.Title,
       new Article
       {
           Title = "ASP Identity - customize users and roles",
           Author = "John Atten",
           DateCreated = DateTime.Now,
           UserID = 1,
           ArticleUrl = "http://johnatten.com/2014/06/22/asp-net-identity-2-0-customizing-users-and-roles/",
           Votes = 1

       },

      



        new Article
        {
            Title = "ASP MVC - adding a new field",
            Author = "Microsoft",
            DateCreated = DateTime.Now,
            UserID = 2,
            ArticleUrl = "https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/adding-a-new-field",
            Votes = 3
        },

         new Article
         {
             Title = "Querying in Entity Framework Core",
             Author = "Entity Framework ",
             DateCreated = DateTime.Now,
             UserID = 3,
             ArticleUrl = "https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx",
             Votes = 5
         }
         ,

         new Article
         {
             Title = "Entity Framework Core",
             Author = "Test User",
             DateCreated = DateTime.Now,
             UserID = 3,
             ArticleUrl = "https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx",
             Votes = 5
         }
         ,

         new Article
         {
             Title = "Entity Framework Core Tutorial",
             Author = "Test User Jr",
             DateCreated = DateTime.Now,
             UserID = 3,
             ArticleUrl = "https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx",
             Votes = 5
         }

         );
        }

    }
}
