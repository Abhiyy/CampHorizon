using Camphorizon.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    public class GalleryController : BaseController
    {
        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            CreateViewBagForGallery();
            return View();
        }


        void CreateViewBagForGallery()
        {
            _commonHelper = new CommonHelper();

            ViewBag.Gallery = _commonHelper.GetImages(CommonHelper.ImageType.Gallery,-1);
            _commonHelper = null;

        }

    }
    
}
