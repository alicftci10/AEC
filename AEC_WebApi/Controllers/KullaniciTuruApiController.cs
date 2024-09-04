using AEC_Business.Interfaces;
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
    }
}
