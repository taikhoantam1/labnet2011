using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using LabnetClient.Constant;
using DataRepository;
using DomainModel;
using AutoMapper;
using LibraryFuntion;

namespace LabnetClient.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Test/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindTestSectionNames(string searchText)
        {
            var result =Repository.GetTestSectionByName(searchText);
            return Json(result);
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            TestViewModel model = new TestViewModel();
            model.ViewMode = ViewMode.Create;
            model.Autocomplete.JsonData = Repository.GetTestSectionByName("").ToJson();
            return View("Create", model);
        } 

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult Create(TestViewModel model)
        {
            if ((model.Test.LowIndex == null && model.Test.HighIndex != null)
                || (model.Test.LowIndex != null && model.Test.HighIndex == null))
            {
                ModelState.AddModelError("Low and High are not valid", Resources.TestStrings.TestCreate_IndexError);
            }

            if (!Repository.IsValidTest(model.Test.Name))
            {
                ModelState.AddModelError("Test name already exists", Resources.TestStrings.Test_Create_NameError);
            }

            if (!ModelState.IsValid)
            {
                model.Autocomplete.JsonData = Repository.GetTestSectionByName("").ToJson(); ;
                return View("Create", model);
            }

            Test test = Mapper.Map<VMTest, Test>(model.Test);
            Repository.TestInsert(test);
            return RedirectToAction("Index");
        }
        
        //
        // GET: /Test/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Test/Edit/5

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
        // GET: /Test/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Test/Delete/5

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
