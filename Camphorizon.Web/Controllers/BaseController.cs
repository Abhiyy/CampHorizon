using camphorizon.Data.Model;
using Camphorizon.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public camphorizonEntities db;
        public CommonHelper _commonHelper;
        public BaseController()
        {
            db = new camphorizonEntities();
            var homePageConfig = db.ApplicationSettings.Where(x => x.Type == "Contact" || x.Type=="HomePage").ToList();
            ViewBag.Contact = homePageConfig.Where(x => x.Name == "Helpline").FirstOrDefault().Value;
            ViewBag.HelpEmail = homePageConfig.Where(x => x.Name == "HelpEmail").FirstOrDefault().Value;
            ViewBag.Address = homePageConfig.Where(x => x.Name == "Address").FirstOrDefault().Value;
            
            ViewBag.WebsiteName = homePageConfig.Where(x => x.Name == "WebsiteName").FirstOrDefault().Value;
            ViewBag.WelcomeText = homePageConfig.Where(x => x.Name == "WelcomeText").FirstOrDefault().Value;
            db = null;
        }

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }
    }
}
