using SportLife.Bll.Contracts;
using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
using SportLife.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SportLife.Models.Models.Enums;

namespace SportLife.Controllers
{
    public class HomeController : BaseController
    {
        //  private IUserBus _iUserBus;
        private IMessageBus _iMesBus;
        private IEventBus _iEventBus;


        public HomeController(IUserBus iUserBus, IMessageBus iMesBus, IEventBus iEventBus)
            : base(iUserBus)
        {
            _iMesBus = iMesBus;
            _iEventBus = iEventBus;
        }

        public ActionResult Index()
        {
            var events = _iEventBus.GetAllEvents().Take(3).ToList();

            return View("TempleteIntegration2", events);
        }


        public ActionResult TempleteIntegration2()
        {
            var events = _iEventBus.GetAllEvents().Take(3).ToList();

            return View(events);
        }

        public ActionResult Draws()
        {
            return View();
        }

        public ActionResult Register(int mode = 2, string redirect = "Home") // mode=1 ===>>>login
        {
            ViewBag.Mode = mode;


            return View(new UserViewModel { RedirectController = redirect });
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
            UserViewModel accessUser = _iUserBus.CheckCredentials(user);
            if (accessUser != null && accessUser.Role != AccessRolesEnum.NoAcces)
            {
                PutInSessionUser(accessUser, Response);

                return JsonSerialization(new { success = true, redirect = redirect });
            }

            return JsonSerialization(new { success = false, message = (accessUser != null && accessUser.Role == AccessRolesEnum.AccountNotActivated) ? "Please check your email and activate your account" : "Credentials are not valid" });
        }

        public ActionResult ActivateAccount(string accessToken)
        {
            bool acctivated = _iUserBus.ActivataAccount(accessToken);

            ViewBag.Message = acctivated ? "Account active !" : "Something went wrong . Please contact the administrator";

            return RedirectToAction("Index");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            RemoveSessionData();

            return RedirectToAction("Index");
        }

        private void PutInSessionUser(UserViewModel user, HttpResponseBase response)
        {
            var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(30), true, user.Role.GetStringValue());
            string cookieContents = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
            {
                Expires = authTicket.Expiration,
                Path = FormsAuthentication.FormsCookiePath
            };
            response.Cookies.Add(cookie);
            Session.Add("user", user);
        }

        public ActionResult Logout(string redirect = "index")
        {
            Session.RemoveAll();

            return JsonSerialization(new { redirect = redirect });
        }

        private void RemoveSessionData()
        {
            Session.RemoveAll();
        }

        public ActionResult Chat()
        {
            List<ChatMessage> msgs = _iMesBus.GetLast();

            return View(msgs);
        }

        public String generateDb()
        {
            _iUserBus.addFirst();

            return "OK";
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult FourOFour()
        {
            return View();
        }
    }
}