using camphorizon.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camphorizon.Web.ViewModels
{
    public class ImageUploadViewModel
    {
        public int ImageID { get; set; }
        public string Name { get; set; }
        public string OfferHeading { get; set; }
        public string OfferDetails { get; set; }
        public short ImageType { get; set; }
        public string ImageTypeName { get; set; }
        public SelectList ImageTypes { get; set; }
        public string Imagelink { get; set; }
        public HttpPostedFileBase[] Images { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public int Order { get; set; }

        public ImageUploadViewModel() { }

        public ImageUploadViewModel(ImagesCatalogue model, bool isDetail = false)
        {
            this.ImageID = model.Id;
            this.Name = model.Name;
            this.Status = model.Active.Value ? "Active" : "Non-Active";
            if (isDetail)
            {
                this.Order = (int)model.SlideOrder;
                this.OfferHeading = model.OfferHeading;
                this.OfferDetails = model.OfferDetails;
                this.ImageTypeName = model.ImageFor == 1 ? "Package" : model.ImageFor == 2 ? "Slider" : model.ImageFor == 3 ? "Gallery" : "Room";
                this.ImageType = (short)model.ImageFor;
                this.Imagelink = model.ImageLink;
                this.Active = model.Active.HasValue ? model.Active.Value : false;
            }

        }
    }
}