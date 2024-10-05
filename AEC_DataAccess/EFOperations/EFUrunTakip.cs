using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id

                            join lap in db.Laptops on x.LaptopId equals lap.Id into a
                            from laptop in a.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into b
                            from monitor in b.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into c
                            from mouse in c.DefaultIfEmpty()

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
                                UpdatedByName = kul2.Ad + " " + kul2.Soyad,
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

                return List;
            }
        }

        public List<UrunTakipDataModel> TamamlandiList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id

                            join lap in db.Laptops on x.LaptopId equals lap.Id into a
                            from laptop in a.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into b
                            from monitor in b.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into c
                            from mouse in c.DefaultIfEmpty()

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
                                UpdatedByName = kul2.Ad + " " + kul2.Soyad,
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

                return List;
            }
        }

        public List<UrunTakipDataModel> IptalList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id

                            join lap in db.Laptops on x.LaptopId equals lap.Id into a
                            from laptop in a.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into b
                            from monitor in b.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into c
                            from mouse in c.DefaultIfEmpty()

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
                                UpdatedByName = kul2.Ad + " " + kul2.Soyad,
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

                return List;
            }
        }
    }
}
