using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrunTakipApiController : BaseApiController
    {
        IUrunTakipService _UrunTakip;
        public UrunTakipApiController(IUrunTakipService UrunTakip)
        {
            _UrunTakip = UrunTakip;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUrunTakip()
        {
            return Ok(_UrunTakip.GetAllUrunTakip());
        }

        [HttpGet]
        [Authorize]
        public IActionResult BekliyorList(string? searchTerm)
        {
            return Ok(_UrunTakip.BekliyorList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult TamamlandiList(string? searchTerm)
        {
            return Ok(_UrunTakip.TamamlandiList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult IptalList(string? searchTerm)
        {
            return Ok(_UrunTakip.IptalList(searchTerm));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFavoriList()
        {
            int pKullaniciId = GetCurrentKullanici(HttpContext).Id;

            return Ok(_UrunTakip.GetFavoriList(pKullaniciId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSepetList()
        {
            int pKullaniciId = GetCurrentKullanici(HttpContext).Id;

            return Ok(_UrunTakip.GetSepetList(pKullaniciId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSiparisList()
        {
            int pKullaniciId = GetCurrentKullanici(HttpContext).Id;

            return Ok(_UrunTakip.GetSiparisList(pKullaniciId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUrunTakip(int pId)
        {
            return Ok(_UrunTakip.GetId(pId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllSepeteEkle()
        {
            var list = _UrunTakip.GetFavoriList(GetCurrentKullanici(HttpContext).Id);

            foreach (var item in list)
            {
                _UrunTakip.AllSepeteEkle(item);
            }

            return Ok(list);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAdetDurum(int pId, int pAdet)
        {
            var urun = _UrunTakip.GetDataModel(pId);

            urun.Adet += pAdet;

            if (urun.Adet < 1) urun.Adet = 1;

            return Ok(_UrunTakip.Update(urun));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSiparisDurum()
        {
            var list = _UrunTakip.GetSepetList(GetCurrentKullanici(HttpContext).Id);

            foreach (var item in list)
            {
                item.SepetDurum = false;
                item.UpdatedBy = item.CreatedBy;
                _UrunTakip.Update(item);
                item.Id = 0;
                item.Favori = null;
                item.SepetDurum = null;
                item.SiparisDurum = 1;
                _UrunTakip.Add(item);
            }

            return Ok(list);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSiparisDurumSellerApp(int pSiparisDurum, int pId)
        {
            var urun = _UrunTakip.GetDataModel(pId);

            urun.SiparisDurum = pSiparisDurum;
            urun.UpdatedBy = GetCurrentKullanici(HttpContext).Id;

            return Ok(_UrunTakip.Update(urun));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFavoriDurum(int pLaptopId, int pMonitorId, int pMouseId)
        {
            UrunTakipDataModel model = new UrunTakipDataModel();

            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (pLaptopId > 0)
            {
                model.LaptopId = pLaptopId;
            }
            else if (pMonitorId > 0)
            {
                model.MonitorId = pMonitorId;
            }
            else if (pMouseId > 0)
            {
                model.MouseId = pMouseId;
            }

            return Ok(_UrunTakip.AddFavori(model));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSepetDurum(int pLaptopId, int pMonitorId, int pMouseId, int pAdet)
        {
            UrunTakipDataModel model = new UrunTakipDataModel();

            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;
            model.Adet = pAdet;

            if (pLaptopId > 0)
            {
                model.LaptopId = pLaptopId;
            }
            else if (pMonitorId > 0)
            {
                model.MonitorId = pMonitorId;
            }
            else if (pMouseId > 0)
            {
                model.MouseId = pMouseId;
            }

            return Ok(_UrunTakip.AddSepet(model));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUpdate([FromBody] UrunTakipDataModel model)
        {
            model.CreatedBy = GetCurrentKullanici(HttpContext).Id;

            if (model.Id > 0)
            {
                return Ok(Update(model));
            }

            return Ok(_UrunTakip.Add(model));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] UrunTakipDataModel model)
        {
            return Ok(_UrunTakip.Update(model));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int pId)
        {
            return Ok(_UrunTakip.Delete(pId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllFavoriDelete()
        {
            var list = _UrunTakip.GetFavoriList(GetCurrentKullanici(HttpContext).Id);

            foreach (var item in list)
            {
                _UrunTakip.AddFavori(item);
            }

            return Ok(list);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllSepetDelete()
        {
            var list = _UrunTakip.GetSepetList(GetCurrentKullanici(HttpContext).Id);

            foreach (var item in list)
            {
                _UrunTakip.AllSepetDelete(item);
            }

            return Ok(list);
        }
    }
}
