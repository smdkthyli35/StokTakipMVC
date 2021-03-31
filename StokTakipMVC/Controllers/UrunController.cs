using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakipMVC.Models.Entity;

namespace StokTakipMVC.Controllers
{
    public class UrunController : Controller
    {
        MvcStokTakipEntities db = new MvcStokTakipEntities();
        // GET: Urun
        public ActionResult Index(string p)
        {
            //var urunler = db.TBLURUNLER.Where(m => m.DURUM == true).ToList();
            var urunler = db.TBLURUNLER.Where(m => m.DURUM == true); 
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(m => m.AD.Contains(p) && m.DURUM == true);
            }
            return View(urunler.ToList());
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AD,
                                                 Value = i.ID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p)
        {
            var ktgr = db.TBLKATEGORI.Where(m => m.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            p.TBLKATEGORI = ktgr;
            db.TBLURUNLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from i in db.TBLKATEGORI.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.AD,
                                            Value = i.ID.ToString()
                                        }).ToList();

            var urun = db.TBLURUNLER.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", urun);
        }

        public ActionResult UrunGuncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.ID);
            urun.AD = p.AD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.ALISFIYAT = p.ALISFIYAT;
            urun.SATISFIYAT = p.SATISFIYAT;
            var kategori = db.TBLKATEGORI.Where(m => m.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            urun.KATEGORI = kategori.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(TBLURUNLER p1)
        {
            var urunbul = db.TBLURUNLER.Find(p1.ID);
            urunbul.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}