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

        public async Task<IActionResult> UrunlerListSayfasi(int? pId, string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HomeApi";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"/GetHomeSearchList?searchTerm={searchTerm}";
                }
                else if (pId != null)
                {
                    url += $"/GetHomeSearchIdList?pId={pId}";
                }
                else
                {
                    return View(AllUrunlerList());
                }

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
