using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Web.Areas.Blog.Models
{
    public class BlogArchiveYearViewModel
    {
        public BlogArchiveYearViewModel()
        {
            this.Months = new HashSet<BlogArchiveMonthViewModel>();
        }

        public int Year { get; set; }

        public ICollection<BlogArchiveMonthViewModel> Months { get; set; }
    }
}