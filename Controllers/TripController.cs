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
    public class TripController : Controller
    {
        private HolidayDBEntities db = new HolidayDBEntities();

        // GET: /Trip/
        public ActionResult Index(string origin, string destination, string startDate)
        {
            ViewBag.origin = new SelectList(db.Trips.OrderBy(t => t.From).Select(t => new { t.From }).Distinct(), "From", "From");
            ViewBag.destination = new SelectList(db.Trips.OrderBy(t => t.To).Select(t => new { t.To }).Distinct(), "To", "To");
            var trips = db.Trips.Where(t => t.From.Contains(origin) && t.To.Contains(destination)).OrderBy(p => p.From).ToList();
            return View(trips.Where(t => t.TripStartDate.Value.ToString("dd/MM/yyyy").Contains(startDate)));
        }

        // GET: /Trip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: /Trip/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Trip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TripId,From,To,Description,Price,TripStartDate,TripEndDate")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        // GET: /Trip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: /Trip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TripId,From,To,Description,Price,TripStartDate,TripEndDate")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: /Trip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: /Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            try
            {
                db.Trips.Remove(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch 
            {
                ModelState.AddModelError("Error", "Unable to delete this trip. Please check if there is any order associated with this trip!");
                return View(trip);
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
