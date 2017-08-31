using Camphorizon.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            CreateViewBag();
            return View();
        }

        public ActionResult About()
        {
            CreateViewBagAbout();
            return View();
        }

        void CreateViewBag()
        {
            _commonHelper = new CommonHelper();
            ViewBag.SliderImages = _commonHelper.GetImages(CommonHelper.ImageType.Slider);
            _commonHelper = null;
        }

        void CreateViewBagAbout() {
            _commonHelper = new CommonHelper();

            ViewBag.AboutText = _commonHelper.GetAboutContent();
            _commonHelper = null;
        }
    }
}
