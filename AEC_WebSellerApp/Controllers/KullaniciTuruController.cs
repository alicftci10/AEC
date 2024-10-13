using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class KullaniciTuruController : BaseController
    {
        public KullaniciTuruController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> KullaniciTuruListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciTuruApi/GetKullaniciTuruList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<KullaniciTuruDataModel> modelList = new List<KullaniciTuruDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciTuruDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> KullaniciTuruSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                MessageBox();

                KullaniciTuruDataModel model = new KullaniciTuruDataModel();

                if (pId > 0)
                {
                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciTuruApi/GetKullaniciTuru";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<KullaniciTuruDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenKullaniciTuruAdi", model.TurAdi);
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
        public async Task<IActionResult> KullaniciTuruSayfasi(KullaniciTuruDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                if (ModelState.IsValid)
                {
                    string? secilenKullaniciTuruAdi = HttpContext.Session.GetString("secilenKullaniciTuruAdi");

                    if (model.Id == 0)
                    {
                        secilenKullaniciTuruAdi = null;
                    }

                    LoadKullaniciTuruList();

                    if (model.TurAdi != secilenKullaniciTuruAdi)
                    {
                        var KullaniciTuruAdiList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciTuruAdiList"));

                        if (KullaniciTuruAdiList != null)
                        {
                            if (KullaniciTuruAdiList.Contains(model.TurAdi))
                            {
                                ModelState.AddModelError("TurAdi", "Bu Kullanıcı Türü Adı daha önce kullanılmış. Lütfen farklı kullanıcı türü deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciTuruApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("KullaniciTuruSayfasi");
                    }
                    else
                    {
                        return RedirectToAction("ErrorSayfasi", "Error");
                    }
                }

                model.IsSuccess = true;
                return View(model);
            }
        }

        public async Task<IActionResult> KullaniciTuruSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciTuruApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("KullaniciTuruSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}
