using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶聯絡人> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.IsDelete != true);
        }
        public override void Delete(客戶聯絡人 entity)
        {
            entity.IsDelete = true;
        }
        public int Count(int Cid)
        {
            return base.All().Where(p => (p.IsDelete != true) && (p.客戶Id == Cid)).Count();
        }
        public IQueryable<客戶聯絡人> Search(string 職稱, string keyword)
        {
            var data = this.All().Where(p => p.IsDelete != true).ToList().AsQueryable();

            if (!string.IsNullOrEmpty(職稱) && 職稱!="ALL")
                data = data.Where(p => p.職稱 == 職稱);
            if (!string.IsNullOrEmpty(keyword))
                data = data.Where(p => p.姓名.Contains(keyword));

            return data;
        }
        public IQueryable<string> SelectItem()
        {
            var data = this.All().Where(c=>c.IsDelete != true).Select(c => c.職稱).Distinct().ToList();

            data.Add("ALL");

            return data.AsQueryable();
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}