using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SSDApiController : BaseApiController
    {
        ISSDService _SSD;
        public SSDApiController(ISSDService SSD)
        {
            _SSD = SSD;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllSSD()
        {
            return Ok(_SSD.GetAllSSD());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSSDList(string? searchTerm)
        {
            return Ok(_SSD.GetSSDList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSSD(int pId)
        {
            return Ok(_SSD.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] SSDDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_SSD.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] SSDDataModel model)
        {
            return Ok(_SSD.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_SSD.Delete(pId));
        }
    }
}