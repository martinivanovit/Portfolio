using MyPortfolio.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortfolio.Web.Areas.Blog.Models;

namespace MyPortfolio.Web.Areas.Blog.Controllers
{
    public class HomeController : BaseController
    {
        private const int DEFAULT_POSTS_COUNT = 10;
        // GET: /Blog/Home/
        public ActionResult Index()
        {
            var result = this.Data
                .BlogPosts
                .All()
                .OrderByDescending(b => b.CreationDate)
                .Take(DEFAULT_POSTS_COUNT)
                .Select(BlogPostViewModel.FromBlogPost);

            ViewBag.AllPostsCount = this.Data.BlogPosts.All().Count();

            return View(result);
        }

        public ActionResult GetPosts(int postsToSkip, 
            int postsCount = DEFAULT_POSTS_COUNT)
        {
            int allPostsCount = this.Data.BlogPosts.All().Count();
            if (postsToSkip >= allPostsCount)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "No more posts");
            }

            var result = this.Data
                .BlogPosts
                .All()
                .OrderByDescending(b => b.CreationDate)
                .Skip(postsToSkip)
                .Take(DEFAULT_POSTS_COUNT)
                .Select(BlogPostViewModel.FromBlogPost);

            return PartialView("_BlogPostsList", result);
        }  
	}
}