using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrunYorumApiController : BaseApiController
    {
        IUrunYorumService _UrunYorum;
        public UrunYorumApiController(IUrunYorumService UrunYorum)
        {
            _UrunYorum = UrunYorum;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllUrunYorum()
        {
            return Ok(_UrunYorum.GetAllUrunYorum());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUrunYorumLaptopList(int pLaptopId)
        {
            return Ok(_UrunYorum.GetUrunYorumLaptopList(pLaptopId));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUrunYorumMonitorList(int pMonitorId)
        {
            return Ok(_UrunYorum.GetUrunYorumMonitorList(pMonitorId));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUrunYorumMouseList(int pMouseId)
        {
            return Ok(_UrunYorum.GetUrunYorumMouseList(pMouseId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunYorum(int pId)
        {
            return Ok(_UrunYorum.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] UrunYorumDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_UrunYorum.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] UrunYorumDataModel model)
        {
            return Ok(_UrunYorum.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_UrunYorum.Delete(pId));
        }
    }
}
