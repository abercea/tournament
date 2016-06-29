using SportLife.Bll.Contracts;
using SportLife.Dal.DomainModels;
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
        private IMessageBus _iMesBus;


        public AdminController(IEventBus iEventBus, IUserBus iUserBus, IMessageBus m)
            : base(iUserBus)
        {
            _iEventBus = iEventBus;
            _iMesBus = m;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Event(EventViewModel ev)
        {
            InitSession();

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
            var model = _iEventBus.GetAllEvents();
            InitSession();
            return View(model);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Event(int eventId = 0)
        {
            EventViewModel model = new EventViewModel();

            ViewBag.Title = "Add event";

            if (eventId > 0)
            {
                model = _iEventBus.GetById(eventId);
                ViewBag.Title = "Edit event";
            }

            return View(model);
        }

        public string DeleteEvent(int evId)
        {
            try
            {
                string name = _iEventBus.Delete(evId);

                return "The event was deleted " + name + " !!!";
            }
            catch (Exception ex)
            {
                return "Event can't be deleted !! Please contact the administrator";
            }
        }

        public string Details(int eventId)
        {
            return "in progress";
        }

        public JsonResult SaveMess(string mess, int UserId)
        {
            try
            {
                var messDb = new ChatMessage { UserId = UserId, Content = mess, DateCreated = DateTime.Now };
                _iMesBus.save(messDb);
            }
            catch (Exception e)
            {

            }


            return JsonSerialization(new { status = "OK" });
        }


        public ActionResult GetMesseges()
        {
            List<ChatMessage> msgs = _iMesBus.GetLast();

            return JsonSerialization(msgs);
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Upolcad()
        {
            return null;
        }

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                    var fileName1 = Path.GetFileName(file.FileName);


                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);

                }

            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}