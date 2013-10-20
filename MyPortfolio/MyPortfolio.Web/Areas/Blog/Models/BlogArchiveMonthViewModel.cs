using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Web.Areas.Blog.Models
{
    public class BlogArchiveMonthViewModel
    {
        public DateTime Date { get; set; }

        public int PostsCount { get; set; }
    }
}