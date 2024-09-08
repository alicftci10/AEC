using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KullaniciKartApiController : BaseApiController
    {
        IKullaniciKartService _kullanicikart;
        public KullaniciKartApiController(IKullaniciKartService kullanicikart)
        {
            _kullanicikart = kullanicikart;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetKullaniciKartListesi(int pId)
        {
            return Ok(_kullanicikart.GetKullaniciKartListesi(pId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetKullaniciKart(int pId)
        {
            return Ok(_kullanicikart.GetKullaniciKart(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] KullaniciKartDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_kullanicikart.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] KullaniciKartDataModel model)
        {
            return Ok(_kullanicikart.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_kullanicikart.Delete(pId));
        }
    }
}
