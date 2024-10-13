using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class UrunTakipController : BaseController
    {
        public UrunTakipController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> BekliyorSayfasi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                MessageBox();

                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/BekliyorList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunTakipDataModel> modelList = new List<UrunTakipDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(response.Content.ReadAsStringAsync().Result);

                    return View(modelList);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public async Task<IActionResult> TamamlandiSayfasi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/TamamlandiList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunTakipDataModel> modelList = new List<UrunTakipDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(response.Content.ReadAsStringAsync().Result);

                    return View(modelList);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public async Task<IActionResult> IptalSayfasi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/IptalList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunTakipDataModel> modelList = new List<UrunTakipDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(response.Content.ReadAsStringAsync().Result);

                    return View(modelList);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public void SiparisDurum(int pSiparisDurum, int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + $"/api/UrunTakipApi/GetSiparisDurumSellerApp?pSiparisDurum={pSiparisDurum}&pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.SiparisDurum == 2)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 1);
                        }
                        else if (model.SiparisDurum == 3)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 2);
                        }
                    }
                }
            }
        }
    }
}