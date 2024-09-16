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
    public class EFCozunurluk : GenericRepository<Cozunurluk>, IEFCozunurlukRepository
    {
        public EFCozunurluk(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<CozunurlukDataModel> GetCozunurlukList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Cozunurluks

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.CozunurlukId equals k.Id

                            select new CozunurlukDataModel
                            {
                                Id = x.Id,
                                CozunurlukId = x.CozunurlukId,
                                CozunurlukIdName = k.KategoriAdi,
                                CozunurlukAdi = x.CozunurlukAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.CozunurlukIdName) && i.CozunurlukIdName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CozunurlukAdi) && i.CozunurlukAdi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }
    }
}
