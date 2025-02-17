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
    public class KullaniciManager : BaseManager, IKullaniciService
    {
        private readonly IEFKullaniciRepository _KullaniciRepository;
        public KullaniciManager(IEFKullaniciRepository KullaniciRepository)
        {
            _KullaniciRepository = KullaniciRepository;
        }

        public List<KullaniciDataModel> GetAllKullanici()
        {
            List<KullaniciDataModel> listKullanici = new List<KullaniciDataModel>();

            List<Kullanici> list = _KullaniciRepository.GetAll() as List<Kullanici>;

            if (list != null)
            {
                foreach (Kullanici kul in list)
                {
                    KullaniciDataModel model = new KullaniciDataModel();
                    model.Id = kul.Id;
                    model.Ad = kul.Ad;
                    model.Soyad = kul.Soyad;
                    model.KullaniciAdi = kul.KullaniciAdi;
                    model.Sifre = kul.Sifre;
                    model.Email = kul.Email;
                    model.Telefon = kul.Telefon;
                    model.Adres = kul.Adres;
                    model.KullaniciTuruId = kul.KullaniciTuruId;
                    model.CreatedAt = kul.CreatedAt;
                    model.CreatedBy = kul.CreatedBy;

                    listKullanici.Add(model);
                }
            }

            return listKullanici;
        }

        public KullaniciDataModel PersonelGiris(KullaniciDataModel model)
        {
            Kullanici kul = _KullaniciRepository.PersonelGiris(model, out string? ErrorMessage);

            model.Id = kul.Id;
            model.Ad = kul.Ad;
            model.Soyad = kul.Soyad;
            model.KullaniciAdi = kul.KullaniciAdi;
            model.Sifre = kul.Sifre;
            model.Email = kul.Email;
            model.Telefon = kul.Telefon;
            model.Adres = kul.Adres;
            model.KullaniciTuruId = kul.KullaniciTuruId;
            model.CreatedAt = kul.CreatedAt;
            model.CreatedBy = kul.CreatedBy;
            model.ErrorMessage = ErrorMessage;

            return model;
        }

        public KullaniciDataModel Giris(KullaniciDataModel model)
        {
            Kullanici kul = _KullaniciRepository.Giris(model, out string? ErrorMessage);

            model.Id = kul.Id;
            model.Ad = kul.Ad;
            model.Soyad = kul.Soyad;
            model.KullaniciAdi = kul.KullaniciAdi;
            model.Sifre = kul.Sifre;
            model.Email = kul.Email;
            model.Telefon = kul.Telefon;
            model.Adres = kul.Adres;
            model.KullaniciTuruId = kul.KullaniciTuruId;
            model.CreatedAt = kul.CreatedAt;
            model.CreatedBy = kul.CreatedBy;
            model.ErrorMessage = ErrorMessage;

            return model;
        }

        private Kullanici GetDataModel(KullaniciDataModel model)
        {
            Kullanici item = new Kullanici();

            item.Ad = model.Ad;
            item.Soyad = model.Soyad;
            item.KullaniciAdi = model.KullaniciAdi;
            item.Sifre = model.Sifre;
            item.Email = model.Email;
            item.Telefon = model.Telefon;
            item.Adres = model.Adres;
            item.KullaniciTuruId = model.KullaniciTuruId.Value;
            item.CreatedAt = DateTime.Now;
            if (model.CreatedBy != null)
            {
                item.CreatedBy = model.CreatedBy;
            }
           
            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<KullaniciDataModel> GetPersonelList(string searchTerm)
        {
            return _KullaniciRepository.PersonelList(searchTerm);
        }

        public List<KullaniciDataModel> GetMusteriList(string searchTerm)
        {
            return _KullaniciRepository.MusteriList(searchTerm);
        }

        public Kullanici GetId(int pId)
        {
            return _KullaniciRepository.GetSelect(pId);
        }

        public int Add(KullaniciDataModel item)
        {
            return _KullaniciRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(KullaniciDataModel item)
        {
            return _KullaniciRepository.Update(GetDataModel(item)).Id;
        }

        public Kullanici Delete(int pId)
        {
            return _KullaniciRepository.Delete(pId);
        }
    }
}
