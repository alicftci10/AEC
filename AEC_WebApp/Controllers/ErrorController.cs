using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AEC_WebApp.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public IActionResult ErrorSayfasi()
        {
            int? MessageBox = HttpContext.Session.GetInt32("MessageBox");
            if (MessageBox == 4)
            {
                TempData["MessageBox"] = 4;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 5)
            {
                TempData["MessageBox"] = 5;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 6)
            {
                TempData["MessageBox"] = 6;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }
            else if (MessageBox == 7)
            {
                TempData["MessageBox"] = 7;
                HttpContext.Session.SetInt32("MessageBox", 3);
            }

            return View();
        }
    }
}
