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
    public class EFIsletimSistemi : GenericRepository<IsletimSistemi>, IEFIsletimSistemiRepository
    {
        public EFIsletimSistemi(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<IsletimSistemiDataModel> GetIsletimSistemiList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.IsletimSistemis

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.IsletimSistemiId equals k.Id

                            select new IsletimSistemiDataModel
                            {
                                Id = x.Id,
                                IsletimSistemiId = x.IsletimSistemiId,
                                IsletimSistemiIdName = k.KategoriAdi,
                                IsletimSistemiAdi = x.IsletimSistemiAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.IsletimSistemiIdName) && i.IsletimSistemiIdName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.IsletimSistemiAdi) && i.IsletimSistemiAdi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }
    }
}
