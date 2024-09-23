using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace AEC_WebApp.Controllers
{
    public class HomeController : BaseController
    {
		public HomeController(IMemoryCache IMemoryCache)
		{
			_memoryCacheBase = IMemoryCache;
		}

		public IActionResult Index()
        {
            return View();
        }
    }
}
