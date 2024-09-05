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
                    ViewData["KullaniciId"] = model.Id;
                    ViewData["KullaniciFullName"] = model.Ad + " " + model.Soyad;
                    ViewData["KullaniciTelefon"] = model.Telefon;
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

        public void LoadKullaniciTuruDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciTuruApi/GetAllKullaniciTuru";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciTuruDataModel> modelList = new List<KullaniciTuruDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciTuruDataModel>>(text.Content.ReadAsStringAsync().Result);
                }

                List<SelectListItem> listPersonel = new List<SelectListItem>();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in modelList)
                {
                    if (item.KullaniciTuruAd != "Müşteri")
                    {
                        listPersonel.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.KullaniciTuruAd });
                        list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.KullaniciTuruAd });
                    }
                    else
                    {
                        list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.KullaniciTuruAd });
                    }
                }

                ViewBag.KullaniciTuruPersonelList = listPersonel;
                ViewBag.KullaniciTuruList = list;
            }
        }

        public void LoadKullaniciList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetAllKullanici";

                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciDataModel> kullanicilar = new List<KullaniciDataModel>();

                if (text != null)
                {
                    kullanicilar = JsonConvert.DeserializeObject<List<KullaniciDataModel>>(text.Content.ReadAsStringAsync().Result);
                }

                List<string> kullaniciAdi = new List<string>();
                List<string> email = new List<string>();

                foreach (var item in kullanicilar)
                {
                    kullaniciAdi.Add(item.KullaniciAdi);
                    email.Add(item.Email);
                }

                HttpContext.Session.SetString("KullaniciAdiList", JsonConvert.SerializeObject(kullaniciAdi));
                HttpContext.Session.SetString("KullaniciEmailList", JsonConvert.SerializeObject(email));
            }
        }
    }
}
