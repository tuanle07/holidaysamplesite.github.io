using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;

namespace Assignment_2.Controllers
{
    public class CustomerController : Controller
    {
        private HolidayDBEntities db = new HolidayDBEntities();

        // GET: /Customer/
        public ActionResult Index(string firstName, string lastName, string mobile)
        {
            var customers = from c in db.Customers
                         select c;
            customers = customers.Where(c => c.CustFName.Contains(firstName) && c.CustLName.Contains(lastName) && c.CustMobile.Contains(mobile));
            return View(customers.OrderBy(c => c.CustFName));
        }

        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Customer/Create
        public ActionResult Create()
        {
            Customer customer = new Customer();
            ViewBag.State = new SelectList(db.States, "StateCD", "StateName");
            //ViewBag.CustState = db.States;
            return View(customer);
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CustID,CustFName,CustLName,CustDOB,CustGender,CustStreet,CustState,CustPostalCD,CustPhone,CustMobile,CustEmail,CustRemark,CustRegisterdDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustRegisterdDate = System.DateTime.Now;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.State = new SelectList(db.States, "StateCD", "StateName", customer.CustState);
            return View(customer);
        }

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CustID,CustFName,CustLName,CustDOB,CustGender,CustStreet,CustState,CustPostalCD,CustPhone,CustMobile,CustEmail,CustRemark,CustRegisterdDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States, "StateCD", "StateName", customer.CustState);
            //ViewBag.CustState = db.States;
            return View(customer);
        }

        // GET: /Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            try
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Error", "Unable to delete this customer. Please check if there is any order associated with this customer!");
                return View(customer);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
