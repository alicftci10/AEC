﻿using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class KullaniciKartController : BaseController
    {
        public async Task<IActionResult> KullaniciKartListesi(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKartListesi";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<KullaniciKartDataModel> modelList = new List<KullaniciKartDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciKartDataModel>>(response.Content.ReadAsStringAsync().Result);

                    List<string> KartAdi = new List<string>();
                    List<string> KartNumarasi = new List<string>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            KartAdi.Add(item.KartAdi);
                            KartNumarasi.Add(item.KartNumarasi);
                        }

                        HttpContext.Session.SetString("KullaniciKartAdiList", JsonConvert.SerializeObject(KartAdi));
                        HttpContext.Session.SetString("KullaniciKartNumarasiList", JsonConvert.SerializeObject(KartNumarasi));
                    }
                }

                HttpContext.Session.SetInt32("secilenKullaniciKartId", pId);
                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> KullaniciKartSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
                if (MessageBox == 1)
                {
                    TempData["MessageBox"] = 4;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 2)
                {
                    TempData["MessageBox"] = 5;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }

                if (CurrentKullanici.KullaniciTuruId == 2)
                {
                    if (pId != CurrentKullanici.Id)
                    {
                        pId = CurrentKullanici.Id;
                    }
                }

                KullaniciKartDataModel model = new KullaniciKartDataModel();

                if (pId > 0)
                {
                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKart";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<KullaniciKartDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenKullaniciKartAdi", model.KartAdi);
                        HttpContext.Session.SetString("secilenKullaniciKartNumarasi", model.KartNumarasi);
                    }
                }

                return PartialView(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciKartSayfasi(KullaniciKartDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        int? KullaniciId = HttpContext.Session.GetInt32("secilenKullaniciKartId");
                        model.KullaniciId = KullaniciId.Value;
                    }

                    string? secilenKullaniciKartAdi = HttpContext.Session.GetString("secilenKullaniciKartAdi");
                    string? secilenKullaniciKartNumarasi = HttpContext.Session.GetString("secilenKullaniciKartNumarasi");

                    string kullanicikartadisonuc = "";
                    string kullanicikartnumarasisonuc = "";

                    if (model.KartAdi != secilenKullaniciKartAdi)
                    {
                        var KullaniciKartAdiList = HttpContext.Session.GetString("KullaniciKartAdiList");

                        if (KullaniciKartAdiList != null)
                        {
                            if (KullaniciKartAdiList.Contains(model.KartAdi))
                            {
                                kullanicikartadisonuc = "1";
                            }
                        }
                    }

                    if (model.KartNumarasi != secilenKullaniciKartNumarasi)
                    {
                        var KullaniciKartNumarasiList = HttpContext.Session.GetString("KullaniciKartNumarasiList");

                        if (KullaniciKartNumarasiList != null)
                        {
                            if (KullaniciKartNumarasiList.Contains(model.KartNumarasi))
                            {
                                kullanicikartnumarasisonuc = "2";
                            }
                        }
                    }

                    if (kullanicikartadisonuc == "1" && kullanicikartnumarasisonuc == "2")
                    {
                        ModelState.AddModelError("KartAdi", "Bu Kart Adı daha önce kullanılmış. Lütfen farklı Kart Adı deneyin!");
                        ModelState.AddModelError("KartNumarasi", "Bu Kart Numarasında kartınız mevcut. Lütfen farklı Kart Numarası deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullanicikartadisonuc != "1" && kullanicikartnumarasisonuc == "2")
                    {
                        ModelState.AddModelError("KartNumarasi", "Bu Kart Numarasında kartınız mevcut. Lütfen farklı Kart Numarası deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullanicikartadisonuc == "1" && kullanicikartnumarasisonuc != "2")
                    {
                        ModelState.AddModelError("KartAdi", "Bu Kart Adı daha önce kullanılmış. Lütfen farklı Kart Adı deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("KullaniciDetay", "Kullanici");
                    }
                }

                model.IsSuccess = true ;
                return View(model);
            }
        }
    }
}
