using Feeddit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Feeddit.DAL
{
    public class FeedditInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FeedditContext>
    {
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
         ID = 1
     },
      new User
      {
          UserName = "testUser",
          Password = "Biljeznica384",
          ID = 2
      },
      new User
      {
          UserName = "testUser2",
          Password = "Mobitel384",
          ID = 3
      }
     );


            context.Articles.AddOrUpdate(i => i.Title,
         new Article
         {
             Title = "ASP Identity - customize users and roles",
             Author = "John Atten",
             DateCreated = DateTime.Now,
             UserID = 12,
             ArticleUrl = "http://johnatten.com/2014/06/22/asp-net-identity-2-0-customizing-users-and-roles/",
             Votes = 1

         },





          new Article
          {
              Title = "ASP MVC - adding a new field",
              Author = "Microsoft",
              DateCreated = DateTime.Now,
              UserID = 7,
              ArticleUrl = "https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/adding-a-new-field",
              Votes = 3
          },

           new Article
           {
               Title = "Querying in Entity Framework Core",
               Author = "Entity Framework ",
               DateCreated = DateTime.Now,
               UserID = 5,
               ArticleUrl = "https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx",
               Votes = 5
           });
        }

    }
}