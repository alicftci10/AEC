﻿using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class CPUController : BaseController
    {
        public async Task<IActionResult> CPUListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/GetListCPU";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<CPUDataModel> modelList = new List<CPUDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<CPUDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> CPUSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

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

                CPUDataModel model = new CPUDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        if (pId != CurrentKullanici.Id)
                        {
                            pId = CurrentKullanici.Id;
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetKullanici";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<CPUDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenKullaniciAdi", model.KullaniciAdi);
                        HttpContext.Session.SetString("secilenEmail", model.Email);
                    }
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CPUSayfasi(CPUDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

                if (ModelState.IsValid)
                {
                    LoadKullaniciList();

                    string? secilenKullaniciAdi = HttpContext.Session.GetString("secilenKullaniciAdi");
                    string? secilenEmail = HttpContext.Session.GetString("secilenEmail");

                    string kullaniciadisonuc = "";
                    string emailsonuc = "";

                    if (model.Id == 0)
                    {
                        secilenKullaniciAdi = null;
                        secilenEmail = null;
                    }

                    if (model.KullaniciAdi != secilenKullaniciAdi)
                    {
                        var KullaniciAdiList = HttpContext.Session.GetString("KullaniciAdiList");

                        if (!string.IsNullOrEmpty(KullaniciAdiList))
                        {
                            if (KullaniciAdiList.Contains(model.KullaniciAdi))
                            {
                                kullaniciadisonuc = "1";
                            }
                        }
                    }

                    if (model.Email != secilenEmail)
                    {
                        var EmailList = HttpContext.Session.GetString("KullaniciEmailList");

                        if (!string.IsNullOrEmpty(EmailList))
                        {
                            if (EmailList.Contains(model.Email))
                            {
                                emailsonuc = "2";
                            }
                        }
                    }

                    if (kullaniciadisonuc == "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullaniciadisonuc != "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullaniciadisonuc == "1" && emailsonuc != "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("CPUSayfasi");
                    }
                }

                model.IsSuccess = true;
                return View(model);
            }
        }

        public async Task<IActionResult> CPUSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                HttpContext.Session.SetInt32("MessageBox", 2);
                return RedirectToAction("CPUSayfasi");
            }
        }
    }
}
