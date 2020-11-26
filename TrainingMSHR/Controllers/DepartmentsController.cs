using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingMSHR.Context;
using TrainingMSHR.Models;

namespace TrainingMSHR.Controllers
{
    public class DepartmentsController : Controller
    {
        MyContext myContext = new MyContext();

        public ActionResult Index()
        {
            var result = myContext.Departments.Include("Division").ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            var result = myContext.Divisions.OrderBy(x => x.Name)
                .Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }).ToArray();
            ViewBag.Division_Id = result;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, DateTimeOffset createdOn, int Division_Id)
        {
            var department = new Department(0, name, createdOn, Division_Id);
            myContext.Departments.Add(department);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Edit(int id)
        {
            var result = myContext.Departments.Include("Division").SingleOrDefault(x => x.Id.Equals(id));
            var getDivision = myContext.Divisions.OrderBy(x => x.Name)
                .Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                    Selected = false
                }).ToArray();
            foreach (var item in getDivision)
            {
                if (item.Value.Equals(result.Division.Id.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.Divisions = getDivision;
            if(result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string name, DateTimeOffset createdOn, int divisionId)
        {
            var department = new Department(id, name, createdOn, divisionId);
            myContext.Entry(department).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var department = myContext.Departments.Find(id);
            if(department != null)
            {
                myContext.Departments.Remove(department);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }  
            }
            return View();
        }
    }
}