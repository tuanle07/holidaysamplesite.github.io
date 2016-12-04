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
    public class OrderController : Controller
    {
        private HolidayDBEntities db = new HolidayDBEntities();

        // GET: /Order/
        public ActionResult Index(string firstName, string lastName, string mobile)
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Trip).Where(c => c.Customer.CustFName.Contains(firstName) && c.Customer.CustLName.Contains(lastName) && c.Customer.CustMobile.Contains(mobile));
            return View(orders.OrderBy(p => p.Customer.CustFName).ToList());
        }

        // GET: /Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Order/Create
        public ActionResult Create()
        {
            ViewBag.CustID = new SelectList(db.Customers.OrderBy(c => c.CustFName), "CustID", "CustDesc");
            ViewBag.TripFrom = new SelectList(db.Trips.Select(t => new { t.From }).Distinct().OrderBy(t => t.From), "From", "From");
            //ViewBag.TripTo = new SelectList(Enumerable.Empty<SelectListItem>(), "To", "To");
            //ViewBag.TripStartDate = new SelectList(Enumerable.Empty<SelectListItem>(), "TripId", "TripStartDate");
            return View();
        }

        // POST: /Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderNo,OrderDate,CustID,TripID,No_Of_Pass")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = System.DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustID = new SelectList(db.Customers.OrderBy(c => c.CustFName), "CustID", "CustDesc", order.CustID);
            ViewBag.TripFrom = new SelectList(db.Trips.Select(t => new { t.From }).Distinct().OrderBy(t => t.From), "From", "From");
            //ViewBag.TripTo = new SelectList(Enumerable.Empty<SelectListItem>(), "To", "To");
            //ViewBag.TripStartDate = new SelectList(Enumerable.Empty<SelectListItem>(), "TripId", "TripStartDate", order.TripID);
            return View(order);
        }

        // GET: /Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            int tripid = order.TripID;
            //string from = order.Trip.From;
            ViewBag.CustID = new SelectList(db.Customers.OrderBy(c => c.CustFName), "CustID", "CustDesc", order.CustID);
            ViewBag.TripFrom = new SelectList(db.Trips.Select(t => new { t.From }).Distinct().OrderBy(t => t.From), "From", "From", order.Trip.From);
            ViewBag.TripTo = new SelectList(db.Trips.Where(t => t.From == order.Trip.From).Select(t => new { t.To }).Distinct(), "To", "To", order.Trip.To);
            ViewBag.TripStartDate = new SelectList(db.Trips.Where(t => t.From == order.Trip.From && t.To == order.Trip.To).Select(t => new { t.TripStartDate }).Distinct(), "TripStartDate", "TripStartDate", order.Trip.TripStartDate);
            //ViewBag.TripTo = new SelectList(db.Trips.Where(t => t.TripId == tripid).Select(t => new { t.TripId, t.To }), "TripId", "To", order.TripID);
            //ViewBag.TripStartDate = new SelectList(db.Trips.Where(t => t.TripId == tripid).Select(t => new { t.TripId, t.TripStartDate }), "TripId", "TripStartDate", order.TripID);
            ViewBag.TripEndDate = new SelectList(db.Trips.Where(t => t.TripId == tripid).Select(t => new { t.TripId, t.TripEndDate }), "TripId", "TripEndDate", order.TripID);
            //ViewBag.TripID = new SelectList(db.Trips, "TripId", "From", order.TripID);
            return View(order);
        }

        // POST: /Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderNo,OrderDate,CustID,TripID,No_Of_Pass")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int tripid = order.TripID;
            ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustDesc", order.CustID);
            ViewBag.TripFrom = new SelectList(db.Trips.Select(t => new { t.From }).Distinct().OrderBy(t => t.From), "From", "From", order.Trip.From);
            ViewBag.TripTo = new SelectList(db.Trips.Where(t => t.From == order.Trip.From).Select(t => new { t.To }).Distinct(), "To", "To", order.Trip.To);
            ViewBag.TripStartDate = new SelectList(db.Trips.Where(t => t.From == order.Trip.From && t.To == order.Trip.To).Select(t => new { t.TripStartDate }).Distinct(), "TripStartDate", "TripStartDate", order.Trip.TripStartDate);
            ViewBag.TripEndDate = new SelectList(db.Trips.Where(t => t.TripId == tripid).Select(t => new { t.TripId, t.TripEndDate }), "TripId", "TripEndDate", order.TripID);
            return View(order);
        }

        // GET: /Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult FillTo(string from)
        {
            var tripTos = db.Trips.Where(t => t.From == from).Select(t => new { tripTo = t.To }).Distinct().ToArray();
            return Json(tripTos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillTripStartDate(string from, string to)
        {
            var tripStartDates = db.Trips.Where(t => t.From == from && t.To == to).Select(t => new
            {
                tripStartDate = t.TripStartDate
            }).Distinct().ToArray();
            return Json(tripStartDates, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillTripEndDate(string from, string to, DateTime startdate)
        {
            var tripEndDates = db.Trips.Where(t => t.From == from && t.To == to && t.TripStartDate == startdate).Select(t => new
            {
                tripId = t.TripId,
                tripEndDate = t.TripEndDate
            }).Distinct().ToArray();
            return Json(tripEndDates, JsonRequestBehavior.AllowGet);
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
