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
    public class EFSSD : GenericRepository<Ssd>, IEFSSDRepository
    {
        public EFSSD(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<SSDDataModel> GetSSDList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Ssds

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.DepolamaId equals k.Id

                            select new SSDDataModel
                            {
                                Id = x.Id,
                                DepolamaId = x.DepolamaId,
                                DepolamaIdName = k.KategoriAdi,
                                DepolamaAdi = x.DepolamaAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.DepolamaIdName) && i.DepolamaIdName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.DepolamaAdi) && i.DepolamaAdi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }
    }
}