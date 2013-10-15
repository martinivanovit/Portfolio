using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortfolio.Web.Areas.Admin.Models
{
    public class EditCommentModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}