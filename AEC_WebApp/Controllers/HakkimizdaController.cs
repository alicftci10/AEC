using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AEC_WebApp.Controllers
{
    public class HakkimizdaController : BaseController
    {
        public HakkimizdaController(IMemoryCache memoryCache) { _memoryCacheBase = memoryCache; }

        public IActionResult HakkimizdaSayfasi()
        {
            MessageBox();

            HakkimizdaDataModel model = new HakkimizdaDataModel();

            model = LoadHakkimizdaInfo();

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("ErrorSayfasi", "Error");
        }

        public IActionResult IletisimSayfasi()
        {
            MessageBox();

            HakkimizdaDataModel model = new HakkimizdaDataModel();

            model = LoadHakkimizdaInfo();

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("ErrorSayfasi", "Error");
        }

        public IActionResult GizlilikSayfasi()
        {
            MessageBox();

            return View();
        }

        public IActionResult SiparisIadelerSayfasi()
        {
            MessageBox();

            return View();
        }

        public IActionResult SartlarKosullarSayfasi()
        {
            MessageBox();

            return View();
        }
    }
}
