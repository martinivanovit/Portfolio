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
using MyPortfolio.Web.Areas.Admin.Models;
using MyPortfolio.Web.Controllers;

namespace MyPortfolio.Web.Areas.Admin.Controllers
{
    public class CommentsController : BaseController
    {
        private const int DEFAULT_PAGE_SIZE = 15;

        public ActionResult Index(int? page)
        {
            var pageValue = page.GetValueOrDefault() - 1;

            if (pageValue < 0)
            {
                pageValue = 0;
            }

            var commentsToSkip = DEFAULT_PAGE_SIZE * pageValue;
            var commentsCount = this.Data.Comments.All().Count();

            if (commentsToSkip >= commentsCount)
            {
                commentsToSkip = 0;
            }

            SetPagerInfo(pageValue + 1, commentsCount);

            var comments = this.Data
                .Comments
                .All()
                .Include(c => c.Author)
                .OrderByDescending(b => b.CreationDate)
                .Skip(commentsToSkip)
                .Take(DEFAULT_PAGE_SIZE);

            return View(comments.ToList());
           // return View(this.Data.Comments.All().ToList());
        }

        private void SetPagerInfo(int page, int commentsCount)
        {
            ViewBag.CurrentPage = page;
            ViewBag.PagesCount = commentsCount / DEFAULT_PAGE_SIZE;
            if (commentsCount % DEFAULT_PAGE_SIZE > 0)
            {
                ViewBag.PagesCount += 1;
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = this.Data
                .Comments
                .GetById(id.GetValueOrDefault());

            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: /Admin/Comments/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var comment = this.Data.Comments.GetById(commentModel.Id);
                comment.Content = commentModel.Content;
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new Comment() { Id = commentModel.Id, Content = commentModel.Content });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = this.Data
                .Comments
                .GetById(id.GetValueOrDefault());

            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = this.Data.Comments.GetById(id);
            this.Data.Comments.Delete(comment);
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
