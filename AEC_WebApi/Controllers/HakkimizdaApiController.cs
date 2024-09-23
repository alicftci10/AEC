using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HakkimizdaApiController : BaseApiController
    {
        IHakkimizdaService _Hakkimizda;
        public HakkimizdaApiController(IHakkimizdaService Hakkimizda)
        {
            _Hakkimizda = Hakkimizda;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetHakkimizda()
        {
            return Ok(_Hakkimizda.GetHakkimizda());
        }

		[HttpGet]
		[AllowAnonymous]
		public IActionResult GetHakkimizdaAllowAnonymous()
		{
			return Ok(_Hakkimizda.GetHakkimizda());
		}

		[HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] HakkimizdaDataModel model)
        {
            model.UpdatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_Hakkimizda.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] HakkimizdaDataModel model)
        {
            return Ok(_Hakkimizda.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_Hakkimizda.Delete(pId));
        }
    }
}
