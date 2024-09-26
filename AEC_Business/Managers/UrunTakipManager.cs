using AEC_Business.Interfaces;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Managers
{
    public class UrunTakipManager : BaseManager, IUrunTakipService
    {
        private readonly IEFUrunTakipRepository _UrunTakipRepository;
        public UrunTakipManager(IEFUrunTakipRepository UrunTakipRepository)
        {
            _UrunTakipRepository = UrunTakipRepository;
        }

        public List<UrunTakipDataModel> GetAllUrunTakip()
        {
            List<UrunTakipDataModel> listUrunTakip = new List<UrunTakipDataModel>();

            List<UrunTakip> list = _UrunTakipRepository.GetAll() as List<UrunTakip>;

            if (list != null)
            {
                foreach (UrunTakip item in list)
                {
                    UrunTakipDataModel model = new UrunTakipDataModel();
                    model.Id = item.Id;
                    model.LaptopId = item.LaptopId;
                    model.MonitorId = item.MonitorId;
                    model.MouseId = item.MouseId;
                    model.Adet = item.Adet;
                    model.Favori = item.Favori;
                    model.SepetDurum = item.SepetDurum;
                    model.SiparisDurum = item.SiparisDurum;
                    model.UpdatedAt = item.UpdatedAt;
                    model.UpdatedBy = item.UpdatedBy;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listUrunTakip.Add(model);
                }
            }

            return listUrunTakip;
        }

        private UrunTakip GetDataModel(UrunTakipDataModel model)
        {
            UrunTakip item = new UrunTakip();

            item.LaptopId = model.LaptopId;
            item.MonitorId = model.MonitorId;
            item.MouseId = model.MouseId;
            item.Adet = model.Adet;
            item.Favori = model.Favori;
            item.SepetDurum = model.SepetDurum;
            item.SiparisDurum = model.SiparisDurum;
            
            if (model.Id > 0)
            {
                item.Id = model.Id;
                item.UpdatedAt = DateTime.Now;
                item.UpdatedBy = model.UpdatedBy;
            }
            else
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedBy = model.CreatedBy.Value;
            }

            return item;
        }

        public List<UrunTakipDataModel> GetUrunTakipList(string searchTerm)
        {
            return _UrunTakipRepository.GetUrunTakipList(searchTerm);
        }

        public UrunTakip GetId(int pId)
        {
            return _UrunTakipRepository.GetSelect(pId);
        }

        public int Add(UrunTakipDataModel item)
        {
            return _UrunTakipRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(UrunTakipDataModel item)
        {
            return _UrunTakipRepository.Update(GetDataModel(item)).Id;
        }

        public UrunTakip Delete(int pId)
        {
            return _UrunTakipRepository.Delete(pId);
        }
    }
}
