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
    public class LaptopManager : BaseManager, ILaptopService
    {
        private readonly IEFLaptopRepository _LaptopRepository;
        public LaptopManager(IEFLaptopRepository LaptopRepository)
        {
            _LaptopRepository = LaptopRepository;
        }

        public List<LaptopDataModel> GetAllLaptop()
        {
            List<LaptopDataModel> listLaptop = new List<LaptopDataModel>();

            List<Laptop> list = _LaptopRepository.GetAll() as List<Laptop>;

            if (list != null)
            {
                foreach (Laptop item in list)
                {
                    LaptopDataModel model = new LaptopDataModel();
                    model.Id = item.Id;
                    model.LaptopAdi = item.LaptopAdi;
                    model.Fiyat = item.Fiyat;
                    model.IsletimSistemiId = item.IsletimSistemiId;
                    model.IslemciId = item.IslemciId;
                    model.EkranKartiId = item.EkranKartiId;
                    model.BellekId = item.BellekId;
                    model.DepolamaId = item.DepolamaId;
                    model.CozunurlukId = item.CozunurlukId;
                    model.YenilemeHiziId = item.YenilemeHiziId;
                    model.Klavye = item.Klavye;
                    model.Boyut = item.Boyut;
                    model.Agirlik = item.Agirlik;
                    model.Batarya = item.Batarya;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listLaptop.Add(model);
                }
            }

            return listLaptop;
        }

        private Laptop GetDataModel(LaptopDataModel model)
        {
            Laptop item = new Laptop();

            item.LaptopAdi = model.LaptopAdi;
            item.Fiyat = model.Fiyat.Value;
            item.IsletimSistemiId = model.IsletimSistemiId.Value;
            item.IslemciId = model.IslemciId.Value;
            item.EkranKartiId = model.EkranKartiId.Value;
            item.BellekId = model.BellekId.Value;
            item.DepolamaId = model.DepolamaId.Value;
            item.CozunurlukId = model.CozunurlukId.Value;
            item.YenilemeHiziId = model.YenilemeHiziId.Value;
            item.Klavye = model.Klavye;
            item.Boyut = model.Boyut;
            item.Agirlik = model.Agirlik;
            item.Batarya = model.Batarya;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<LaptopDataModel> GetLaptopList(string searchTerm)
        {
            return _LaptopRepository.GetLaptopList(searchTerm);
        }

        public List<LaptopDataModel> GetLaptopList()
        {
            return _LaptopRepository.GetLaptopList();
        }

        public LaptopDataModel GetLaptopId(int pId)
        {
            return _LaptopRepository.GetLaptopId(pId);
        }

        public Laptop GetId(int pId)
        {
            return _LaptopRepository.GetSelect(pId);
        }

        public int Add(LaptopDataModel item)
        {
            return _LaptopRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(LaptopDataModel item)
        {
            return _LaptopRepository.Update(GetDataModel(item)).Id;
        }

        public Laptop Delete(int pId)
        {
            return _LaptopRepository.Delete(pId);
        }
    }
}
