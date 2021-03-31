using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakipMVC.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace StokTakipMVC.Controllers
{
    public class MusteriController : Controller
    {
        MvcStokTakipEntities db = new MvcStokTakipEntities();
        [Authorize]
        // GET: Musteri
        public ActionResult Index(int sayfa = 1)
        {
            //var musteriler = db.TBLMUSTERI.ToList();
            //var musteriler = db.TBLMUSTERI.ToList().ToPagedList(sayfa, 3);
            var musteriler = db.TBLMUSTERI.Where(m => m.DURUM == true).ToList().ToPagedList(sayfa, 3);
            return View(musteriler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERI p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.DURUM = true;
            db.TBLMUSTERI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(TBLMUSTERI p)
        {
            var musteribul = db.TBLMUSTERI.Find(p.ID);
            musteribul.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERI.Find(id);
            return View("MusteriGetir", mus);
        }

        public ActionResult MusteriGuncelle(TBLMUSTERI p)
        {
            var musteri = db.TBLMUSTERI.Find(p.ID);
            musteri.AD = p.AD;
            musteri.SOYAD = p.SOYAD;
            musteri.SEHIR = p.SEHIR;
            musteri.BAKIYE = p.BAKIYE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}