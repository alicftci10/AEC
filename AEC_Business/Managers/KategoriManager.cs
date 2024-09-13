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
    public class KategoriManager : BaseManager, IKategoriService
    {
        private readonly IEFKategoriRepository _KategoriRepository;
        public KategoriManager(IEFKategoriRepository KategoriRepository)
        {
            _KategoriRepository = KategoriRepository;
        }

        public List<KategoriDataModel> GetAllKategori()
        {
            List<KategoriDataModel> listKategori = new List<KategoriDataModel>();

            List<Kategori> list = _KategoriRepository.GetAll() as List<Kategori>;

            if (list != null)
            {
                foreach (Kategori item in list)
                {
                    KategoriDataModel model = new KategoriDataModel();
                    model.Id = item.Id;
                    model.KategoriAdi = item.KategoriAdi;
                    model.MainKategoriId = item.MainKategoriId;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listKategori.Add(model);
                }
            }

            return listKategori;
        }

        private Kategori GetDataModel(KategoriDataModel model)
        {
            Kategori item = new Kategori();

            item.KategoriAdi = model.KategoriAdi;
            item.MainKategoriId = model.MainKategoriId;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<KategoriDataModel> GetKategoriList(string searchTerm)
        {
            return _KategoriRepository.GetKategoriList(searchTerm);
        }

        public Kategori GetId(int pId)
        {
            return _KategoriRepository.GetSelect(pId);
        }

        public int Add(KategoriDataModel item)
        {
            return _KategoriRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(KategoriDataModel item)
        {
            return _KategoriRepository.Update(GetDataModel(item)).Id;
        }

        public Kategori Delete(int pId)
        {
            return _KategoriRepository.Delete(pId);
        }
    }
}
