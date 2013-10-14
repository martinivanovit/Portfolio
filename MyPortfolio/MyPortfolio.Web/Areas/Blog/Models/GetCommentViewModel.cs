using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Web.Areas.Blog.Models
{
    public class GetCommentViewModel
    {
        public static Func<Comment, GetCommentViewModel> FromComment
        {
            get
            {
                return comment => new GetCommentViewModel
                {
                    AuthorName = comment.Author != null
                                    ? comment.Author.UserName
                                    : comment.AuthorName,
                    Comment = comment.Content,
                    CreationDate = comment.CreationDate
                };
            }
        }

        public string AuthorName { get; set; }

        public string Comment { get; set; }

        public DateTime CreationDate { get; set; }
    }
}