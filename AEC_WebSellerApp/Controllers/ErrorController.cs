using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AEC_WebSellerApp.Controllers
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
