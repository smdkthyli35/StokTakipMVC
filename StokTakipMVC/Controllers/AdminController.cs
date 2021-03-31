using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakipMVC.Models.Entity;

namespace StokTakipMVC.Controllers
{
    public class AdminController : Controller
    {
        MvcStokTakipEntities db = new MvcStokTakipEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniAdmin(TBLADMİN p)
        {
            db.TBLADMİN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}