using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class MouseController : BaseController
    {
        public MouseController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> MouseListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MouseApi/GetMouseList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<MouseDataModel> modelList = new List<MouseDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<MouseDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> MouseSayfasi(int? pId)
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

                MouseDataModel model = new MouseDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/MouseApi/GetMouse";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<MouseDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenMouseAdi", model.MouseAdi);
                    }
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MouseSayfasi(MouseDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                if (ModelState.IsValid)
                {
                    string? secilenMouseAdi = HttpContext.Session.GetString("secilenMouseAdi");

                    if (model.Id == 0)
                    {
                        secilenMouseAdi = null;
                    }

                    LoadMouseList();

                    if (model.MouseAdi != secilenMouseAdi)
                    {
                        var MouseAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("MouseAdiList"));

                        if (MouseAdi != null)
                        {
                            if (MouseAdi.Contains(model.MouseAdi))
                            {
                                ModelState.AddModelError("MouseAdi", "Bu Mouse Adı daha önce kullanılmış. Lütfen farklı Mouse Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/MouseApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var kayitedilenmodel = JsonConvert.DeserializeObject<MouseDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetInt32("secilenMouseId", kayitedilenmodel.Id);
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("MouseSayfasi");

                    }
                }

                model.IsSuccess = true;
                return View(model);
            }
        }

        public async Task<IActionResult> MouseResimAddUpdate(int? pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
                if (CurrentKullanici.KullaniciTuruId == 2)
                {
                    return RedirectToAction("MouseSayfasi");
                }

                if (pMouseId == null)
                {
                    pMouseId = HttpContext.Session.GetInt32("secilenMouseId");
                }

                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/GetMouseResmiList";

                url += $"?pMouseId={pMouseId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunResmiDataModel> model = new List<UrunResmiDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunResmiDataModel>>(response.Content.ReadAsStringAsync().Result);
                    HttpContext.Session.SetInt32("secilenMouseId", pMouseId.Value);
                }

                return PartialView(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MouseResimAddUpdate(List<IFormFile> ResimUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/AddUpdateMouse";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                int? MouseId = HttpContext.Session.GetInt32("secilenMouseId");

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    foreach (var dosya in ResimUrl)
                    {
                        if (dosya.Length > 0)
                        {
                            var dosyaIcerigi = new StreamContent(dosya.OpenReadStream());
                            dosyaIcerigi.Headers.ContentType = new MediaTypeHeaderValue(dosya.ContentType);
                            multipartFormContent.Add(dosyaIcerigi, "ResimUrl", dosya.FileName);
                        }
                    }

                    if (MouseId.HasValue)
                    {
                        multipartFormContent.Add(new StringContent(MouseId.Value.ToString()), "MouseId");
                    }

                    var response = await client.PostAsync(url, multipartFormContent);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 4);
                        return RedirectToAction("MouseSayfasi");
                    }
                }

                return PartialView(ResimUrl);
            }
        }

        public async Task<IActionResult> MouseDetay(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MouseApi/GetMouseId";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                MouseDataModel model = new MouseDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<MouseDataModel>(response.Content.ReadAsStringAsync().Result);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> MouseDetayResim(int pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/GetMouseResmiList";

                url += $"?pMouseId={pMouseId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunResmiDataModel> model = new List<UrunResmiDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunResmiDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(model);
            }
        }

        public async Task<IActionResult> MouseSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/MouseApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                HttpContext.Session.SetInt32("MessageBox", 2);
                return RedirectToAction("MouseSayfasi");
            }
        }
    }
}
