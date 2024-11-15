using BookService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrethrenDirectory.Controllers
{
    [MyAuthFilter]
    public class AssemblyRequestsController : Controller
    {
        // GET: AssemblyRequests
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.RequestId = id;
            return View();
        }
    }
}