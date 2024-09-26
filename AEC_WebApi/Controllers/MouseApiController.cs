using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MouseApiController : BaseApiController
    {
        IMouseService _Mouse;
        IUrunResmiService _UrunResmi;
        public MouseApiController(IMouseService Mouse, IUrunResmiService urunResmi)
        {
            _Mouse = Mouse;
            _UrunResmi = urunResmi;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllMouse()
        {
            return Ok(_Mouse.GetAllMouse());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMouseList(string? searchTerm)
        {
            return Ok(_Mouse.GetMouseList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMouseId(int pId)
        {
            return Ok(_Mouse.GetMouseId(pId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMouse(int pId)
        {
            return Ok(_Mouse.GetId(pId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] MouseDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                Update(model);
            }
            else
            {
                model.Id = _Mouse.Add(model);
            }

            return Ok(model);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] MouseDataModel model)
        {
            return Ok(_Mouse.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            var deleteList = _UrunResmi.GetMouseResmiList(pId);
            foreach (var item in deleteList)
            {
                _UrunResmi.Delete(item.Id);
            }

            return Ok(_Mouse.Delete(pId));
        }
    }
}
