using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RAMApiController : BaseApiController
    {
        IRAMService _RAM;
        public RAMApiController(IRAMService RAM)
        {
            _RAM = RAM;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllRAM()
        {
            return Ok(_RAM.GetAllRAM());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRAMList(string? searchTerm)
        {
            return Ok(_RAM.GetRAMList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRAM(int pId)
        {
            return Ok(_RAM.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] RAMDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_RAM.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] RAMDataModel model)
        {
            return Ok(_RAM.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_RAM.Delete(pId));
        }
    }
}
