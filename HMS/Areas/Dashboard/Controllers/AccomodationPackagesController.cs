using HMS.Areas.Dashboard.ViewModels;
using HMS.Entity;
using HMS.Service;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        public ActionResult Index(string searchTerm ,int? accomodationTypeID,int? page)
        {
            int recordSize=5;
            page = page ?? 1;
            AccomodationPackagesListingModel model = new AccomodationPackagesListingModel();
            model.SearchTerm = searchTerm;
            model.AccomodationTypeID = accomodationTypeID;
            model.AccomodationPackages = accomodationPackageService.SearchAccomodationPackages(searchTerm,accomodationTypeID,page.Value,recordSize);
            model.AccomodationTypes = accomodationTypeService.GetAccomodationTypes();
            var totalRecords= accomodationPackageService.SearchAccomodationPackagesCount(searchTerm, accomodationTypeID);
            model.Pager = new Pager(totalRecords, page,recordSize);
            return View(model);
        }


        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationPackageActionModels model = new AccomodationPackageActionModels();

            if (ID.HasValue)
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(ID.Value);
                model.ID = accomodationPackage.ID;
                model.AccomodationTypeID = accomodationPackage.AccomodationTypeID;
                model.Name = accomodationPackage.Name;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.FeePerNight = accomodationPackage.FeePerNight;
              
            }
            model.AccomodationTypes = accomodationTypeService.GetAccomodationTypes();
            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationPackageActionModels model)
        {
            JsonResult Json = new JsonResult();

            var result = false;
            if (model.ID > 0)//we are try to edit record
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(model.ID);
                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;
                result = accomodationPackageService.UpdateAccomodationPackage(accomodationPackage);
            }
            else//we are try to create record
            {
                AccomodationPackage accomodationPackage = new AccomodationPackage();

                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;

                result = accomodationPackageService.SaveAccomodationPackage(accomodationPackage);

            }

            if (result)
            {
                Json.Data = new { Success = true };
            }
            else
            {
                Json.Data = new { Success = false, Message = "Unable to Perform action  on Accomodation Package." };
            }
            return Json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationPackageActionModels model = new AccomodationPackageActionModels();
            var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(ID);
            model.ID = accomodationPackage.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionModels model)
        {
            JsonResult Json = new JsonResult();

            var result = false;
            var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(model.ID);

            result = accomodationPackageService.DeleteAccomodationPackage(accomodationPackage);

            if (result)
            {
                Json.Data = new { Success = true };
            }
            else
            {
                Json.Data = new { Success = false, Message = "Unable to Perform action  on Accomodation Types." };
            }
            return Json;
        }

    }
}