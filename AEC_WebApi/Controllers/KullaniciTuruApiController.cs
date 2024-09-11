using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KullaniciTuruApiController : BaseApiController
    {
        IKullaniciTuruService _kullanicituru;
        public KullaniciTuruApiController(IKullaniciTuruService kullanicituru)
        {
            _kullanicituru = kullanicituru;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllKullaniciTuru()
        {
            return Ok(_kullanicituru.GetAllKullaniciTuru());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetKullaniciTuruList(string? searchTerm)
        {
            return Ok(_kullanicituru.GetKullaniciTuruList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetKullaniciTuru(int pId)
        {
            return Ok(_kullanicituru.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] KullaniciTuruDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_kullanicituru.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] KullaniciTuruDataModel model)
        {
            return Ok(_kullanicituru.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_kullanicituru.Delete(pId));
        }
    }
}
