using Plasma_Work_01.Models;
using Plasma_Work_01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plasma_Work_01.Controllers
{
    public class CoronaPaitentAndReasonsController : Controller
    {
        // GET: CoronaPaitentAndReasons
        CoronaPositveRepositories repo = new CoronaPositveRepositories();
        public ActionResult Index()
        {
            var data = repo.GetAllRelated();
            return View(data);
        }
        public ActionResult Create()
        {

            ViewBag.PlasmaDonations = repo.GetDonatorNameForDropDown();
            return View();
        }
        [HttpPost]
        public JsonResult CreatePost(CoronaPositivePaitent cp)
        {
            if (ModelState.IsValid)
            {
                repo.InsertCoronaPaitentWithReasons(cp);
                return Json(new { success = true, message = "Data save succeeded" }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { success = false, message = "Data save failed" }, JsonRequestBehavior.DenyGet);
        }
    }
}