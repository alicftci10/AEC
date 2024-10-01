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
            int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
            if (MessageBox == 1)
            {
                TempData["MessageBox"] = 1;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }

            return View();
        }
    }
}
