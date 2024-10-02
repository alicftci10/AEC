using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace AEC_WebApp.Controllers
{
    public class KullaniciController : BaseController
    {
        public KullaniciController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public async Task<IActionResult> KullaniciSayfasi(int? pId)
        {
            using (HttpClient client = new HttpClient())
            {
                KullaniciDataModel model = new KullaniciDataModel();

                if (pId > 0)
                {
                    if (pId != CurrentKullanici.Id)
                    {
                        pId = CurrentKullanici.Id;
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
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciSayfasi(KullaniciDataModel model)
        {
            using (HttpClient client = new HttpClient())
            {
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

                    string url = ConfigurationInfo.ApiUrl + "/api/KullaniciApi";

                    if (model.Id > 0)
                    {
                        url += "/AddUpdate";
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                    }
                    else
                    {
                        url += "/MusteriAddAllowAnonymous";
                    }
                   
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        model.IsSuccess = true;
                        return View(model);
                    }
                }

                return View(model);
            }
        }
    }
}
