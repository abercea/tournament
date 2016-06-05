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
            return View("TempleteIntegration2");
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

        public ActionResult Register(int mode = 2) // mode=1 ===>>>login
        {
            ViewBag.Mode = mode;
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
                var relativeUrl = Url.Action("ActivateAccount");  //..or one of the other .Action() overloads
                var currentUrl = Request.Url;

                var absoluteUrl = new Uri(currentUrl, relativeUrl);

                response = _iUserBus.RegisterUser(user, absoluteUrl.ToString());

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(UserViewModel user, string redirect = "index")
        {
            AccessRolesEnum access = _iUserBus.CheckCredentials(user);
            if ((int)access > 0 && access != AccessRolesEnum.AccountNotActivated)
            {
                PutInSessionUser(user);

                return JsonSerialization(new { success = true, redirect = redirect });
            }

            return JsonSerialization(new { success = false, message = access == AccessRolesEnum.AccountNotActivated ? "Please check your email and activate your account" : "Credentials are not valid" });
        }

        public ActionResult ActivateAccount(string accessToken)
        {
            bool acctivated = _iUserBus.ActivataAccount(accessToken);

            ViewBag.Message = acctivated ? "Account active !" : "Something went wrong . Please contact the administrator";

            return RedirectToAction("Index");
        }

        private void PutInSessionUser(UserViewModel user)
        {
            Session.Add("user", user);
        }

        public ActionResult Logout(string redirect = "index")
        {
            Session.RemoveAll();

            return JsonSerialization(new { redirect = redirect });
        }

        private void RemoveSessionData()
        {
            throw new NotImplementedException();
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