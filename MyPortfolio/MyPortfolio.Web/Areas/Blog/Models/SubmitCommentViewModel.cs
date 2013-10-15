using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyPortfolio.Web.Areas.Blog.Models
{
    public class SubmitCommentViewModel
    {
        [Required]
        [MinLength(8)]
        [MaxLength(250)]
        [RegularExpression("<(.|\n)*?>", ErrorMessage="Comment field cannot contain HTML tags")]
        public string Comment { get; set; }

        [Required]
        public int BlogPostId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        [RegularExpression(@"^[_a-z0-9-A-Z]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")]
        public string AuthorEmail { get; set; }
    }
}
