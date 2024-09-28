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
        IHomeService _HomeService;
        ILaptopService _LaptopService;
        IMonitorService _MonitorService;
        IMouseService _MouseService;
        public HomeApiController(IHomeService homeService, ILaptopService laptopService, IMonitorService monitorService, IMouseService mouseService)
        {
            _HomeService = homeService;
            _LaptopService = laptopService;
            _MonitorService = monitorService;
            _MouseService = mouseService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeList()
        {
            HomeDataModel model = new HomeDataModel();

            model.LaptopList = _LaptopService.GetLaptopList();

            model.MonitorList = _MonitorService.GetMonitorList();

            model.MouseList = _MouseService.GetMouseList();

            return Ok(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeSearchList(string searchTerm)
        {
            HomeDataModel model = new HomeDataModel();

            model.LaptopList = _LaptopService.GetLaptopList(searchTerm);

            model.MonitorList = _MonitorService.GetMonitorList(searchTerm);

            model.MouseList = _MouseService.GetMouseList(searchTerm);

            return Ok(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeSearchIdList(int pId)
        {
            HomeDataModel model = new HomeDataModel();

            if (pId == 1)
            {
                model.LaptopList = _LaptopService.GetLaptopList();
            }
            else if (pId == 2)
            {
                model.MonitorList = _MonitorService.GetMonitorList();
            }
            else if (pId == 30)
            {
                model.MouseList = _MouseService.GetMouseList();
            }
            else
            {
                return Ok(_HomeService.GetHomeSearchList(pId));
            }

            return Ok(model);
        }
    }
}
