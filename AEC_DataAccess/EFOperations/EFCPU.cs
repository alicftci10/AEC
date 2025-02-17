﻿using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.DataModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFCPU : GenericRepository<Cpu>, IEFCPURepository
    {
        public EFCPU(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<CPUDataModel> GetCPUList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Cpus

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            join k in db.Kategoris on x.IslemciSerisiId equals k.Id

                            select new CPUDataModel
                            {
                                Id = x.Id,
                                IslemciSerisiId = x.IslemciSerisiId,
                                IslemciSerisiName = k.KategoriAdi,
                                IslemciMimarisi = x.IslemciMimarisi,
                                IslemciAdi = x.IslemciAdi,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = kul.Ad + " " + kul.Soyad

                            }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => (!string.IsNullOrEmpty(i.IslemciSerisiName) && i.IslemciSerisiName.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.IslemciMimarisi) && i.IslemciMimarisi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.IslemciAdi) && i.IslemciAdi.ToLower().Contains(searchTerm)) ||
                                           (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                           (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                           ).ToList();
                }

                return List;
            }
        }
    }
}
