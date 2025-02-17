﻿using AEC_Business.Interfaces;
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
                item.CreatedAt = model.CreatedAt.Value;
                item.CreatedBy = model.CreatedBy.Value;
            }
            else
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedBy = model.CreatedBy.Value;
            }

            return item;
        }

        public List<UrunTakipDataModel> BekliyorList(string searchTerm)
        {
            return _UrunTakipRepository.BekliyorList(searchTerm);
        }

        public List<UrunTakipDataModel> TamamlandiList(string searchTerm)
        {
            return _UrunTakipRepository.TamamlandiList(searchTerm);
        }

        public List<UrunTakipDataModel> IptalList(string searchTerm)
        {
            return _UrunTakipRepository.IptalList(searchTerm);
        }

        public List<UrunTakipDataModel> GetFavoriList(int pKullaniciId)
        {
            return _UrunTakipRepository.GetFavoriList(pKullaniciId);
        }

        public List<UrunTakipDataModel> GetSepetList(int pKullaniciId)
        {
            return _UrunTakipRepository.GetSepetList(pKullaniciId);
        }

        public List<UrunTakipDataModel> GetSiparisList(int pKullaniciId)
        {
            return _UrunTakipRepository.GetSiparisList(pKullaniciId);
        }

        public UrunTakipDataModel GetDataModel(int pId)
        {
            return _UrunTakipRepository.GetDataModel(pId);
        }

        public UrunTakip GetId(int pId)
        {
            return _UrunTakipRepository.GetSelect(pId);
        }

        public UrunTakip AllSepeteEkle(UrunTakipDataModel item)
        {
            var sepetDurumId = _UrunTakipRepository.GetSepetFavoriDurum(item);

            if (sepetDurumId.SepetDurum == true)
            {
                sepetDurumId.Adet++;
            }
            else
            {
                sepetDurumId.Adet = 1;
                sepetDurumId.SepetDurum = true;
                sepetDurumId.UpdatedBy = item.CreatedBy;
            }

            return _UrunTakipRepository.Update(GetDataModel(sepetDurumId));
        }

        public UrunTakip AddFavori(UrunTakipDataModel item)
        {
            var favoriDurumId = _UrunTakipRepository.GetSepetFavoriDurum(item);

            if (favoriDurumId.Id > 0)
            {
                if (favoriDurumId.Favori == true)
                {
                    favoriDurumId.Favori = false;
                }
                else
                {
                    favoriDurumId.Favori = true;
                }
                
                favoriDurumId.UpdatedBy = item.CreatedBy;
                return _UrunTakipRepository.Update(GetDataModel(favoriDurumId));
            }
            else
            {
                item.Favori = true;
                return _UrunTakipRepository.Add(GetDataModel(item));
            }
        }

        public UrunTakip AddSepet(UrunTakipDataModel item)
        {
            var sepetDurumId = _UrunTakipRepository.GetSepetFavoriDurum(item);

            if (sepetDurumId.Id > 0)
            {
                if (sepetDurumId.SepetDurum == true)
                {
                    sepetDurumId.SepetDurum = false;
                    sepetDurumId.Adet = 1;
                }
                else
                {
                    sepetDurumId.SepetDurum = true;
                    sepetDurumId.Adet = item.Adet;
                }

                sepetDurumId.UpdatedBy = item.CreatedBy;
                return _UrunTakipRepository.Update(GetDataModel(sepetDurumId));
            }
            else
            {
                item.SepetDurum = true;
                return _UrunTakipRepository.Add(GetDataModel(item));
            }
        }

        public int Add(UrunTakipDataModel item)
        {
            return _UrunTakipRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(UrunTakipDataModel item)
        {
            return _UrunTakipRepository.Update(GetDataModel(item)).Id;
        }

        public UrunTakip AllSepetDelete(UrunTakipDataModel item)
        {
            var sepetDurumId = _UrunTakipRepository.GetSepetFavoriDurum(item);

            sepetDurumId.Adet = 1;
            sepetDurumId.SepetDurum = false;
            sepetDurumId.UpdatedBy = item.CreatedBy;

            return _UrunTakipRepository.Update(GetDataModel(sepetDurumId));
        }

        public UrunTakip Delete(int pId)
        {
            return _UrunTakipRepository.Delete(pId);
        }  
    }
}
