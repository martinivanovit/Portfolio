using Microsoft.AspNet.Identity.EntityFramework;
using MyPortfolio.Models;
using System.Data.Entity;

namespace MyPortfolio.Data
{
    public class ApplicationDbContext : IdentityDbContextWithCustomUser<ApplicationUser>
    {
        public IDbSet<BlogPost> BlogPosts { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<FeedBack> FeedBack { get; set; }

        public IDbSet<Project> Projects { get; set; }
    }
}
