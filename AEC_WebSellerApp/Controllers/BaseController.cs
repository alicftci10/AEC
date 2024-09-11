﻿using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace AEC_WebSellerApp.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        public KullaniciDataModel CurrentKullanici
        {
            get
            {
                KullaniciDataModel model = new KullaniciDataModel();
                string sessionKullanici = HttpContext.Session.GetString("Kullanici");
                if (!string.IsNullOrEmpty(sessionKullanici))
                {//Session Dolu
                    model = JsonConvert.DeserializeObject<KullaniciDataModel>(sessionKullanici);
                }

                return model;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessionKullanici = HttpContext.Session.GetString("Kullanici");

            if (!string.IsNullOrEmpty(sessionKullanici))
            {//Session Dolu
                KullaniciDataModel model = JsonConvert.DeserializeObject<KullaniciDataModel>(sessionKullanici);

                if (model != null)
                {
                    ViewData["KullaniciId"] = model.Id;
                    ViewData["KullaniciFullName"] = model.Ad + " " + model.Soyad;
                    ViewData["KullaniciTelefon"] = model.Telefon;
                    if (model.KullaniciTuruId == 1)
                    {
                        ViewData["KullaniciTuru"] = "Admin";
                    }
                    else
                    {
                        ViewData["KullaniciTuru"] = "Personel";
                    }
                }
            }
            else
            {//Session Boş Logine yönlendir
                context.Result = new RedirectResult("/Authentication/LoginEkrani");
            }

        }

        public void LoadKullaniciList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetAllKullanici";

                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciDataModel> modelList = new List<KullaniciDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciDataModel>>(text.Content.ReadAsStringAsync().Result);
                }

                List<string> kullaniciAdi = new List<string>();
                List<string> email = new List<string>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        kullaniciAdi.Add(item.KullaniciAdi);
                        email.Add(item.Email);
                    }

                    HttpContext.Session.SetString("KullaniciAdiList", JsonConvert.SerializeObject(kullaniciAdi));
                    HttpContext.Session.SetString("KullaniciEmailList", JsonConvert.SerializeObject(email));
                }
            }
        }

        public void LoadKullaniciKartList(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKartListesi";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciKartDataModel> modelList = new List<KullaniciKartDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciKartDataModel>>(text.Content.ReadAsStringAsync().Result);
                }

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
        }


        public void LoadKullaniciTuruList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciTuruApi/GetAllKullaniciTuru";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciTuruDataModel> modelList = new List<KullaniciTuruDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciTuruDataModel>>(text.Content.ReadAsStringAsync().Result);
                }

                List<string> kullanicituruAdi = new List<string>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        kullanicituruAdi.Add(item.TurAdi);
                    }

                    HttpContext.Session.SetString("KullaniciTuruAdiList", JsonConvert.SerializeObject(kullanicituruAdi));
                }
            }
        }

        public void LoadKullaniciTuruDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciTuruApi/GetAllKullaniciTuru";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KullaniciTuruDataModel> modelList = new List<KullaniciTuruDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciTuruDataModel>>(text.Content.ReadAsStringAsync().Result);
                }

                List<SelectListItem> listPersonel = new List<SelectListItem>();
                List<SelectListItem> list = new List<SelectListItem>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        if (item.TurAdi == "Admin" || item.TurAdi == "Personel")
                        {
                            listPersonel.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.TurAdi });
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.TurAdi });
                        }
                        else
                        {
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.TurAdi });
                        }
                    }

                    ViewBag.KullaniciTuruPersonelList = listPersonel;
                    ViewBag.KullaniciTuruList = list;
                }
            }
        }
    }
}
