using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL_RememberWords.Context;
using DAL_RememberWords.Entities;
using Microsoft.AspNet.Identity;

namespace MVC_RememberWords.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Groups
        public ActionResult Index()
        {
            ViewBag.word = (from w in  db.Words.ToList()
                            where w.Date <= DateTime.Now.Date && w.Group.UserId == User.Identity.GetUserId()
                            select w).ToList().Count;
            var result = from g in db.Groups.ToList()
                        where g.UserId==User.Identity.GetUserId()
                        select g;
            return View(result.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                group.UserId = User.Identity.GetUserId();
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
        [HttpGet]
        public ActionResult Start()
        {
            var words = from w in db.Words.ToList()
                            where w.Date <= DateTime.Now.Date && w.Group.UserId == User.Identity.GetUserId()
                            select w;
            if (words.ToList().Count == 0)
            {
                return RedirectToAction("Index");
            }
            Word word = words.ToList()[new Random().Next(words.ToList().Count)];
            return View(word);
        }
        [HttpPost]
        public ActionResult Start(int Id, string result)
        {
            if (ModelState.IsValid)
            {
                Word oldWord = db.Words.Find(Id);
                if (oldWord.Discription.Split(' ').Contains(result))
                {
                    oldWord.Date = DateTime.Now.AddDays(oldWord.Level).Date;
                    oldWord.Level *= 2;
                    ViewBag.result = true;
                    ViewBag.components = oldWord.Discription.Split(' ');
                }
                else
                {
                    oldWord.Level = oldWord.Level == 1 ? oldWord.Level : oldWord.Level/2;
                    ViewBag.result = false;
                    ViewBag.components = oldWord.Discription.Split(' ');
                }
                db.Entry(oldWord).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Result");
        }
    
    }
}
