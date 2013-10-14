using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Web.Areas.Blog.Models
{
    public class SubmitBlogPostModel
    {
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}