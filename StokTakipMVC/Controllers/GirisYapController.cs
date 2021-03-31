using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakipMVC.Models.Entity;
using System.Web.Security;

namespace StokTakipMVC.Controllers
{
    public class GirisYapController : Controller
    {
        MvcStokTakipEntities db = new MvcStokTakipEntities();
        // GET: GirisYap
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(TBLADMİN t)
        {
            var bilgiler = db.TBLADMİN.FirstOrDefault(x => x.KULLANICI == t.KULLANICI && x.SİFRE == t.SİFRE);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KULLANICI, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();
            }
            
        }
    }
}