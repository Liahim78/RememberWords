using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL_RememberWords.Context;
using DAL_RememberWords.Entities;
using Microsoft.AspNet.Identity;

namespace MVC_RememberWords.Controllers
{
    [Authorize]
    public class WordsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Words
        public ActionResult Index(int? groupId)
        {
            ViewBag.groupId = groupId;
            ViewBag.GroupName = (from g in db.Groups
                where g.Id == groupId
                select g.Name).SingleOrDefault();
            var result = from w in db.Words.ToList()
                         where w.GroupId == groupId
                         select w;
            return View(result.ToList());
        }

        // GET: Words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // GET: Words/Create
        public ActionResult Create(int? groupId)
        {
            ViewBag.groupId = groupId;
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WordValue,Discription")] Word word, int? groupId)
        {
            if (ModelState.IsValid)
            {
                word.Level = 1;
                word.Date = DateTime.Now.Date;
                word.GroupId = (int)groupId;
                db.Words.Add(word);
                db.SaveChanges();
                return RedirectToAction("Index", groupId);
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", word.GroupId);
            return View(word);
        }

        // GET: Words/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", word.GroupId);
            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WordValue,Discription,Level,Date")] Word word)
        {
            if (ModelState.IsValid)
            {
                Word oldWord = db.Words.Find(word.Id);
                oldWord.WordValue = word.WordValue;
                oldWord.Discription = word.Discription;
                oldWord.Date = word.Date;
                oldWord.Level = word.Level;
                db.Entry(oldWord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", word.GroupId);
            return View(word);
        }

        // GET: Words/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Word word = db.Words.Find(id);
            db.Words.Remove(word);
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
