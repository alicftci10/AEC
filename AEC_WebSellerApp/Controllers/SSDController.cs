using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class SSDController : BaseController
    {
        public SSDController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> SSDListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/SSDApi/GetSSDList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<SSDDataModel> modelList = new List<SSDDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<SSDDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> SSDSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadSSDDropDown();

                MessageBox();

                SSDDataModel model = new SSDDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/SSDApi/GetSSD";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<SSDDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenDepolamaAdi", model.DepolamaAdi);
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
        public async Task<IActionResult> SSDSayfasi(SSDDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadSSDDropDown();

                if (ModelState.IsValid)
                {
                    string? secilenDepolamaAdi = HttpContext.Session.GetString("secilenDepolamaAdi");

                    if (model.Id == 0)
                    {
                        secilenDepolamaAdi = null;
                    }

                    LoadSSDList();

                    if (model.DepolamaAdi != secilenDepolamaAdi)
                    {
                        var DepolamaAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("DepolamaAdiList"));

                        if (DepolamaAdi != null)
                        {
                            if (DepolamaAdi.Contains(model.DepolamaAdi))
                            {
                                ModelState.AddModelError("DepolamaAdi", "Bu Depolama Adı daha önce kullanılmış. Lütfen farklı Depolama Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/SSDApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("SSDSayfasi");
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

        public async Task<IActionResult> SSDSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/SSDApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("SSDSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}
