using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class CozunurlukController : BaseController
    {
        public CozunurlukController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> CozunurlukListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/CozunurlukApi/GetCozunurlukList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<CozunurlukDataModel> modelList = new List<CozunurlukDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<CozunurlukDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> CozunurlukSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadCozunurlukDropDown();

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

                CozunurlukDataModel model = new CozunurlukDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/CozunurlukApi/GetCozunurluk";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<CozunurlukDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenCozunurlukAdi", model.CozunurlukAdi);

                        return View(model);
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
        public async Task<IActionResult> CozunurlukSayfasi(CozunurlukDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadCozunurlukDropDown();

                if (ModelState.IsValid)
                {
                    string? secilenCozunurlukAdi = HttpContext.Session.GetString("secilenCozunurlukAdi");

                    if (model.Id == 0)
                    {
                        secilenCozunurlukAdi = null;
                    }

                    LoadCozunurlukList();

                    if (model.CozunurlukAdi != secilenCozunurlukAdi)
                    {
                        var CozunurlukAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("CozunurlukAdiList"));

                        if (CozunurlukAdi != null)
                        {
                            if (CozunurlukAdi.Contains(model.CozunurlukAdi))
                            {
                                ModelState.AddModelError("CozunurlukAdi", "Bu Çözünürlük Adı daha önce kullanılmış. Lütfen farklı Çözünürlük Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/CozunurlukApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("CozunurlukSayfasi");
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

        public async Task<IActionResult> CozunurlukSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/CozunurlukApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("CozunurlukSayfasi");
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }
    }
}
