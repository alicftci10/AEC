using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationApiController : BaseApiController
    {
        private readonly IKullaniciService _Kullanici;
        public AuthenticationApiController(IKullaniciService Kullanici)
        {
            _Kullanici = Kullanici;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PersonelGiris([FromBody] KullaniciDataModel model)
        {
            var kullanici = _Kullanici.PersonelGiris(model);

            if (kullanici.Id > 0)
            {
                kullanici.JwtToken = GenerateJwtToken(kullanici);
            }

            return Ok(kullanici);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Giris([FromBody] KullaniciDataModel model)
        {
            var kullanici = _Kullanici.Giris(model);

            if (kullanici.Id > 0)
            {
                kullanici.JwtToken = GenerateJwtToken(kullanici);
            }

            return Ok(kullanici);
        }
    }
}
