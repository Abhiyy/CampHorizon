using Camphorizon.Web.Helpers;
using Camphorizon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    public class ReservationsController : BaseController
    {
        //
        // GET: /Reservations/
        [Authorize]
        public ActionResult Index()
        {
            _commonHelper = new CommonHelper();
            var requests = _commonHelper.GetReservationRequests();
            _commonHelper = null;
            return View(requests);
        }

        [HttpPost]
        public ActionResult Create(ReservationModel model)
        {
            _commonHelper = new CommonHelper();
            if (_commonHelper.CreateReservationRequest(model) > 0)
                return Json(new { messgae = "success" });
            else
                return Json(new { messgae = "error" });

            return View();
        }
    }
}
