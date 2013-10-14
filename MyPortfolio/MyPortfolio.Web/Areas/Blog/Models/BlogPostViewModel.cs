using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Web.Areas.Blog.Models
{
    public class BlogPostViewModel
    {
        public static Func<BlogPost, BlogPostViewModel> FromBlogPost
        {
            get
            {
                return blogPost => new BlogPostViewModel
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    AuthorName = blogPost.Author.UserName,
                    Content = blogPost.Content,
                    CreationDate = blogPost.CreationDate,
                    LastModificationDate = blogPost.LastModificationDate,
                    ViewsCount = blogPost.ViewsCount,
                    Comments = blogPost.Comments
                                    .OrderByDescending(comment => comment.CreationDate)
                                    .Take(5)
                };
            }
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ViewsCount { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastModificationDate { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}