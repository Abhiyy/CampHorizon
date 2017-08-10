using Camphorizon.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Controllers
{
    [Authorize]
    public class ImageController : BaseController
    {
        //
        // GET: /Images/


        public ActionResult Index()
        {
            _commonHelper = new Helpers.CommonHelper();
            var lstImages = _commonHelper.GetUploadedImages();
            return View(lstImages);
        }

        public ActionResult Upload()
        {
            var model = new ImageUploadViewModel();
            _commonHelper = new Helpers.CommonHelper();
            model.ImageTypes = _commonHelper.GetImageTypes();
            _commonHelper = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(ImageUploadViewModel model)
        {

            if (model.Images.Count() > 0)
            {
                _commonHelper = new Helpers.CommonHelper();
                model.ImageTypes = _commonHelper.GetImageTypes();
                if (_commonHelper.UploadImages(model) > 0)
                {
                    Success("The image(s) upload completed successfully.", true);
                    return View(model);
                }
                else
                {
                    Danger("The image(s) can not be uploaded.", true);
                }
            }
            else
            {
                Danger("Please select a file(s) to upload.", true);
            }

            return View(model);
        }




    }
}
