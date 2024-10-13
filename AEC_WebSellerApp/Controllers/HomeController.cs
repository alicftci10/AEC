using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace AEC_WebSellerApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public IActionResult Index()
        {
            MessageBox();

            HomeDataModel model = new HomeDataModel();

            model.LaptopList = LoadLaptopList();
            model.MonitorList = LoadMonitorList();
            model.MouseList = LoadMouseList();
            model.KullaniciList = LoadKullaniciList();
            model.UrunTakipList = AllUrunTakipList();
            model.UrunResmiList = AllUrunResmiList();
            model.UrunYorumList = AllUrunYorumList();
         
            return View(model);
        }
    }
}
