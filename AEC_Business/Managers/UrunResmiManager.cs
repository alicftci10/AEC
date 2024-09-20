using AEC_Business.Interfaces;
using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AEC_Business.Managers
{
    public class UrunResmiManager : BaseManager, IUrunResmiService
    {
        private readonly IEFUrunResmiRepository _UrunResmiRepository;
        public UrunResmiManager(IEFUrunResmiRepository UrunResmiRepository)
        {
            _UrunResmiRepository = UrunResmiRepository;
        }

        public List<UrunResmiDataModel> GetAllUrunResmi()
        {
            List<UrunResmiDataModel> listUrunResmi = new List<UrunResmiDataModel>();

            List<UrunResmi> list = _UrunResmiRepository.GetAll() as List<UrunResmi>;

            if (list != null)
            {
                foreach (UrunResmi item in list)
                {
                    UrunResmiDataModel model = new UrunResmiDataModel();
                    model.Id = item.Id;
                    model.LaptopId = item.LaptopId;
                    model.MonitorId = item.MonitorId;
                    model.ResimUrl = item.ResimUrl;
                    model.ResimBoyutu = item.ResimBoyutu;
                    model.ResimTuru = item.ResimTuru;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listUrunResmi.Add(model);
                }
            }

            return listUrunResmi;
        }

        public bool AddUpdate(int LaptopId, List<IFormFile> ResimUrl, int CreatedBy)
        {
            string imageFilePath = "Laptop_" + LaptopId;
            string imagePath = ConfigurationInfo.UrunResmiFolderUrl + "\\" + imageFilePath;
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            else
            {
                var deleteList = GetLaptopResmiList(LaptopId);
                foreach (var item in deleteList)
                {
                    Delete(item.Id);
                }
            }

            int index = 1;

            foreach (var file in ResimUrl)
            {
                if (ResimUrl == null || file.Length == 0 || LaptopId <= 0)
                {
                    return false;
                }

                if (file.Length > 0)
                {
                    var filePath = Path.Combine(imagePath, index + Path.GetExtension(imagePath + file.FileName));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    UrunResmiDataModel img = new UrunResmiDataModel();

                    img.LaptopId = LaptopId;
                    img.ResimUrl = imageFilePath + "\\" + index + Path.GetExtension(imagePath + file.FileName);
                    img.ResimBoyutu = file.Length;
                    img.ResimTuru = file.ContentType;
                    img.CreatedBy = CreatedBy;

                    Add(img);

                    index++;
                }
            }

            return true;
        }

        private UrunResmi GetDataModel(UrunResmiDataModel model)
        {
            UrunResmi item = new UrunResmi();

            item.LaptopId = model.LaptopId;
            item.MonitorId = model.MonitorId;
            item.ResimUrl = model.ResimUrl;
            item.ResimBoyutu = model.ResimBoyutu.Value;
            item.ResimTuru = model.ResimTuru;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<UrunResmiDataModel> GetLaptopResmiList(int pLaptopId)
        {
            return _UrunResmiRepository.GetLaptopResmiList(pLaptopId);
        }

        public List<UrunResmiDataModel> GetMonitorResmiList(int pMonitorId)
        {
            return _UrunResmiRepository.GetMonitorResmiList(pMonitorId);
        }

        public UrunResmi GetId(int pId)
        {
            return _UrunResmiRepository.GetSelect(pId);
        }

        public int Add(UrunResmiDataModel item)
        {
            return _UrunResmiRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(UrunResmiDataModel item)
        {
            return _UrunResmiRepository.Update(GetDataModel(item)).Id;
        }

        public UrunResmi Delete(int pId)
        {
            return _UrunResmiRepository.Delete(pId);
        }
    }
}
