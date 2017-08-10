using camphorizon.Data.Model;
using Camphorizon.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.Helpers
{
    public class CommonHelper
    {
        public WebPortalConfigurationsViewModel GetHomeConfiguration()
        {

            using (camphorizonEntities db = new camphorizonEntities())
            {

                var config = db.ApplicationSettings.Where(c => c.Type == "Contact").ToList();
                return new WebPortalConfigurationsViewModel()
                {
                    Address = config.Where(x => x.Name == "Address").FirstOrDefault().Value,
                    Contact = config.Where(x => x.Name == "Helpline").FirstOrDefault().Value,
                    HelpEmail = config.Where(x => x.Name == "HelpEmail").FirstOrDefault().Value
                };
            }

        }

        public bool UpdateHomeConfiguration(WebPortalConfigurationsViewModel model)
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                var config = db.ApplicationSettings.Where(x => x.Name == "Address").FirstOrDefault();
                config.Value = model.Address;
                db.Entry(config).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                config = db.ApplicationSettings.Where(x => x.Name == "Helpline").FirstOrDefault();
                config.Value = model.Contact;
                db.Entry(config).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                config = db.ApplicationSettings.Where(x => x.Name == "HelpEmail").FirstOrDefault();
                config.Value = model.HelpEmail;
                db.Entry(config).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }


        }

        public SelectList GetImageTypes()
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {

                return new SelectList(db.ApplicationSettings.Where(x => x.Type == "ImageTypes" && x.Name != "Package").Select(x => new { ID = x.Value, Name = x.Name }).ToList(), "ID", "Name");
            }

        }


        public int UploadImages(ImageUploadViewModel model)
        {
            string uploadDirectory = ConfigurationManager.AppSettings["UploadDirectoryImages"].ToString();
            using (camphorizonEntities db = new camphorizonEntities())
            {
                var order = db.ImagesCatalogues.Where(x => x.ImageFor == model.ImageType).Max(x => x.SlideOrder);
                int lastOrder = order.HasValue ? order.Value : 0;
                foreach (var img in model.Images)
                {
                    string uploadFileLink = UploadFile(img, uploadDirectory);
                    lastOrder++;
                    db.ImagesCatalogues.Add(new ImagesCatalogue()
                    {
                        Active = model.Active,
                        ImageFor = model.ImageType,
                        ImageLink = uploadFileLink,
                        Name = model.Name,
                        OfferDetails = model.OfferDetails,
                        OfferHeading = model.OfferHeading,
                        SlideOrder = (short)lastOrder
                    });


                }

                return db.SaveChanges();
            }
        }

        public string UploadFile(HttpPostedFileBase file, string uploadDirectory)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath(uploadDirectory), fileName);
                file.SaveAs(path);
                return uploadDirectory + "/" + fileName;
            }

            return string.Empty;
        }

        public List<ImageUploadViewModel> GetUploadedImages()
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                var lstImageIDs = db.ImagesCatalogues.Where(x => x.ImageFor.Value != 1 || x.ImageFor.Value != 4).Select(x => x.Id).ToList();
                List<ImageUploadViewModel> lstImages = new List<ImageUploadViewModel>();
                foreach (var imgID in lstImageIDs)
                {
                    lstImages.Add(GetUploadedImage(imgID, db));
                }
                return lstImages;
            }
        }

        public ImageUploadViewModel GetUploadedImage(int id, camphorizonEntities db, bool isDetails = false)
        {

            return new ImageUploadViewModel(db.ImagesCatalogues.Where(x => x.Id == id).First(), isDetails);

        }
    }
}