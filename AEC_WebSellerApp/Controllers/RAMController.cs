using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class RAMController : BaseController
    {
        public RAMController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> RAMListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/RAMApi/GetRAMList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<RAMDataModel> modelList = new List<RAMDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<RAMDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> RAMSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadRAMDropDown();

                MessageBox();

                RAMDataModel model = new RAMDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/RAMApi/GetRAM";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<RAMDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenBellekAdi", model.BellekAdi);
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
        public async Task<IActionResult> RAMSayfasi(RAMDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadRAMDropDown();

                if (ModelState.IsValid)
                {
                    string? secilenBellekAdi = HttpContext.Session.GetString("secilenBellekAdi");

                    if (model.Id == 0)
                    {
                        secilenBellekAdi = null;
                    }

                    LoadRAMList();

                    if (model.BellekAdi != secilenBellekAdi)
                    {
                        var BellekAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("BellekAdiList"));

                        if (BellekAdi != null)
                        {
                            if (BellekAdi.Contains(model.BellekAdi))
                            {
                                ModelState.AddModelError("BellekAdi", "Bu Bellek Adı daha önce kullanılmış. Lütfen farklı Bellek Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/RAMApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("RAMSayfasi");
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

        public async Task<IActionResult> RAMSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/RAMApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("RAMSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}
