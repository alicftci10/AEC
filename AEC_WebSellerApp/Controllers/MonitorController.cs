﻿using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class MonitorController : BaseController
    {
        public MonitorController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> MonitorListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MonitorApi/GetMonitorList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<MonitorDataModel> modelList = new List<MonitorDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<MonitorDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> MonitorSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadCozunurlukList();
                LoadYenilemeHiziList();

                MessageBox();

                MonitorDataModel model = new MonitorDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/MonitorApi/GetMonitor";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<MonitorDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenMonitorAdi", model.MonitorAdi);
                    }
                    else
                    {
                        return RedirectToAction("ErrorSayfasi", "Error");
                    }
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MonitorSayfasi(MonitorDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadCozunurlukList();
                LoadYenilemeHiziList();

                if (ModelState.IsValid)
                {
                    string? secilenMonitorAdi = HttpContext.Session.GetString("secilenMonitorAdi");

                    if (model.Id == 0)
                    {
                        secilenMonitorAdi = null;
                    }

                    LoadMonitorList();

                    if (model.MonitorAdi != secilenMonitorAdi)
                    {
                        var MonitorAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("MonitorAdiList"));

                        if (MonitorAdi != null)
                        {
                            if (MonitorAdi.Contains(model.MonitorAdi))
                            {
                                ModelState.AddModelError("MonitorAdi", "Bu Monitör Adı daha önce kullanılmış. Lütfen farklı Monitör Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/MonitorApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var kayitedilenmodel = JsonConvert.DeserializeObject<MonitorDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetInt32("secilenMonitorId", kayitedilenmodel.Id);
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("MonitorSayfasi");
                    }
                    else
                    {
                        return RedirectToAction("ErrorSayfasi", "Error");
                    }
                }

                model.IsSuccess = true;
                return View(model);
            }
        }

        public async Task<IActionResult> MonitorResimAddUpdate(int? pMonitorId)
        {
            using (HttpClient client = new HttpClient())
            {
                if (CurrentKullanici.KullaniciTuruId == 2)
                {
                    return RedirectToAction("MonitorSayfasi");
                }

                if (pMonitorId == null)
                {
                    pMonitorId = HttpContext.Session.GetInt32("secilenMonitorId");
                }

                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/GetMonitorResmiList";

                url += $"?pMonitorId={pMonitorId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunResmiDataModel> model = new List<UrunResmiDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunResmiDataModel>>(response.Content.ReadAsStringAsync().Result);
                    HttpContext.Session.SetInt32("secilenMonitorId", pMonitorId.Value);
                }

                return PartialView(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MonitorResimAddUpdate(List<IFormFile> ResimUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/AddUpdateMonitor";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                int? MonitorId = HttpContext.Session.GetInt32("secilenMonitorId");

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    foreach (var dosya in ResimUrl)
                    {
                        if (dosya.Length > 0)
                        {
                            var dosyaIcerigi = new StreamContent(dosya.OpenReadStream());
                            dosyaIcerigi.Headers.ContentType = new MediaTypeHeaderValue(dosya.ContentType);
                            multipartFormContent.Add(dosyaIcerigi, "ResimUrl", dosya.FileName);
                        }
                    }

                    if (MonitorId.HasValue)
                    {
                        multipartFormContent.Add(new StringContent(MonitorId.Value.ToString()), "MonitorId");
                    }

                    var response = await client.PostAsync(url, multipartFormContent);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 4);
                        return RedirectToAction("MonitorSayfasi");
                    }
                }

                return PartialView(ResimUrl);
            }
        }

        public async Task<IActionResult> MonitorDetay(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MonitorApi/GetMonitorId";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                MonitorDataModel model = new MonitorDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<MonitorDataModel>(response.Content.ReadAsStringAsync().Result);

                    return View(model);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public async Task<IActionResult> MonitorDetayResim(int pMonitorId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/GetMonitorResmiList";

                url += $"?pMonitorId={pMonitorId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunResmiDataModel> model = new List<UrunResmiDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunResmiDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(model);
            }
        }

        public async Task<IActionResult> MonitorSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MonitorApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("MonitorSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}
