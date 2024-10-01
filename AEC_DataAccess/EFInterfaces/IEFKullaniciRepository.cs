﻿using AEC_DataAccess.DBModels;
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
        Kullanici PersonelGiris(KullaniciDataModel model, out string? ErrorMessage);

        Kullanici Giris(KullaniciDataModel model, out string? ErrorMessage);

        List<KullaniciDataModel> PersonelList(string searchTerm);

        List<KullaniciDataModel> MusteriList(string searchTerm);
    }
}
