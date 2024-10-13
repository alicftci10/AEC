using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class LaptopController : BaseController
    {
        public LaptopController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> LaptopListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/GetLaptopList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<LaptopDataModel> modelList = new List<LaptopDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<LaptopDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> LaptopSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadIsletimSistemiList();
                LoadCPUList();
                LoadGPUList();
                LoadRAMList();
                LoadSSDList();
                LoadCozunurlukList();
                LoadYenilemeHiziList();

                MessageBox();

                LaptopDataModel model = new LaptopDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/GetLaptop";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<LaptopDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenLaptopAdi", model.LaptopAdi);
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
        public async Task<IActionResult> LaptopSayfasi(LaptopDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadIsletimSistemiList();
                LoadCPUList();
                LoadGPUList();
                LoadRAMList();
                LoadSSDList();
                LoadCozunurlukList();
                LoadYenilemeHiziList();

                if (ModelState.IsValid)
                {
                    string? secilenLaptopAdi = HttpContext.Session.GetString("secilenLaptopAdi");

                    if (model.Id == 0)
                    {
                        secilenLaptopAdi = null;
                    }

                    LoadLaptopList();

                    if (model.LaptopAdi != secilenLaptopAdi)
                    {
                        var LaptopAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("LaptopAdiList"));

                        if (LaptopAdi != null)
                        {
                            if (LaptopAdi.Contains(model.LaptopAdi))
                            {
                                ModelState.AddModelError("LaptopAdi", "Bu Laptop Adı daha önce kullanılmış. Lütfen farklı Laptop Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var kayitedilenmodel = JsonConvert.DeserializeObject<LaptopDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetInt32("secilenLaptopId", kayitedilenmodel.Id);
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("LaptopSayfasi");
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

        public async Task<IActionResult> LaptopResimAddUpdate(int? pLaptopId)
        {
            using (HttpClient client = new HttpClient())
            {
                if (CurrentKullanici.KullaniciTuruId == 2)
                {
                    return RedirectToAction("LaptopSayfasi");
                }

                if (pLaptopId == null)
                {
                    pLaptopId = HttpContext.Session.GetInt32("secilenLaptopId");
                }

                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/GetLaptopResmiList";

                url += $"?pLaptopId={pLaptopId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<UrunResmiDataModel> model = new List<UrunResmiDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<List<UrunResmiDataModel>>(response.Content.ReadAsStringAsync().Result);
                    HttpContext.Session.SetInt32("secilenLaptopId", pLaptopId.Value);
                }

                return PartialView(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LaptopResimAddUpdate(List<IFormFile> ResimUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/AddUpdateLaptop";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                int? LaptopId = HttpContext.Session.GetInt32("secilenLaptopId");

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

                    if (LaptopId.HasValue)
                    {
                        multipartFormContent.Add(new StringContent(LaptopId.Value.ToString()), "LaptopId");
                    }

                    var response = await client.PostAsync(url, multipartFormContent);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 4);
                        return RedirectToAction("LaptopSayfasi");
                    }
                }

                return PartialView(ResimUrl);
            }
        }

        public async Task<IActionResult> LaptopDetay(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/GetLaptopId";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                LaptopDataModel model = new LaptopDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<LaptopDataModel>(response.Content.ReadAsStringAsync().Result);

                    return View(model);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public async Task<IActionResult> LaptopDetayResim(int pLaptopId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunResmiApi/GetLaptopResmiList";

                url += $"?pLaptopId={pLaptopId}";

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

        public async Task<IActionResult> LaptopSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/LaptopApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("LaptopSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}