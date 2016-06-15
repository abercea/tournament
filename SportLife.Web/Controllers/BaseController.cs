using SportLife.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using SportLife.Dal.Dao;
using SportLife.Bll.Contracts;


namespace SportLife.Web.Controllers
{
    public class BaseController : Controller
    {
        public UserViewModel _user { get; set; }

        public IUserBus _iUserBus;

        public BaseController(IUserBus iUserBus)
        {
            _iUserBus = iUserBus;
        }

        public BaseController()
        {
        }

        private ContentResult SerializeObject(object obj)
        {
            string content = new JavaScriptSerializer().Serialize(obj);

            return new ContentResult() { Content = content, ContentType = "application/json" };
        }

        protected JsonResult JsonSerialization(Object obj)
        {
            var jsonResult = Json(obj, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        public string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        public string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public string RenderAlertPopup(string message = "", string title = "")
        {
            ViewBag.Content = message;
            ViewBag.Title = title;
            var modal = RenderPartialViewToString("~/Views/SharedPopups/GenericPopup.cshtml");

            return modal;
        }

        public void InitSession()
        {
            _user = Session["user"] as UserViewModel;
            if (_user == null)
            {
                var name = User.Identity.GetUserName();
                if (name != null)
                {
                    _user = _iUserBus.GetByEmail(name);
                    Session.Add("user", _user);
                }
            }
        }
    }
}