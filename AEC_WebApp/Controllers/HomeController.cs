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
            MessageBox();

            return View(AllUrunlerList());
        }

        public async Task<IActionResult> UrunlerListSayfasi(int? pId, string searchTerm, decimal? min, decimal? max)
        {
            using (HttpClient client = new HttpClient())
            {
                MessageBox();

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
                MessageBox();

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
                MessageBox();

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
                MessageBox();

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
    }
}
