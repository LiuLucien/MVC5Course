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
        // GET: EF
        public ActionResult Index()
        {

            var db = new FabricsEntities();

            db.Product.Add(new Product()
            {
                ProductName = "BMW",
                Price = 1,
                Stock = 1,
                Active = true
            });

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
            var data = db.Product.ToList();
            return View(data);
        }
    }
}