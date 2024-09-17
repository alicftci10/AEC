using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LaptopApiController : BaseApiController
    {
        ILaptopService _Laptop;
        public LaptopApiController(ILaptopService Laptop)
        {
            _Laptop = Laptop;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllLaptop()
        {
            return Ok(_Laptop.GetAllLaptop());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetLaptopList(string? searchTerm)
        {
            return Ok(_Laptop.GetLaptopList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetLaptop(int pId)
        {
            return Ok(_Laptop.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] LaptopDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_Laptop.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] LaptopDataModel model)
        {
            return Ok(_Laptop.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_Laptop.Delete(pId));
        }
    }
}