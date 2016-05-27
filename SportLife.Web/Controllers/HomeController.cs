using SportLife.Bll.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportLife.Controllers
{
    public class HomeController : Controller
    {
        private IUserBus _iUserBus;

        public HomeController(IUserBus iUserBus)
        {
            _iUserBus = iUserBus;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TempleteIntegration()
        {
            return View();
        }
        public ActionResult TempleteIntegration2()
        {
            return View();
        }

        public ActionResult Draws()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}