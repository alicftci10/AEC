using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GPUApiController : BaseApiController
    {
        IGPUService _GPU;
        public GPUApiController(IGPUService GPU)
        {
            _GPU = GPU;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllGPU()
        {
            return Ok(_GPU.GetAllGPU());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetGPUList(string? searchTerm)
        {
            return Ok(_GPU.GetGPUList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetGPU(int pId)
        {
            return Ok(_GPU.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] GPUDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_GPU.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] GPUDataModel model)
        {
            return Ok(_GPU.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_GPU.Delete(pId));
        }
    }
}
