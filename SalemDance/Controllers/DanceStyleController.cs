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

namespace SalemDance.Controllers
{
    public class DanceStyleController : Controller
    {
        private DanceContext db = new DanceContext();

        // GET: DanceStyle
        public ActionResult Index(int? id)
        {
            var viewModel = new DanceMovesData();
            viewModel.DanceStyles = db.DanceStyles
                .Include(i => i.DanceMoves)
                .OrderBy(i => i.StyleName);
            

            if (id != null)
            {
                ViewBag.DanceStyleID = id.Value;
                viewModel.DanceMoves = viewModel.DanceStyles.Where(
                    i => i.DanceStyleID == id.Value).Single().DanceMoves
                    .OrderBy(i => i.DanceMoveName);
            }

            return View(viewModel);
        }

        // GET: DanceStyle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceStyle danceStyle = db.DanceStyles.Find(id);
            if (danceStyle == null)
            {
                return HttpNotFound();
            }
            return View(danceStyle);
        }

        // GET: DanceStyle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DanceStyle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DanceStyleID,StyleName,MusicGenre")] DanceStyle danceStyle)
        {
            if (ModelState.IsValid)
            {
                db.DanceStyles.Add(danceStyle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danceStyle);
        }

        // GET: DanceStyle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceStyle danceStyle = db.DanceStyles.Find(id);
            if (danceStyle == null)
            {
                return HttpNotFound();
            }
            return View(danceStyle);
        }

        // POST: DanceStyle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DanceStyleID,StyleName,MusicGenre")] DanceStyle danceStyle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danceStyle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danceStyle);
        }

        // GET: DanceStyle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceStyle danceStyle = db.DanceStyles.Find(id);
            if (danceStyle == null)
            {
                return HttpNotFound();
            }
            return View(danceStyle);
        }

        // POST: DanceStyle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanceStyle danceStyle = db.DanceStyles.Find(id);
            db.DanceStyles.Remove(danceStyle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: DanceStyle/AddMove/5
        public ActionResult AddMove(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceStyle danceStyle = db.DanceStyles.Find(id);
            if (danceStyle == null)
            {
                return HttpNotFound();
            }
            DanceMove move = new DanceMove();
            move.DanceStyleID = danceStyle.DanceStyleID;
            move.DanceStyle = danceStyle;
            ViewBag.DanceStyleID = id;
            return View(move);
        }

        // POST: DanceStyle/AddMove/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMove([Bind(Include = "DanceMoveID,DanceStyleID,DanceMoveName,Link")] DanceMove danceMove, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceStyle danceStyle = db.DanceStyles.Find(id);
            if (danceStyle == null)
            {
                return HttpNotFound();
            }
            
            danceMove.DanceStyleID = danceStyle.DanceStyleID;
            danceMove.DanceStyle = danceStyle;
 
            if (ModelState.IsValid)
            {
                db.DanceMoves.Add(danceMove);
                db.SaveChanges();
                return RedirectToAction("Index/" + id.Value);
            }

            ViewBag.DanceStyleID = new SelectList(db.DanceStyles, "DanceStyleID", "StyleName", danceMove.DanceStyleID);
            return View(danceMove);
        }

        // GET: DanceStyle/EditMove/5
        public ActionResult EditMove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceMove danceMove = db.DanceMoves.Find(id);
            if (danceMove == null)
            {
                return HttpNotFound();
            }
            ViewBag.DanceStyleID = new SelectList(db.DanceStyles, "DanceStyleID", "StyleName", danceMove.DanceStyleID);

            return View(danceMove); 
        }

        // POST: DanceStyle/EditMove/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMove([Bind(Include = "DanceMoveID,DanceStyleID,DanceMoveName,Link")] DanceMove danceMove)
        {
            if (ModelState.IsValid)
            {
                int styleID = danceMove.DanceStyleID;
                db.Entry(danceMove).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + styleID);
            }
            return View(danceMove);
        }

        // GET: DanceStyle/DeleteMove/5
        public ActionResult DeleteMove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceMove danceMove = db.DanceMoves.Find(id);
            if (danceMove == null)
            {
                return HttpNotFound();
            }
            ViewBag.DanceStyleID = danceMove.DanceStyleID; 

            return View(danceMove);
        }

        // POST: DanceStyle/DeleteMove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMove(int id)
        {
            DanceMove danceMove = db.DanceMoves.Find(id);
            int styleID = danceMove.DanceStyleID;
            ViewBag.DanceStyleID = styleID; 
            db.DanceMoves.Remove(danceMove);
            db.SaveChanges();
            return RedirectToAction("Index/" + styleID);
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
