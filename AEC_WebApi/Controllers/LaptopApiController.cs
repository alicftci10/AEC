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
        IUrunResmiService _UrunResmi;
        public LaptopApiController(ILaptopService Laptop, IUrunResmiService urunResmi)
        {
            _Laptop = Laptop;
            _UrunResmi = urunResmi;
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
        public IActionResult GetLaptopId(int pId)
        {
            return Ok(_Laptop.GetLaptopId(pId));
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
                Update(model);
            }
            else
            {
                model.Id = _Laptop.Add(model);
            }

            return Ok(model);
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
            var deleteList = _UrunResmi.GetLaptopResmiList(pId);
            foreach (var item in deleteList)
            {
                _UrunResmi.Delete(item.Id);
            }

            return Ok(_Laptop.Delete(pId));
        }
    }
}