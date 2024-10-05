using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AEC_WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        public IMemoryCache _memoryCacheBase;

        public BaseController()
        {

        }

        public KullaniciDataModel CurrentKullanici
        {
            get
            {
                KullaniciDataModel model = new KullaniciDataModel();
                string? sessionKullanici = HttpContext.Session.GetString("Kullanici");
                if (!string.IsNullOrEmpty(sessionKullanici))
                {//Session Dolu
                    model = JsonConvert.DeserializeObject<KullaniciDataModel>(sessionKullanici);
                }

                if (model != null)
                {
                    return model;
                }
                else
                {
                    return new KullaniciDataModel();
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["KategoriList"] = LoadKategoriList();
            AllUrunlerList();
            LoadHakkimizdaInfo();

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
                    else if (model.KullaniciTuruId == 2)
                    {
                        ViewData["KullaniciTuru"] = "Personel";
                    }
                    else if (model.KullaniciTuruId == 3)
                    {
                        ViewData["KullaniciTuru"] = "Müşteri";
                    }
                }
            }
        }

        public List<KategoriDataModel> LoadKategoriList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetAllKategoriAllowAnonymous";
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> kategoriList = new List<KategoriDataModel>();

                if (text != null)
                {
                    kategoriList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (kategoriList != null)
                    {
                        kategoriList = GetKategoriCount(kategoriList);
                        return kategoriList;
                    }
                }

                return new List<KategoriDataModel>();
            }
        }

        public List<KategoriDataModel> GetKategoriCount(List<KategoriDataModel> kategoriList)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetKategoriCount";

                var json = JsonConvert.SerializeObject(kategoriList);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content);
                var text = response.Result;

                if (text != null)
                {
                    kategoriList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (kategoriList != null)
                    {
                        return kategoriList;
                    }
                }

                return new List<KategoriDataModel>(); ;
            }
        }

        public HomeDataModel AllUrunlerList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetHomeList";
                var response = client.GetAsync(url);
                var text = response.Result;

                HomeDataModel model = new HomeDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<HomeDataModel>(text.Content.ReadAsStringAsync().Result);
                }

                if (model != null)
                {
                    if (model.LaptopList != null && model.LaptopList.Count > 0)
                    {
                        ViewData["AllLaptop"] = model.LaptopList.Count;
                    }

                    if (model.MonitorList != null && model.MonitorList.Count > 0)
                    {
                        ViewData["AllMonitor"] = model.MonitorList.Count;
                    }

                    if (model.MouseList != null && model.MouseList.Count > 0)
                    {
                        ViewData["AllMouse"] = model.MouseList.Count;
                    }

                    return model;
                }
                else
                {
                    return new HomeDataModel();
                }
            }
        }

        public HomeDataModel UrunlerSearchPriceList(decimal? min, decimal? max)
        {
            HomeDataModel model = new HomeDataModel();
            model.LaptopList = new List<LaptopDataModel>();
            model.MonitorList = new List<MonitorDataModel>();
            model.MouseList = new List<MouseDataModel>();
            model.KategoriIdName = "Ali E-Commerce / ARAMA SONUÇLARI";

            var urunlerList = AllUrunlerList();

            if (urunlerList != null)
            {
                if (urunlerList.LaptopList != null && urunlerList.LaptopList.Count > 0)
                {
                    foreach (var item in urunlerList.LaptopList)
                    {
                        if (item.Fiyat >= min && item.Fiyat <= max)
                        {
                            model.LaptopList.Add(item);
                        }
                    }
                }

                if (urunlerList.MonitorList != null && urunlerList.MonitorList.Count > 0)
                {
                    foreach (var item in urunlerList.MonitorList)
                    {
                        if (item.Fiyat >= min && item.Fiyat <= max)
                        {
                            model.MonitorList.Add(item);
                        }
                    }
                }

                if (urunlerList.MouseList != null && urunlerList.MouseList.Count > 0)
                {
                    foreach (var item in urunlerList.MouseList)
                    {
                        if (item.Fiyat >= min && item.Fiyat <= max)
                        {
                            model.MouseList.Add(item);
                        }
                    }
                }

                return model;
            }
            else
            {
                return new HomeDataModel();
            }
        }

		public HakkimizdaDataModel LoadHakkimizdaInfo()
        {
            HakkimizdaDataModel model = new HakkimizdaDataModel();

            if (_memoryCacheBase != null && !_memoryCacheBase.TryGetValue("HakkimizdaCacheValue", out model))
            {//Cache'de yoksa API'den getir.
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync(ConfigurationInfo.ApiUrl + "/api/HakkimizdaApi/GetHakkimizdaAllowAnonymous").Result;
                    if (response != null)
                    {
                        model = JsonConvert.DeserializeObject<HakkimizdaDataModel>(response.Content.ReadAsStringAsync().Result);
                    }
                }

                var cacheEntryOptionsSliding = new MemoryCacheEntryOptions().
                    SetSlidingExpiration(TimeSpan.FromHours(ConfigurationInfo.MemoryCacheTimeOut));

                _memoryCacheBase.Set("HakkimizdaCacheValue", model, cacheEntryOptionsSliding);
                //cache de yoksa cache e datamodel yazsın.
            }
            else
            {//Cache'de varsa cache den getir.
                _memoryCacheBase.TryGetValue("HakkimizdaCacheValue", out model);
            }

            if (model != null)
            {
                string whatsappLink = "https://wa.me/9" + model.Telefon + "?text=Bilgi%20Almak%20istiyorum";

                ViewData["Hakkimizda_whatsappLink"] = whatsappLink;
                ViewData["Hakkimizda_Telefon"] = model.Telefon;
                ViewData["Hakkimizda_Adres"] = model.Adres;
                ViewData["Hakkimizda_Email"] = model.Email;
                ViewData["Hakkimizda_CalismaGunleri"] = model.CalismaGunleri;

                return model;
            }
            else
            {
                return new HakkimizdaDataModel();
            }
        }

        public void LoadKullaniciList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetAllKullanici";

                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciDataModel> modelList = new List<KullaniciDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> kullaniciAdi = new List<string>();
                    List<string> email = new List<string>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
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

        public void LoadKullaniciKartList(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKartListesi";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciKartDataModel> modelList = new List<KullaniciKartDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciKartDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> KartAdi = new List<string>();
                    List<string> KartNumarasi = new List<string>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            KartAdi.Add(item.KartAdi);
                            KartNumarasi.Add(item.KartNumarasi);
                        }

                        HttpContext.Session.SetString("KullaniciKartAdiList", JsonConvert.SerializeObject(KartAdi));
                        HttpContext.Session.SetString("KullaniciKartNumarasiList", JsonConvert.SerializeObject(KartNumarasi));
                    }
                }
            }
        }
    }
}
