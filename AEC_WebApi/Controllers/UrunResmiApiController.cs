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
        public IActionResult GetLaptopResmiList(int LaptopId)
        {
            return Ok(_UrunResmi.GetLaptopResmiList(LaptopId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMonitorResmiList(int MonitorId)
        {
            return Ok(_UrunResmi.GetMonitorResmiList(MonitorId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunResmi(int pId)
        {
            return Ok(_UrunResmi.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] UrunResmiDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_UrunResmi.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] UrunResmiDataModel model)
        {
            return Ok(_UrunResmi.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_UrunResmi.Delete(pId));
        }
    }
}
