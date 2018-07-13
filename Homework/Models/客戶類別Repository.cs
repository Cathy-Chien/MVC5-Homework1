using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework.Models
{   
	public  class 客戶類別Repository : EFRepository<客戶類別>, I客戶類別Repository
	{
        public 客戶類別 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.分類Id == id);
        }
        public IQueryable<客戶類別> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.isDelete != true);
        }

        public IQueryable<客戶類別> SelectItem()
        {
            var data = this.All().Where(p => p.isDelete != true).ToList();
            客戶類別 all = new 客戶類別();
            all.分類Id = 0;
            all.客戶分類 = "ALL";
            data.Add(all);

            return data.AsQueryable();
        }


        public override void Delete(客戶類別 entity)
        {
            entity.isDelete = true;
        }
    }

	public  interface I客戶類別Repository : IRepository<客戶類別>
	{

	}
}