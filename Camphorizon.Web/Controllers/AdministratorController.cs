﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/

        public ActionResult Index()
        {
            return View();
        }

    }
}
