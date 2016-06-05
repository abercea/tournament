using SportLife.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportLife.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Event(int eventId = 0)
        {
            EventViewModel model = new EventViewModel();
           
            if (eventId > 0)
            {

            }

            return View(model);
        }
    }
}