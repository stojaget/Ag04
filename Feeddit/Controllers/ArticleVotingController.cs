using Feeddit.DAL;
using Feeddit.Models;
using Feeddit.ViewModel;
using NLog;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Feeddit.Controllers
{
    [CustomAuthorize]
    public class ArticleVotingController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: ArticleVoting
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewBag.VotesSortParm = String.IsNullOrEmpty(sortOrder) ? "votes_desc" : "";

            FeedditContext context = new FeedditContext();

            List<ArticleVoting> ArticleVotingVM = new List<ArticleVoting>();

            //var votingList = (from article in context.Articles

            //                  join vote in context.Votes on article.ID equals vote.ArticleID
            //                  join user in context.Users on vote.UserID equals user.ID
            //                  select new { article.Title, article.Author, article.Votes, article.ArticleUrl, vote.VoteNumber, article.ID, userID = user.ID });
            var votingList = (from article in context.Articles

                              join vote in context.Votes on article.ID equals vote.ArticleID
                              join user in context.Users on article.UserID equals user.ID
                              select new { article.Title, article.Author, article.Votes, article.ArticleUrl, vote.VoteNumber, article.ID, userID = user.ID });

            if (votingList.Count() == 0)
            {
                TempData["alert"] = "Trenutno nema članaka za prikaz.";
                TempData["aType"] = "danger";
            }
            foreach (var item in votingList)

            {

                ArticleVoting objcvm = new ArticleVoting(); // ViewModel

                objcvm.Title = item.Title;

                objcvm.Author = item.Author;

                objcvm.Votes = item.Votes;

                objcvm.ArticleUrl = item.ArticleUrl;

                objcvm.VoteNumber = item.VoteNumber;

                objcvm.ArticleID = item.ID;

                objcvm.UserID = item.userID;

                ArticleVotingVM.Add(objcvm);

            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //if (searchString == null ||  searchString.Trim().Count() < 4)
            //{
            //    ViewBag.ErrorMessage = "Za pretragu morate unijeti bar 3 znaka.";
            //    return View();
            //}



            if (!String.IsNullOrEmpty(searchString))
            {
                //votingList = votingList.Where(s => s.Title.Contains(searchString));
                ArticleVotingVM = ArticleVotingVM.Where(s => s.Title.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "title_desc":
                    //votingList = votingList.OrderByDescending(s => s.Title);
                    ArticleVotingVM = ArticleVotingVM.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author_desc":
                    //votingList = votingList.OrderByDescending(s => s.Author);
                    ArticleVotingVM = ArticleVotingVM.OrderByDescending(s => s.Author).ToList();
                    break;
                case "votes_desc":
                    //votingList = votingList.OrderByDescending(s => s.Votes);
                    ArticleVotingVM = ArticleVotingVM.OrderByDescending(s => s.Votes).ToList();
                    break;
                default:
                    //votingList = votingList.OrderBy(s => s.Title);
                    ArticleVotingVM = ArticleVotingVM.OrderBy(s => s.Title).ToList();
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(ArticleVotingVM.ToPagedList(pageNumber, pageSize));

        }


        public ActionResult Upvote(long id)
        {

            using (FeedditContext db = new FeedditContext())
            {
                int UserID = Convert.ToInt32(Session["UserID"]);
                Article article = db.Articles.Find(id);
                Vote vote = db.Votes.Where(b => b.ArticleID == article.ID && b.UserID == UserID).FirstOrDefault();
                if (vote != null)
                {
                    int brGlasova = vote.VoteNumber;
                    if (brGlasova == 0)
                    {
                        vote.VoteNumber = 1;
                        article.Votes++;
                        db.SaveChanges();
                        TempData["alert"] = "Vaš glas je uspješno zaprimljen.";
                        TempData["aType"] = "success";
                    }
                    else
                    {
                        TempData["alert"] = "Već ste glasali!";
                        TempData["aType"] = "danger";
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult Downvote(long id)
        {

            using (FeedditContext db = new FeedditContext())
            {
                int UserID = Convert.ToInt32(Session["UserID"]);
                Article article = db.Articles.Find(id);
                Vote vote = db.Votes.Where(b => b.ArticleID == article.ID && b.UserID == UserID).FirstOrDefault();
                if (vote != null)
                {
                    int brGlasova = vote.VoteNumber;
                    if (brGlasova == 1)
                    {
                        vote.VoteNumber = 0;
                        article.Votes--;
                        db.SaveChanges();
                        TempData["alert"] = "Vaš glas je uspješno opozvan.";
                        TempData["aType"] = "success";
                    }
                    else
                    {
                        TempData["alert"] = "Već ste glasali!";
                        TempData["aType"] = "danger";
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }


        // GET: Articles/Create
        public ActionResult Create()
        {
            using (FeedditContext db = new FeedditContext())
            {
                int id = (int)Session["UserID"];
                var userDetails = db.Users.Where(x => x.ID == id).FirstOrDefault();
                if (userDetails != null)
                {
                    ViewBag.Name = userDetails.Name;
                }
            }


            return View();
        }

        // POST: Articles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,ArticleUrl,Author")] Article article)
        {
            FeedditContext db = new FeedditContext();
            try
            {
                if (ModelState.IsValid)
                {
                    // get id of currently logged user
                    int UserID = Convert.ToInt32(Session["UserID"]);
                    article.UserID = UserID;
                    article.DateCreated = DateTime.Now;
                    db.Articles.Add(article);

                    db.SaveChanges();
                    // mora se za glas kreirati novi zapis
                    Vote vote = new Vote();
                    vote.ArticleID = article.ID;
                    vote.UserID = UserID;
                    vote.VoteNumber = 0;
                    db.Votes.Add(vote);
                    db.SaveChanges();
                    TempData["alert"] = "Članak uspješno unesen.";
                    TempData["aType"] = "success";
                    logger.Info("Created new article, id = " + article.ID + " , date created: " + DateTime.Now + ", user id = " + article.UserID);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["alert"] = "Error occured during article creation. Please try again.";
                    TempData["aType"] = "danger";

                }
            }
            catch (DataException ex)
            {
                logger.Error("Error during article creation: " + ex.Message, ex);
                ModelState.AddModelError("ex", "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(article);
        }
    }

}