using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeApiController : BaseApiController
    {
        ILaptopService _LaptopService;
        IMonitorService _MonitorService;
        IMouseService _MouseService;
        public HomeApiController(ILaptopService laptopService, IMonitorService monitorService, IMouseService mouseService)
        {
            _LaptopService = laptopService;
            _MonitorService = monitorService;
            _MouseService = mouseService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeList(string? searchTerm)
        {
            HomeDataModel model = new HomeDataModel();

            var laptopList = _LaptopService.GetLaptopList(searchTerm);

            var monitorList = _MonitorService.GetMonitorList(searchTerm);

            var mouseList = _MouseService.GetMouseList(searchTerm);

            model.LaptopList = laptopList;

            model.MonitorList = monitorList;

            model.MouseList = mouseList;

            return Ok(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeSearchList(int? pId)
        {
            HomeDataModel model = new HomeDataModel();

            var laptopList = _LaptopService.GetLaptopList(searchTerm);

            var monitorList = _MonitorService.GetMonitorList(searchTerm);

            var mouseList = _MouseService.GetMouseList(searchTerm);

            model.LaptopList = laptopList;

            model.MonitorList = monitorList;

            model.MouseList = mouseList;

            return Ok(model);
        }
    }
}
