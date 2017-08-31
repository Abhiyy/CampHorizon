using Camphorizon.Web.Helpers;
using Camphorizon.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    public class AmmenitiesController : BaseController
    {
        //
        // GET: /Ammenities/

        public ActionResult Index()
        {
            _commonHelper = new CommonHelper();
            var ammenities = _commonHelper.GetAmmenities();
            return View(ammenities);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AmmenitiesViewModel model)
        {
            _commonHelper = new CommonHelper();
            if (_commonHelper.CreateAmmenities(model) > 0)
            {
                Success("The ammenity created successfully.", true);
                return RedirectToAction("Index");
            }
            else {
                Danger("The ammenity can not be added.", true);

            }
            _commonHelper = null;
            return View(model);
        
        }

    }
}
