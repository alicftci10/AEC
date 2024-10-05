using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class GPUController : BaseController
    {
        public GPUController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> GPUListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/GPUApi/GetGPUList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<GPUDataModel> modelList = new List<GPUDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<GPUDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> GPUSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadGPUDropDown();

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

                GPUDataModel model = new GPUDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/GPUApi/GetGPU";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<GPUDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenEkranKartiAdi", model.EkranKartiAdi);
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
        public async Task<IActionResult> GPUSayfasi(GPUDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadGPUDropDown();

                if (ModelState.IsValid)
                {
                    string? secilenEkranKartiAdi = HttpContext.Session.GetString("secilenEkranKartiAdi");

                    if (model.Id == 0)
                    {
                        secilenEkranKartiAdi = null;
                    }

                    LoadGPUList();

                    if (model.EkranKartiAdi != secilenEkranKartiAdi)
                    {
                        var EkranKartiAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("EkranKartiAdiList"));

                        if (EkranKartiAdi != null)
                        {
                            if (EkranKartiAdi.Contains(model.EkranKartiAdi))
                            {
                                ModelState.AddModelError("EkranKartiAdi", "Bu Ekran Kartı Adı daha önce kullanılmış. Lütfen farklı Ekran Kartı Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/GPUApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("GPUSayfasi");
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

        public async Task<IActionResult> GPUSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/GPUApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("GPUSayfasi");
                }
                else
                {
                    return RedirectToAction("ErrorSayfasi", "Error");
                }
            }
        }
    }
}
