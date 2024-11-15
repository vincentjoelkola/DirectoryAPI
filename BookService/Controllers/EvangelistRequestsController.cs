using BookService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrethrenDirectory.Controllers
{
    [MyAuthFilter]
    public class EvangelistRequestsController : Controller
    {
        // GET: EvangelistRequests
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