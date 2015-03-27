using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practice1.Models;
using System.Net;
using System.Data;
using System.Data.Entity;
namespace Practice1.Controllers
{
    public class SayHelloController : Controller
    {
        private FabricsEntities2 db = new FabricsEntities2();

    

        // GET: SayHello
        public ActionResult Index()
        {
            var db = new FabricsEntities2();
            //var data = db.Client.ToList();
            var data2 = db.sel_clients();
            return View(data2); 
        }
        public string Welcome(string name, int numTimes = 1)
        {
            return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
           
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           Client client = db.Client.Find(id);
           foreach (Order order in client.Order)
           {
               db.OrderLine.RemoveRange(order.OrderLine);
           }
           db.Order.RemoveRange(client.Order);
           db.Client.Remove(client);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult JsonTest()
        {
            db.Configuration.LazyLoadingEnabled = false; //停用延遲載入,不要有循環參考
            var data = db.Client.Find(5);
            return Json(data);
        }

       

    }
}