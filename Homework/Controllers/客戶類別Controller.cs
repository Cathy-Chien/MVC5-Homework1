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
    public class 客戶類別Controller : BaseController
    {
        客戶類別Repository categoryRepo;

        public 客戶類別Controller()
        {
            categoryRepo = RepositoryHelper.Get客戶類別Repository();
        }

        // GET: 客戶類別
        public ActionResult Index()
        {
            var data = categoryRepo.All().AsQueryable();

            return View(data.Take(10));
        }

        // GET: 客戶類別/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶類別 客戶類別 = categoryRepo.Find(id.Value);
            if (客戶類別 == null)
            {
                return HttpNotFound();
            }
            return View(客戶類別);
        }

        // GET: 客戶類別/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶類別/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶分類")] 客戶類別 客戶類別)
        {
            if (ModelState.IsValid)
            {
                categoryRepo.Add(客戶類別);
                categoryRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶類別);
        }

        // GET: 客戶類別/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶類別 客戶類別 = categoryRepo.Find(id.Value);
            if (客戶類別 == null)
            {
                return HttpNotFound();
            }
            return View(客戶類別);
        }

        // POST: 客戶類別/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶分類")] 客戶類別 客戶類別)
        {
            if (ModelState.IsValid)
            {
                var db = categoryRepo.UnitOfWork.Context;
                db.Entry(客戶類別).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶類別);
        }

        // GET: 客戶類別/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶類別 客戶類別 = categoryRepo.Find(id.Value);
            if (客戶類別 == null)
            {
                return HttpNotFound();
            }
            return View(客戶類別);
        }

        // POST: 客戶類別/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            {
                客戶類別 客戶類別 = categoryRepo.Find(id);
                categoryRepo.Delete(客戶類別);
                categoryRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                categoryRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
