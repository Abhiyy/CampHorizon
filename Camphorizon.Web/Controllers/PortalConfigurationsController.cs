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
    public class PortalConfigurationsController : BaseController
    {
        //
        // GET: /PortalConfigurations/
        CommonHelper _commonHelper;
        public ActionResult Index()
        {
            _commonHelper = new CommonHelper();

            return View(_commonHelper.GetHomeConfiguration());
        }

        [HttpPost]
        public ActionResult Index(WebPortalConfigurationsViewModel model)
        {

            _commonHelper = new CommonHelper();

            if (_commonHelper.UpdateHomeConfiguration(model))
            {
                Success("The home configurations updated successfully.", true);
            }
            else
            {
                Danger("The home configurations not updated successfully.", true);
            }

            return View();
        }

    }
}
