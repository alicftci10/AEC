using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrunTakipApiController : BaseApiController
    {
        IUrunTakipService _UrunTakip;
        ILaptopService _LaptopService;
        IMonitorService _MonitorService;
        IMouseService _MouseService;
        public UrunTakipApiController(IUrunTakipService UrunTakip, ILaptopService laptopService, IMonitorService monitorService, IMouseService mouseService)
        {
            _UrunTakip = UrunTakip;
            _LaptopService = laptopService;
            _MonitorService = monitorService;
            _MouseService = mouseService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUrunTakip()
        {
            return Ok(_UrunTakip.GetAllUrunTakip());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunTakipList(string? searchTerm)
        {
            return Ok(_UrunTakip.GetUrunTakipList(searchTerm));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUrunlerList(string? searchTerm)
        {
            UrunTakipDataModel model = new UrunTakipDataModel();

            var laptopList = _LaptopService.GetLaptopList(searchTerm);

            var monitorList = _MonitorService.GetMonitorList(searchTerm);

            var mouseList = _MouseService.GetMouseList(searchTerm);

            model.LaptopList = laptopList;

            model.MonitorList = monitorList;

            model.MouseList = mouseList;

            return Ok(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunTakip(int pId)
        {
            return Ok(_UrunTakip.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] UrunTakipDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_UrunTakip.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] UrunTakipDataModel model)
        {
            return Ok(_UrunTakip.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_UrunTakip.Delete(pId));
        }
    }
}
