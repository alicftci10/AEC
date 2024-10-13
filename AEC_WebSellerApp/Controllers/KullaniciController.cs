using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AEC_WebSellerApp.Controllers
{
    public class KullaniciController : BaseController
    {
        public KullaniciController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> PersonelListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetListPersonel";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<KullaniciDataModel> modelList = new List<KullaniciDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> PersonelSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

                MessageBox();

                KullaniciDataModel model = new KullaniciDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        if (pId != CurrentKullanici.Id)
                        {
                            pId = CurrentKullanici.Id;
                        }
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetKullanici";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<KullaniciDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenKullaniciAdi", model.KullaniciAdi);
                        HttpContext.Session.SetString("secilenEmail", model.Email);
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
        public async Task<IActionResult> PersonelSayfasi(KullaniciDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

                if (ModelState.IsValid)
                {
                    LoadKullaniciList();

                    string? secilenKullaniciAdi = HttpContext.Session.GetString("secilenKullaniciAdi");
                    string? secilenEmail = HttpContext.Session.GetString("secilenEmail");

                    string kullaniciadisonuc = "";
                    string emailsonuc = "";

                    if (model.Id == 0)
                    {
                        secilenKullaniciAdi = null;
                        secilenEmail = null;
                    }

                    if (model.KullaniciAdi != secilenKullaniciAdi)
                    {
                        var KullaniciAdiList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciAdiList"));

                        if (KullaniciAdiList != null)
                        {
                            if (KullaniciAdiList.Contains(model.KullaniciAdi))
                            {
                                kullaniciadisonuc = "1";
                            }
                        }
                    }

                    if (model.Email != secilenEmail)
                    {
                        var EmailList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciEmailList"));

                        if (EmailList != null)
                        {
                            if (EmailList.Contains(model.Email))
                            {
                                emailsonuc = "2";
                            }
                        }
                    }

                    if (kullaniciadisonuc == "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullaniciadisonuc != "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullaniciadisonuc == "1" && emailsonuc != "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("PersonelSayfasi");
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

        public async Task<IActionResult> PersonelSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("PersonelSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        public async Task<IActionResult> KullaniciDetay(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

                MessageBox();

                if (CurrentKullanici.KullaniciTuruId == 2)
                {
                    if (pId != CurrentKullanici.Id)
                    {
                        pId = CurrentKullanici.Id;
                    }
                }

                if (pId == 0)
                {
                    int? secilenKullaniciId = HttpContext.Session.GetInt32("secilenkullaniciId");

                    if (secilenKullaniciId != null)
                    {
                        pId = secilenKullaniciId.Value;
                    }
                }

                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetKullanici";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                KullaniciDataModel model = new KullaniciDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<KullaniciDataModel>(response.Content.ReadAsStringAsync().Result);

                    HttpContext.Session.SetString("secilenKullaniciAdi", model.KullaniciAdi);
                    HttpContext.Session.SetString("secilenEmail", model.Email);
                    HttpContext.Session.SetInt32("secilenkullaniciId", model.Id);

                    return View(model);
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciDetay(KullaniciDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

                if (ModelState.IsValid)
                {
                    LoadKullaniciList();

                    string? secilenKullaniciAdi = HttpContext.Session.GetString("secilenKullaniciAdi");
                    string? secilenEmail = HttpContext.Session.GetString("secilenEmail");

                    string kullaniciadisonuc = "";
                    string emailsonuc = "";

                    if (model.KullaniciAdi != secilenKullaniciAdi)
                    {
                        var KullaniciAdiList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciAdiList"));

                        if (KullaniciAdiList != null)
                        {
                            if (KullaniciAdiList.Contains(model.KullaniciAdi))
                            {
                                kullaniciadisonuc = "1";
                            }
                        }
                    }

                    if (model.Email != secilenEmail)
                    {
                        var EmailList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciEmailList"));

                        if (EmailList != null)
                        {
                            if (EmailList.Contains(model.Email))
                            {
                                emailsonuc = "2";
                            }
                        }
                    }

                    if (kullaniciadisonuc == "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        return View(model);
                    }
                    else if (kullaniciadisonuc != "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        return View(model);
                    }
                    else if (kullaniciadisonuc == "1" && emailsonuc != "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        HttpContext.Session.SetInt32("secilenkullaniciId", model.Id);
                        return RedirectToAction("KullaniciDetay");
                    }
                    else
                    {
                        return RedirectToAction("ErrorSayfasi", "Error");
                    }
                }

                return View(model);
            }
        }

        public async Task<IActionResult> MusteriListesi(string searchTerm)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetListMusteri";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    url += $"?searchTerm={searchTerm}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<KullaniciDataModel> modelList = new List<KullaniciDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> MusteriSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

                MessageBox();

                KullaniciDataModel model = new KullaniciDataModel();

                if (pId > 0)
                {
                    if (CurrentKullanici.KullaniciTuruId == 2)
                    {
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/GetKullanici";

                    url += $"?pId={pId}";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        model = JsonConvert.DeserializeObject<KullaniciDataModel>(response.Content.ReadAsStringAsync().Result);

                        HttpContext.Session.SetString("secilenKullaniciAdi", model.KullaniciAdi);
                        HttpContext.Session.SetString("secilenEmail", model.Email);
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
        public async Task<IActionResult> MusteriSayfasi(KullaniciDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadKullaniciTuruDropDown();

                if (CurrentKullanici.KullaniciTuruId == 2)
                {
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    LoadKullaniciList();

                    string? secilenKullaniciAdi = HttpContext.Session.GetString("secilenKullaniciAdi");
                    string? secilenEmail = HttpContext.Session.GetString("secilenEmail");

                    string kullaniciadisonuc = null;
                    string emailsonuc = null;

                    if (model.Id == 0)
                    {
                        secilenKullaniciAdi = null;
                        secilenEmail = null;
                    }

                    if (model.KullaniciAdi != secilenKullaniciAdi)
                    {
                        var KullaniciAdiList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciAdiList"));

                        if (KullaniciAdiList != null)
                        {
                            if (KullaniciAdiList.Contains(model.KullaniciAdi))
                            {
                                kullaniciadisonuc = "1";
                            }
                        }
                    }

                    if (model.Email != secilenEmail)
                    {
                        var EmailList = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("KullaniciEmailList"));

                        if (EmailList != null)
                        {
                            if (EmailList.Contains(model.Email))
                            {
                                emailsonuc = "2";
                            }
                        }
                    }

                    if (kullaniciadisonuc == "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullaniciadisonuc != "1" && emailsonuc == "2")
                    {
                        ModelState.AddModelError("Email", "Bu Email daha önce kullanılmış. Lütfen farklı Email deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }
                    else if (kullaniciadisonuc == "1" && emailsonuc != "2")
                    {
                        ModelState.AddModelError("KullaniciAdi", "Bu Kullanıcı Adı daha önce kullanılmış. Lütfen farklı kullanıcı adı deneyin!");
                        model.IsSuccess = true;
                        return View(model);
                    }

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/AddUpdate";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 1);
                        return RedirectToAction("MusteriSayfasi");
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

        public async Task<IActionResult> MusteriSil(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi/Delete";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetInt32("MessageBox", 2);
                    return RedirectToAction("MusteriSayfasi");
                }

                return RedirectToAction("ErrorSayfasi", "Error");
            }
        }
    }
}
