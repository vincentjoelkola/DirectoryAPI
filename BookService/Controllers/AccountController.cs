using BookService.Models;
using BrethrenModels;
using BrethrenRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookService.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserMaster model)
        {
            if (ModelState.IsValid)
            {
                var user = await new UserMasterRepository().ValidateUser(model.UserName, model.UserPassword);

                if (user != null)
                {
                    Session["UserInfo"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Name or Password");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["UserInfo"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
