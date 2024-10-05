using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class HakkimizdaController : BaseController
    {
        public HakkimizdaController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> HakkimizdaSayfasi()
        {
            using (HttpClient client = new HttpClient())
            {
                int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
                if (MessageBox == 1)
                {
                    TempData["MessageBox"] = 1;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }

                HakkimizdaDataModel model = new HakkimizdaDataModel();

                string url = ConfigurationInfo.ApiUrl + "/api/HakkimizdaApi/GetHakkimizda";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<HakkimizdaDataModel>(response.Content.ReadAsStringAsync().Result);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> HakkimizdaSayfasi(HakkimizdaDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/HakkimizdaApi/AddUpdate";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 1);
                    return RedirectToAction("HakkimizdaSayfasi");
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }
    }
}