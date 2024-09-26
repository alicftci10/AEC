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

        public List<UrunTakipDataModel> GetUrunTakipList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.UrunTakips

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join kul2 in db.Kullanicis on x.UpdatedBy equals kul2.Id

                            select new UrunTakipDataModel
                            {
                                Id = x.Id,
                                LaptopId = x.LaptopId,
                                MonitorId = x.MonitorId,
                                MouseId = x.MouseId,
                                Adet = x.Adet,
                                Favori = x.Favori,
                                SepetDurum = x.SepetDurum,
                                SiparisDurum = x.SiparisDurum,
                                UpdatedAt = x.UpdatedAt,
                                UpdatedBy = x.UpdatedBy,
                                UpdatedByName = kul2.Ad + " " + kul2.Soyad,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad

                            }).ToList();

                //if (!string.IsNullOrEmpty(searchTerm))
                //{
                //    searchTerm = searchTerm.ToLower();
                //    List = List.Where(i => (!string.IsNullOrEmpty(i.IslemciSerisiName) && i.IslemciSerisiName.ToLower().Contains(searchTerm)) ||
                //                           (!string.IsNullOrEmpty(i.IslemciMimarisi) && i.IslemciMimarisi.ToLower().Contains(searchTerm)) ||
                //                           (!string.IsNullOrEmpty(i.IslemciAdi) && i.IslemciAdi.ToLower().Contains(searchTerm)) ||
                //                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                //                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                //                           ).ToList();
                //}

                return List;
            }
        }
    }
}
