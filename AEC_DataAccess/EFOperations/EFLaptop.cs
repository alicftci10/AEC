using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.DataModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFLaptop : GenericRepository<Laptop>, IEFLaptopRepository
    {
        public EFLaptop(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<LaptopDataModel> GetLaptopList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Laptops

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join isletsis in db.IsletimSistemis on x.IsletimSistemiId equals isletsis.Id

                            join islemci in db.Cpus on x.IslemciId equals islemci.Id

                            join ekran in db.Gpus on x.EkranKartiId equals ekran.Id

                            join bellek in db.Rams on x.BellekId equals bellek.Id

                            join depolama in db.Ssds on x.DepolamaId equals depolama.Id

                            join cozunurluk in db.Cozunurluks on x.CozunurlukId equals cozunurluk.Id

                            join yenhizi in db.YenilemeHizis on x.YenilemeHiziId equals yenhizi.Id

                            select new LaptopDataModel
                            {
                                Id = x.Id,
                                LaptopAdi = x.LaptopAdi,
                                Fiyat = x.Fiyat,
                                IsletimSistemiId = x.IsletimSistemiId,
                                IsletimSistemiIdName = isletsis.IsletimSistemiAdi,
                                IslemciId = x.IslemciId,
                                IslemciIdName = islemci.IslemciAdi,
                                EkranKartiId = x.EkranKartiId,
                                EkranKartiIdName = ekran.EkranKartiAdi,
                                BellekId = x.BellekId,
                                BellekIdName = bellek.BellekAdi,
                                DepolamaId = x.DepolamaId,
                                DepolamaIdName = depolama.DepolamaAdi,
                                CozunurlukId = x.CozunurlukId,
                                CozunurlukIdName = cozunurluk.CozunurlukAdi,
                                YenilemeHiziId = x.YenilemeHiziId,
                                YenilemeHiziIdName = yenhizi.YenilemeHiziAdi,
                                Klavye = x.Klavye,
                                Boyut = x.Boyut,
                                Agirlik = x.Agirlik,
                                Batarya = x.Batarya,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad,
                                ResimUrl = db.UrunResmis.Where(i=>i.LaptopId == x.Id).Select(i => i.ResimUrl).FirstOrDefault()

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.LaptopAdi) && i.LaptopAdi.ToLower().Contains(searchTerm)) ||
                                           (i.Fiyat != null && i.Fiyat.ToString().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }
    }
}
