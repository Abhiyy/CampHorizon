using Camphorizon.Web.Helpers;
using Camphorizon.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    [Authorize]
    public class PackagesController : BaseController
    {
        //
        // GET: /Packages/

        public ActionResult Index()
        {
            _commonHelper = new CommonHelper();
            var packages = _commonHelper.GetPackages();
            _commonHelper = null;
            return View(packages);
        }


        public ActionResult Create()
        {
            _commonHelper = new CommonHelper();
            var model = new PackageViewModel();
            model.PackageTypes = _commonHelper.GetPackageTypes();
            model.CancellationTerms = _commonHelper.BuildCancellationCheckList();
            model.Ammenities = _commonHelper.BuildAmmenitiesCheckList();
            _commonHelper = null;
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(PackageViewModel model)
        {

            _commonHelper = new CommonHelper();
            if (_commonHelper.CreatePackage(model, User.Identity.Name))
            {
                Success("The package has been created successfully.", true);
                return RedirectToAction("Index");
            }
            else
                Danger("The package has been created successfully.", false);

            model.PackageTypes = _commonHelper.GetPackageTypes();
            _commonHelper = null;
            return View(model);
        }

    }
}
