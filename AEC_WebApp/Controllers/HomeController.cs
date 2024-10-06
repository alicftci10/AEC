using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace AEC_WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> Index()
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

            return View(AllUrunlerList());
        }

        public async Task<IActionResult> UrunlerListSayfasi(int? pId, string searchTerm, decimal? min, decimal? max)
        {
            using (HttpClient client = new HttpClient())
            {
                int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
                if (MessageBox == 4)
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

                HomeDataModel model = new HomeDataModel();

                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"/GetHomeSearchList?searchTerm={searchTerm}";
                }
                else if (pId != null)
                {
                    url += $"/GetHomeSearchIdList?pId={pId}";
                }
                else if (min != null && max != null)
                {
                    model = UrunlerSearchPriceList(min, max);
                    return View(model);
                }
                else
                {
                    return View(AllUrunlerList());
                }

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<HomeDataModel>(response.Content.ReadAsStringAsync().Result);

                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        public async Task<IActionResult> LaptopDetay(int? pLaptopId)
        {
            using (HttpClient client = new HttpClient())
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

                int? secilenUrunId = HttpContext.Session.GetInt32("secilenUrunId");
                if (pLaptopId == null)
                {
                    pLaptopId = secilenUrunId;
                }

                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetUrunDetay";

                url += $"?pLaptopId={pLaptopId}";

                var response = await client.GetAsync(url);

                HomeDataModel model = new HomeDataModel();

                model = AllUrunlerList();

                if (response.IsSuccessStatusCode)
                {
                    var laptop = JsonConvert.DeserializeObject<HomeDataModel>(response.Content.ReadAsStringAsync().Result);

                    if (laptop != null)
                    {
                        model.GetLaptop = laptop.GetLaptop;
                        HttpContext.Session.SetInt32("secilenUrunId", laptop.GetLaptop.Id);
                        model.UrunResmiList = laptop.UrunResmiList;
                        model.UrunYorumList = laptop.UrunYorumList;
                    }

                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        public async Task<IActionResult> MonitorDetay(int? pMonitorId)
        {
            using (HttpClient client = new HttpClient())
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

                int? secilenUrunId = HttpContext.Session.GetInt32("secilenUrunId");
                if (pMonitorId == null)
                {
                    pMonitorId = secilenUrunId;
                }

                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetUrunDetay";

                url += $"?pMonitorId={pMonitorId}";

                var response = await client.GetAsync(url);

                HomeDataModel model = new HomeDataModel();

                model = AllUrunlerList();

                if (response.IsSuccessStatusCode)
                {
                    var monitor = JsonConvert.DeserializeObject<HomeDataModel>(response.Content.ReadAsStringAsync().Result);

                    if (monitor != null)
                    {
                        model.GetMonitor = monitor.GetMonitor;
                        HttpContext.Session.SetInt32("secilenUrunId", monitor.GetMonitor.Id);
                        model.UrunResmiList = monitor.UrunResmiList;
                        model.UrunYorumList = monitor.UrunYorumList;
                    }

                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        public async Task<IActionResult> MouseDetay(int? pMouseId)
        {
            using (HttpClient client = new HttpClient())
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

                int? secilenUrunId = HttpContext.Session.GetInt32("secilenUrunId");
                if (pMouseId == null)
                {
                    pMouseId = secilenUrunId;
                }

                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetUrunDetay";
             
                url += $"?pMouseId={pMouseId}";

                var response = await client.GetAsync(url);

                HomeDataModel model = new HomeDataModel();

                model = AllUrunlerList();

                if (response.IsSuccessStatusCode)
                {
                    var mouse = JsonConvert.DeserializeObject<HomeDataModel>(response.Content.ReadAsStringAsync().Result);

                    if (mouse != null)
                    {
                        model.GetMouse = mouse.GetMouse;
                        HttpContext.Session.SetInt32("secilenUrunId", mouse.GetMouse.Id);
                        model.UrunResmiList = mouse.UrunResmiList;
                        model.UrunYorumList = mouse.UrunYorumList;
                    }

                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> UrunYorum(UrunYorumDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunYorumApi/AddUpdate";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 1);

                    if (model.LaptopId != null)
                    {
                        return RedirectToAction("LaptopDetay");
                    }
                    else if (model.MonitorId != null)
                    {
                        return RedirectToAction("MonitorDetay");
                    }
                    else
                    {
                        return RedirectToAction("MouseDetay");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        public async Task<IActionResult> UrunYorumSil(int pUrun ,int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunYorumApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);

                    if (pUrun == 1)
                    {
                        return RedirectToAction("LaptopDetay");
                    }
                    else if (pUrun == 2)
                    {
                        return RedirectToAction("MonitorDetay");
                    }
                    else
                    {
                        return RedirectToAction("MouseDetay");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        public void FavoriDurum(int? pLaptopId, int? pMonitorId, int? pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetFavoriDurum";

                if (pLaptopId != null)
                {
                    url += $"?pLaptopId={pLaptopId}";
                }
                else if (pMonitorId != null)
                {
                    url += $"?pMonitorId={pMonitorId}";
                }
                else if (pMouseId != null)
                {
                    url += $"?pMouseId={pMouseId}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.Favori == true)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 4);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("MessageBox", 5);
                        }
                    }
                }
            }
        }

        public void SepetDurum(int? pLaptopId, int? pMonitorId, int? pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetSepetDurum";

                if (pLaptopId != null)
                {
                    url += $"?pLaptopId={pLaptopId}";
                }
                else if (pMonitorId != null)
                {
                    url += $"?pMonitorId={pMonitorId}";
                }
                else if (pMouseId != null)
                {
                    url += $"?pMouseId={pMouseId}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.SepetDurum == true)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 6);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("MessageBox", 7);
                        }
                    }
                }
            }
        }
    }
}
