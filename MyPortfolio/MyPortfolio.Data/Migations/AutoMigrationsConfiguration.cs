using MyPortfolio.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MyPortfolio.Data.Migations
{
    public class AutoMigrationsConfiguration 
        : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public AutoMigrationsConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.BlogPosts.Count() > 0)
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = "martin"
            };

            var randomNumbersGenerator = new Random();

            for (int i = 0; i < 10; i++)
            {
                var blogPost = new BlogPost
                {
                    Author = user,
                    Content = "Some sample content to test our super awesome blog posts. We need something longer different from lorem ipsum",
                    CreationDate = DateTime.Now,
                    Title = "Realy Nice Post Number #" + i,
                    ViewsCount = randomNumbersGenerator.Next(10, 500)
                };

                for (int j = 0; j < 10; j++)
                {
                    var comment = new Comment
                    {
                        AuthorName = "Annonymous",
                        AuthorEmail = "mail@mail.org",
                        Content = "I'm comment number #" + j + " and I think this post is pretty good.",
                        CreationDate = DateTime.Now,
                        Post = blogPost
                    };
                    blogPost.Comments.Add(comment);
                }

                context.BlogPosts.Add(blogPost);

                var feedback = new FeedBack
                {
                    AuthorEmail = "feedBack@mail.net",
                    AuthorName = "Feedback_Giver" + i,
                    Content = "I think you should work on that thing at the home page",
                    CreationDate = DateTime.Now
                };

                context.FeedBack.Add(feedback);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
