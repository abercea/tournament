using SportLife.Bll.Contracts;
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

        private IEventBus _iEventBus;

        public AdminController(IEventBus iEventBus)
        {
            _iEventBus = iEventBus;
        }

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Event(EventViewModel ev)
        {
            bool status = _iEventBus.AdEdit(ev);

            return null;
        }

        public ActionResult Table()
        {
            return View();
        }
        public ActionResult Event2(int eventId = 0)
        {
            EventViewModel model = new EventViewModel();

            if (eventId > 0)
            {

            }

            return View(model);
        }
    }
}