using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MYFITNESSAPP.Models;

namespace MYFITNESSAPP.Controllers
{
    public class TrainingTypesController : Controller
    {
        private MYFITNESSAPPEntities db = new MYFITNESSAPPEntities();

        // GET: TrainingTypes
        public ActionResult Index()
        {
            var trainingType = db.TrainingType.Include(t => t.Trainer);
            return View(trainingType.ToList());
        }

        // GET: TrainingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingType = db.TrainingType.Find(id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        // GET: TrainingTypes/Create
        public ActionResult Create()
        {
            ViewBag.TrainingTrainerID = new SelectList(db.Trainer, "TrainerID", "TrainerFullName");
            return View();
        }

        // POST: TrainingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingTypeID,TrainingName,TrainingDesc,TrainingPhoto,TrainingTrainerID")] TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                db.TrainingType.Add(trainingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainingTrainerID = new SelectList(db.Trainer, "TrainerID", "TrainerFullName", trainingType.TrainingTrainerID);
            return View(trainingType);
        }

        // GET: TrainingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingType = db.TrainingType.Find(id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainingTrainerID = new SelectList(db.Trainer, "TrainerID", "TrainerFullName", trainingType.TrainingTrainerID);
            return View(trainingType);
        }

        // POST: TrainingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingTypeID,TrainingName,TrainingDesc,TrainingPhoto,TrainingTrainerID")] TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainingTrainerID = new SelectList(db.Trainer, "TrainerID", "TrainerFullName", trainingType.TrainingTrainerID);
            return View(trainingType);
        }

        // GET: TrainingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingType = db.TrainingType.Find(id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        // POST: TrainingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingType trainingType = db.TrainingType.Find(id);
            db.TrainingType.Remove(trainingType);
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
