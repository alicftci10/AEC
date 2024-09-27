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
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetHomeList";

                var response = await client.GetAsync(url);

                HomeDataModel model = new HomeDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<HomeDataModel>(response.Content.ReadAsStringAsync().Result);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> UrunlerListSayfasi(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi/GetHomeList";

                var response = await client.GetAsync(url);

                HomeDataModel model = new HomeDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<HomeDataModel>(response.Content.ReadAsStringAsync().Result);
                }

                return View(model);
            }
        }
    }
}
