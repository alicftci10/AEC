using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebApp.Controllers
{
    public class UrunYorumController : BaseController
    {
        public UrunYorumController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> UrunYorumSayfasi()
        {
            using (HttpClient client = new HttpClient())
            {
                MessageBox();

                string url = ConfigurationInfo.ApiUrl + "/api/UrunYorumApi/UrunYorumKullaniciList";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = await client.GetAsync(url);

                List<UrunYorumDataModel> modelList = new List<UrunYorumDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<UrunYorumDataModel>>(response.Content.ReadAsStringAsync().Result);

                    return View(modelList);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
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
                        return RedirectToAction("LaptopDetay", "Home");
                    }
                    else if (model.MonitorId != null)
                    {
                        return RedirectToAction("MonitorDetay", "Home");
                    }
                    else
                    {
                        return RedirectToAction("MouseDetay", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        public async Task<IActionResult> UrunYorumSil(int pUrun, int pId)
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
                        return RedirectToAction("LaptopDetay", "Home");
                    }
                    else if (pUrun == 2)
                    {
                        return RedirectToAction("MonitorDetay", "Home");
                    }
                    else if (pUrun == 3)
                    {
                        return RedirectToAction("MouseDetay", "Home");
                    }
                    else
                    {
                        return RedirectToAction("UrunYorumSayfasi");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }
    }
}
