using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.Web.Areas.Work.Models
{
    public class ProjectViewModel
    {
        public static Func<Project, ProjectViewModel> FromProject
        {
            get
            {
                return project => new ProjectViewModel
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    DemoUrl = project.DemoUrl,
                    SourceUrl = project.SourceUrl,
                    ImageUrl = project.ImageUrl
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DemoUrl { get; set; }

        public string SourceUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}