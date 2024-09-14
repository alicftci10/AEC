using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult LoginEkrani(string p)
        {
            KullaniciDataModel model = new KullaniciDataModel();

            if (p == "logout")
            {
                HttpContext.Session.Clear();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginEkrani(KullaniciDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                KullaniciList();

                var kullaniciAdiList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciAdiList"));
                var emailList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciEmailList"));

                if (!string.IsNullOrEmpty(model.EmailorKullaniciAdi) && kullaniciAdiList != null && emailList != null)
                {
                    if (kullaniciAdiList.Contains(model.EmailorKullaniciAdi))
                    {
                        model.KullaniciAdi = model.EmailorKullaniciAdi;
                    }
                    else if (emailList.Contains(model.EmailorKullaniciAdi))
                    {
                        model.Email = model.EmailorKullaniciAdi;
                    }
                }

                string url = ConfigurationInfo.ApiUrl + "/api/AuthenticationApi/Giris";

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                var text = response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<KullaniciDataModel>(text.Result);

                    if (model != null && model.Id > 0)
                    {
                        HttpContext.Session.SetString("Kullanici", text.Result);

                        return RedirectToAction("Index", "Home");
                    }
                    else if (model.ErrorMessage == "1")
                    {
                        ModelState.AddModelError("Sifre", "Şifre yanlış!! lütfen tekrar deneyiniz.");
                    }
                    else
                    {
                        ModelState.AddModelError("Sifre", "Kullanıcı Adı veya Şifre yanlış!! lütfen tekrar deneyiniz.");
                    }
                }

                return View(model);
            }
        }

        private void KullaniciList()
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
