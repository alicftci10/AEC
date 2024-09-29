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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["KategoriList"] = LoadKategoriList();
            AllUrunlerList();
            LoadHakkimizdaInfo();
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
                }

                if (kategoriList != null)
                {
                    kategoriList = GetKategoriCount(kategoriList);
                    return kategoriList;
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
                }

                if (kategoriList != null)
                {
                    return kategoriList;
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

                return new HomeDataModel();
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
    }
}
