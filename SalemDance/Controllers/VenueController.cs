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
    public class VenueController : Controller
    {
        private DanceContext db = new DanceContext();

        // GET: Venue
        public ViewResult Index(string searchString)
        {
            var venues = from v in db.Venues
                           select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                venues = venues.Where(s => s.VenueName.Contains(searchString)
                                       || s.Address.Contains(searchString));
            }
            venues = venues.OrderBy(s => s.VenueName);
                    
            return View(venues.ToList());
        }

        // GET: Venue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // GET: Venue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VenueID,VenueName,Address,Website,Phone")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Venues.Add(venue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venue);
        }

        // GET: Venue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VenueID,VenueName,Address,Website,Phone")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venue);
        }

        // GET: Venue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venue venue = db.Venues.Find(id);
            db.Venues.Remove(venue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Venue/AddReview/5
        public ActionResult AddReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.VenueID = id;
            Venue e = db.Venues.Find(id);
            ViewBag.Venue = e;
            if (e == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        // POST: Venue/AddReview
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview([Bind(Include = "ReviewID,ReviewerLastName,ReviewerFirstName,Stars,Comment")] Review review, int? id)
        {
            if (ModelState.IsValid)
            {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Venue e = db.Venues.Find(id);
                if (e == null)
                {
                    return HttpNotFound();
                }
                review.VenueID = e.VenueID;
                review.EventID = null;
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", review.EventID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", review.VenueID);
            return View(review);
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
