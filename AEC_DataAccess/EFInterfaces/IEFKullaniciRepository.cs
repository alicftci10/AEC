using AEC_DataAccess.DBModels;
using AEC_DataAccess.GenericRepository.Interfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFInterfaces
{
    public interface IEFKullaniciRepository : IRepository<Kullanici>
    {
        Kullanici GetKullanici(KullaniciDataModel model, out string? ErrorMessage);
    }
}
