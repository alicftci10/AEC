using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IsletimSistemiApiController : BaseApiController
    {
        IIsletimSistemiService _IsletimSistemi;
        public IsletimSistemiApiController(IIsletimSistemiService IsletimSistemi)
        {
            _IsletimSistemi = IsletimSistemi;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllIsletimSistemi()
        {
            return Ok(_IsletimSistemi.GetAllIsletimSistemi());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetIsletimSistemiList(string? searchTerm)
        {
            return Ok(_IsletimSistemi.GetIsletimSistemiList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetIsletimSistemi(int pId)
        {
            return Ok(_IsletimSistemi.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] IsletimSistemiDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_IsletimSistemi.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] IsletimSistemiDataModel model)
        {
            return Ok(_IsletimSistemi.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_IsletimSistemi.Delete(pId));
        }
    }
}
