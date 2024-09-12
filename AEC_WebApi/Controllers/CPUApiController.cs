using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CPUApiController : BaseApiController
    {
        ICPUService _CPU;
        public CPUApiController(ICPUService CPU)
        {
            _CPU = CPU;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllCPU()
        {
            return Ok(_CPU.GetAllCPU());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCPUList(string? searchTerm)
        {
            return Ok(_CPU.GetCPUList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCPU(int pId)
        {
            return Ok(_CPU.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] CPUDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_CPU.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] CPUDataModel model)
        {
            return Ok(_CPU.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_CPU.Delete(pId));
        }
    }
}
