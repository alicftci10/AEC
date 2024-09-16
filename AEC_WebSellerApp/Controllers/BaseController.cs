using AEC_Entities.Configuration;
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
                }

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
                }

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
                }

                List<string> IslemciAdi = new List<string>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        IslemciAdi.Add(item.IslemciAdi);
                    }

                    HttpContext.Session.SetString("IslemciAdiList", JsonConvert.SerializeObject(IslemciAdi));
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
                }

                List<SelectListItem> list = new List<SelectListItem>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        if (item.AltKategori != null && item.OrtaKategori == "İşlemci Donanımları")
                        {
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                        }
                    }

                    ViewBag.IslemciSerisiList = list;
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
                }

                List<string> EkranKartiAdi = new List<string>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        EkranKartiAdi.Add(item.EkranKartiAdi);
                    }

                    HttpContext.Session.SetString("EkranKartiAdiList", JsonConvert.SerializeObject(EkranKartiAdi));
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
                }

                List<SelectListItem> list = new List<SelectListItem>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        if (item.AltKategori != null && item.OrtaKategori == "Ekran Kartı Donanımları")
                        {
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                        }
                    }

                    ViewBag.EkranKartiSerisiList = list;
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
                }

                List<string> BellekAdi = new List<string>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        BellekAdi.Add(item.BellekAdi);
                    }

                    HttpContext.Session.SetString("BellekAdiList", JsonConvert.SerializeObject(BellekAdi));
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
                }

                List<SelectListItem> list = new List<SelectListItem>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        if (item.AltKategori != null && item.OrtaKategori == "Bellek")
                        {
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                        }
                    }

                    ViewBag.BellekList = list;
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
                }

                List<string> DepolamaAdi = new List<string>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        DepolamaAdi.Add(item.DepolamaAdi);
                    }

                    HttpContext.Session.SetString("DepolamaAdiList", JsonConvert.SerializeObject(DepolamaAdi));
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
                }

                List<SelectListItem> list = new List<SelectListItem>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        if (item.AltKategori != null && item.OrtaKategori == "Depolama")
                        {
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                        }
                    }

                    ViewBag.DepolamaList = list;
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
                }

                List<string> YenilemeHiziAdi = new List<string>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        YenilemeHiziAdi.Add(item.YenilemeHiziAdi);
                    }

                    HttpContext.Session.SetString("YenilemeHiziAdiList", JsonConvert.SerializeObject(YenilemeHiziAdi));
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
                }

                List<SelectListItem> list = new List<SelectListItem>();

                if (modelList != null)
                {
                    foreach (var item in modelList)
                    {
                        if (item.AltKategori != null && item.OrtaKategori == "Yenileme Hızı")
                        {
                            list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.AltKategori });
                        }
                    }

                    ViewBag.YenilemeHiziList = list;
                }
            }
        }
    }
}
