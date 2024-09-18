﻿using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class LaptopController : BaseController
    {
        public async Task<IActionResult> LaptopListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/GetLaptopList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<LaptopDataModel> modelList = new List<LaptopDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<LaptopDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> LaptopSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadIsletimSistemiList();
                LoadCPUList();
                LoadGPUList();
                LoadRAMList();
                LoadSSDList();
                LoadCozunurlukList();
                LoadYenilemeHiziList();

                int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
                if (MessageBox == 1)
                {
                    TempData["MessageBox"] = 1;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 2)
                {
                    TempData["MessageBox"] = 2;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }

                LaptopDataModel model = new LaptopDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/GetLaptop";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<LaptopDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenLaptopAdi", model.LaptopAdi);
                    }
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LaptopSayfasi(LaptopDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadIsletimSistemiList();
                LoadCPUList();
                LoadGPUList();
                LoadRAMList();
                LoadSSDList();
                LoadCozunurlukList();
                LoadYenilemeHiziList();

                if (ModelState.IsValid)
                {
                    string? secilenLaptopAdi = HttpContext.Session.GetString("secilenLaptopAdi");

                    if (model.Id == 0)
                    {
                        secilenLaptopAdi = null;
                    }

                    LoadLaptopList();

                    if (model.LaptopAdi != secilenLaptopAdi)
                    {
                        var LaptopAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("LaptopAdiList"));

                        if (LaptopAdi != null)
                        {
                            if (LaptopAdi.Contains(model.LaptopAdi))
                            {
                                ModelState.AddModelError("LaptopAdi", "Bu Laptop Adı daha önce kullanılmış. Lütfen farklı Laptop Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("LaptopSayfasi");
                    }
                }

                model.IsSuccess = true;
                return View(model);
            }
        }

        public async Task<IActionResult> LaptopSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                HttpContext.Session.SetInt32("MessageBox", 2);
                return RedirectToAction("LaptopSayfasi");
            }
        }
    }
}