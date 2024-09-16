using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YenilemeHiziApiController : BaseApiController
    {
        IYenilemeHiziService _YenilemeHizi;
        public YenilemeHiziApiController(IYenilemeHiziService YenilemeHizi)
        {
            _YenilemeHizi = YenilemeHizi;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllYenilemeHizi()
        {
            return Ok(_YenilemeHizi.GetAllYenilemeHizi());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetYenilemeHiziList(string? searchTerm)
        {
            return Ok(_YenilemeHizi.GetYenilemeHiziList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetYenilemeHizi(int pId)
        {
            return Ok(_YenilemeHizi.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] YenilemeHiziDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_YenilemeHizi.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] YenilemeHiziDataModel model)
        {
            return Ok(_YenilemeHizi.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_YenilemeHizi.Delete(pId));
        }
    }
}
