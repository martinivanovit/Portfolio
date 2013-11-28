using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public string CreationDateFormated
        {
            get
            {
                if (this.CreationDate != null)
                {
                    DateTimeFormatInfo mfi = new DateTimeFormatInfo();   
                    string monthName = mfi.GetAbbreviatedMonthName(this.CreationDate.Month);
                    string formatedDate = this.CreationDate.Day + " " +  
                        monthName + " " +
                        this.CreationDate.Year;
                    return formatedDate;
                }
                return null;
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