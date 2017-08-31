using camphorizon.Data.Model;
using Camphorizon.Web.Models;
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
        public enum ImageType
        {
        Package =1,
        Slider=2,
        Gallery=3,
        Room=4
        }

        public List<ImagesCatalogue> GetImages(ImageType imgType)
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                return db.ImagesCatalogues.Where(x => x.ImageFor == (short)ImageType.Slider).ToList();
            }
        
        }

        public string GetAboutContent()
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                return db.ApplicationSettings.Where(x => x.Name == "AboutText").FirstOrDefault().Value;
            }
        }
        public WebPortalConfigurationsViewModel GetHomeConfiguration()
        {

            using (camphorizonEntities db = new camphorizonEntities())
            {

                var config = db.ApplicationSettings.Where(c => c.Type == "Contact" || c.Type == "HomePage"||c.Type=="AboutPage").ToList();
                return new WebPortalConfigurationsViewModel()
                {
                    Address = config.Where(x => x.Name == "Address").FirstOrDefault().Value,
                    Contact = config.Where(x => x.Name == "Helpline").FirstOrDefault().Value,
                    HelpEmail = config.Where(x => x.Name == "HelpEmail").FirstOrDefault().Value,
                    WebsiteName = config.Where(x=>x.Name=="WebsiteName").FirstOrDefault().Value,
                    WelcomeText = config.Where(x => x.Name == "WelcomeText").FirstOrDefault().Value,
                    AboutText = config.Where(x => x.Name == "AboutText").FirstOrDefault().Value

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
               
                config = db.ApplicationSettings.Where(x => x.Name == "WebsiteName").FirstOrDefault();
                config.Value = model.WebsiteName;
                db.Entry(config).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                config = db.ApplicationSettings.Where(x => x.Name == "WelcomeText").FirstOrDefault();
                config.Value = model.WelcomeText;
                db.Entry(config).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                config = db.ApplicationSettings.Where(x => x.Name == "AboutText").FirstOrDefault();
                config.Value = model.AboutText;
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

        public int UploadPackageImages(HttpPostedFileBase[] files, ImageType imgtype, string packageName)
        {
            
            string uploadDirectory = ConfigurationManager.AppSettings["UploadDirectoryImages"].ToString();
            using (camphorizonEntities db = new camphorizonEntities())
            {
                var order = db.ImagesCatalogues.Where(x => x.ImageFor == (short)imgtype).Max(x => x.SlideOrder);
                int lastOrder = order.HasValue ? order.Value : 0;
                foreach (var img in files)
                {
                    string uploadFileLink = UploadFile(img, uploadDirectory);
                    lastOrder++;
                    db.ImagesCatalogues.Add(new ImagesCatalogue()
                    {
                        Active =true,
                        ImageFor = (short)imgtype,
                        ImageLink = uploadFileLink,
                        Name = packageName+" "+lastOrder.ToString(),
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

        public SelectList GetPackageTypes()
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {

                return new SelectList(db.ApplicationSettings.Where(x => x.Type == "PackageType").Select(x => new { ID = x.Value, Name = x.Name }).ToList(), "ID", "Name");
            }

        }

        public bool CreatePackage(PackageViewModel model, string user)
        {
            bool isSave = false;
            using (camphorizonEntities db = new camphorizonEntities())
            {

                var package = new Package()
                {
                    Active = model.Active,
                    CreatedBy = user,
                    CreatedDate = DateTime.UtcNow,
                    Description = model.Description,
                    Duration = model.Duration,
                    Name = model.Name,
                    Price = model.Price,
                    UpdatedBy = user,
                    UpdatedDate = DateTime.UtcNow
                };

                db.Packages.Add(package);

                if (db.SaveChanges() > 0)
                {
                    isSave = true;
                }
                else
                {
                    isSave = false;
                }

                db.PackageInclusions.Add(new PackageInclusion()
                {
                    CreateDate = DateTime.UtcNow,
                    CreatedBy = user,
                    Inclusions = model.Inclusions,
                    PackageID = package.Id,
                    UpdateDate = DateTime.UtcNow,
                    UpdatedBy = user
                });

                db.PackageItineraries.Add(new PackageItinerary()
                {
                    CreateDate = DateTime.UtcNow,
                    CreatedBy = user,
                    Itinerary = model.Itinerary,
                    PackageID = package.Id,
                    UpdateDate = DateTime.UtcNow,
                    UpdatedBy = user
                });

                db.PackageExclusions.Add(new PackageExclusion()
                {
                    CreateDate = DateTime.UtcNow,
                    CreatedBy = user,
                    Exclusions = model.Exclusions,
                    PackageID = package.Id,
                    UpdateDate = DateTime.UtcNow,
                    UpdatedBy = user
                });

                if (UploadPackageImages(model.Images, ImageType.Package, model.Name) > 0)
                    isSave = true;
                else
                    isSave = false;

                foreach (var ammenity in model.Ammenities)
                {
                    if(ammenity.Checked)
                        db.PackageFacilityMappings.Add(new PackageFacilityMapping() {  FacilityID=ammenity.Id, PackageID=package.Id });
                }

               
                foreach (var ct in model.CancellationTerms)
                {
                    if (ct.Checked) {
                        db.PackageCancellationTermsMappings.Add(new PackageCancellationTermsMapping() { CancellationTermID = ct.Id, PackageID=package.Id});
                    }
                }
               


                if (db.SaveChanges() > 0)
                    isSave = true;
                else
                    isSave = false;
            }
            return isSave;
        }

        public List<PackageViewModel> GetPackages(bool isNonActiveIncluded = true)
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                List<int> packageIDs = null;

                List<PackageViewModel> lstPackages = new List<PackageViewModel>();

                if (isNonActiveIncluded)
                    packageIDs = db.Packages.Select(x => x.Id).ToList();
                else
                    packageIDs = db.Packages.Where(x => x.Active == true).Select(x => x.Id).ToList();

                foreach (var id in packageIDs)
                {
                    lstPackages.Add(GetPackage(id, db));
                }

                return lstPackages;
            }
        }

        public PackageViewModel GetPackage(int id, camphorizonEntities db)
        {
            var package = new PackageViewModel(db.Packages.Where(x => x.Id == id).FirstOrDefault(), false);

            return package;
        }

        public int CreateCancellationTerm(CancellationTermViewModel model) {

            using (camphorizonEntities db = new camphorizonEntities())
            {
                db.CancellationTerms.Add(new CancellationTerm() {CancellationTerm1= model.CancellationTerm });

                return db.SaveChanges();
            }
        }

        public List<CancellationTermViewModel> GetCancellationTerms()
        {

            using (camphorizonEntities db = new camphorizonEntities()) {
                return db.CancellationTerms.Select(x => new CancellationTermViewModel() {  CancellationTerm=x.CancellationTerm1, ID=x.Id}).ToList();
            }
        }

        public List<CancellationTermCheckList> BuildCancellationCheckList() {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                return db.CancellationTerms.Select(x => new CancellationTermCheckList() { Id = x.Id, Name = x.CancellationTerm1 }).ToList();
            }
        }

        public List<AmmenitiesCheckList> BuildAmmenitiesCheckList()
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                return db.Facilities.Select(x => new AmmenitiesCheckList() { Id = x.Id, Name = x.Facility1 }).ToList();
            }
        }

        public int CreateAmmenities(AmmenitiesViewModel model)
        {
            using (camphorizonEntities db = new camphorizonEntities()) {
                db.Facilities.Add(new Facility() {  Facility1=model.Name});

                return db.SaveChanges();
            }
        
        }
        public List<AmmenitiesViewModel> GetAmmenities()
        {
            using (camphorizonEntities db = new camphorizonEntities())
            {
                return db.Facilities.Select(x => new AmmenitiesViewModel() { ID=x.Id, Name=x.Facility1 }).ToList();
            }
        
        }

        public int CreateReservationRequest(ReservationModel model) {
            using (camphorizonEntities db = new camphorizonEntities()) {
                db.ReservationRequests.Add(new ReservationRequest() {  Adults = model.Adults,
                                                                       CheckInDate =model.CheckInDate,
                                                                       CheckOutDate = model.CheckOutDate,
                                                                       Children = model.Children,
                                                                       Contact=model.Contact,
                                                                       DateCreated=DateTime.UtcNow,
                                                                       Email=model.Email,
                                                                       ForDays = (model.CheckOutDate - model.CheckInDate).Days
                                                                       });
                return db.SaveChanges();
            }
           
        }

        public List<ReservationRequest> GetReservationRequests()
        {
            using (camphorizonEntities db = new camphorizonEntities()) {
                return db.ReservationRequests.ToList();
            }
        }

        public List<ImagesCatalogue> GetImages(ImageType imgtype, int packageID = -1)
        {
            List<ImagesCatalogue> lstImages = new List<ImagesCatalogue>();
            camphorizonEntities db = new camphorizonEntities();
            switch (imgtype)
            {
                case ImageType.Gallery:
                    lstImages = db.ImagesCatalogues.Where(x => x.ImageFor == (short)ImageType.Gallery).OrderBy(x => x.SlideOrder).ToList();

                    break;

                case ImageType.Package:
                    lstImages = db.ImagesCatalogues.Where(x => x.ImageFor == (short)ImageType.Package && x.PackageID == packageID).ToList();
                    break;

                case ImageType.Room:
                    lstImages = db.ImagesCatalogues.Where(x => x.ImageFor == (short)ImageType.Room && x.PackageID == packageID).ToList();
                    break;

                case ImageType.Slider:
                    lstImages = db.ImagesCatalogues.Where(x => x.ImageFor == (short)ImageType.Slider).OrderBy(x => x.SlideOrder).ToList();
                    break;
            }

            return lstImages;

        }
    }
}