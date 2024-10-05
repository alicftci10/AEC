using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace AEC_WebSellerApp.Controllers
{
    public class BaseController : Controller
    {
        public IMemoryCache _memoryCacheBase;

        public BaseController()
        {

        }

        public KullaniciDataModel CurrentKullanici
        {
            get
            {
                KullaniciDataModel model = new KullaniciDataModel();
                string? sessionKullanici = HttpContext.Session.GetString("Kullanici");
                if (!string.IsNullOrEmpty(sessionKullanici))
                {//Session Dolu
                    model = JsonConvert.DeserializeObject<KullaniciDataModel>(sessionKullanici);
                }

                if (model != null)
                {
                    return model;
                }
                else
                {
                    return new KullaniciDataModel();
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessionKullanici = HttpContext.Session.GetString("Kullanici");

            if (!string.IsNullOrEmpty(sessionKullanici))
            {//Session Dolu

                LoadHakkimizdaInfo();

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
                    else if (model.KullaniciTuruId == 2)
                    {
                        ViewData["KullaniciTuru"] = "Personel";
                    }
                    else if (model.KullaniciTuruId == 3)
                    {
                        ViewData["KullaniciTuru"] = "Müşteri";
                    }
                }
            }
            else
            {//Session Boş Logine yönlendir
                context.Result = new RedirectResult("/Authentication/LoginEkrani");
            }
        }

        public HakkimizdaDataModel LoadHakkimizdaInfo()
        {
            HakkimizdaDataModel model = new HakkimizdaDataModel();

            if (_memoryCacheBase != null && !_memoryCacheBase.TryGetValue("HakkimizdaCacheValue", out model))
            {//Cache'de yoksa API'den getir.
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                    var response = client.GetAsync(ConfigurationInfo.ApiUrl + "/api/HakkimizdaApi/GetHakkimizda").Result;
                    if (response != null)
                    {
                        model = JsonConvert.DeserializeObject<HakkimizdaDataModel>(response.Content.ReadAsStringAsync().Result);
                    }
                }

                var cacheEntryOptionsSliding = new MemoryCacheEntryOptions().
                    SetSlidingExpiration(TimeSpan.FromHours(ConfigurationInfo.MemoryCacheTimeOut));

                _memoryCacheBase.Set("HakkimizdaCacheValue", model, cacheEntryOptionsSliding);
                //cache de yoksa cache e datamodel yazsın.
            }
            else
            {//Cache'de varsa cache den getir.
                _memoryCacheBase.TryGetValue("HakkimizdaCacheValue", out model);
            }

            if (model != null)
            {
                string whatsappLink = "https://wa.me/9" + model.Telefon + "?text=Bilgi%20Almak%20istiyorum";

                ViewData["Hakkimizda_whatsappLink"] = whatsappLink;
                ViewData["Hakkimizda_Telefon"] = model.Telefon;
                ViewData["Hakkimizda_Adres"] = model.Adres;
                ViewData["Hakkimizda_Email"] = model.Email;
                ViewData["Hakkimizda_CalismaGunleri"] = model.CalismaGunleri;

                return model;
            }
            else
            {
                return new HakkimizdaDataModel();
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

                    List<SelectListItem> listPersonel = new List<SelectListItem>();
                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.Id == 1 || item.Id == 2)
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
        }

        public void LoadKategoriList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetAllKategori";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> KategoriAdi = new List<string>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            KategoriAdi.Add(item.KategoriAdi);
                        }

                        HttpContext.Session.SetString("KategoriAdiList", JsonConvert.SerializeObject(KategoriAdi));
                    }
                }
            }
        }

        public void LoadKategoriDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori == null && item.OrtaKategori != null)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.UstKategori + " > " + item.OrtaKategori });
                            }
                            else if (item.AltKategori == null && item.OrtaKategori == null)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.UstKategori });
                            }
                        }

                        ViewBag.KategoriList = list;
                    }
                }
            }
        }

        public void LoadCPUList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/CPUApi/GetAllCPU";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<CPUDataModel> modelList = new List<CPUDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<CPUDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> IslemciAdi = new List<string>();
                    List<SelectListItem> IslemciAdiList = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            IslemciAdi.Add(item.IslemciAdi);
                            IslemciAdiList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.IslemciAdi });
                        }

                        HttpContext.Session.SetString("IslemciAdiList", JsonConvert.SerializeObject(IslemciAdi));
                        ViewBag.IslemciAdiList = IslemciAdiList;
                    }
                }
            }
        }

        public void LoadCPUDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori != null && item.MainKategoriId == 4)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                            }
                        }

                        ViewBag.IslemciSerisiList = list;
                    }
                }
            }
        }

        public void LoadGPUList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/GPUApi/GetAllGPU";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<GPUDataModel> modelList = new List<GPUDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<GPUDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> EkranKartiAdi = new List<string>();
                    List<SelectListItem> EkranKartiAdiList = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            EkranKartiAdi.Add(item.EkranKartiAdi);
                            EkranKartiAdiList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.EkranKartiAdi });
                        }

                        HttpContext.Session.SetString("EkranKartiAdiList", JsonConvert.SerializeObject(EkranKartiAdi));
                        ViewBag.EkranKartiAdiList = EkranKartiAdiList;
                    }
                }
            }
        }

        public void LoadGPUDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori != null && item.MainKategoriId == 3)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                            }
                        }

                        ViewBag.EkranKartiSerisiList = list;
                    }
                }
            }
        }

        public void LoadRAMList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/RAMApi/GetAllRAM";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<RAMDataModel> modelList = new List<RAMDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<RAMDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> BellekAdi = new List<string>();
                    List<SelectListItem> BellekAdiList = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            BellekAdi.Add(item.BellekAdi);
                            BellekAdiList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.BellekAdi });
                        }

                        HttpContext.Session.SetString("BellekAdiList", JsonConvert.SerializeObject(BellekAdi));
                        ViewBag.BellekAdiList = BellekAdiList;
                    }
                }
            }
        }

        public void LoadRAMDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori != null && item.MainKategoriId == 5)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                            }
                        }

                        ViewBag.BellekList = list;
                    }
                }
            }
        }

        public void LoadSSDList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/SSDApi/GetAllSSD";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<SSDDataModel> modelList = new List<SSDDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<SSDDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> DepolamaAdi = new List<string>();
                    List<SelectListItem> DepolamaAdiList = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            DepolamaAdi.Add(item.DepolamaAdi);
                            DepolamaAdiList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.DepolamaAdi });
                        }

                        HttpContext.Session.SetString("DepolamaAdiList", JsonConvert.SerializeObject(DepolamaAdi));
                        ViewBag.DepolamaAdiList = DepolamaAdiList;
                    }
                }
            }
        }

        public void LoadSSDDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori != null && item.MainKategoriId == 25)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                            }
                        }

                        ViewBag.DepolamaList = list;
                    }
                }
            }
        }

        public void LoadYenilemeHiziList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/YenilemeHiziApi/GetAllYenilemeHizi";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<YenilemeHiziDataModel> modelList = new List<YenilemeHiziDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<YenilemeHiziDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> YenilemeHiziAdi = new List<string>();
                    List<SelectListItem> YenilemeHiziAdiList = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            YenilemeHiziAdi.Add(item.YenilemeHiziAdi);
                            YenilemeHiziAdiList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.YenilemeHiziAdi });
                        }

                        HttpContext.Session.SetString("YenilemeHiziAdiList", JsonConvert.SerializeObject(YenilemeHiziAdi));
                        ViewBag.YenilemeHiziAdiList = YenilemeHiziAdiList;
                    }
                }
            }
        }

        public void LoadYenilemeHiziDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori != null && item.MainKategoriId == 7)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                            }
                        }

                        ViewBag.YenilemeHiziList = list;
                    }
                }
            }
        }

        public void LoadCozunurlukList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/CozunurlukApi/GetAllCozunurluk";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<CozunurlukDataModel> modelList = new List<CozunurlukDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<CozunurlukDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> CozunurlukAdi = new List<string>();
                    List<SelectListItem> CozunurlukAdiList = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            CozunurlukAdi.Add(item.CozunurlukAdi);
                            CozunurlukAdiList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.CozunurlukAdi });
                        }

                        HttpContext.Session.SetString("CozunurlukAdiList", JsonConvert.SerializeObject(CozunurlukAdi));
                        ViewBag.CozunurlukAdiList = CozunurlukAdiList;
                    }
                }
            }
        }

        public void LoadCozunurlukDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori != null && item.MainKategoriId == 8)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                            }
                        }

                        ViewBag.CozunurlukList = list;
                    }
                }
            }
        }

        public void LoadIsletimSistemiList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/IsletimSistemiApi/GetAllIsletimSistemi";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<IsletimSistemiDataModel> modelList = new List<IsletimSistemiDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<IsletimSistemiDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> IsletimSistemiAdi = new List<string>();
                    List<SelectListItem> IsletimSistemiAdiList = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            IsletimSistemiAdi.Add(item.IsletimSistemiAdi);
                            IsletimSistemiAdiList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.IsletimSistemiAdi });
                        }

                        HttpContext.Session.SetString("IsletimSistemiAdiList", JsonConvert.SerializeObject(IsletimSistemiAdi));
                        ViewBag.IsletimSistemiAdiList = IsletimSistemiAdiList;
                    }
                }
            }
        }

        public void LoadIsletimSistemiDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<SelectListItem> list = new List<SelectListItem>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            if (item.AltKategori != null && item.MainKategoriId == 6)
                            {
                                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                            }
                        }

                        ViewBag.IsletimSistemiList = list;
                    }
                }
            }
        }

        public void LoadLaptopList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/GetAllLaptop";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<LaptopDataModel> modelList = new List<LaptopDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<LaptopDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> LaptopAdi = new List<string>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            LaptopAdi.Add(item.LaptopAdi);
                        }

                        HttpContext.Session.SetString("LaptopAdiList", JsonConvert.SerializeObject(LaptopAdi));
                    }
                }
            }
        }

        public void LoadMonitorList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MonitorApi/GetAllMonitor";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<MonitorDataModel> modelList = new List<MonitorDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<MonitorDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> MonitorAdi = new List<string>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            MonitorAdi.Add(item.MonitorAdi);
                        }

                        HttpContext.Session.SetString("MonitorAdiList", JsonConvert.SerializeObject(MonitorAdi));
                    }
                }
            }
        }

        public void LoadMouseList()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MouseApi/GetAllMouse";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<MouseDataModel> modelList = new List<MouseDataModel>();

                if (text != null)
                {
                    modelList = JsonConvert.DeserializeObject<List<MouseDataModel>>(text.Content.ReadAsStringAsync().Result);

                    List<string> MouseAdi = new List<string>();

                    if (modelList != null)
                    {
                        foreach (var item in modelList)
                        {
                            MouseAdi.Add(item.MouseAdi);
                        }

                        HttpContext.Session.SetString("MouseAdiList", JsonConvert.SerializeObject(MouseAdi));
                    }
                }
            }
        }
    }
}
