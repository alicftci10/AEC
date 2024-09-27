using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrunTakipApiController : BaseApiController
    {
        IUrunTakipService _UrunTakip;
        public UrunTakipApiController(IUrunTakipService UrunTakip)
        {
            _UrunTakip = UrunTakip;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUrunTakip()
        {
            return Ok(_UrunTakip.GetAllUrunTakip());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunTakipList(string? searchTerm)
        {
            return Ok(_UrunTakip.GetUrunTakipList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunTakip(int pId)
        {
            return Ok(_UrunTakip.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] UrunTakipDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_UrunTakip.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] UrunTakipDataModel model)
        {
            return Ok(_UrunTakip.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_UrunTakip.Delete(pId));
        }
    }
}
