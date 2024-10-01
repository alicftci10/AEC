using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;

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

            return View(AllUrunlerList());
        }

        public async Task<IActionResult> UrunlerListSayfasi(int? pId, string searchTerm, decimal? min, decimal? max)
        {
            using (HttpClient client = new HttpClient())
            {
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
                }

                return View(model);
            }
        }

        public async Task<IActionResult> LaptopDetay(int pLaptopId)
        {
            using (HttpClient client = new HttpClient())
            {
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
                        model.UrunResmiList = laptop.UrunResmiList;
					}
                }

                return View(model);
            }
        }

        public async Task<IActionResult> MonitorDetay(int pMonitorId)
        {
            using (HttpClient client = new HttpClient())
            {
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
                        model.UrunResmiList = monitor.UrunResmiList;
                    }
                }

                return View(model);
            }
        }

        public async Task<IActionResult> MouseDetay(int pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
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
                        model.UrunResmiList = mouse.UrunResmiList;
                    }
                }

                return View(model);
            }
        }
    }
}
