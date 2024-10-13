using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;

namespace AEC_WebApp.Controllers
{
    public class UrunTakipController : BaseController
    {
        public UrunTakipController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> SepetSayfasi()
        {
            using (HttpClient client = new HttpClient())
            {
                MessageBox();

                LoadKullaniciKartDropDown();

                int? SiparisOnay = HttpContext.Session.GetInt32("SiparisOnay");
                if (SiparisOnay == 1)
                {
                    TempData["SiparisOnay"] = 1;
                    HttpContext.Session.SetInt32("SiparisOnay", 3);
                }

                int? SiparisBos = HttpContext.Session.GetInt32("SiparisBos");
                if (SiparisBos == 1)
                {
                    TempData["SiparisBos"] = 1;
                    HttpContext.Session.SetInt32("SiparisBos", 3);
                }

                int? secilenKartErrorMessage = HttpContext.Session.GetInt32("secilenKartErrorMessage");
                if (secilenKartErrorMessage == 1)
                {
                    TempData["ErrorMessage"] = "Lütfen Ödeme Yöntemi Seçiniz!!";
                    HttpContext.Session.SetInt32("secilenKartErrorMessage", 3);
                }

                int? CheckboxErrorMessage = HttpContext.Session.GetInt32("CheckboxErrorMessage");
                if (CheckboxErrorMessage == 1)
                {
                    TempData["CheckboxErrorMessage"] = "Lütfen Şartlar ve Koşulları Onaylayın!!";
                    HttpContext.Session.SetInt32("CheckboxErrorMessage", 3);
                }

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetSepetList";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(response.Content.ReadAsStringAsync().Result);

                    return View(model);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public async Task<IActionResult> SiparisSayfasi()
        {
            using (HttpClient client = new HttpClient())
            {
                MessageBox();

                int? SiparisOnay = HttpContext.Session.GetInt32("SiparisOnay");
                if (SiparisOnay == 1)
                {
                    TempData["SiparisOnay"] = 1;
                    HttpContext.Session.SetInt32("SiparisOnay", 3);
                }

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetSiparisList";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(response.Content.ReadAsStringAsync().Result);

                    return View(model);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public void FavoriDurum(int? pLaptopId, int? pMonitorId, int? pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetFavoriDurum";

                if (pLaptopId != null)
                {
                    url += $"?pLaptopId={pLaptopId}";
                }
                else if (pMonitorId != null)
                {
                    url += $"?pMonitorId={pMonitorId}";
                }
                else if (pMouseId != null)
                {
                    url += $"?pMouseId={pMouseId}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.Favori == true)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 4);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("MessageBox", 5);
                        }
                    }
                }
            }
        }

        public void SepetDurum(int? pLaptopId, int? pMonitorId, int? pMouseId, int? pAdet)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetSepetDurum";

                if (pAdet == null)
                {
                    pAdet = 1;
                }

                if (pLaptopId != null)
                {
                    url += $"?pLaptopId={pLaptopId}&pAdet={pAdet}";
                }
                else if (pMonitorId != null)
                {
                    url += $"?pMonitorId={pMonitorId}&pAdet={pAdet}";
                }
                else if (pMouseId != null)
                {
                    url += $"?pMouseId={pMouseId}&pAdet={pAdet}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.SepetDurum == true)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 6);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("MessageBox", 7);
                        }
                    }
                }
            }
        }

        public void AdetDurum(int pId, int pAdet)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + $"/api/UrunTakipApi/GetAdetDurum?pId={pId}&pAdet={pAdet}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.SepetDurum == false)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 7);
                        }
                    }
                }
            }
        }

        public void SiparisDurum(int? secilenKart, int? terms)
        {
            using (HttpClient client = new HttpClient())
            {
                if (!secilenKart.HasValue)
                {
                    HttpContext.Session.SetInt32("secilenKartErrorMessage", 1);
                }
                else if (!terms.HasValue)
                {
                    HttpContext.Session.SetInt32("CheckboxErrorMessage", 1);
                }
                else
                {
                    string url = ConfigurationInfo.ApiUrl + $"/api/UrunTakipApi/GetSiparisDurum";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                    var response = client.GetAsync(url);
                    var text = response.Result;

                    List<UrunTakipDataModel> modelList = new List<UrunTakipDataModel>();

                    if (text != null)
                    {
                        modelList = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                        if (modelList != null && modelList.Count > 0)
                        {
                            HttpContext.Session.SetInt32("SiparisOnay", 1);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("SiparisBos", 1);
                        }
                    }
                }
            }
        }

        public void AllSepeteEkle()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/AllSepeteEkle";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 8);
                    }
                }
            }
        }

        public void AllFavoriDelete()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/AllFavoriDelete";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 9);
                    }
                }
            }
        }

        public void AllSepetDelete()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/AllSepetDelete";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 10);
                    }
                }
            }
        }
    }
}
