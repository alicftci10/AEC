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
    public class EFCPU : GenericRepository<Cpu>, IEFCPURepository
    {
        public EFCPU(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<CPUDataModel> GetCPUList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Cpus.AsQueryable()

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            select new CPUDataModel
                            {
                                Id = x.Id,
                                IslemciSerisi = x.IslemciSerisi,
                                IslemciMimarisi = x.IslemciMimarisi,
                                IslemciAdi = x.IslemciAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = db.Kullanicis.Where(i => i.Id == x.CreatedBy).Select(i => i.Ad + " " + i.Soyad).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => i.IslemciSerisi.ToLower().Contains(searchTerm) ||
                                    i.IslemciMimarisi.ToLower().Contains(searchTerm) ||
                                    i.IslemciAdi.ToLower().Contains(searchTerm) ||
                                    i.CreatedByName.ToLower().Contains(searchTerm) ||
                                    Convert.ToString(i.CreatedAt).Contains(searchTerm)).ToList();
                }

                return List;
            }
        }
    }
}
