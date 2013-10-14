using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class FeedBack
    {
        public int Id { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(250)]
        public string Content { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string AuthorName { get; set; }

        [Required]
        public string AuthorEmail { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
