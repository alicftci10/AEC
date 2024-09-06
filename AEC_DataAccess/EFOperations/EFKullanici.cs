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

        public Kullanici Giris(KullaniciDataModel model, out string? ErrorMessage)
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

        public List<KullaniciDataModel> PersonelList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var kullaniciList = (from k in db.Kullanicis.AsQueryable()
                                     join
                                         kul in db.Kullanicis on k.CreatedBy equals kul.Id

                                     where (k.KullaniciTuruId != 3)

                                     select new KullaniciDataModel
                                     {
                                         Id = k.Id,
                                         Ad = k.Ad,
                                         Soyad = k.Soyad,
                                         FullName = k.Ad + " " + k.Soyad,
                                         KullaniciAdi = k.KullaniciAdi,
                                         Sifre = k.Sifre,
                                         Email = k.Email,
                                         Telefon = k.Telefon,
                                         Adres = k.Adres,
                                         KullaniciTuruId = k.KullaniciTuruId,
                                         KullaniciTuruName = k.KullaniciTuruId == 1 ? "Admin" : "Personel",
                                         CreatedAt = k.CreatedAt,
                                         CreatedBy = k.CreatedBy,
                                         CreatedByName = db.Kullanicis.Where(i => i.Id == k.CreatedBy).Select(i => i.Ad + " " + i.Soyad).FirstOrDefault()

                                     }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    kullaniciList = kullaniciList.Where(i => i.FullName.ToLower().Contains(searchTerm) ||
                                     i.Email.ToLower().Contains(searchTerm) ||
                                     i.Telefon.Contains(searchTerm) ||
                                     i.CreatedByName.ToLower().Contains(searchTerm) ||
                                    Convert.ToString(i.CreatedAt).Contains(searchTerm) ||
                                    i.KullaniciTuruName.ToLower().Contains(searchTerm)).ToList();
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
                    FullName = i.Ad + " " + i.Soyad,
                    KullaniciAdi = i.KullaniciAdi,
                    Sifre = i.Sifre,
                    Email = i.Email,
                    Telefon = i.Telefon,
                    Adres = i.Adres,
                    KullaniciTuruId = i.KullaniciTuruId,
                    KullaniciTuruName = "Müşteri",
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    kullaniciList = kullaniciList.Where(i => i.FullName.ToLower().Contains(searchTerm) ||
                                     i.Email.ToLower().Contains(searchTerm) ||
                                     i.Telefon.Contains(searchTerm) ||
                                    i.Adres.ToLower().Contains(searchTerm) ||
                                    Convert.ToString(i.CreatedAt).Contains(searchTerm)).ToList();
                }

                return kullaniciList;
            }
        }
    }
}
