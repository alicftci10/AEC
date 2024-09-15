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
    public class EFRAM : GenericRepository<Ram>, IEFRAMRepository
    {
        public EFRAM(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<RAMDataModel> GetRAMList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Rams.AsQueryable()

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.BellekId equals k.Id

                            select new RAMDataModel
                            {
                                Id = x.Id,
                                BellekId = x.BellekId,
                                BellekIdName = db.Kategoris.Where(i => i.Id == x.BellekId).Select(i => i.KategoriAdi).FirstOrDefault(),
                                BellekAdi = x.BellekAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = db.Kullanicis.Where(i => i.Id == x.CreatedBy).Select(i => i.Ad + " " + i.Soyad).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.BellekIdName) && i.BellekIdName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.BellekAdi) && i.BellekAdi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(Convert.ToString(i.CreatedAt)) && Convert.ToString(i.CreatedAt).Contains(searchTerm))).ToList();
                }

                return List;
            }
        }
    }
}
