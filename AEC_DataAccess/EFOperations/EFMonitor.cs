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
using Monitor = AEC_DataAccess.DBModels.Monitor;

namespace AEC_DataAccess.EFOperations
{
    public class EFMonitor : GenericRepository<Monitor>, IEFMonitorRepository
    {
        public EFMonitor(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<MonitorDataModel> GetMonitorList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Monitors

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join cozunurluk in db.Cozunurluks on x.CozunurlukId equals cozunurluk.Id

                            join yenhizi in db.YenilemeHizis on x.YenilemeHiziId equals yenhizi.Id

                            select new MonitorDataModel
                            {
                                Id = x.Id,
                                MonitorAdi = x.MonitorAdi,
                                Fiyat = x.Fiyat,
                                CozunurlukId = x.CozunurlukId,
                                CozunurlukIdName = cozunurluk.CozunurlukAdi,
                                YenilemeHiziId = x.YenilemeHiziId,
                                YenilemeHiziIdName = yenhizi.YenilemeHiziAdi,
                                EkranYapisi = x.EkranYapisi,
                                Hdr = x.Hdr,
                                Boyut = x.Boyut,
                                Agirlik = x.Agirlik,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => i.MonitorId == x.Id).Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.MonitorAdi) && i.MonitorAdi.ToLower().Contains(searchTerm)) ||
                                           (i.Fiyat != null && i.Fiyat.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CozunurlukIdName) && i.CozunurlukIdName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.YenilemeHiziIdName) && i.YenilemeHiziIdName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }

        public List<MonitorDataModel> GetMonitorList()
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Monitors

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join cozunurluk in db.Cozunurluks on x.CozunurlukId equals cozunurluk.Id

                            join yenhizi in db.YenilemeHizis on x.YenilemeHiziId equals yenhizi.Id

                            select new MonitorDataModel
                            {
                                Id = x.Id,
                                MonitorAdi = x.MonitorAdi,
                                Fiyat = x.Fiyat,
                                CozunurlukId = x.CozunurlukId,
                                CozunurlukIdName = cozunurluk.CozunurlukAdi,
                                YenilemeHiziId = x.YenilemeHiziId,
                                YenilemeHiziIdName = yenhizi.YenilemeHiziAdi,
                                EkranYapisi = x.EkranYapisi,
                                Hdr = x.Hdr,
                                Boyut = x.Boyut,
                                Agirlik = x.Agirlik,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => i.MonitorId == x.Id).Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                return List;
            }
        }

        public MonitorDataModel GetMonitorId(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var monitor = (from x in db.Monitors

                              join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                              join cozunurluk in db.Cozunurluks on x.CozunurlukId equals cozunurluk.Id

                              join yenhizi in db.YenilemeHizis on x.YenilemeHiziId equals yenhizi.Id

                              where x.Id == pId

                              select new MonitorDataModel
                              {
                                  Id = x.Id,
                                  MonitorAdi = x.MonitorAdi,
                                  Fiyat = x.Fiyat,
                                  CozunurlukId = x.CozunurlukId,
                                  CozunurlukIdName = cozunurluk.CozunurlukAdi,
                                  YenilemeHiziId = x.YenilemeHiziId,
                                  YenilemeHiziIdName = yenhizi.YenilemeHiziAdi,
                                  EkranYapisi = x.EkranYapisi,
                                  Hdr = x.Hdr,
                                  Boyut = x.Boyut,
                                  Agirlik = x.Agirlik,
                                  CreatedAt = x.CreatedAt,
                                  CreatedBy = x.CreatedBy,
                                  CreatedByName = kul.Ad + " " + kul.Soyad,
                                  ResimUrl = db.UrunResmis.Where(i => i.MonitorId == x.Id).Select(i => i.ResimUrl).FirstOrDefault()

                              }).FirstOrDefault();

                return monitor;
            }
        }
    }
}
