using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using MVC5Course.Models;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var Product = new Product()
            {
                ProductName = "BMW",
                Price = 2,
                Stock = 1,
                Active = true
            };

            db.Product.Add(Product);

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
            //var datapk = Product.ProductId;

            //var data = db.Product.Where(s => s.ProductId == datapk).ToList();
            var data = db.Product.OrderByDescending(s => s.ProductId).ToList();
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
    }
}