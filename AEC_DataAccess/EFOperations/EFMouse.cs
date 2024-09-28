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
    public class EFMouse : GenericRepository<Mouse>, IEFMouseRepository
    {
        public EFMouse(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<MouseDataModel> GetMouseList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Mice

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            select new MouseDataModel
                            {
                                Id = x.Id,
                                MouseAdi = x.MouseAdi,
                                Fiyat = x.Fiyat,
                                Renk = x.Renk,
                                Dpi = x.Dpi,
                                TusSayisi = x.TusSayisi,
                                BaglantiOzellikleri = x.BaglantiOzellikleri,
                                Boyut = x.Boyut,
                                Agirlik = x.Agirlik,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => i.MouseId == x.Id).Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.MouseAdi) && i.MouseAdi.ToLower().Contains(searchTerm)) ||
                                           (i.Fiyat != null && i.Fiyat.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.Renk) && i.Renk.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }

        public List<MouseDataModel> GetMouseList()
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Mice

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            select new MouseDataModel
                            {
                                Id = x.Id,
                                MouseAdi = x.MouseAdi,
                                Fiyat = x.Fiyat,
                                Renk = x.Renk,
                                Dpi = x.Dpi,
                                TusSayisi = x.TusSayisi,
                                BaglantiOzellikleri = x.BaglantiOzellikleri,
                                Boyut = x.Boyut,
                                Agirlik = x.Agirlik,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i => i.MouseId == x.Id).Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                return List;
            }
        }

        public MouseDataModel GetMouseId(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var mouse = (from x in db.Mice

                              join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                              where x.Id == pId

                              select new MouseDataModel
                              {
                                  Id = x.Id,
                                  MouseAdi = x.MouseAdi,
                                  Fiyat = x.Fiyat,
                                  Renk = x.Renk,
                                  Dpi = x.Dpi,
                                  TusSayisi = x.TusSayisi,
                                  BaglantiOzellikleri = x.BaglantiOzellikleri,
                                  Boyut = x.Boyut,
                                  Agirlik = x.Agirlik,
                                  CreatedAt = x.CreatedAt,
                                  CreatedBy = x.CreatedBy,
                                  CreatedByName = kul.Ad + " " + kul.Soyad,
                                  ResimUrl = db.UrunResmis.Where(i => i.MouseId == x.Id).Select(i => i.ResimUrl).FirstOrDefault()

                              }).FirstOrDefault();

                return mouse;
            }
        }
    }
}
