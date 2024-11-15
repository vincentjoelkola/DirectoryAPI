using BookService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookService.Controllers
{
    [MyAuthFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Index1()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var employee = _db.Employees.Find(id);
            //if (employee == null)
            //    return HttpNotFound();
            //var serializer = new JavaScriptSerializer();
            ViewBag.SelectedAssemblyId = id;
            return View();
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var employee = _db.Employees.Find(id);
            //if (employee == null)
            //    return HttpNotFound();
            //var serializer = new JavaScriptSerializer();
            //ViewBag.SelectedEmployee = serializer.Serialize(employee);
            return View();
        }
    }
}
