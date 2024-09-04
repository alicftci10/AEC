using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFKullaniciTuru : GenericRepository<KullaniciTuru>, IEFKullaniciTuruRepository
    {
        public EFKullaniciTuru(AecommerceDbContext AECContext) : base(AECContext) { }


    }
}
