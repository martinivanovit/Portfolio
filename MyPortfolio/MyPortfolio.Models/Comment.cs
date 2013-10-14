using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class Comment
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

        public virtual ApplicationUser Author { get; set; }

        public virtual BlogPost Post { get; set; }
    }
}
