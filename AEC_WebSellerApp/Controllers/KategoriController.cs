using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class KategoriController : BaseController
    {
        public KategoriController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> KategoriListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategoriList";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<KategoriDataModel> modelList = new List<KategoriDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KategoriDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> KategoriSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKategoriDropDown();

                MessageBox();

                KategoriDataModel model = new KategoriDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/GetKategori";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<KategoriDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenKategoriAdi", model.KategoriAdi);
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
        public async Task<IActionResult> KategoriSayfasi(KategoriDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKategoriDropDown();

                if (model.MainKategoriId == null)
                {
                    ModelState.AddModelError("MainKategoriId", "Ana Kategori Boş Girilemez!!");
                    model.IsSuccess = true;
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    string? secilenKategoriAdi = HttpContext.Session.GetString("secilenKategoriAdi");

                    if (model.Id == 0)
                    {
                        secilenKategoriAdi = null;
                    }

                    LoadKategoriList();

                    if (model.KategoriAdi != secilenKategoriAdi)
                    {
                        var KategoriAdi = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KategoriAdiList"));

                        if (KategoriAdi != null)
                        {
                            if (KategoriAdi.Contains(model.KategoriAdi))
                            {
                                ModelState.AddModelError("KategoriAdi", "Bu Kategori Adı daha önce kullanılmış. Lütfen farklı Kategori Adı deneyin!");
                                model.IsSuccess = true;
                                return View(model);
                            }
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("KategoriSayfasi");
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

        public async Task<IActionResult> KategoriSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KategoriApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("KategoriSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}