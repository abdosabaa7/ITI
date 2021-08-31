using HMS.Areas.Dashboard.ViewModels;
using HMS.Entity;
using HMS.Service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypeService accomodationTypesService = new AccomodationTypeService();
       
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypeListingModel model = new AccomodationTypeListingModel();
            model.SearchTerm = searchTerm;
            model.AccomodationTypes = accomodationTypesService.SearchAccomodationTypes(searchTerm);

           
            return View(model);
        }

       
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            if (ID.HasValue)
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypesByID(ID.Value);
                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }
            else
            {

            }
            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {
            JsonResult Json = new JsonResult();

            var result = false;
            if (model.ID>0)//we are try to edit record
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypesByID(model.ID);
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;
                result = accomodationTypesService.UpdateAccomodationTypes(accomodationType);
            }
            else//we are try to create record
            {
                AccomodationType accomodationType = new AccomodationType();
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                 result = accomodationTypesService.SaveAccomodationTypes(accomodationType);

            }

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

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();
            var accomodationType = accomodationTypesService.GetAccomodationTypesByID(ID);
            model.ID = accomodationType.ID;
           
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult Json = new JsonResult();

            var result = false;
            var accomodationType = accomodationTypesService.GetAccomodationTypesByID(model.ID);
           
            result = accomodationTypesService.DeleteAccomodationTypes(accomodationType);
            
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
