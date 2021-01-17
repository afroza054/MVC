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
    public class CoronaPaitentsController : Controller
    {
        CoronaPaitentDbContext db = new CoronaPaitentDbContext();
        public ActionResult Index(int page = 1)
        {
            int perPage = 4;
            var data = db.CoronaPositivePaitents
                .OrderBy(x => x.CoronaPositivePaitentId)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.CoronaPositivePaitents.Count() / perPage);
            return View(data);
        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PlasmaDonations = db.PlasmaDonations.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Create(CoronaPositivePaitent cp)
        {
            
            if (ModelState.IsValid)
            {
                db.CoronaPositivePaitents.Add(cp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlasmaDonations = db.PlasmaDonations.ToList();
            return View(cp);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.PlasmaDonations = db.PlasmaDonations.ToList();
            var pd = db.CoronaPositivePaitents.First(x => x.CoronaPositivePaitentId == id);
            //ViewBag.PlasmaDonations = db.PlasmaDonations.ToList();
            return View(pd);
        }
        [HttpPost]
        public ActionResult Edit(CoronaPositivePaitent cp)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(cp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlasmaDonations = db.PlasmaDonations.ToList();
            return View(cp);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            ViewBag.PlasmaDonations = db.PlasmaDonations.ToList();
            var pd = db.CoronaPositivePaitents.First(x => x.CoronaPositivePaitentId == id);
            //ViewBag.PlasmaDonations = db.PlasmaDonations.ToList();
            return View(pd);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {

            {
                var pd = new CoronaPositivePaitent { CoronaPositivePaitentId = id };
                db.Entry(pd).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}