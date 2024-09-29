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

                LaptopDataModel model = new LaptopDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<LaptopDataModel>(response.Content.ReadAsStringAsync().Result);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> UrunResim(int? pLaptopId , int? pMonitorId , int? pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi";

                if (pLaptopId != null)
                {
                    url += $"/GetLaptopResmiList?pLaptopId={pLaptopId}";
                }
                else if (pMonitorId != null)
                {
                    url += $"/GetMonitorResmiList?pMonitorId={pMonitorId}";
                }
                else if (pMouseId != null)
                {
                    url += $"/GetMouseResmiList?pMouseId={pMouseId}";
                }

                var response = await client.GetAsync(url);

                List<UrunResmiDataModel> model = new List<UrunResmiDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunResmiDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(model);
            }
        }
    }
}
