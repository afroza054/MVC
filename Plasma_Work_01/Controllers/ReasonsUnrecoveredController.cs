using Plasma_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
//using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Plasma_Work_01.Controllers
{
    //[Authorize]
    public class ReasonsUnrecoveredController : Controller
    {
        CoronaPaitentDbContext db = new CoronaPaitentDbContext();
        public ActionResult Index(int page = 1)
        {
            int perPage = 4;
            var data = db.ReasonsforNotRecoveredCoronaPaitents
                .OrderBy(x => x.ReasonsforNotRecoveredCoronaPaitentId)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.ReasonsforNotRecoveredCoronaPaitents.Count() / perPage);
            return View(data);
        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CoronaPositivePaitents = db.CoronaPositivePaitents.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Create(ReasonsforNotRecoveredCoronaPaitent cp)
        {
            
            if (ModelState.IsValid)
            {
                db.ReasonsforNotRecoveredCoronaPaitents.Add(cp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoronaPositivePaitents = db.CoronaPositivePaitents.ToList();
            return View(cp);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.CoronaPositivePaitents = db.CoronaPositivePaitents.ToList();
            var pd = db.ReasonsforNotRecoveredCoronaPaitents.First(x => x.ReasonsforNotRecoveredCoronaPaitentId == id);
            //ViewBag.CoronaPositivePaitents = db.CoronaPositivePaitents.ToList();
            return View(pd);
        }
        [HttpPost]
        public ActionResult Edit(ReasonsforNotRecoveredCoronaPaitent cp)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(cp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoronaPositivePaitents = db.CoronaPositivePaitents.ToList();
            return View(cp);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            ViewBag.CoronaPositivePaitents = db.CoronaPositivePaitents.ToList();
            var pd = db.ReasonsforNotRecoveredCoronaPaitents.First(x => x.ReasonsforNotRecoveredCoronaPaitentId == id);
            //ViewBag.CoronaPositivePaitents = db.CoronaPositivePaitents.ToList();
            return View(pd);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {

            {
                var pd = new ReasonsforNotRecoveredCoronaPaitent { ReasonsforNotRecoveredCoronaPaitentId = id };
                db.Entry(pd).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}
