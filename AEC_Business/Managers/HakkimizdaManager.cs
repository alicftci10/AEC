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
    public class HakkimizdaManager : BaseManager, IHakkimizdaService
    {
        private readonly IEFHakkimizdaRepository _HakkimizdaRepository;
        public HakkimizdaManager(IEFHakkimizdaRepository HakkimizdaRepository)
        {
            _HakkimizdaRepository = HakkimizdaRepository;
        }

        private Hakkimizdum GetDataModel(HakkimizdaDataModel model)
        {
            Hakkimizdum item = new Hakkimizdum();

            item.Vizyon = model.Vizyon;
            item.Misyon = model.Misyon;
            item.Hikaye = model.Hikaye;
            item.Telefon = model.Telefon;
            item.Adres = model.Adres;
            item.Email = model.Email;
            item.CalismaGunleri = model.CalismaGunleri;
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = model.UpdatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public HakkimizdaDataModel GetHakkimizda()
        {
            HakkimizdaDataModel model = new HakkimizdaDataModel();
            List<Hakkimizdum> list = _HakkimizdaRepository.GetAll() as List<Hakkimizdum>;

            if (list != null && list.Count() > 0)
            {
                Hakkimizdum item = list[0] as Hakkimizdum;

                model.Id = item.Id;
                model.Vizyon = item.Vizyon;
                model.Misyon = item.Misyon;
                model.Hikaye = item.Hikaye;
                model.Telefon = item.Telefon;
                model.Adres = item.Adres;
                model.Email = item.Email;
                model.CalismaGunleri = item.CalismaGunleri;
            }

            return model;
        }

        public int Add(HakkimizdaDataModel item)
        {
            return _HakkimizdaRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(HakkimizdaDataModel item)
        {
            return _HakkimizdaRepository.Update(GetDataModel(item)).Id;
        }

        public Hakkimizdum Delete(int pId)
        {
            return _HakkimizdaRepository.Delete(pId);
        }
    }
}
