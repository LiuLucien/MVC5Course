using System;
using System.Linq;
using System.Data.Entity.Validation;
using MVC5Course.Models;
using System.Web.Mvc;

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