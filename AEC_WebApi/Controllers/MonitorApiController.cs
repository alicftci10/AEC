using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MonitorApiController : BaseApiController
    {
        IMonitorService _Monitor;
        IUrunResmiService _UrunResmi;
        public MonitorApiController(IMonitorService Monitor, IUrunResmiService urunResmi)
        {
            _Monitor = Monitor;
            _UrunResmi = urunResmi;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllMonitor()
        {
            return Ok(_Monitor.GetAllMonitor());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMonitorList(string? searchTerm)
        {
            return Ok(_Monitor.GetMonitorList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMonitorId(int pId)
        {
            return Ok(_Monitor.GetMonitorId(pId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMonitor(int pId)
        {
            return Ok(_Monitor.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] MonitorDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                Update(model);
            }
            else
            {
                model.Id = _Monitor.Add(model);
            }

            return Ok(model);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] MonitorDataModel model)
        {
            return Ok(_Monitor.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            var deleteList = _UrunResmi.GetMonitorResmiList(pId);
            foreach (var item in deleteList)
            {
                _UrunResmi.Delete(item.Id);
            }

            return Ok(_Monitor.Delete(pId));
        }
    }
}