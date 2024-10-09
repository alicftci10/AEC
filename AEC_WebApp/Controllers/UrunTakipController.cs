using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AEC_WebApp.Controllers
{
    public class UrunTakipController : BaseController
    {
        public UrunTakipController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public void FavoriDurum(int? pLaptopId, int? pMonitorId, int? pMouseId)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetFavoriDurum";

                if (pLaptopId != null)
                {
                    url += $"?pLaptopId={pLaptopId}";
                }
                else if (pMonitorId != null)
                {
                    url += $"?pMonitorId={pMonitorId}";
                }
                else if (pMouseId != null)
                {
                    url += $"?pMouseId={pMouseId}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.Favori == true)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 4);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("MessageBox", 5);
                        }
                    }
                }
            }
        }

        public void SepetDurum(int? pLaptopId, int? pMonitorId, int? pMouseId, int? pAdet)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/GetSepetDurum";

                if (pAdet == null)
                {
                    pAdet = 1;
                }

                if (pLaptopId != null)
                {
                    url += $"?pLaptopId={pLaptopId}&pAdet={pAdet}";
                }
                else if (pMonitorId != null)
                {
                    url += $"?pMonitorId={pMonitorId}&pAdet={pAdet}";
                }
                else if (pMouseId != null)
                {
                    url += $"?pMouseId={pMouseId}&pAdet={pAdet}";
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                UrunTakipDataModel model = new UrunTakipDataModel();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<UrunTakipDataModel>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        if (model.SepetDurum == true)
                        {
                            HttpContext.Session.SetInt32("MessageBox", 6);
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("MessageBox", 7);
                        }
                    }
                }
            }
        }

        public void AdetDurum(int pId,int pAdet)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + $"/api/UrunTakipApi/GetAdetDurum?pId={pId}&pAdet={pAdet}";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                if (text != null)
                {

                }
            }
        }

        public void AllSepeteEkle()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/AllSepeteEkle";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 8);
                    }
                }
            }
        }

        public void AllFavoriDelete()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/AllFavoriDelete";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 9);
                    }
                }
            }
        }

        public void AllSepetDelete()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConfigurationInfo.ApiUrl + "/api/UrunTakipApi/AllSepetDelete";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentKullanici.JwtToken);
                var response = client.GetAsync(url);
                var text = response.Result;

                List<UrunTakipDataModel> model = new List<UrunTakipDataModel>();

                if (text != null)
                {
                    model = JsonConvert.DeserializeObject<List<UrunTakipDataModel>>(text.Content.ReadAsStringAsync().Result);

                    if (model != null)
                    {
                        HttpContext.Session.SetInt32("MessageBox", 10);
                    }
                }
            }
        }
    }
}
