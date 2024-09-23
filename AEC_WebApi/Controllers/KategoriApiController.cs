using AEC_Business.Interfaces;
using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KategoriApiController : BaseApiController
    {
        IKategoriService _kategori;
        public KategoriApiController(IKategoriService kategori)
        {
            _kategori = kategori;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllKategori()
        {
            return Ok(_kategori.GetAllKategori());
        }

		[HttpGet]
		[AllowAnonymous]
		public IActionResult GetAllKategoriAllowAnonymous()
		{
			return Ok(_kategori.GetAllKategori());
		}

		[HttpGet]
        [Authorize]
        public IActionResult GetKategoriList(string? searchTerm)
        {
            return Ok(_kategori.GetKategoriList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetKategori(int pId)
        {
            return Ok(_kategori.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] KategoriDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_kategori.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] KategoriDataModel model)
        {
            return Ok(_kategori.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_kategori.Delete(pId));
        }
    }
}
