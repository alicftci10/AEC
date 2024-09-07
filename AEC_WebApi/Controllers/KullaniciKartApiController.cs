using AEC_Business.Interfaces;
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
    }
}
