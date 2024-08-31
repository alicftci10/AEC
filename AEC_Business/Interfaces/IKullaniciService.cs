﻿using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IKullaniciService
    {
        List<KullaniciDataModel> GetAllKullanici();

        KullaniciDataModel GetKullanici(KullaniciDataModel model);

        Kullanici GetId(int pId);

        int Add(KullaniciDataModel item);

        int Update(KullaniciDataModel item);

        Kullanici Delete(int pId);
    }
}
