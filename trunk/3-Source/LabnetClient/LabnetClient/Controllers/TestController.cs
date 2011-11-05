using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using LabnetClient.Constant;
using DataRepository;

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
            List<TestSection> lstResult = Repository.GetTestSectionByName(searchText);
            var result = lstResult.Select(p => new {Name=p.Name,Id=p.Id  });
            return Json(result);
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            TestViewModel model = new TestViewModel();
            model.ViewMode = ViewMode.Create;
            model.Test.IsActive = true;

            return View("Create", model);
        } 

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult Create(TestViewModel model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
