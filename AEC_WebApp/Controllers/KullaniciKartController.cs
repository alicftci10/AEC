using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AEC_WebApp.Controllers
{
    public class KullaniciKartController : BaseController
    {
        public KullaniciKartController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> KullaniciKartListesi(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
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
                else if (MessageBox == 4)
                {
                    TempData["MessageBox"] = 4;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 5)
                {
                    TempData["MessageBox"] = 5;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 6)
                {
                    TempData["MessageBox"] = 6;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 7)
                {
                    TempData["MessageBox"] = 7;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }

                List<KullaniciKartDataModel> modelList = new List<KullaniciKartDataModel>();

                if (pId == 0)
                {
                    pId = CurrentKullanici.Id;
                }

                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKartListesi";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciKartDataModel>>(response.Content.ReadAsStringAsync().Result);

                    HttpContext.Session.SetInt32("secilenKullaniciKartId", pId);
                    return View(modelList);
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }

        public async Task<IActionResult> KullaniciKartSayfasi(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
                if (MessageBox == 4)
                {
                    TempData["MessageBox"] = 4;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 5)
                {
                    TempData["MessageBox"] = 5;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 6)
                {
                    TempData["MessageBox"] = 6;
                    HttpContext.Session.SetInt32("MessageBox", 3);
                }
                else if (MessageBox == 7)
                {
                    TempData["MessageBox"] = 7;
                    HttpContext.Session.SetInt32("MessageBox", 3);
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

                        if (model.KullaniciId != CurrentKullanici.Id)
                        {
                            return RedirectToAction("KullaniciKartListesi");
                        }

                        HttpContext.Session.SetString("secilenKullaniciKartAdi", model.KartAdi);
                        HttpContext.Session.SetString("secilenKullaniciKartNumarasi", model.KartNumarasi);
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
        public async Task<IActionResult> KullaniciKartSayfasi(KullaniciKartDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                if (model.KartSktay == null || model.KartSktyil == null)
                {
                    ModelState.AddModelError("KartSktay", "Kart Ay/Yıl Boş Girilemez!!");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    string? secilenKullaniciKartAdi = HttpContext.Session.GetString("secilenKullaniciKartAdi");
                    string? secilenKullaniciKartNumarasi = HttpContext.Session.GetString("secilenKullaniciKartNumarasi");

                    string kullanicikartadisonuc = "";
                    string kullanicikartnumarasisonuc = "";

                    if (model.Id == 0)
                    {
                        secilenKullaniciKartAdi = null;
                        secilenKullaniciKartNumarasi = null;
                    }

                    LoadKullaniciKartList(model.KullaniciId);

                    if (model.KartAdi != secilenKullaniciKartAdi)
                    {
                        var KullaniciKartAdiList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciKartAdiList"));

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
                        var KullaniciKartNumarasiList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciKartNumarasiList"));

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
                        ModelState.AddModelError("KartNumarasi", "Bu Kart Numarasında kartınız mevcut. Lütfen farklı Kart deneyin!");
                        return View(model);
                    }
                    else if (kullanicikartadisonuc != "1" && kullanicikartnumarasisonuc == "2")
                    {
                        ModelState.AddModelError("KartNumarasi", "Bu Kart Numarasında kartınız mevcut. Lütfen farklı Kart Numarası deneyin!");
                        return View(model);
                    }
                    else if (kullanicikartadisonuc == "1" && kullanicikartnumarasisonuc != "2")
                    {
                        ModelState.AddModelError("KartAdi", "Bu Kart Adı daha önce kullanılmış. Lütfen farklı Kart Adı deneyin!");
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
                        return RedirectToAction("KullaniciKartListesi");
                    }
                    else
                    {
                        return RedirectToAction("ErrorSayfasi", "Error");
                    }
                }

                return View(model);
            }
        }

        public async Task<IActionResult> KullaniciKartSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("KullaniciKartListesi");
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }
    }
}
