using BookService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookService.Controllers
{
    [MyAuthFilter]
    public class EvangelistController : Controller
    {
        // GET: Evangelist
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.SelectedEvangelistId = id;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}