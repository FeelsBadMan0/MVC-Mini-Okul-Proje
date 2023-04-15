using OgrenciNotMVC.Models;
using OgrenciNotMVC.Models.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace OgrenciNotMVC.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var notlar = db.TBLNOTLAR.ToList();
            return View(notlar);
        }

        [HttpGet]
        public ActionResult YeniSinav()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSinav(TBLNOTLAR tbn)
        {
            db.TBLNOTLAR.Add(tbn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult NotGetir(int id)
        {
            var notlar = db.TBLNOTLAR.Find(id);
            return View("NotGetir", notlar);
        }

        [HttpPost]
        public ActionResult NotGetir(Class1 model, TBLNOTLAR p, int SINAV1 = 0, int SINAV2 = 0, int SINAV3 = 0, int PROJE = 0)
        {
            if (model.islem == "Hesapla")
            {
                int ortalama = (SINAV1 + SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ort = ortalama;
            }
            if (model.islem == "NotGuncelle")
            {
                var notlar = db.TBLNOTLAR.Find(p.NOTID);
                notlar.SINAV1 = p.SINAV1;
                notlar.SINAV2 = p.SINAV2;
                notlar.SINAV3 = p.SINAV3;
                notlar.PROJE = p.PROJE;
                notlar.ORTALAMA = p.ORTALAMA;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}