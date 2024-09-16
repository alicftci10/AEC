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
    public class IsletimSistemiManager : BaseManager, IIsletimSistemiService
    {
        private readonly IEFIsletimSistemiRepository _IsletimSistemiRepository;
        public IsletimSistemiManager(IEFIsletimSistemiRepository IsletimSistemiRepository)
        {
            _IsletimSistemiRepository = IsletimSistemiRepository;
        }

        public List<IsletimSistemiDataModel> GetAllIsletimSistemi()
        {
            List<IsletimSistemiDataModel> listIsletimSistemi = new List<IsletimSistemiDataModel>();

            List<IsletimSistemi> list = _IsletimSistemiRepository.GetAll() as List<IsletimSistemi>;

            if (list != null)
            {
                foreach (IsletimSistemi item in list)
                {
                    IsletimSistemiDataModel model = new IsletimSistemiDataModel();
                    model.Id = item.Id;
                    model.IsletimSistemiId = item.IsletimSistemiId;
                    model.IsletimSistemiAdi = item.IsletimSistemiAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listIsletimSistemi.Add(model);
                }
            }

            return listIsletimSistemi;
        }

        private IsletimSistemi GetDataModel(IsletimSistemiDataModel model)
        {
            IsletimSistemi item = new IsletimSistemi();

            item.IsletimSistemiId = model.IsletimSistemiId.Value;
            item.IsletimSistemiAdi = model.IsletimSistemiAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<IsletimSistemiDataModel> GetIsletimSistemiList(string searchTerm)
        {
            return _IsletimSistemiRepository.GetIsletimSistemiList(searchTerm);
        }

        public IsletimSistemi GetId(int pId)
        {
            return _IsletimSistemiRepository.GetSelect(pId);
        }

        public int Add(IsletimSistemiDataModel item)
        {
            return _IsletimSistemiRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(IsletimSistemiDataModel item)
        {
            return _IsletimSistemiRepository.Update(GetDataModel(item)).Id;
        }

        public IsletimSistemi Delete(int pId)
        {
            return _IsletimSistemiRepository.Delete(pId);
        }
    }
}
