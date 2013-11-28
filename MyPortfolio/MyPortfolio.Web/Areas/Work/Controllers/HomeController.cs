using MyPortfolio.Web.Areas.Work.Models;
using MyPortfolio.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Web.Areas.Work.Controllers
{
    public class HomeController : BaseController
    {
        private const int DEFAULT_PAGE_SIZE = 9;

        public ActionResult Index(int? page)
        {
            var pageValue = page.GetValueOrDefault() - 1;
                
            if (pageValue < 0)
            {
                pageValue = 0;
            }

            var postsToSkip = DEFAULT_PAGE_SIZE * pageValue;
            var blogPostsCount = this.Data.BlogPosts.All().Count();

            if (postsToSkip >= blogPostsCount)
            {
                postsToSkip = 0;
            }

            SetPagerInfo(pageValue + 1, blogPostsCount);

            var blogPosts = this.Data
                .Projects
                .All()
                .OrderBy(p => p.Id)
                .Skip(postsToSkip)
                .Take(DEFAULT_PAGE_SIZE)
                .Select(ProjectViewModel.FromProject);

            return View(blogPosts.ToList());
        }

        private void SetPagerInfo(int page, int blogPostsCount)
        {
            ViewBag.CurrentPage = page;
            ViewBag.PagesCount = blogPostsCount / DEFAULT_PAGE_SIZE;
            if (blogPostsCount % DEFAULT_PAGE_SIZE > 0)
            {
                ViewBag.PagesCount += 1;
            }
        }
	}
}