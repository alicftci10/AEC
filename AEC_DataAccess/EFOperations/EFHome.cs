using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFHome
    {
        public HomeDataModel GetHomeSearchList(int pId)
        {
            List<LaptopDataModel> laptop = new List<LaptopDataModel>();
            List<MonitorDataModel> monitor = new List<MonitorDataModel>();

            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                HomeDataModel model = new HomeDataModel();

                var islemciList = db.Cpus.Where(i => i.IslemciSerisiId == pId).ToList();

                var ekranKartiList = db.Gpus.Where(i => i.EkranKartiSerisiId == pId).ToList();

                var bellekList = db.Rams.Where(i => i.BellekId == pId).ToList();

                var depolamaList = db.Ssds.Where(i => i.DepolamaId == pId).ToList();

                var yenilemehiziList = db.YenilemeHizis.Where(i => i.YenilemeHiziId == pId).ToList();

                var cozunurlukList = db.Cozunurluks.Where(i => i.CozunurlukId == pId).ToList();

                var isletimsistemiList = db.IsletimSistemis.Where(i => i.IsletimSistemiId == pId).ToList();

                if (islemciList.Count > 0)
                {
                    foreach (var item in islemciList)
                    {
                        laptop.AddRange(IslemciList(item.Id));
                    }
                }
                else if (ekranKartiList.Count > 0)
                {
                    foreach (var item in ekranKartiList)
                    {
                        laptop.AddRange(EkranKartiList(item.Id));
                    }
                }
                else if (bellekList.Count > 0)
                {
                    foreach (var item in bellekList)
                    {
                        laptop.AddRange(BellekList(item.Id));
                    }
                }
                else if (depolamaList.Count > 0)
                {
                    foreach (var item in depolamaList)
                    {
                        laptop.AddRange(DepolamaList(item.Id));
                    }
                }
                else if (yenilemehiziList.Count > 0)
                {
                    foreach (var item in yenilemehiziList)
                    {
                        monitor.AddRange(YenilemeHiziList(item.Id));
                    }
                }
                else if (cozunurlukList.Count > 0)
                {
                    foreach (var item in cozunurlukList)
                    {
                        monitor.AddRange(CozunurlukList(item.Id));
                    }
                }
                else if (isletimsistemiList.Count > 0)
                {
                    foreach (var item in isletimsistemiList)
                    {
                        laptop.AddRange(IsletimSistemiList(item.Id));
                    }
                }

                model.LaptopList = laptop;
                model.MonitorList = monitor;

                return model;
            }
        }

        private List<LaptopDataModel> IslemciList(int pId)
        {
            HomeDataModel model = new HomeDataModel();

            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var laptop = db.Laptops.Where(i => i.IslemciId == pId).Select(i => new LaptopDataModel
                {
                    Id = i.Id,
                    LaptopAdi = i.LaptopAdi,
                    Fiyat = i.Fiyat,
                    IsletimSistemiId = i.IsletimSistemiId,
                    IslemciId = i.IslemciId,
                    EkranKartiId = i.EkranKartiId,
                    BellekId = i.BellekId,
                    DepolamaId = i.DepolamaId,
                    CozunurlukId = i.CozunurlukId,
                    YenilemeHiziId = i.YenilemeHiziId,
                    Klavye = i.Klavye,
                    Boyut = i.Boyut,
                    Agirlik = i.Agirlik,
                    Batarya = i.Batarya,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                foreach (var item in laptop)
                {
                    item.ResimUrl = db.UrunResmis.Where(i => i.LaptopId == item.Id).Select(i => i.ResimUrl).FirstOrDefault();
                }

                if (laptop != null)
                {
                    return laptop;
                }
                else
                {
                    return new List<LaptopDataModel>();
                }
            }
        }

        private List<LaptopDataModel> EkranKartiList(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var laptop = db.Laptops.Where(i => i.EkranKartiId == pId).Select(i => new LaptopDataModel
                {
                    Id = i.Id,
                    LaptopAdi = i.LaptopAdi,
                    Fiyat = i.Fiyat,
                    IsletimSistemiId = i.IsletimSistemiId,
                    IslemciId = i.IslemciId,
                    EkranKartiId = i.EkranKartiId,
                    BellekId = i.BellekId,
                    DepolamaId = i.DepolamaId,
                    CozunurlukId = i.CozunurlukId,
                    YenilemeHiziId = i.YenilemeHiziId,
                    Klavye = i.Klavye,
                    Boyut = i.Boyut,
                    Agirlik = i.Agirlik,
                    Batarya = i.Batarya,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                foreach (var item in laptop)
                {
                    item.ResimUrl = db.UrunResmis.Where(i => i.LaptopId == item.Id).Select(i => i.ResimUrl).FirstOrDefault();
                }

                if (laptop != null)
                {
                    return laptop;
                }
                else
                {
                    return new List<LaptopDataModel>();
                }
            }
        }

        private List<LaptopDataModel> BellekList(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var laptop = db.Laptops.Where(i => i.BellekId == pId).Select(i => new LaptopDataModel
                {
                    Id = i.Id,
                    LaptopAdi = i.LaptopAdi,
                    Fiyat = i.Fiyat,
                    IsletimSistemiId = i.IsletimSistemiId,
                    IslemciId = i.IslemciId,
                    EkranKartiId = i.EkranKartiId,
                    BellekId = i.BellekId,
                    DepolamaId = i.DepolamaId,
                    CozunurlukId = i.CozunurlukId,
                    YenilemeHiziId = i.YenilemeHiziId,
                    Klavye = i.Klavye,
                    Boyut = i.Boyut,
                    Agirlik = i.Agirlik,
                    Batarya = i.Batarya,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                foreach (var item in laptop)
                {
                    item.ResimUrl = db.UrunResmis.Where(i => i.LaptopId == item.Id).Select(i => i.ResimUrl).FirstOrDefault();
                }

                if (laptop != null)
                {
                    return laptop;
                }
                else
                {
                    return new List<LaptopDataModel>();
                }
            }
        }

        private List<LaptopDataModel> DepolamaList(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var laptop = db.Laptops.Where(i => i.DepolamaId == pId).Select(i => new LaptopDataModel
                {
                    Id = i.Id,
                    LaptopAdi = i.LaptopAdi,
                    Fiyat = i.Fiyat,
                    IsletimSistemiId = i.IsletimSistemiId,
                    IslemciId = i.IslemciId,
                    EkranKartiId = i.EkranKartiId,
                    BellekId = i.BellekId,
                    DepolamaId = i.DepolamaId,
                    CozunurlukId = i.CozunurlukId,
                    YenilemeHiziId = i.YenilemeHiziId,
                    Klavye = i.Klavye,
                    Boyut = i.Boyut,
                    Agirlik = i.Agirlik,
                    Batarya = i.Batarya,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                foreach (var item in laptop)
                {
                    item.ResimUrl = db.UrunResmis.Where(i => i.LaptopId == item.Id).Select(i => i.ResimUrl).FirstOrDefault();
                }

                if (laptop != null)
                {
                    return laptop;
                }
                else
                {
                    return new List<LaptopDataModel>();
                }
            }
        }

        private List<MonitorDataModel> YenilemeHiziList(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var monitor = db.Monitors.Where(i => i.YenilemeHiziId == pId).Select(i => new MonitorDataModel
                {
                    Id = i.Id,
                    MonitorAdi = i.MonitorAdi,
                    Fiyat = i.Fiyat,
                    CozunurlukId = i.CozunurlukId,
                    YenilemeHiziId = i.YenilemeHiziId,
                    EkranYapisi = i.EkranYapisi,
                    Hdr = i.Hdr,
                    Boyut = i.Boyut,
                    Agirlik = i.Agirlik,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                foreach (var item in monitor)
                {
                    item.ResimUrl = db.UrunResmis.Where(i => i.MonitorId == item.Id).Select(i => i.ResimUrl).FirstOrDefault();
                }

                if (monitor != null)
                {
                    return monitor;
                }
                else
                {
                    return new List<MonitorDataModel>();
                }
            }
        }

        private List<MonitorDataModel> CozunurlukList(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var monitor = db.Monitors.Where(i => i.CozunurlukId == pId).Select(i => new MonitorDataModel
                {
                    Id = i.Id,
                    MonitorAdi = i.MonitorAdi,
                    Fiyat = i.Fiyat,
                    CozunurlukId = i.CozunurlukId,
                    YenilemeHiziId = i.YenilemeHiziId,
                    EkranYapisi = i.EkranYapisi,
                    Hdr = i.Hdr,
                    Boyut = i.Boyut,
                    Agirlik = i.Agirlik,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                foreach (var item in monitor)
                {
                    item.ResimUrl = db.UrunResmis.Where(i => i.MonitorId == item.Id).Select(i => i.ResimUrl).FirstOrDefault();
                }

                if (monitor != null)
                {
                    return monitor;
                }
                else
                {
                    return new List<MonitorDataModel>();
                }
            }
        }

        private List<LaptopDataModel> IsletimSistemiList(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var laptop = db.Laptops.Where(i => i.IsletimSistemiId == pId).Select(i => new LaptopDataModel
                {
                    Id = i.Id,
                    LaptopAdi = i.LaptopAdi,
                    Fiyat = i.Fiyat,
                    IsletimSistemiId = i.IsletimSistemiId,
                    IslemciId = i.IslemciId,
                    EkranKartiId = i.EkranKartiId,
                    BellekId = i.BellekId,
                    DepolamaId = i.DepolamaId,
                    CozunurlukId = i.CozunurlukId,
                    YenilemeHiziId = i.YenilemeHiziId,
                    Klavye = i.Klavye,
                    Boyut = i.Boyut,
                    Agirlik = i.Agirlik,
                    Batarya = i.Batarya,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                foreach (var item in laptop)
                {
                    item.ResimUrl = db.UrunResmis.Where(i => i.LaptopId == item.Id).Select(i => i.ResimUrl).FirstOrDefault();
                }

                if (laptop != null)
                {
                    return laptop;
                }
                else
                {
                    return new List<LaptopDataModel>();
                }
            }
        }
    }
}