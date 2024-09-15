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
    public class EFGPU : GenericRepository<Gpu>, IEFGPURepository
    {
        public EFGPU(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<GPUDataModel> GetGPUList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Gpus.AsQueryable()

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.EkranKartiSerisiId equals k.Id

                            select new GPUDataModel
                            {
                                Id = x.Id,
                                EkranKartiSerisiId = x.EkranKartiSerisiId,
                                EkranKartiSerisiName = db.Kategoris.Where(i => i.Id == x.EkranKartiSerisiId).Select(i => i.KategoriAdi).FirstOrDefault(),
                                EkranKartiAdi = x.EkranKartiAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = db.Kullanicis.Where(i => i.Id == x.CreatedBy).Select(i => i.Ad + " " + i.Soyad).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.EkranKartiSerisiName) && i.EkranKartiSerisiName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.EkranKartiAdi) && i.EkranKartiAdi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(Convert.ToString(i.CreatedAt)) && Convert.ToString(i.CreatedAt).Contains(searchTerm))).ToList();
                }

                return List;
            }
        }
    }
}
