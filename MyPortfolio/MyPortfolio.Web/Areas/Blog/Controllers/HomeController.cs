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
        //
        // GET: /Blog/Home/
        public ActionResult Index()
        {
            var result = this.Data
                .BlogPosts
                .All()
                .Select(BlogPostViewModel.FromBlogPost);

            return View(result);
        }
	}
}