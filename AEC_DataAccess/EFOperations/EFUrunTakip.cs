using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFUrunTakip : GenericRepository<UrunTakip>, IEFUrunTakipRepository
    {
        public EFUrunTakip(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<UrunTakipDataModel> BekliyorList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id into a
                            from kullanici in a.DefaultIfEmpty()

                            join lap in db.Laptops on x.LaptopId equals lap.Id into b
                            from laptop in b.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into c
                            from monitor in c.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into d
                            from mouse in d.DefaultIfEmpty()

                            where x.SiparisDurum == 1

                            select new UrunTakipDataModel
                            {
                                Id = x.Id,
                                LaptopId = x.LaptopId,
                                MonitorId = x.MonitorId,
                                MouseId = x.MouseId,
                                UrunAdi = x.LaptopId == null ? (x.MonitorId == null ? mouse.MouseAdi : monitor.MonitorAdi) : laptop.LaptopAdi,
                                Fiyat = x.LaptopId == null ? (x.MonitorId == null ? mouse.Fiyat * x.Adet : monitor.Fiyat * x.Adet) : laptop.Fiyat * x.Adet,
                                Adet = x.Adet,
                                Favori = x.Favori,
                                SepetDurum = x.SepetDurum,
                                SiparisDurum = x.SiparisDurum,
                                UpdatedAt = x.UpdatedAt,
                                UpdatedBy = x.UpdatedBy,
                                UpdatedByName = kullanici.Ad + " " + kullanici.Soyad,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => (x.LaptopId == null ? (x.MonitorId == null ? i.MouseId : i.MonitorId) : i.LaptopId) ==
                                                                    (x.LaptopId == null ? (x.MonitorId == null ? mouse.Id : monitor.Id) : laptop.Id))
                                                                    .Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (i.Id.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.UrunAdi) && i.UrunAdi.ToLower().Contains(searchTerm)) ||
                                           (i.Adet.HasValue && i.Adet.ToString().Contains(searchTerm)) ||
                                           (i.Fiyat.HasValue && i.Fiyat.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                List = List.OrderByDescending(i => i.UpdatedAt ?? i.CreatedAt).ToList();

                return List;
            }
        }

        public List<UrunTakipDataModel> TamamlandiList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id into a
                            from kullanici in a.DefaultIfEmpty()

                            join lap in db.Laptops on x.LaptopId equals lap.Id into b
                            from laptop in b.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into c
                            from monitor in c.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into d
                            from mouse in d.DefaultIfEmpty()

                            where x.SiparisDurum == 2

                            select new UrunTakipDataModel
                            {
                                Id = x.Id,
                                LaptopId = x.LaptopId,
                                MonitorId = x.MonitorId,
                                MouseId = x.MouseId,
                                UrunAdi = x.LaptopId == null ? (x.MonitorId == null ? mouse.MouseAdi : monitor.MonitorAdi) : laptop.LaptopAdi,
                                Fiyat = x.LaptopId == null ? (x.MonitorId == null ? mouse.Fiyat * x.Adet : monitor.Fiyat * x.Adet) : laptop.Fiyat * x.Adet,
                                Adet = x.Adet,
                                Favori = x.Favori,
                                SepetDurum = x.SepetDurum,
                                SiparisDurum = x.SiparisDurum,
                                UpdatedAt = x.UpdatedAt,
                                UpdatedBy = x.UpdatedBy,
                                UpdatedByName = kullanici.Ad + " " + kullanici.Soyad,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => (x.LaptopId == null ? (x.MonitorId == null ? i.MouseId : i.MonitorId) : i.LaptopId) ==
                                                                    (x.LaptopId == null ? (x.MonitorId == null ? mouse.Id : monitor.Id) : laptop.Id))
                                                                    .Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (i.Id.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.UrunAdi) && i.UrunAdi.ToLower().Contains(searchTerm)) ||
                                           (i.Adet.HasValue && i.Adet.ToString().Contains(searchTerm)) ||
                                           (i.Fiyat.HasValue && i.Fiyat.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                List = List.OrderByDescending(i => i.UpdatedAt ?? i.CreatedAt).ToList();

                return List;
            }
        }

        public List<UrunTakipDataModel> IptalList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id into a
                            from kullanici in a.DefaultIfEmpty()

                            join lap in db.Laptops on x.LaptopId equals lap.Id into b
                            from laptop in b.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into c
                            from monitor in c.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into d
                            from mouse in d.DefaultIfEmpty()

                            where x.SiparisDurum == 3

                            select new UrunTakipDataModel
                            {
                                Id = x.Id,
                                LaptopId = x.LaptopId,
                                MonitorId = x.MonitorId,
                                MouseId = x.MouseId,
                                UrunAdi = x.LaptopId == null ? (x.MonitorId == null ? mouse.MouseAdi : monitor.MonitorAdi) : laptop.LaptopAdi,
                                Fiyat = x.LaptopId == null ? (x.MonitorId == null ? mouse.Fiyat * x.Adet : monitor.Fiyat * x.Adet) : laptop.Fiyat * x.Adet,
                                Adet = x.Adet,
                                Favori = x.Favori,
                                SepetDurum = x.SepetDurum,
                                SiparisDurum = x.SiparisDurum,
                                UpdatedAt = x.UpdatedAt,
                                UpdatedBy = x.UpdatedBy,
                                UpdatedByName = kullanici.Ad + " " + kullanici.Soyad,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => (x.LaptopId == null ? (x.MonitorId == null ? i.MouseId : i.MonitorId) : i.LaptopId) ==
                                                                    (x.LaptopId == null ? (x.MonitorId == null ? mouse.Id : monitor.Id) : laptop.Id))
                                                                    .Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (i.Id.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.UrunAdi) && i.UrunAdi.ToLower().Contains(searchTerm)) ||
                                           (i.Adet.HasValue && i.Adet.ToString().Contains(searchTerm)) ||
                                           (i.Fiyat.HasValue && i.Fiyat.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                List = List.OrderByDescending(i => i.UpdatedAt ?? i.CreatedAt).ToList();

                return List;
            }
        }

        public List<UrunTakipDataModel> GetFavoriList(int pKullaniciId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id into a
                            from kullanici in a.DefaultIfEmpty()

                            join lap in db.Laptops on x.LaptopId equals lap.Id into b
                            from laptop in b.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into c
                            from monitor in c.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into d
                            from mouse in d.DefaultIfEmpty()

                            where x.Favori == true && x.CreatedBy == pKullaniciId

                            select new UrunTakipDataModel
                            {
                                Id = x.Id,
                                LaptopId = x.LaptopId,
                                MonitorId = x.MonitorId,
                                MouseId = x.MouseId,
                                UrunAdi = x.LaptopId == null ? (x.MonitorId == null ? mouse.MouseAdi : monitor.MonitorAdi) : laptop.LaptopAdi,
                                Fiyat = x.LaptopId == null ? (x.MonitorId == null ? mouse.Fiyat : monitor.Fiyat) : laptop.Fiyat,
                                Adet = x.Adet,
                                Favori = x.Favori,
                                SepetDurum = x.SepetDurum,
                                SiparisDurum = x.SiparisDurum,
                                UpdatedAt = x.UpdatedAt,
                                UpdatedBy = x.UpdatedBy,
                                UpdatedByName = kullanici.Ad + " " + kullanici.Soyad,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => (x.LaptopId == null ? (x.MonitorId == null ? i.MouseId : i.MonitorId) : i.LaptopId) ==
                                                                    (x.LaptopId == null ? (x.MonitorId == null ? mouse.Id : monitor.Id) : laptop.Id))
                                                                    .Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                List = List.OrderBy(i => i.UpdatedAt ?? i.CreatedAt).ToList();

                return List;
            }
        }

        public List<UrunTakipDataModel> GetSepetList(int pKullaniciId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id into a
                            from kullanici in a.DefaultIfEmpty()

                            join lap in db.Laptops on x.LaptopId equals lap.Id into b
                            from laptop in b.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into c
                            from monitor in c.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into d
                            from mouse in d.DefaultIfEmpty()

                            where x.SepetDurum == true && x.CreatedBy == pKullaniciId

                            select new UrunTakipDataModel
                            {
                                Id = x.Id,
                                LaptopId = x.LaptopId,
                                MonitorId = x.MonitorId,
                                MouseId = x.MouseId,
                                UrunAdi = x.LaptopId == null ? (x.MonitorId == null ? mouse.MouseAdi : monitor.MonitorAdi) : laptop.LaptopAdi,
                                Fiyat = x.LaptopId == null ? (x.MonitorId == null ? mouse.Fiyat * x.Adet : monitor.Fiyat * x.Adet) : laptop.Fiyat * x.Adet,
                                Adet = x.Adet,
                                Favori = x.Favori,
                                SepetDurum = x.SepetDurum,
                                SiparisDurum = x.SiparisDurum,
                                UpdatedAt = x.UpdatedAt,
                                UpdatedBy = x.UpdatedBy,
                                UpdatedByName = kullanici.Ad + " " + kullanici.Soyad,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => (x.LaptopId == null ? (x.MonitorId == null ? i.MouseId : i.MonitorId) : i.LaptopId) ==
                                                                    (x.LaptopId == null ? (x.MonitorId == null ? mouse.Id : monitor.Id) : laptop.Id))
                                                                    .Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                List = List.OrderBy(i => i.UrunAdi).ToList();

                return List;
            }
        }

        public UrunTakipDataModel GetSepetFavoriDurum(UrunTakipDataModel model)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                if (model.LaptopId != null)
                {
                    var laptop = db.UrunTakips.Where(i => i.CreatedBy == model.CreatedBy && i.LaptopId == model.LaptopId && i.SiparisDurum == null)

                        .Select(i => new UrunTakipDataModel
                        {
                            Id = i.Id,
                            LaptopId = i.LaptopId,
                            MonitorId = i.MonitorId,
                            MouseId = i.MouseId,
                            Adet = i.Adet,
                            Favori = i.Favori,
                            SepetDurum = i.SepetDurum,
                            SiparisDurum = i.SiparisDurum,
                            UpdatedAt = i.UpdatedAt,
                            UpdatedBy = i.UpdatedBy,
                            CreatedAt = i.CreatedAt,
                            CreatedBy = i.CreatedBy

                        }).FirstOrDefault();

                    if (laptop != null)
                    {
                        return laptop;
                    }
                    else
                    {
                        return new UrunTakipDataModel();
                    }
                }
                else if (model.MonitorId != null)
                {
                    var monitor = db.UrunTakips.Where(i => i.CreatedBy == model.CreatedBy && i.MonitorId == model.MonitorId && i.SiparisDurum == null)

                        .Select(i => new UrunTakipDataModel
                        {
                            Id = i.Id,
                            LaptopId = i.LaptopId,
                            MonitorId = i.MonitorId,
                            MouseId = i.MouseId,
                            Adet = i.Adet,
                            Favori = i.Favori,
                            SepetDurum = i.SepetDurum,
                            SiparisDurum = i.SiparisDurum,
                            UpdatedAt = i.UpdatedAt,
                            UpdatedBy = i.UpdatedBy,
                            CreatedAt = i.CreatedAt,
                            CreatedBy = i.CreatedBy

                        }).FirstOrDefault();

                    if (monitor != null)
                    {
                        return monitor;
                    }
                    else
                    {
                        return new UrunTakipDataModel();
                    }
                }
                else if (model.MouseId != null)
                {
                    var mouse = db.UrunTakips.Where(i => i.CreatedBy == model.CreatedBy && i.MouseId == model.MouseId && i.SiparisDurum == null)

                        .Select(i => new UrunTakipDataModel
                        {
                            Id = i.Id,
                            LaptopId = i.LaptopId,
                            MonitorId = i.MonitorId,
                            MouseId = i.MouseId,
                            Adet = i.Adet,
                            Favori = i.Favori,
                            SepetDurum = i.SepetDurum,
                            SiparisDurum = i.SiparisDurum,
                            UpdatedAt = i.UpdatedAt,
                            UpdatedBy = i.UpdatedBy,
                            CreatedAt = i.CreatedAt,
                            CreatedBy = i.CreatedBy

                        }).FirstOrDefault();

                    if (mouse != null)
                    {
                        return mouse;
                    }
                    else
                    {
                        return new UrunTakipDataModel();
                    }
                }
                else
                {
                    return new UrunTakipDataModel();
                }
            }
        }

        public UrunTakipDataModel GetDataModel(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var urun = db.UrunTakips.Select(i=> new UrunTakipDataModel
                {
                    Id = i.Id,
                    LaptopId = i.LaptopId,
                    MonitorId = i.MonitorId,
                    MouseId = i.MouseId,
                    Adet = i.Adet,
                    Favori = i.Favori,
                    SepetDurum = i.SepetDurum,
                    SiparisDurum = i.SiparisDurum,
                    UpdatedAt = i.UpdatedAt,
                    UpdatedBy = i.UpdatedBy,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).FirstOrDefault(i => i.Id == pId);

                if (urun != null)
                {
                    return urun;
                }
                else
                {
                    return new UrunTakipDataModel();
                }
                
            }
        }
    }
}
