using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrunResmiApiController : BaseApiController
    {
        IUrunResmiService _UrunResmi;
        public UrunResmiApiController(IUrunResmiService UrunResmi)
        {
            _UrunResmi = UrunResmi;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUrunResmi()
        {
            return Ok(_UrunResmi.GetAllUrunResmi());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetLaptopResmiList(int pLaptopId)
        {
            return Ok(_UrunResmi.GetLaptopResmiList(pLaptopId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMonitorResmiList(int pMonitorId)
        {
            return Ok(_UrunResmi.GetMonitorResmiList(pMonitorId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunResmi(int pId)
        {
            return Ok(_UrunResmi.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdateLaptop([FromForm] int LaptopId, [FromForm] List<IFormFile> ResimUrl)
        {
            return Ok(_UrunResmi.AddUpdateLaptop(LaptopId, ResimUrl, GetCurrentKullanici(HttpContext).Id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdateMonitor([FromForm] int MonitorId, [FromForm] List<IFormFile> ResimUrl)
        {
            return Ok(_UrunResmi.AddUpdateMonitor(MonitorId, ResimUrl, GetCurrentKullanici(HttpContext).Id));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_UrunResmi.Delete(pId));
        }
    }
}
