using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFKategori : GenericRepository<Kategori>, IEFKategoriRepository
    {
        public EFKategori(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<KategoriDataModel> GetKategoriList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Kategoris.AsQueryable()

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.MainKategoriId equals k.Id into a
                            from KategoriTable in a.DefaultIfEmpty()

                            select new KategoriDataModel
                            {
                                Id = x.Id,
                                KategoriAdi = x.KategoriAdi,
                                MainKategoriId = x.MainKategoriId,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = db.Kullanicis.Where(i => i.Id == x.CreatedBy).Select(i => i.Ad + " " + i.Soyad).FirstOrDefault()

                            }).ToList();

                List<KategoriDataModel> ustList = List.Where(i => i.MainKategoriId == null).ToList();

                foreach (var item in ustList)
                {
                    item.UstKategori = item.KategoriAdi;

                    List<KategoriDataModel> ortalist = List.Where(i => i.MainKategoriId == item.Id).ToList();
                    foreach (var item2 in ortalist)
                    {
                        item2.UstKategori = item.UstKategori;
                        item2.OrtaKategori = item2.KategoriAdi;

                        List<KategoriDataModel> altlist = List.Where(i => i.MainKategoriId == item2.Id).ToList();
                        foreach (var item3 in altlist)
                        {
                            item3.UstKategori = item.UstKategori;
                            item3.OrtaKategori = item2.OrtaKategori;
                            item3.AltKategori = item3.KategoriAdi;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.UstKategori) && i.UstKategori.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.OrtaKategori) && i.OrtaKategori.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.AltKategori) && i.AltKategori.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(Convert.ToString(i.CreatedAt)) && Convert.ToString(i.CreatedAt).Contains(searchTerm))).ToList();
                }

                return List;
            }
        }
    }
}
