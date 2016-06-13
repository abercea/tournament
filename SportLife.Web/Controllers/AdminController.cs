using SportLife.Bll.Contracts;
using SportLife.Models.Models;
using SportLife.Models.Models.Enums;
using SportLife.Web.Atributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportLife.Web.Controllers
{
    [SecurityManagerAtribute]
    public class AdminController : BaseController
    {

        private IEventBus _iEventBus;

        public AdminController(IEventBus iEventBus, IUserBus iUserBus)
            : base(iUserBus)
        {
            _iEventBus = iEventBus;

        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Event(EventViewModel ev)
        {
            InitSessin();

            if (!ModelState.IsValid)
            {
                return View("Event", ev);
            }

            bool status = _iEventBus.AdEdit(ev, _user);
            ViewBag.Message = status ? "Event was successfully added" : "An error has cured, please try again or contact the administrator";
            ViewBag.State = status ? ActionType.AddOk : ActionType.AddFail;

            if (status)
            {
                ViewBag.Title = "Add event";
                ModelState.Clear();

                return View("Event", new EventViewModel());
            }

            return View("Event", ev);
        }

        public ActionResult Table()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Event(int eventId = 0)
        {
            EventViewModel model = new EventViewModel();

            ViewBag.Title = "Add event";

            if (eventId > 0)
            {
                ViewBag.Title = "Edit event";
            }

            return View(model);
        }
    }
}