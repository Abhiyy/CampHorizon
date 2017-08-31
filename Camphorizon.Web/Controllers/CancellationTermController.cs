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
    public class CancellationTermController : BaseController
    {
        //
        // GET: /CancellationTerm/

        public ActionResult Index()
        {
            _commonHelper = new CommonHelper();
            var terms = _commonHelper.GetCancellationTerms();
            return View(terms);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CancellationTermViewModel model)
        {
            _commonHelper = new CommonHelper();

            if (_commonHelper.CreateCancellationTerm(model) > 0)
            {
                Success("The cancellation term added successfully.", true);
                return RedirectToAction("Index");
            }
            else {
                Danger("The cancellation term was not able to add. Please try again", false);
                return View(model);
            }

            _commonHelper = null;
           
        }
    }
}
