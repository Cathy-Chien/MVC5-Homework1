using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Homework.Models;

namespace Homework.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {
        客戶聯絡人Repository contactRepo;
        客戶資料Repository repo;
        public 客戶聯絡人Controller()
        {
            contactRepo = RepositoryHelper.Get客戶聯絡人Repository();
            repo = RepositoryHelper.Get客戶資料Repository(contactRepo.UnitOfWork);
        }


        //// GET: 客戶聯絡人
        //public ActionResult Index()
        //{
        //    var data = contactRepo.All().AsQueryable();
        //    ViewBag.職稱 = new SelectList(contactRepo.SelectItem(),"ALL");
        //    return View(data.Take(10));
        //}
        [Authorize]
        public ViewResult Index(string sortOrder)
        {
            ViewBag.職稱 = new SelectList(contactRepo.SelectItem(), "ALL");

            ViewBag.職稱Sort = String.IsNullOrEmpty(sortOrder) ? "職稱_desc" : "";
            ViewBag.姓名Sort = sortOrder == "姓名" ? "姓名_desc" : "姓名";
            ViewBag.EmailSort = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.手機Sort = sortOrder == "手機" ? "手機_desc" : "手機";
            ViewBag.電話Sort = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.客戶名稱Sort = sortOrder == "客戶名稱" ? "客戶名稱_desc" : "客戶名稱";


            var data = contactRepo.All().AsQueryable();
            switch (sortOrder)
            {
                case "職稱_desc":
                    data = data.OrderByDescending(w => w.職稱);
                    break;
                case "姓名_desc":
                    data = data.OrderByDescending(w => w.姓名);
                    break;
                case "姓名":
                    data = data.OrderBy(w => w.姓名);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(w => w.Email);
                    break;
                case "Email":
                    data = data.OrderBy(w => w.Email);
                    break;
                case "手機_desc":
                    data = data.OrderByDescending(w => w.手機);
                    break;
                case "手機":
                    data = data.OrderBy(w => w.手機);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(w => w.電話);
                    break;
                case "電話":
                    data = data.OrderBy(w => w.電話);
                    break;
                case "客戶名稱_desc":
                    data = data.OrderByDescending(w => w.客戶Id);
                    break;
                case "客戶名稱":
                    data = data.OrderBy(w => w.客戶Id);
                    break;
                default:
                    data = data.OrderBy(w => w.客戶Id);
                    break;
            }
            return View(data.Take(10));
        }
        [HttpPost]
        public FileResult Export()
        {
            // ClosedXML的用法 先new一個Excel Workbook
            using (XLWorkbook wb = new XLWorkbook())
            {
                //取得我要塞入Excel內的資料
                var data = contactRepo.All().Select(c => new { c.職稱, c.姓名, c.Email, c.手機,c.電話 }).AsQueryable();
                //一個wrokbook內至少會有一個worksheet,並將資料Insert至這個位於A1這個位置上
                var ws = wb.Worksheets.Add("客戶聯絡人", 1);

                ws.Cell(1, 1).InsertData(data);

                //因為是用Query的方式,這個地方要用串流的方式來存檔
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    //請注意 一定要加入這行,不然Excel會是空檔
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    //注意Excel的ContentType,是要用這個"application/vnd.ms-excel" 不曉得為什麼網路上有的Excel ContentType超長,xlsx會錯 xls反而不會
                    return this.File(memoryStream.ToArray(), "application/vnd.ms-excel", "Download.xlsx");
                }
            }
        }

        [Route("search")]
        public ActionResult Search(string keyword, string 職稱)
        {
            var data = contactRepo.Search(職稱, keyword);
            ViewBag.職稱 = new SelectList(contactRepo.SelectItem(), 職稱);

            return View("Index", data);
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = contactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo.All().AsQueryable(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                contactRepo.Add(客戶聯絡人);
                contactRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo.All().AsQueryable(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = contactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo.All().AsQueryable(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db = contactRepo.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges(); ;
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo.All().AsQueryable(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = contactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = contactRepo.Find(id);
            contactRepo.Delete(客戶聯絡人);
            contactRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contactRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
