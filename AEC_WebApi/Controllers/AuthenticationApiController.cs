using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
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
        public IActionResult Giris([FromBody] KullaniciDataModel model)
        {
            var kullanici = _Kullanici.GetKullanici(model);

            if (kullanici.Id > 0)
            {
                kullanici.JwtToken = GenerateJwtToken(kullanici);
            }

            return Ok(kullanici);
        }
    }
}
