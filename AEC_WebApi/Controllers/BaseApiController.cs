using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AEC_WebApi.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        internal string GenerateJwtToken(KullaniciDataModel pModel)
        {
            var key = Encoding.UTF8.GetBytes("UCi9U2H{53(1RePt{Cwc8H9B>5q%rHkS");
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: "AECJWTIssuer",
                    audience: "AECJWTAudience",
                    claims: new List<Claim>
                    {
                        new Claim("KullaniciAdi",pModel.KullaniciAdi),
                        new Claim("AdSoyad", pModel.Ad + " " + pModel.Soyad),
                        new Claim("KullaniciTuru",pModel.KullaniciTuruId.ToString()),
                        new Claim("Id", pModel.Id.ToString())
                    },
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(6)),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return jwtToken;
        }

        internal KullaniciDataModel GetCurrentKullanici(HttpContext context)
        {
            KullaniciDataModel userLogin = new KullaniciDataModel();
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            SecurityToken securityToken = new JwtSecurityTokenHandler().ReadToken(token);

            if (securityToken != null)
            {
                JwtSecurityToken jwt = securityToken as JwtSecurityToken;

                if (jwt != null)
                {
                    var AdSoyad = userLogin.Ad + " " + userLogin.Soyad;

                    userLogin.KullaniciAdi = jwt.Payload["KullaniciAdi"].ToString();
                    AdSoyad = jwt.Payload["AdSoyad"].ToString();
                    userLogin.KullaniciTuruId = Convert.ToInt32(jwt.Payload["KullaniciTuru"]);
                    userLogin.Id = Convert.ToInt32(jwt.Payload["Id"]);
                }
            }

            return userLogin;
        }
    }
}