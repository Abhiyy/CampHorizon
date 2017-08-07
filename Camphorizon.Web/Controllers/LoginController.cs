using Camphorizon.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Camphorizon.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (ValidateUser(model))
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);
                    return RedirectToAction("Index", "Administrator");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        bool ValidateUser(LoginViewModel user)
        {
            string username = ConfigurationManager.AppSettings["username"].ToString();
            string pwd = ConfigurationManager.AppSettings["pwd"].ToString();

            if (user.username == username && user.password == pwd)
            {
                return true;
            }

            return false;
        }

    }
}
