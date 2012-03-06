using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DomainModel.Constant;
using DataRepository;
using DomainModel;
using AutoMapper;

namespace LabnetClient.Controllers
{
    public class NhomXetNghiemController : BaseController
    {
        //
        // GET: /NhomXetNghiem/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /NhomXetNghiem/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /NhomXetNghiem/Create

        public ActionResult Create()
        {
            TestSectionViewModel model = new TestSectionViewModel();
            model.ViewMode = ViewMode.Create;
            return View("Create", model);
        } 

        //
        // POST: /NhomXetNghiem/Create

        [HttpPost]
        public ActionResult Create(TestSectionViewModel model)
        {
            if (!Repository.IsValidTestSection(model.TestSection.Name))
            {
                ModelState.AddModelError("Test section name already exists", Resources.TestSectionStrings.TestSection_NameError);
            }

            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            TestSection ts = Mapper.Map<VMTestSection, TestSection>(model.TestSection);
            Repository.TestSectionInsert(ts);

            return RedirectToAction("Create");
        }
        
        //
        // GET: /NhomXetNghiem/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /NhomXetNghiem/Edit/5

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
        // GET: /NhomXetNghiem/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /NhomXetNghiem/Delete/5

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
