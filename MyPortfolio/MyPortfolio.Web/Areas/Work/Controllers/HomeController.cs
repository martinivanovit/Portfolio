using MyPortfolio.Web.Areas.Work.Models;
using MyPortfolio.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Web.Areas.Work.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var result = this.Data
                .Projects.All()
                .Select(ProjectViewModel.FromProject);

            return View(result);
        }
	}
}