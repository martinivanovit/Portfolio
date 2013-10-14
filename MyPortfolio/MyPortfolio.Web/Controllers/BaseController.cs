using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortfolio.Data.Repositories;

namespace MyPortfolio.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
            : base()
        {
            this.Data = new UowData();
        }

        public UowData Data { get; set; }
    }
}