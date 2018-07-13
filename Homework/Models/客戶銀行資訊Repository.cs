using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶銀行資訊> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.IsDelete != true);
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            entity.IsDelete = true;
        }
        public int Count(int Cid)
        {
            return base.All().Where(p => (p.IsDelete != true) && (p.客戶Id==Cid)).Count();
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}