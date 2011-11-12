using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DataRepository;
using AutoMapper;
using DomainModel;

namespace LabnetClient.Controllers
{
    public class DoctorController : BaseController
    {
        //
        // GET: /Doctor/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Doctor/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Doctor/Create

        public ActionResult Create()
        {
            DoctorViewModel model = new DoctorViewModel();
            model.Doctor.IsActive = true;
            return View("Create", model);
        } 

        //
        // POST: /Doctor/Create

        [HttpPost]
        public ActionResult Create(DoctorViewModel model)
        {
            if (!Repository.IsValidDoctor(model.Doctor.Name))
            {
                ModelState.AddModelError("Doctor name already exists", Resources.DoctorStrings.DoctorInsert_ErrorExist);
            }

            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            Doctor doctor = Mapper.Map<VMDoctor, Doctor>(model.Doctor);
            Repository.DoctorInsert(doctor);
            return View();
            
        }
        
        //
        // GET: /Doctor/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Doctor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Doctor/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Doctor/Delete/5

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
