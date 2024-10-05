using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AEC_WebApp.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public IActionResult ErrorSayfasi()
        {
            return View();
        }
    }
}
