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

namespace MyPortfolio.Web.Areas.Blog.Controllers
{
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
	}
}