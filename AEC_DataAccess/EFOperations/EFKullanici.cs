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
    public class EFKullanici : GenericRepository<Kullanici>, IEFKullaniciRepository
    {
        public EFKullanici(AecommerceDbContext AECContext) : base(AECContext) { }

        public Kullanici GetKullanici(KullaniciDataModel model, out string? ErrorMessage)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var kullanici = db.Kullanicis.FirstOrDefault(i => i.Sifre == model.Sifre && i.KullaniciAdi == model.KullaniciAdi || i.Email == model.Email);
                var kullaniciadi = db.Kullanicis.FirstOrDefault(i => i.KullaniciAdi == model.KullaniciAdi || i.Email == model.Email);
                var sifre = db.Kullanicis.FirstOrDefault(i => i.Sifre == model.Sifre);

                if (kullanici != null)
                {
                    ErrorMessage = null;
                    return kullanici;
                }
                else if (kullaniciadi != null && kullaniciadi.Sifre != model.Sifre)
                {
                    ErrorMessage = "1";
                }
                else if (sifre != null && sifre.KullaniciAdi != model.KullaniciAdi)
                {
                    ErrorMessage = "2";
                }
                else
                {
                    ErrorMessage = "3";
                }

                return new Kullanici();
            }
        }

        public List<KullaniciDataModel> PersonelList(string KullaniciAdi, int? KullaniciTuru, string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var searchList = db.Kullanicis.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchList = searchList.Where(i => i.Ad.Contains(searchTerm) ||
                                     i.Soyad.Contains(searchTerm) ||
                                     i.Email.Contains(searchTerm) ||
                                     i.Telefon.Contains(searchTerm));
                }

                var kullaniciList = (from k in searchList
                                     join
                                         kul in db.Kullanicis on k.CreatedBy equals kul.Id

                                     where (k.KullaniciTuruId != 3)

                                     select new KullaniciDataModel
                                     {
                                         Id = k.Id,
                                         Ad = k.Ad,
                                         Soyad = k.Soyad,
                                         KullaniciAdi = k.KullaniciAdi,
                                         Sifre = k.Sifre,
                                         Email = k.Email,
                                         Telefon = k.Telefon,
                                         Adres = k.Adres,
                                         KullaniciTuruId = k.KullaniciTuruId,
                                         KartId = k.KartId,
                                         CreatedAt = k.CreatedAt,
                                         CreatedByName = db.Kullanicis.Where(i => i.Id == k.CreatedBy).Select(i => i.Ad + " " + i.Soyad).FirstOrDefault()

                                     }).ToList();

                if (KullaniciTuru == 2)
                {
                    foreach (var item in kullaniciList)
                    {
                        if (item.KullaniciAdi != KullaniciAdi)
                        {
                            item.Sifre = "****";
                        }
                    }
                }

                return kullaniciList;
            }
        }

        public List<KullaniciDataModel> MusteriList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var kullaniciList = db.Kullanicis.Where(i => i.KullaniciTuruId == 3).Select(i => new KullaniciDataModel
                {
                    Id = i.Id,
                    Ad = i.Ad,
                    Soyad = i.Soyad,
                    KullaniciAdi = i.KullaniciAdi,
                    Sifre = i.Sifre,
                    Email = i.Email,
                    Telefon = i.Telefon,
                    Adres = i.Adres,
                    KullaniciTuruId = i.KullaniciTuruId,
                    KartId = i.KartId,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                return kullaniciList;
            }
        }
    }
}
