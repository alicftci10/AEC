﻿using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class IsletimSistemiController : BaseController
    {
        public IsletimSistemiController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> IsletimSistemiListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/IsletimSistemiApi/GetIsletimSistemiList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<IsletimSistemiDataModel> modelList = new List<IsletimSistemiDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<IsletimSistemiDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> IsletimSistemiSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadIsletimSistemiDropDown();

                MessageBox();

                IsletimSistemiDataModel model = new IsletimSistemiDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/IsletimSistemiApi/GetIsletimSistemi";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<IsletimSistemiDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenIsletimSistemiAdi", model.IsletimSistemiAdi);
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
        public async Task<IActionResult> IsletimSistemiSayfasi(IsletimSistemiDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadIsletimSistemiDropDown();

                if (ModelState.IsValid)
                {
                    string? secilenIsletimSistemiAdi = HttpContext.Session.GetString("secilenIsletimSistemiAdi");

                    if (model.Id == 0)
                    {
                        secilenIsletimSistemiAdi = null;
                    }

                    LoadIsletimSistemiList();

                    if (model.IsletimSistemiAdi != secilenIsletimSistemiAdi)
                    {
                        var IsletimSistemiAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("IsletimSistemiAdiList"));

                        if (IsletimSistemiAdi != null)
                        {
                            if (IsletimSistemiAdi.Contains(model.IsletimSistemiAdi))
                            {
                                ModelState.AddModelError("IsletimSistemiAdi", "Bu İşletim Sistemi Sürümü daha önce kullanılmış. Lütfen farklı İşletim Sistemi Sürümü deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/IsletimSistemiApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("IsletimSistemiSayfasi");
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

        public async Task<IActionResult> IsletimSistemiSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/IsletimSistemiApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("IsletimSistemiSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}
