using Plasma_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plasma_Work_01.Controllers
{
    //[Authorize]
    public class PlasmaDonationsController : Controller
    {
        // GET: PlasmaDonations

        CoronaPaitentDbContext db = new CoronaPaitentDbContext();


        public ActionResult Index(int page = 1)
        {
            int perPage = 5;
            var data = db.PlasmaDonations
                .OrderBy(x => x.PlasmaDonationId)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.PlasmaDonations.Count() / perPage);
            return View(data);
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PlasmaDonation pd)
        {

            if (ModelState.IsValid)
            {
                db.PlasmaDonations.Add(pd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pd);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var pd = db.PlasmaDonations.First(x => x.PlasmaDonationId == id);
            return View(pd);
        }
        [HttpPost]
        public ActionResult Edit(PlasmaDonation pd)
        {

            if (ModelState.IsValid)
            {
                db.Entry(pd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pd);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var pd = db.PlasmaDonations.First(x => x.PlasmaDonationId == id);
            return View(pd);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {

            {
                var pd = new PlasmaDonation { PlasmaDonationId = id };
                db.Entry(pd).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}
