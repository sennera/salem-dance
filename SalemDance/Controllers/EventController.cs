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
using SalemDance.ViewModels;
using System.Data.Entity.Infrastructure;

namespace SalemDance.Controllers
{
    public class EventController : Controller
    {
        private DanceContext db = new DanceContext();

        // GET: Event
        public ActionResult Index(string sortOrder, string searchString)
        {
            LinkedList<string> list = new LinkedList<string>();
            list.AddLast("");
            list.AddLast("Minimum Age");
            list.AddLast("Entry");
            list.AddLast("Food");
            list.AddLast("Classes");
            list.AddLast("Event Name");
            list.AddLast("Venue Name");
            list.AddLast("Age Restriction");
            list.AddLast("Minimum Age");
            list.AddLast("Days");
            list.AddLast("Entry");
            list.AddLast("Food");
            list.AddLast("Classes");
            list.AddLast("Style Name");
            ViewBag.SortOrder = new SelectList(list);
           
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EventSortParm = String.IsNullOrEmpty(sortOrder) ? "event_desc" : "";
            ViewBag.VenueSortParm = sortOrder == "Venue Name" ? "venue_desc" : "Venue Name";
            ViewBag.DanceSortParm = sortOrder == "Style Name" ? "style_desc" : "Style Name";
            ViewBag.AgeRestrictSortParm = sortOrder == "Age Restriction" ? "age_desc" : "Age Restriction";
            ViewBag.MinAgeSortParm = sortOrder == "Minimum Age" ? "minAge_desc" : "Minimum Age";
            ViewBag.DaysSortParm = sortOrder == "Days" ? "days_desc" : "Days";
            ViewBag.EntrySortParm = sortOrder == "Entry" ? "entry_desc" : "Entry";
            ViewBag.FoodSortParm = sortOrder == "Food" ? "food_desc" : "Food";
            ViewBag.ClassesSortParm = sortOrder == "Classes" ? "classes_desc" : "Classes";

            var events = from e in db.Events
                         select e;


            //var viewModel = new EventIndexData();
            //viewModel.Events = db.Events
            //    .Include(i => i.DanceStyle)
            //    .Include(i => i.Venue)
            //    .Include(i => i.DaysOpen)
            //    .OrderBy(i => i.EventName);

            //if (!string.IsNullOrEmpty(sortOrder))
            //{
            //    events = events.Where(e => e.EventName.Contains(sortOrder));
            //}

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.EventName.Contains(searchString)
                    || e.Venue.VenueName.Contains(searchString)
                    || e.DanceStyle.StyleName.Contains(searchString));
                 //  || e.DaysOpen.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;

           switch (sortOrder)
            {
               case "event_desc":
                   events = events.OrderByDescending(e => e.EventName);
                   break;

                case "Venue Name":
                    events = events.OrderBy(e => e.Venue.VenueName);
                    break;
                case "venue_desc":
                    events = events.OrderByDescending(e => e.Venue.VenueName);
                    break;
                case "Style Name":
                    events = events.OrderBy(e => e.DanceStyle.StyleName);
                    break;
                case "style_desc":
                    events = events.OrderByDescending(e => e.DanceStyle.StyleName);
                    break;

                case "Age Restriction":
                    events = events.OrderBy(e => e.AgeRestriction);
                    break;
                case "age_desc":
                    events = events.OrderByDescending(e => e.AgeRestriction);
                    break;

                case "Minimum Age":
                    events = events.OrderBy(e => e.MinAge);
                    break;
                case "minAge_desc":
                    events = events.OrderByDescending(e => e.MinAge);
                    break;
                case "Entry":
                    events = events.OrderBy(e => e.EntryCost);
                    break;
                case "entry_desc":
                    events = events.OrderByDescending(e => e.EntryCost);
                    break;

                case "Food":
                    events = events.OrderBy(e => e.FoodServed);
                    break;
                case "food_desc":
                    events = events.OrderByDescending(e => e.FoodServed);
                    break;

               default:
                    events = events.OrderBy(e => e.EventName);
                    break;
 
            
            }

         
            return View(events.ToList());
           // return View(viewModel);
        }
        

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event ev = db.Events
                .Include(i => i.DaysOpen)
                .Include(i => i.Venue)
                .Include(i => i.DanceStyle)
                .Where(i => i.EventID == id)
                .Single();
            if (ev == null)
            {
                return HttpNotFound();
            }
            PopulateDaysWithEventData(ev);

