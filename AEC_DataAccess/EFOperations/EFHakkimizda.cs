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
    public class EFHakkimizda : GenericRepository<Hakkimizdum>, IEFHakkimizdaRepository
    {
        public EFHakkimizda(AecommerceDbContext AECContext) : base(AECContext) { }
    }
}
