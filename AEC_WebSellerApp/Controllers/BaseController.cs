using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace AEC_WebSellerApp.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() 
        {
            
        }

        public KullaniciDataModel CurrentKullanici
        {
            get
            {
                KullaniciDataModel model = new KullaniciDataModel();
                string sessionKullanici = HttpContext.Session.GetString("Kullanici");
                if (!string.IsNullOrEmpty(sessionKullanici))
                {//Session Dolu
                    model = JsonConvert.DeserializeObject<KullaniciDataModel>(sessionKullanici);
                }

                return model;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessionKullanici = HttpContext.Session.GetString("Kullanici");

            if (!string.IsNullOrEmpty(sessionKullanici))
            {//Session Dolu
                KullaniciDataModel model = JsonConvert.DeserializeObject<KullaniciDataModel>(sessionKullanici);

                if (model != null)
                {
                    ViewData["AdSoyad"] = model.Ad + " " + model.Soyad;
                    ViewData["Telefon"] = model.Telefon;
                    if (model.KullaniciTuruId == 1)
                    {
                        ViewData["KullaniciTuru"] = "Admin";
                    }
                    else
                    {
                        ViewData["KullaniciTuru"] = "Personel";
                    }
                }
            }
            else
            {//Session Boş Logine yönlendir
                context.Result = new RedirectResult("/Authentication/LoginEkrani");
            }

        }

        

        //public void LoadViewBag()
        //{
        //    using (HamsterContext hs = new HamsterContext())
        //    {
        //        ViewBag.Yetki = hs.Yetkis.Select(i => new SelectListItem()
        //        {
        //            Value = i.Id.ToString(),
        //            Text = i.YetkiAdi
        //        }).ToList();
        //    }
        //}
    }
}
