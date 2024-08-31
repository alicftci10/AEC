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
    }
}
