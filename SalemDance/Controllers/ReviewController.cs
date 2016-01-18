using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalemDance.DAL;
using SalemDance.Models;
using System.Data.Entity.Infrastructure;


namespace SalemDance.Controllers
{
    public class ReviewController : Controller
    {
        private DanceContext db = new DanceContext();

         //GET: Review
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Event).Include(r => r.Venue);
            return View(reviews.ToList());

            //var events = new SelectList(
            // db.Reviews.Include(r => r.Event).ToList());

            //ViewBag.Events = events;
            //return View(events.ToList());
        }

        // GET: Review/Details/5
        //public ActionResult Index(int? SelectedEvents)
        //{
        //    var events = db.Events.OrderBy(q => q.EventName).ToList();
        //    ViewBag.SelectedEvents = new SelectList(events, "EventID", "Stars", SelectedEvents);
        //    int eventID = SelectedEvents.GetValueOrDefault();

        //    IQueryable<Review> reviews = db.Reviews
        //        .Where(c => !SelectedEvents.HasValue || c.EventID == eventID)
        //        .OrderBy(d => d.ReviewID)
        //        .Include(d => d.Event);
        //    var sql = reviews.ToString();
        //    return View(reviews.ToList());
        //}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Review/Create/4
        public ActionResult Create(int? eventID, int? venueID)
        {
            if (eventID != null)
            {
                ViewBag.EventID = eventID;
                Event e = db.Events.Find(eventID);
                if (e == null)
                {
                    return HttpNotFound();
                }
                ViewBag.VenueID = e.VenueID;
            }
            else if (venueID != null) 
            {
                ViewBag.VenueID = venueID;
                Venue v = db.Venues.Find(venueID);
                if (v == null)
                {
                    return HttpNotFound();
                }
                ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,ReviewerLastName,ReviewerFirstName,Stars,Comment,Title,Timestamp = DateTime.Now,VenueID,EventID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", review.EventID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", review.VenueID);
            return View(review);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", review.EventID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", review.VenueID);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,ReviewerLastName,ReviewerFirstName,Stars,Comment,Title,Timestamp,VenueID,EventID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", review.EventID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", review.VenueID);
            return View(review);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
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
