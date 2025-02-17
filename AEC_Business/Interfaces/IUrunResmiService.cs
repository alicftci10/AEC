﻿using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IUrunResmiService
    {
        List<UrunResmiDataModel> GetAllUrunResmi();

        bool AddUpdateLaptop(int LaptopId, List<IFormFile> ResimUrl, int CreatedBy);

        bool AddUpdateMonitor(int MonitorId, List<IFormFile> ResimUrl, int CreatedBy);

        bool AddUpdateMouse(int MouseId, List<IFormFile> ResimUrl, int CreatedBy);

        List<UrunResmiDataModel> GetLaptopResmiList(int pLaptopId);

        List<UrunResmiDataModel> GetMonitorResmiList(int pMonitorId);

        List<UrunResmiDataModel> GetMouseResmiList(int pMouseId);

        UrunResmi GetId(int pId);

        int Add(UrunResmiDataModel item);

        int Update(UrunResmiDataModel item);

        UrunResmi Delete(int pId);
    }
}
