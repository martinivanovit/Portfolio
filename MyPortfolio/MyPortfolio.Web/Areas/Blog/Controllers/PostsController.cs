using MyPortfolio.Models;
using MyPortfolio.Web.Areas.Blog.Models;
using MyPortfolio.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text;
using System.Web.Script.Serialization;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Areas.Blog.Controllers
{
    [ValidateInputAttribute(false)]
    public class PostsController : BaseController
    {
        public ActionResult Index(int? id)
        {
            this.Response.Redirect("~/Blog");
            return null;
        }

        public ActionResult Details(int id)
        {
            var post = this.Data.BlogPosts.GetById(id);
            var postModel = BlogPostViewModel.FromBlogPost(post);
            postModel.Comments = post.Comments;

            return View(postModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmitBlogPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = this.Data
                    .ApplicationUsers
                    .All()
                    .FirstOrDefault(u => u.Id == userId);

                var blogPost = new BlogPost
                {
                    AuthorId = userId,
                    Author = user,
                    Content = postModel.Content,
                    CreationDate = DateTime.Now,
                    Title = postModel.Title
                };

                this.Data.BlogPosts.Add(blogPost);
                
                try
                {
                    this.Data.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new DbEntityValidationException();
                }

                this.Response.Redirect("~/Blog/Posts/Details/" + blogPost.Id);
                return null;
            }

            return View(new BlogPost() { Content = postModel.Content, Title = postModel.Title });
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var blogPost = this.Data.BlogPosts.GetById(id);
            
            if (User.Identity.GetUserName() == blogPost.Author.UserName || 
                User.IsInRole("Admin"))
            {
                return View("Create", blogPost);    
            }

            this.Response.Redirect("~/Blog/Posts/Details/" + blogPost.Id);
            return null;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubmitBlogPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                var blogPostId = postModel.Id.GetValueOrDefault();
                
                var blogPost = this.Data.BlogPosts.GetById(blogPostId);
                blogPost.Title = postModel.Title;
                blogPost.Content = postModel.Content;
                blogPost.LastModificationDate = DateTime.Now;

                try
                {
                    this.Data.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new DbEntityValidationException();
                }

                this.Response.Redirect("~/Blog/Posts/Details/" + blogPost.Id);
                return null;
            }

            return View(new BlogPost() { Content = postModel.Content, Title = postModel.Title });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentViewModel commentModel)
        {
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Not valid or empty captcha.");
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.Data.ApplicationUsers.All().FirstOrDefault(u => u.Id == userId);

            if (ModelState.IsValid)
            {
                var blogPost = this.Data.BlogPosts.GetById(commentModel.BlogPostId);
                var username = this.User.Identity.Name;

                var comment = new Comment()
                {
                    Author = user,
                    Content = commentModel.Comment,
                    CreationDate = DateTime.Now,
                    Post = blogPost,
                    AuthorName = commentModel.AuthorName,
                    AuthorEmail = commentModel.AuthorEmail
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var viewModel = new GetCommentViewModel 
                { 
                    AuthorName = comment.AuthorName, 
                    Comment = comment.Content, 
                    CreationDate = comment.CreationDate
                };
                return PartialView("_CommentPartial", viewModel);
            }

            var errorsData = GetModelErrors(ModelState.Values);

            //return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, errorsData);
            this.Response.StatusCode = 400;
            return Json(errorsData, JsonRequestBehavior.AllowGet);
        }

        private string GetModelErrors(ICollection<ModelState> modelStateValues)
        {
            var errorsObj = new
            {
                Errors = new List<object>()
            };
            
            foreach (ModelState modelState in modelStateValues)
            {
                
                foreach (var error in modelState.Errors)
                {
                    errorsObj.Errors.Add(new 
                    {
                        ErrorMessage = error.ErrorMessage
                    });
                }
            }
            var json = new JavaScriptSerializer().Serialize(errorsObj);
            return json;
        }

        // For now searches only by title
        // TODO: Implement search by tags
        public ActionResult Search(string query)
        {
            var result = this.Data
                .BlogPosts.All();

            if (!String.IsNullOrEmpty(query))
            {
                result = result.Where(p => p.Title.Contains(query));    
            }

            var resultModel = result.OrderByDescending(b => b.CreationDate)
                .Select(BlogPostViewModel.FromBlogPost);

            return View(resultModel);
        }


        public ActionResult GetGroupedByDate()
        {
            var result = this.Data
                .BlogPosts.All()
                .GroupBy(post => post.CreationDate.Year);

            var model = new List<BlogArchiveYearViewModel>();

            foreach (var postsByYear in result.ToList())
            {
                var blogArchiveYear = new BlogArchiveYearViewModel();
                var postByYear = postsByYear.GroupBy(post => post.CreationDate.Month);
                
                blogArchiveYear.Year = postsByYear.Key;

                foreach (var postsByMonth in postByYear)
                {
                    var blogArchiveMont = new BlogArchiveMonthViewModel
                        {
                            Date = postsByMonth.First().CreationDate,
                            PostsCount = postsByMonth.Count()
                        };
                    blogArchiveYear.Months.Add(blogArchiveMont);
                }

                model.Add(blogArchiveYear);
            }
            
            return PartialView("_BlogArchiveList", model);
        }
	}
}