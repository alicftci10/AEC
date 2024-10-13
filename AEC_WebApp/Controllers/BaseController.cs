using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            AllUrunYorumList();
            LoadHakkimizdaInfo();

            string sessionKullanici = HttpContext.Session.GetString("Kullanici");

            if (!string.IsNullOrEmpty(sessionKullanici))
            {//Session Dolu

                KullaniciFavoriList();
                KullaniciSepetList();

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

                    if (kategoriList != null && kategoriList.Count > 0)
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

                    if (kategoriList != null && kategoriList.Count > 0)
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
                }

                return new HomeDataModel();
            }
        }

        public List<UrunYorumDataModel> AllUrunYorumList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunYorumApi/GetAllUrunYorum";
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunYorumDataModel> modelList = new List<UrunYorumDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<UrunYorumDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (modelList != null && modelList.Count > 0)
                    {
                        ViewData["YorumList"] = modelList;

                        return modelList;
                    }
                }

                return new List<UrunYorumDataModel>();
            }
        }

        public List<UrunTakipDataModel> KullaniciFavoriList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetFavoriList";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> modelList = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (modelList != null && modelList.Count > 0)
                    {
                        ViewData["KullaniciFavoriList"] = modelList;

                        return modelList;
                    }
                }

                return new List<UrunTakipDataModel>();
            }
        }

        public List<UrunTakipDataModel> KullaniciSepetList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetSepetList";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> modelList = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (modelList != null && modelList.Count > 0)
                    {
                        ViewData["KullaniciSepetList"] = modelList;

                        return modelList;
                    }
                }

                return new List<UrunTakipDataModel>();
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
            model.UrunTakipList = urunlerList.UrunTakipList;

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

            return new HomeDataModel();
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

            return new HakkimizdaDataModel();
        }

        public List<KullaniciDataModel> LoadKullaniciList()
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

                        return modelList;
                    }
                }

                return new List<KullaniciDataModel>();
            }
        }

        public List<KullaniciKartDataModel> LoadKullaniciKartList(int pId)
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

                        return modelList;
                    }
                }

                return new List<KullaniciKartDataModel>();
            }
        }

        public void LoadKullaniciKartDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKartListesi";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciKartDataModel> modelList = new List<KullaniciKartDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciKartDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.KartAdi });
                        }

                        ViewBag.KullaniciKartAdiList = list;
                    }
                }
            }
        }

        public void MessageBox()
        {
            int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
            if (MessageBox == 1)
            {
                TempData["MessageBox"] = 1;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 2)
            {
                TempData["MessageBox"] = 2;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 4)
            {
                TempData["MessageBox"] = 4;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 5)
            {
                TempData["MessageBox"] = 5;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 6)
            {
                TempData["MessageBox"] = 6;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 7)
            {
                TempData["MessageBox"] = 7;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 8)
            {
                TempData["MessageBox"] = 8;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 9)
            {
                TempData["MessageBox"] = 9;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 10)
            {
                TempData["MessageBox"] = 10;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
        }
    }
}