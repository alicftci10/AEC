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
    public class EFUrunResmi : GenericRepository<UrunResmi>, IEFUrunResmiRepository
    {
        public EFUrunResmi(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<UrunResmiDataModel> GetLaptopResmiList(int pLaptopId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = db.UrunResmis.Where(i => i.LaptopId == pLaptopId)
                    .Select(x => new UrunResmiDataModel
                    {
                        Id = x.Id,
                        LaptopId = x.LaptopId,
                        ResimUrl = x.ResimUrl,
                        ResimBoyutu = x.ResimBoyutu,
                        ResimTuru = x.ResimTuru,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy

                    }).ToList();

                return List;
            }
        }

        public List<UrunResmiDataModel> GetMonitorResmiList(int pMonitorId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = db.UrunResmis.Where(i => i.MonitorId == pMonitorId)
                    .Select(x => new UrunResmiDataModel
                    {
                        Id = x.Id,
                        MonitorId = x.MonitorId,
                        ResimUrl = x.ResimUrl,
                        ResimBoyutu = x.ResimBoyutu,
                        ResimTuru = x.ResimTuru,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy

                    }).ToList();

                return List;
            }
        }

        public List<UrunResmiDataModel> GetMouseResmiList(int pMouseId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = db.UrunResmis.Where(i => i.MouseId == pMouseId)
                    .Select(x => new UrunResmiDataModel
                    {
                        Id = x.Id,
                        MouseId = x.MouseId,
                        ResimUrl = x.ResimUrl,
                        ResimBoyutu = x.ResimBoyutu,
                        ResimTuru = x.ResimTuru,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy

                    }).ToList();

                return List;
            }
        }
    }
}
