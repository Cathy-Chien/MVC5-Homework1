using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Helpers;

namespace Homework.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶資料> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.IsDelete != true);
        }
        public override void Delete(客戶資料 entity)
        {
            entity.IsDelete = true;
        }
        public IQueryable<客戶資料> Search(int 客戶分類Id, string keyword)
        {
            var data = this.All().Where(p => p.IsDelete != true).ToList().AsQueryable();

            if (客戶分類Id != 0)
                data = data.Where(p => p.客戶分類Id == 客戶分類Id);
            if (!string.IsNullOrEmpty(keyword))
                data = data.Where(p => p.客戶名稱.Contains(keyword));

            return data;
        }
        public void Update(客戶資料 entity)
        {
            entity.密碼 = Crypto.HashPassword(entity.密碼);
        }

        public bool CheckLogin(LoginVM logData)
        {
            bool ischeck = false;
            var 客戶資料 = this.All().FirstOrDefault(p => p.帳號 == logData.帳號);
            if (客戶資料 != null)
            {
                if(Crypto.VerifyHashedPassword(客戶資料.密碼, logData.密碼))
                {
                    ischeck = true;
                }
            }
            return ischeck;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}