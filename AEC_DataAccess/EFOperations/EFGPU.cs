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
                var List = (from x in db.Gpus

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.EkranKartiSerisiId equals k.Id

                            select new GPUDataModel
                            {
                                Id = x.Id,
                                EkranKartiSerisiId = x.EkranKartiSerisiId,
                                EkranKartiSerisiName = k.KategoriAdi,
                                EkranKartiAdi = x.EkranKartiAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.EkranKartiSerisiName) && i.EkranKartiSerisiName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.EkranKartiAdi) && i.EkranKartiAdi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }
    }
}
