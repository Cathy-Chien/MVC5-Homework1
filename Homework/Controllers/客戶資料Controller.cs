using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework.Models;

namespace Homework.Controllers
{
    public class 客戶資料Controller : BaseController
    {
        客戶資料Repository repo;
        客戶銀行資訊Repository bankRepo;
        客戶聯絡人Repository contactRepo;
        客戶類別Repository categoryRepo;

        public 客戶資料Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
            bankRepo = RepositoryHelper.Get客戶銀行資訊Repository(repo.UnitOfWork);
            contactRepo = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
            categoryRepo = RepositoryHelper.Get客戶類別Repository(repo.UnitOfWork);
        }

        // GET: 客戶資料
        //public ActionResult Index()
        //{
        //    var data = repo.All().AsQueryable();

        //    ViewBag.客戶分類Id = new SelectList(categoryRepo.SelectItem(), "分類Id", "客戶分類", 0);

        //    return View(data.Take(10));
        //}

        public ViewResult Index(string sortOrder)
        {
            ViewBag.客戶分類Id = new SelectList(categoryRepo.SelectItem(), "分類Id", "客戶分類", 0);

            ViewBag.客戶名稱Sort = String.IsNullOrEmpty(sortOrder) ? "客戶名稱_desc" : "";
            ViewBag.客戶類別Sort = sortOrder == "客戶類別" ? "客戶類別_desc" : "客戶類別";
            ViewBag.統一編號Sort = sortOrder == "統一編號" ? "統一編號_desc" : "統一編號";
            ViewBag.電話Sort = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.傳真Sort = sortOrder == "傳真" ? "傳真_desc" : "傳真";
            ViewBag.地址Sort = sortOrder == "地址" ? "地址_desc" : "地址";
            ViewBag.EmailSort = sortOrder == "Email" ? "Email_desc" : "Email";

            var data = repo.All().AsQueryable();
            switch (sortOrder)
            {
                case "客戶名稱_desc":
                    data = data.OrderByDescending(w => w.客戶名稱);
                    break;
                case "客戶類別_desc":
                    data = data.OrderByDescending(w => w.客戶分類Id);
                    break;
                case "客戶類別":
                    data = data.OrderBy(w => w.客戶分類Id);
                    break;
                case "統一編號_desc":
                    data = data.OrderByDescending(w => w.統一編號);
                    break;
                case "統一編號":
                    data = data.OrderBy(w => w.統一編號);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(w => w.電話);
                    break;
                case "電話":
                    data = data.OrderBy(w => w.電話);
                    break;
                case "傳真_desc":
                    data = data.OrderByDescending(w => w.傳真);
                    break;
                case "傳真":
                    data = data.OrderBy(w => w.傳真);
                    break;
                case "地址_desc":
                    data = data.OrderByDescending(w => w.地址);
                    break;
                case "地址":
                    data = data.OrderBy(w => w.地址);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(w => w.Email);
                    break;
                case "Email":
                    data = data.OrderBy(w => w.Email);
                    break;
                default:
                    data = data.OrderBy(w => w.客戶名稱);
                    break;
            }
            return View(data.Take(10));
        }

        public ActionResult Statistics()
        {
            var data = repo.All()
                .Select(c => new 客戶統計ViewModel()
                {
                    Id = c.Id,
                    客戶名稱 = c.客戶名稱
                }).ToList();

            foreach (var item in data)
            {
                item.銀行帳戶數量 = bankRepo.Count(item.Id);
                item.聯絡人數量 = contactRepo.Count(item.Id);
            }

            return View(data);
        }

        [Route("search")]
        public ActionResult Search(string keyword, int 客戶分類Id)
        {
            var data = repo.Search(客戶分類Id, keyword);
            ViewBag.客戶分類Id = new SelectList(categoryRepo.SelectItem(), "分類Id", "客戶分類", 客戶分類Id);

            return View("Index", data);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類Id = new SelectList(categoryRepo.All(), "分類Id", "客戶分類");
            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類Id")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶分類Id = new SelectList(categoryRepo.All(), "分類Id", "客戶分類", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶分類Id = new SelectList(categoryRepo.All(), "分類Id", "客戶分類", 客戶資料.客戶分類Id);

            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類Id")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges(); ;
                return RedirectToAction("Index");
            }
            ViewBag.客戶分類Id = new SelectList(categoryRepo.All(), "分類Id", "客戶分類", 客戶資料.客戶分類Id);

            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
