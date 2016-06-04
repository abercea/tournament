using SportLife.Bll.Contracts;
using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
using SportLife.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportLife.Controllers
{
    public class HomeController : BaseController
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

            return View(new UserViewModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Post(UserViewModel user)
        {
            CustomResponseModel<UserViewModel> response = _iUserBus.ValidateUser(user);
            string modal = string.Empty;

            if (response.WithError)
            {
                ViewBag.Content = response.ViewMessage;
                modal = RenderPartialViewToString("~/Views/SharedPopups/GenericPopup.cshtml");

                return JsonSerialization(new { response = response, html = modal });
            }

            try
            {
                response = _iUserBus.RegisterUser(user);

                ViewBag.Content = response.ViewMessage;
                modal = RenderPartialViewToString("~/Views/SharedPopups/GenericPopup.cshtml");

                return JsonSerialization(new { response = response, html = modal });
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
            }

            return JsonSerialization(new { response = response });
        }

        public ActionResult Chat()
        {
            return View();
        }

        public String generateDb()
        {
            _iUserBus.addFirst();

            return "OK";
        }
    }
}