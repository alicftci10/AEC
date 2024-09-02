﻿using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class KullaniciController : BaseController
    {
        public async Task<IActionResult> PersonelListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetListPersonel";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<KullaniciDataModel> modelList = new List<KullaniciDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                if (CurrentKullanici.KullaniciTuruId == 1)
                {
                    ViewData["KullaniciAdmin"] = modelList;
                }
                
                return View(modelList);
            }
        }

        public IActionResult KullaniciCreate()
        {
            KullaniciDataModel model = new KullaniciDataModel();

            //LoadViewBag();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciCreate(KullaniciDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                //LoadViewBag();

                if (ModelState.IsValid)
                {
                    var KullaniciList = HttpContext.Session.GetString("Kullanicilar");

                    if (KullaniciList.Contains(model.KullaniciAdi))
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        return View(model);
                    }

                    string url = "https://localhost:7120/api/KullaniciApi/Save";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("KullaniciListesi");
                    }
                    else
                    {
                        return View(model);
                    }
                }

                return View(model);
            }
        }

        //public async Task<IActionResult> KullaniciEdit(int pId)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        LoadViewBag();

        //        string url = "https://localhost:7120/api/KullaniciApi/GetKullanici";

        //        url += $"?pId={pId}";

        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

        //        var response = await client.GetAsync(url);

        //        var data = response.Content.ReadAsStringAsync();

        //        KullaniciViewModel model = new KullaniciViewModel();

        //        if (response.IsSuccessStatusCode)
        //        {
        //            model = JsonConvert.DeserializeObject<KullaniciViewModel>(data.Result);

        //            HttpContext.Session.SetString("secilenKullanici", model.KullaniciAdi);

        //            if (CurrentKullanici.Yetki == 2 || model.Id == CurrentKullanici.Id)
        //            {
        //                model.IsSuccess = true;
        //            }

        //            if (CurrentKullanici.Yetki == 2)
        //            {
        //                if (model.Id != CurrentKullanici.Id)
        //                {
        //                    TempData["ErrorMessage"] = "Bunu yapmaya yetkiniz yok. Lütfen yetkiliye başvurunuz.";
        //                    return RedirectToAction("KullaniciListesi");
        //                }
        //            }
        //        }

        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> KullaniciEdit(KullaniciViewModel model)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        LoadViewBag();

        //        if (ModelState.IsValid)
        //        {
        //            string KullaniciAdi = HttpContext.Session.GetString("secilenKullanici");

        //            if (model.KullaniciAdi == KullaniciAdi)
        //            {
        //                goto buraya;
        //            }

        //            string KullaniciList = HttpContext.Session.GetString("Kullanicilar");

        //            List<string> kullaniciListesi = JsonConvert.DeserializeObject<List<string>>(KullaniciList);

        //            if (kullaniciListesi.Contains(model.KullaniciAdi))
        //            {
        //                ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
        //                return View(model);
        //            }

        //        buraya:

        //            string url = "https://localhost:7120/api/KullaniciApi/Update";

        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

        //            var json = JsonConvert.SerializeObject(model);
        //            var content = new StringContent(json, Encoding.UTF8, "application/json");

        //            var response = await client.PutAsync(url, content);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                if (model.Id == CurrentKullanici.Id)
        //                {
        //                    model.JwtToken = CurrentKullanici.JwtToken;

        //                    HttpContext.Session.SetString("Kullanici", JsonConvert.SerializeObject(model));
        //                }

        //                return RedirectToAction("KullaniciListesi");
        //            }
        //            else
        //            {
        //                return View(model);
        //            }
        //        }

        //        return View(model);
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> KullaniciDelete(int pId)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        if (CurrentKullanici.Yetki == 2 || CurrentKullanici.Id == pId)
        //        {
        //            TempData["ErrorMessage"] = "Bunu yapmaya yetkiniz yok. Lütfen yetkiliye başvurunuz.";
        //            return RedirectToAction("KullaniciListesi");
        //        }

        //        string url = "https://localhost:7120/api/KullaniciApi/Delete";

        //        url += $"?pId={pId}";

        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

        //        var response = await client.DeleteAsync(url);

        //        return RedirectToAction("KullaniciListesi");
        //    }
        //}
    }
}
