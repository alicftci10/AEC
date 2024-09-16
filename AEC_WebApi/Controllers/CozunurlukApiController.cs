using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CozunurlukApiController : BaseApiController
    {
        ICozunurlukService _Cozunurluk;
        public CozunurlukApiController(ICozunurlukService Cozunurluk)
        {
            _Cozunurluk = Cozunurluk;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllCozunurluk()
        {
            return Ok(_Cozunurluk.GetAllCozunurluk());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCozunurlukList(string? searchTerm)
        {
            return Ok(_Cozunurluk.GetCozunurlukList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCozunurluk(int pId)
        {
            return Ok(_Cozunurluk.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] CozunurlukDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_Cozunurluk.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] CozunurlukDataModel model)
        {
            return Ok(_Cozunurluk.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_Cozunurluk.Delete(pId));
        }
    }
}
