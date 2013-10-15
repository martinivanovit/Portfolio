using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPortfolio.Models;
using MyPortfolio.Data;
using MyPortfolio.Web.Controllers;

namespace MyPortfolio.Web.Areas.Admin.Controllers
{
    public class PostsController : BaseController
    {
        private const int DEFAULT_PAGE_SIZE = 10;

        // GET: /Admin/Posts/
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
                .BlogPosts
                .All()
                .Include(b => b.Author)
                .OrderByDescending(b => b.CreationDate)
                .ThenBy(b => b.Title)
                .Skip(postsToSkip)
                .Take(DEFAULT_PAGE_SIZE);

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

        // GET: /Admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogPost blogpost = this.Data
                .BlogPosts
                .GetById(id.GetValueOrDefault());

            if (blogpost == null)
            {
                return HttpNotFound();
            }
            return View(blogpost);
        }

        // GET: /Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(this.Data.ApplicationUsers.All(), "Id", "UserName");
            return View();
        }

        // POST: /Admin/Posts/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPost blogpost)
        {
            if (ModelState.IsValid)
            {
                this.Data.BlogPosts.Add(blogpost);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.Data.ApplicationUsers.All(), "Id", "UserName", blogpost.AuthorId);
            return View(blogpost);
        }

        // GET: /Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogPost blogpost = this.Data
                .BlogPosts
                .GetById(id.GetValueOrDefault());

            if (blogpost == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(this.Data.ApplicationUsers.All(), "Id", "UserName", blogpost.AuthorId);
            return View(blogpost);
        }

        // POST: /Admin/Posts/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogPost blogpost)
        {
            //TODO: USE REPOSITORY
            var db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                db.Entry(blogpost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(this.Data.ApplicationUsers.All(), "Id", "UserName", blogpost.AuthorId);
            return View(blogpost);
        }

        // GET: /Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogPost blogpost = this.Data
               .BlogPosts
               .GetById(id.GetValueOrDefault());

            if (blogpost == null)
            {
                return HttpNotFound();
            }
            return View(blogpost);
        }

        // POST: /Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogpost = this.Data.BlogPosts.GetById(id);

            this.Data.BlogPosts.Delete(blogpost);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.Data.Dispose();
            base.Dispose(disposing);
        }
    }
}
