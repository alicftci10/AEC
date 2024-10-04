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
    public class EFUrunYorum : GenericRepository<UrunYorum>, IEFUrunYorumRepository
    {
        public EFUrunYorum(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<UrunYorumDataModel> GetUrunYorumList(int? pLaptopId, int? pMonitorId, int? pMouseId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var YorumList = db.UrunYorums.ToList();

                if (pLaptopId != null)
                {
                    YorumList = YorumList.Where(i => i.LaptopId == pLaptopId).ToList();
                }
                else if (pMonitorId != null)
                {
                    YorumList = YorumList.Where(i => i.MonitorId == pMonitorId).ToList();
                }
                else if (pMouseId != null)
                {
                    YorumList = YorumList.Where(i => i.MouseId == pMouseId).ToList();
                }

                var List = (from x in YorumList

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join lap in db.Laptops on x.LaptopId equals lap.Id into a
                            from laptop in a.DefaultIfEmpty()

                            join mon in db.Monitors on x.MonitorId equals mon.Id into b
                            from monitor in b.DefaultIfEmpty()

                            join mou in db.Mice on x.MouseId equals mou.Id into c
                            from mouse in c.DefaultIfEmpty()

                            select new UrunYorumDataModel
                            {
                                Id = x.Id,
                                LaptopId = x.LaptopId,
                                MonitorId = x.MonitorId,
                                MouseId = x.MouseId,
                                Yorum = x.Yorum,
                                YorumPuan = x.YorumPuan,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad

                            }).ToList();

                return List;
            }
        }
    }
}
