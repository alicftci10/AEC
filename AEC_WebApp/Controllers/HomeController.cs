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

        public async Task<IActionResult> Index(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetUrunlerList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                var response = await client.GetAsync(url);

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(response.Content.ReadAsStringAsync().Result);
                }

                return View(model);
            }
        }
    }
}
