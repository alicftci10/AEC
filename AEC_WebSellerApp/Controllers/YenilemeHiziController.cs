using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class YenilemeHiziController : BaseController
    {
        public YenilemeHiziController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> YenilemeHiziListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/YenilemeHiziApi/GetYenilemeHiziList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<YenilemeHiziDataModel> modelList = new List<YenilemeHiziDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<YenilemeHiziDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> YenilemeHiziSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadYenilemeHiziDropDown();

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

                YenilemeHiziDataModel model = new YenilemeHiziDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/YenilemeHiziApi/GetYenilemeHizi";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<YenilemeHiziDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenYenilemeHiziAdi", model.YenilemeHiziAdi);
                    }
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> YenilemeHiziSayfasi(YenilemeHiziDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadYenilemeHiziDropDown();

                if (ModelState.IsValid)
                {
                    string? secilenYenilemeHiziAdi = HttpContext.Session.GetString("secilenYenilemeHiziAdi");

                    if (model.Id == 0)
                    {
                        secilenYenilemeHiziAdi = null;
                    }

                    LoadYenilemeHiziList();

                    if (model.YenilemeHiziAdi != secilenYenilemeHiziAdi)
                    {
                        var YenilemeHiziAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("YenilemeHiziAdiList"));

                        if (YenilemeHiziAdi != null)
                        {
                            if (YenilemeHiziAdi.Contains(model.YenilemeHiziAdi))
                            {
                                ModelState.AddModelError("YenilemeHiziAdi", "Bu Yenileme Hızı Adı daha önce kullanılmış. Lütfen farklı Yenileme Hızı Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/YenilemeHiziApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("YenilemeHiziSayfasi");
                    }
                }

                model.IsSuccess = true;
                return View(model);
            }
        }

        public async Task<IActionResult> YenilemeHiziSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/YenilemeHiziApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                HttpContext.Session.SetInt32("MessageBox", 2);
                return RedirectToAction("YenilemeHiziSayfasi");
            }
        }
    }
}
