using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakipMVC.Models.Entity;

namespace StokTakipMVC.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        MvcStokTakipEntities db = new MvcStokTakipEntities();
        public ActionResult Index()
        {
            var satislar = db.TBLSATISLAR.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Ürünler
            List<SelectListItem> urun = (from i in db.TBLURUNLER.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.AD,
                                             Value = i.ID.ToString()
                                         }).ToList();

            ViewBag.drop1 = urun;

            //Personel
            List<SelectListItem> personel = (from i in db.TBLPERSONEL.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AD + " " + i.SOYAD,
                                                 Value = i.ID.ToString()
                                             }).ToList();

            ViewBag.drop2 = personel;


            //Müşteriler
            List<SelectListItem> musteri = (from i in db.TBLMUSTERI.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.AD + " " + i.SOYAD,
                                                Value = i.ID.ToString()
                                            }).ToList();

            ViewBag.drop3 = musteri;

            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR p)
        {
            var urun = db.TBLURUNLER.Where(m => m.ID == p.TBLURUNLER.ID).FirstOrDefault();
            var personel = db.TBLPERSONEL.Where(m => m.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            var musteri = db.TBLMUSTERI.Where(m => m.ID == p.TBLMUSTERI.ID).FirstOrDefault();
            p.TBLURUNLER = urun;
            p.TBLPERSONEL = personel;
            p.TBLMUSTERI = musteri;
            p.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString()); 
            db.TBLSATISLAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}