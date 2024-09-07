using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AEC_WebSellerApp.Controllers
{
    public class KullaniciKartController : BaseController
    {
        public async Task<IActionResult> KullaniciKartListesi(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKartListesi";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                List<KullaniciKartDataModel> modelList = new List<KullaniciKartDataModel>();

                if (response.IsSuccessStatusCode)
                {
                    modelList = JsonConvert.DeserializeObject<List<KullaniciKartDataModel>>(response.Content.ReadAsStringAsync().Result);
                }

                return PartialView(modelList);
            }
        }

        public async Task<IActionResult> KullaniciKartSayfasi(int pId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/KullaniciKartApi/GetKullaniciKart";

                url += $"?pId={pId}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);

                var response = await client.GetAsync(url);

                KullaniciKartDataModel model = new KullaniciKartDataModel();

                if (response.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<KullaniciKartDataModel>(response.Content.ReadAsStringAsync().Result);
                }

                return View(model);
            }
        }
    }
}
