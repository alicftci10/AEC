using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.Configuration;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFKategori : GenericRepository<Kategori>, IEFKategoriRepository
    {
        public EFKategori(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<KategoriDataModel> GetKategoriList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var List = (from x in db.Kategoris.AsQueryable()

                            join k in db.Kategoris on x.MainKategoriId equals k.Id

                            join kul in db.Kullanicis on x.CreatedBy equals kul.Id

                            select new KategoriDataModel
                            {
                                Id = x.Id,
                                KategoriAdi = x.KategoriAdi,
                                UstKategori = db.Kategoris.Where(i => i.MainKategoriId == null).Select(i=>i.KategoriAdi).FirstOrDefault(),
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedByName = db.Kullanicis.Where(i => i.Id == x.CreatedBy).Select(i => i.Ad + " " + i.Soyad).FirstOrDefault()

                            }).ToList();

                foreach (var item in List.Where(i=>i.UstKategori != null))
                { 
                    List<KategoriDataModel> ortalist = db.Kategoris.Where(i => i.MainKategoriId == item.Id).ToList();
                    foreach (var item2 in ortalist)
                    {
                        item2.OrtaKategori
                    }
                }

                foreach (var item in List)
                {
                    if (item.OrtaKategori != null)
                    {
                        altKategori.Add(db.Kategoris.Where(i => i.MainKategoriId == item.Id).Select(i => i.KategoriAdi).FirstOrDefault());
                    }
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    List = List.Where(i => i.UstKategori.ToLower().Contains(searchTerm) ||
                                    i.OrtaKategori.ToLower().Contains(searchTerm) ||
                                    i.AltKategori.ToLower().Contains(searchTerm) ||
                                    i.CreatedByName.ToLower().Contains(searchTerm) ||
                                    Convert.ToString(i.CreatedAt).Contains(searchTerm)).ToList();
                }

                return List;
            }
        }

        //public void LoadCategoryDropDown()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string url = ConfigurationInfo.ApiUrl + "/api/CategoryApi/GetCategoryDataModelList";
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CurrentUserInfo.JwtToken);
        //        var response = client.GetAsync(url);
        //        var text = response.Result;

        //        List<CategoryDataModel> categoryModelList = new List<CategoryDataModel>();

        //        if (text != null)
        //        {
        //            categoryModelList = JsonConvert.DeserializeObject<List<CategoryDataModel>>(text.Content.ReadAsStringAsync().Result);
        //        }

        //        List<SelectListItem> list = new List<SelectListItem>();
        //        CategoryList(null, "", categoryModelList, list);
        //        ViewBag.CategoryViewBaglist = list;
        //    }
        //}

        //private void CategoryList(int? pId, string Name, List<CategoryDataModel> categoryModelList, List<SelectListItem> list)
        //{
        //    var categoryList = categoryModelList.Where(i => i.MainCategoryId == pId).ToList();

        //    foreach (var item in categoryList)
        //    {
        //        string categoryName = Name + item.CategoryName + " > ";
        //        list.Add(new SelectListItem { Value = item.Id.ToString(), Text = categoryName });

        //        CategoryList(item.Id, categoryName, categoryModelList, list);
        //    }

        //}
    }
}
