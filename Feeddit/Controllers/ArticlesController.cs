using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Feeddit.Models;
using Microsoft.AspNet.Identity;
using Feeddit.DAL;
using PagedList;
using NLog;

namespace Feeddit.Controllers
{
    [CustomAuthorize]
    public class ArticlesController : Controller
    {
        private FeedditContext db = new FeedditContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: Articles
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewBag.VotesSortParm = String.IsNullOrEmpty(sortOrder) ? "votes_desc" : "";

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

            var articles = from m in db.Articles
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Title.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "title_desc":
                    articles = articles.OrderByDescending(s => s.Title);
                    break;
                case "author_desc":
                    articles = articles.OrderByDescending(s => s.Author);
                    break;
                case "votes_desc":
                    articles = articles.OrderByDescending(s => s.Votes);
                    break;
                default:
                    articles = articles.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (this.db.Articles.Count() == 0)
            {
                TempData["alert"] = "Trenutno nema članaka za prikaz.";
                TempData["aType"] = "danger";
            }
            ViewBag.ListArticle = this.db.Articles.ToList();
            return View(articles.ToPagedList(pageNumber, pageSize));

        }


        public ActionResult IndexKorisnik(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewBag.VotesSortParm = String.IsNullOrEmpty(sortOrder) ? "votes_desc" : "";

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
            var userID = Convert.ToInt32(Session["UserID"]);

            string userRole = Session["Rola"].ToString();

            var articles = from m in db.Articles
                           select m;

            if (!userRole.Contains("Admin"))
            {
                articles = from m in db.Articles.Where(m => m.UserID == userID) select m;

            }

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Title.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "title_desc":
                    articles = articles.OrderByDescending(s => s.Title);
                    break;
                case "author_desc":
                    articles = articles.OrderByDescending(s => s.Author);
                    break;
                case "votes_desc":
                    articles = articles.OrderByDescending(s => s.Votes);
                    break;
                default:
                    articles = articles.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (this.db.Articles.Count() == 0)
            {
                TempData["alert"] = "Trenutno nema članaka za prikaz.";
                TempData["aType"] = "danger";
            }
            ViewBag.ListArticle = this.db.Articles.ToList();
            return View(articles.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult IndexAuto(string Prefix)
        {
            //if (searchTerm.Length < 3)
            //{
            //    return null;
            //}
            //Searching records from list using LINQ query  
            var articles = from m in db.Articles
                           select m;

            if (!String.IsNullOrEmpty(Prefix))
            {
                articles = articles.Where(s => s.Title.Contains(Prefix));
            }
            return Json(articles, JsonRequestBehavior.AllowGet);
        }

        // GET: Articles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
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

        // GET: Articles/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,DateCreated,Title,ArticleUrl,Author,Votes")] Article article)
        {
            // get id of currently logged user
            int UserID = Convert.ToInt32(Session["UserID"]);

            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewBag.VotesSortParm = String.IsNullOrEmpty(sortOrder) ? "votes_desc" : "";

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

            var articles = from m in db.Articles
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Title.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "title_desc":
                    articles = articles.OrderByDescending(s => s.Title);
                    break;
                case "author_desc":
                    articles = articles.OrderByDescending(s => s.Author);
                    break;
                case "votes_desc":
                    articles = articles.OrderByDescending(s => s.Votes);
                    break;
                default:
                    articles = articles.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (this.db.Articles.Count() == 0)
            {
                TempData["alert"] = "Trenutno nema članaka za prikaz.";
                TempData["aType"] = "danger";
            }
            ViewBag.ListArticle = this.db.Articles.ToList();
            return View(articles.ToPagedList(pageNumber, pageSize));
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            logger.Info("Deleted article, id = " + article.ID + " , date: " + DateTime.Now + " , by user id:" + UserID);
            TempData["alert"] = "Article deleted!";
            TempData["aType"] = "success";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteSelected(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var article = this.db.Articles.Find(int.Parse(id));
                this.db.Articles.Remove(article);
                this.db.SaveChanges();
            }
            TempData["alert"] = "Article(s) deleted!";
            TempData["aType"] = "success";
            var articles = from m in db.Articles
                           select m;
            return RedirectToAction("IndexKorisnik");

        }


   
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
