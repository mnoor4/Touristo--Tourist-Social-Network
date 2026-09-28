using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication26.Models;

namespace WebApplication26.Controllers
{
    public class MyEventsController : Controller
    {
        private TouristOEntities db = new TouristOEntities();

        // GET: MyEvents
        public ActionResult Index()
        {
            var myEvents = db.MyEvents.Include(m => m.Event).Include(m => m.Member);
            return View(myEvents.ToList());
        }

        // GET: MyEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyEvent myEvent = db.MyEvents.Find(id);
            if (myEvent == null)
            {
                return HttpNotFound();
            }
            return View(myEvent);
        }

        // GET: MyEvents/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberUsername");
            return View();
        }

        // POST: MyEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,MemberId,MyEventsIntrests")] MyEvent myEvent)
        {
            if (ModelState.IsValid)
            {
                db.MyEvents.Add(myEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName", myEvent.EventId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberUsername", myEvent.MemberId);
            return View(myEvent);
        }

        // GET: MyEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyEvent myEvent = db.MyEvents.Find(id);
            if (myEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName", myEvent.EventId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberUsername", myEvent.MemberId);
            return View(myEvent);
        }

        // POST: MyEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,MemberId,MyEventsIntrests")] MyEvent myEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName", myEvent.EventId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MemberUsername", myEvent.MemberId);
            return View(myEvent);
        }

        // GET: MyEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyEvent myEvent = db.MyEvents.Find(id);
            if (myEvent == null)
            {
                return HttpNotFound();
            }
            return View(myEvent);
        }

        // POST: MyEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyEvent myEvent = db.MyEvents.Find(id);
            db.MyEvents.Remove(myEvent);
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
