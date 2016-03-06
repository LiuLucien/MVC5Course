using System;
using System.Linq;
using System.Data.Entity.Validation;
using MVC5Course.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index(bool? ISActive, string Keyword)
        {
            //AddProduct();
            //var datapk = Product.ProductId;
            //var data = db.Product.Where(s => s.ProductId == datapk).ToList();
            var data = db.Product.OrderByDescending(s => s.ProductId).AsQueryable();

            if (ISActive.HasValue)
            {
                data = data.Where(s => s.Active.HasValue ? s.Active.Value == ISActive : false);
            }
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = data.Where(s => s.ProductName.Contains(Keyword));
            }

            //foreach (var item in data)
            //{
            //    item.Price = item.Price + 1;
            //}
            //SaveChanges();
            return View(data);
        }
        public ActionResult Details(int Id)
        {
            var Data = db.Product.Where(s => s.ProductId == Id).FirstOrDefault();

            if (Data == null)
            {
                return HttpNotFound();
            }
            return View(Data);
        }

        public ActionResult Delete(int Id)
        {
            var Data = db.Product.Where(s => s.ProductId == Id).FirstOrDefault();

            if (Data != null)
            {
                db.OrderLine.RemoveRange(Data.OrderLine);
                db.Product.Remove(Data);
                SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("index");
        }

        public ActionResult QueryPlan()
        {
            var Data = db.Product.Include(s => s.OrderLine).OrderBy(s => s.ProductId).AsQueryable();

            //var data = db.Database.SqlQuery<Product>
            //    (@"
            //        Select * From dbo.Product AS p
            //        where p.ProductId < @p0",10
            //    );
            return View(Data);
        }

        private void AddProduct()
        {
            var Product = new Product()
            {
                ProductName = "BMW",
                Price = 2,
                Stock = 1,
                Active = true
            };

            db.Product.Add(Product);

            SaveChanges();
        }

        private void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException EX)
            {
                foreach (DbEntityValidationResult item in EX.EntityValidationErrors)
                {
                    string entityName = item.Entry.Entity.GetType().Name;

                    foreach (DbValidationError Error in item.ValidationErrors)
                    {
                        throw new Exception(entityName + " 驗證類型失敗： " + Error.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}