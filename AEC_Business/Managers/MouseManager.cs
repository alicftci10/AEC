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
    public class MouseManager : BaseManager, IMouseService
    {
        private readonly IEFMouseRepository _MouseRepository;
        public MouseManager(IEFMouseRepository MouseRepository)
        {
            _MouseRepository = MouseRepository;
        }

        public List<MouseDataModel> GetAllMouse()
        {
            List<MouseDataModel> listMouse = new List<MouseDataModel>();

            List<Mouse> list = _MouseRepository.GetAll() as List<Mouse>;

            if (list != null)
            {
                foreach (Mouse item in list)
                {
                    MouseDataModel model = new MouseDataModel();
                    model.Id = item.Id;
                    model.MouseAdi = item.MouseAdi;
                    model.Fiyat = item.Fiyat;
                    model.Renk = item.Renk;
                    model.Dpi = item.Dpi;
                    model.TusSayisi = item.TusSayisi;
                    model.BaglantiOzellikleri = item.BaglantiOzellikleri;
                    model.Boyut = item.Boyut;
                    model.Agirlik = item.Agirlik;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listMouse.Add(model);
                }
            }

            return listMouse;
        }

        private Mouse GetDataModel(MouseDataModel model)
        {
            Mouse item = new Mouse();

            item.MouseAdi = model.MouseAdi;
            item.Fiyat = model.Fiyat.Value;
            item.Renk = model.Renk;
            item.Dpi = model.Dpi;
            item.TusSayisi = model.TusSayisi;
            item.BaglantiOzellikleri = model.BaglantiOzellikleri;
            item.Boyut = model.Boyut;
            item.Agirlik = model.Agirlik;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<MouseDataModel> GetMouseList(string searchTerm)
        {
            return _MouseRepository.GetMouseList(searchTerm);
        }

        public List<MouseDataModel> GetMouseList()
        {
            return _MouseRepository.GetMouseList();
        }

        public MouseDataModel GetMouseId(int pId)
        {
            return _MouseRepository.GetMouseId(pId);
        }

        public Mouse GetId(int pId)
        {
            return _MouseRepository.GetSelect(pId);
        }

        public int Add(MouseDataModel item)
        {
            return _MouseRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(MouseDataModel item)
        {
            return _MouseRepository.Update(GetDataModel(item)).Id;
        }

        public Mouse Delete(int pId)
        {
            return _MouseRepository.Delete(pId);
        }
    }
}
