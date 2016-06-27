using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportLife.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(int userId)
        {

            return View();
        }
    }
}