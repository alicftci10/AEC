using AEC_Business.Interfaces;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;

namespace AEC_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeApiController : BaseApiController
    {
        IHomeService _HomeService;
        IKategoriService _KategoriService;
        ILaptopService _LaptopService;
        IMonitorService _MonitorService;
        IMouseService _MouseService;
        IUrunResmiService _UrunResmiService;
        IUrunYorumService _UrunYorumService;
        IUrunTakipService _UrunTakipService;
        public HomeApiController(IHomeService homeService, IKategoriService kategoriService,
                                 ILaptopService laptopService, IMonitorService monitorService,
                                 IMouseService mouseService, IUrunResmiService urunResmiService,
                                 IUrunYorumService urunYorumService, IUrunTakipService urunTakipService)
        {
            _HomeService = homeService;
            _KategoriService = kategoriService;
            _LaptopService = laptopService;
            _MonitorService = monitorService;
            _MouseService = mouseService;
            _UrunResmiService = urunResmiService;
            _UrunYorumService = urunYorumService;
            _UrunTakipService = urunTakipService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeList()
        {
            HomeDataModel model = new HomeDataModel();

            model.LaptopList = _LaptopService.GetLaptopList();

            model.MonitorList = _MonitorService.GetMonitorList();

            model.MouseList = _MouseService.GetMouseList();

            model.UrunTakipList = _UrunTakipService.GetAllUrunTakip();

            model.KategoriIdName = "Ali E-Commerce / TÜM ÜRÜNLER";

            return Ok(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeSearchList(string searchTerm)
        {
            HomeDataModel model = new HomeDataModel();

            model.LaptopList = _LaptopService.GetLaptopList(searchTerm);

            model.MonitorList = _MonitorService.GetMonitorList(searchTerm);

            model.MouseList = _MouseService.GetMouseList(searchTerm);

            model.UrunTakipList = _UrunTakipService.GetAllUrunTakip();

            model.KategoriIdName = "Ali E-Commerce / ARAMA SONUÇLARI";

            return Ok(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetHomeSearchIdList(int pId)
        {
            HomeDataModel model = new HomeDataModel();

            if (pId == 1)
            {
                model.LaptopList = _LaptopService.GetLaptopList();
            }
            else if (pId == 2)
            {
                model.MonitorList = _MonitorService.GetMonitorList();
            }
            else if (pId == 30)
            {
                model.MouseList = _MouseService.GetMouseList();
            }
            else
            {
                model = _HomeService.GetHomeSearchList(pId);
            }

            GetKategoriName(pId, model);

            model.UrunTakipList = _UrunTakipService.GetAllUrunTakip();

            return Ok(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUrunDetay(int pLaptopId, int pMonitorId, int pMouseId)
        {
            HomeDataModel model = new HomeDataModel();

            if (pLaptopId > 0)
            {
                model.GetLaptop = _LaptopService.GetLaptopId(pLaptopId);
                model.UrunResmiList = _UrunResmiService.GetLaptopResmiList(pLaptopId);
                model.UrunYorumList = _UrunYorumService.GetUrunYorumLaptopList(pLaptopId);
            }
            else if (pMonitorId > 0)
            {
                model.GetMonitor = _MonitorService.GetMonitorId(pMonitorId);
                model.UrunResmiList = _UrunResmiService.GetMonitorResmiList(pMonitorId);
                model.UrunYorumList = _UrunYorumService.GetUrunYorumMonitorList(pMonitorId);
            }
            else if (pMouseId > 0)
            {
                model.GetMouse = _MouseService.GetMouseId(pMouseId);
                model.UrunResmiList = _UrunResmiService.GetMouseResmiList(pMouseId);
                model.UrunYorumList = _UrunYorumService.GetUrunYorumMouseList(pMouseId);
            }

            return Ok(model);
        }

        private void GetKategoriName(int pId, HomeDataModel model)
        {
            var kategoriList = _KategoriService.GetKategoriList();

            foreach (var item in kategoriList)
            {
                if (item.Id == pId)
                {
                    if (item.AltKategori != null)
                    {
                        model.KategoriIdName = "Ali E-Commerce / " + item.UstKategori + " / " + item.OrtaKategori + " / " + item.AltKategori;
                    }
                    else
                    {
                        model.KategoriIdName = "Ali E-Commerce / " + item.UstKategori;
                    }
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetKategoriCount([FromBody] List<KategoriDataModel> kategoriList)
        {
            foreach (var item in kategoriList)
            {
                var countList = _HomeService.GetHomeSearchList(item.Id);

                if (countList.LaptopList != null && countList.LaptopList.Count > 0)
                {
                    item.Count = countList.LaptopList.Count;
                }
                else if (countList.MonitorList != null && countList.MonitorList.Count > 0)
                {
                    item.Count = countList.MonitorList.Count;
                }
                else
                {
                    item.Count = 0;
                }
            }

            return Ok(kategoriList);
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

            return Ok(_UrunTakipService.AddFavori(model));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSepetDurum(int pLaptopId, int pMonitorId, int pMouseId,int pAdet)
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

            return Ok(_UrunTakipService.AddSepet(model));
        }
    }
}
