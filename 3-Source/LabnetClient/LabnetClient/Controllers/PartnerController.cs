using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DataRepository;
using LabnetClient.Models;
using AutoMapper;
using LabnetClient.App_Code;
using LabnetClient.Constant;

namespace LabnetClient.Controllers
{
    public class PartnerController : BaseController
    {
        //
        // GET: /Partner/

        public ActionResult Index()
        {
            return View();
        }

    
        // GET: /Partner/Create

        public ActionResult Create()
        {
            PartnerViewModel model = new PartnerViewModel();
            model.ViewMode = ViewMode.Create;
            return View("Details",model);
        } 

        //
        // POST: /Partner/Create

        [HttpPost]
        public ActionResult Create(PartnerViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return PartialView("Details", model);
                }
                else
                {
                    Partner partner = Mapper.Map<VMPartner, Partner>(model.Partner);
                    Repository.PartnerInsert(partner);
                }
                return PartialView("Details", model);
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Partner/Edit/5
 
        public ActionResult Edit(int id)
        {
            PartnerViewModel model = new PartnerViewModel();
            model.ViewMode = ViewMode.Edit;
            model.Partner = Mapper.Map<Partner, VMPartner>(Repository.GetPartnerById(id));
            return View("Details", model);
        }

        //
        // POST: /Partner/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PartnerViewModel model)
        {
            // TODO: Add update logic here
            Repository.PartnerUpdate(id,Mapper.Map<VMPartner, Partner>(model.Partner));
            return RedirectToAction("Create");
           
        }

        //
        // GET: /Partner/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Partner/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
