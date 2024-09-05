using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KullaniciApiController : BaseApiController
    {
        IKullaniciService _kullanici;
        public KullaniciApiController(IKullaniciService kullanici)
        {
            _kullanici = kullanici;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllKullanici()
        {
            return Ok(_kullanici.GetAllKullanici());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetListPersonel(string? searchTerm)
        {
            return Ok(_kullanici.GetPersonelList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetListMusteri(string? searchTerm)
        {
            return Ok(_kullanici.GetMusteriList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetKullanici(int pId)
        {
            return Ok(_kullanici.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] KullaniciDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_kullanici.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] KullaniciDataModel model)
        {
            return Ok(_kullanici.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_kullanici.Delete(pId));
        }
    }
}