            return View(ev);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName");
            ViewBag.DanceStyleID = new SelectList(db.DanceStyles, "DanceStyleID", "StyleName");

            var ev = new Event();
            ev.DaysOpen = new List<DayOpen>();
            PopulateDaysWithEventData(ev);
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,VenueID,EventName,Days,StartTime,EndTime,Description,MusicGenre,EntryCost,FoodServed,AgeRestriction,MinAge,HasLessons,DanceStyleID")] Event @event, string[] selectedDays)
        {
            if (selectedDays != null)
            {
                @event.DaysOpen = new List<DayOpen>();
                foreach (var day in selectedDays)
                {
                    var dayToAdd = db.DaysOpen.Find(int.Parse(day));
                    @event.DaysOpen.Add(dayToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateDaysWithEventData(@event);
            return View(@event);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event ev = db.Events
                .Include(i => i.DaysOpen)
                .Include(i => i.Venue)
                .Include(i => i.DanceStyle)
                .Where(i => i.EventID == id)
                .Single();
            PopulateDaysWithEventData(ev);
            if (ev == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName");
            ViewBag.DanceStyleID = new SelectList(db.DanceStyles, "DanceStyleID", "StyleName");
            return View(ev);
        }

        private void PopulateDaysWithEventData(Event ev)
        {
            var allDays = db.DaysOpen;
            var eventDays = new HashSet<DayOfWeek>(ev.DaysOpen.Select(d => d.DayOfWeek));
            var viewModel = new List<DaysWithEventData>();
            foreach (var day in allDays)
            {
                viewModel.Add(new DaysWithEventData
                {
                    DayID = day.ID,
                    DayOpen = day.DayOfWeek,
                    Assigned = eventDays.Contains(day.DayOfWeek) 
                });
            }
            ViewBag.DaysOpen = viewModel;
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedDays)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventToUpdate = db.Events
               .Include(i => i.DaysOpen)
                .Include(i => i.Venue)
                .Include(i => i.DanceStyle)
                .Where(i => i.EventID == id)
                .Single();

            if (TryUpdateModel(eventToUpdate, "",
               new string[] { "EventName", "StartTime", "EndTime", "Description", "DanceStyleID", "MusicGenre", "EntryCost", "FoodServed", "AgeRestriction", "MinAge", "HasLessons" }))
        {
                try
            {
                    UpdateEventDays(selectedDays, eventToUpdate);

                    db.Entry(eventToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDaysWithEventData(eventToUpdate);
            return View(eventToUpdate);
        }
        private void UpdateEventDays(string[] selectedDays, Event eventToUpdate)
        {
            if (selectedDays == null)
            {
                eventToUpdate.DaysOpen = new List<DayOpen>();
                return;
            }

            var selectedDaysHS = new HashSet<string>(selectedDays);
            var eventDays = new HashSet<DayOfWeek>
                (eventToUpdate.DaysOpen.Select(c => c.DayOfWeek));
            foreach (var entry in db.DaysOpen)
            {
                if (selectedDaysHS.Contains(entry.ID.ToString()))
                {
                    if (!eventDays.Contains(entry.DayOfWeek))
                    {
                        eventToUpdate.DaysOpen.Add(entry);
                    }
                }
                else
                {
                    if (eventDays.Contains(entry.DayOfWeek))
                    {
                        eventToUpdate.DaysOpen.Remove(entry);
                    }
                }
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event ev = db.Events
                .Include(i => i.DaysOpen)
                .Include(i => i.Venue)
                .Include(i => i.DanceStyle)
                .Where(i => i.EventID == id)
                .Single();
            if (ev == null)
            {
                return HttpNotFound();
            }
            PopulateDaysWithEventData(ev);

            return View(ev);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Event/AddReview/5
        public ActionResult AddReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.EventID = id;
            Event e = db.Events.Find(id);
            
            if (e == null)
            {
                return HttpNotFound();
            }
            ViewBag.Event = e;
            ViewBag.Venue = e.Venue;
            ViewBag.VenueID = e.VenueID;
            
            return View();
        }

        // POST: Event/AddReview
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview([Bind(Include = "ReviewID,ReviewerLastName,ReviewerFirstName,Stars,Comment")] Review review, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Event e = db.Events.Find(id);
                if (e == null)
                {
                    return HttpNotFound();
                }
                review.EventID = e.EventID;
                review.VenueID = e.VenueID;
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
