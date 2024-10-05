using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class KullaniciKartController : BaseController
    {
        public KullaniciKartController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

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
                }

                HttpContext.Session.SetInt32("secilenKullaniciKartId", pId);
                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> KullaniciKartSayfasi(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
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

                        if (CurrentKullanici.KullaniciTuruId == 2)
                        {
                            if (model.KullaniciId != CurrentKullanici.Id)
                            {
                                return RedirectToAction("KullaniciDetay", "Kullanici");
                            }
                        }

                        HttpContext.Session.SetString("secilenKullaniciKartAdi", model.KartAdi);
                        HttpContext.Session.SetString("secilenKullaniciKartNumarasi", model.KartNumarasi);
                    }
                    else
                    {
                        return RedirectToAction("ErrorSayfasi", "Error");
                    }
                }
                else
                {
                    int? kullaniciId = HttpContext.Session.GetInt32("secilenkullaniciId");
                    if (kullaniciId != null)
                    {
                        model.KullaniciId = kullaniciId.Value;
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
                        int? KullaniciId = HttpContext.Session.GetInt32("secilenKullaniciKartId");
                        model.KullaniciId = KullaniciId.Value;
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
                        ModelState.AddModelError("KartNumarasi", "Bu Kart Numarasında kartınız mevcut. Lütfen farklı Kart Numarası deneyin!");
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
                        HttpContext.Session.SetInt32("MessageBox", 4);
                        return RedirectToAction("KullaniciDetay", "Kullanici");
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
                    HttpContext.Session.SetInt32("MessageBox", 5);
                    return RedirectToAction("KullaniciDetay", "Kullanici");
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }
    }
}
