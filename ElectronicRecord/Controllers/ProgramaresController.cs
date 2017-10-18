using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectronicRecord.DAL;
using ElectronicRecord.Models;

namespace ElectronicRecord.Controllers
{
    public class ProgramaresController : Controller
    {
        private ERecordContext db = new ERecordContext();

        // GET: Programari
        public ActionResult Index()
        {
            var programari = db.Programari.Include(p => p.Medic).Include(p => p.Pacient).Where(s=> s.Medic.NumeUtilizator.Contains(User.Identity.Name)).OrderByDescending(s=>s.Data);
            
            return View(programari.ToList());

        }
        // GET: ProgramariCurente
        public ActionResult CurrentVisit()
        {
            String data = DateTime.Now.Date.ToString("dd.MM.yyyy");
            var programari = db.Programari.Include(p => p.Medic).Include(p => p.Pacient).ToList().Where(s => s.Data.Date.ToString("dd.MM.yyyy").Contains(data) && s.Medic.NumeUtilizator.Contains(User.Identity.Name)).OrderBy(s => s.Data);
            ViewBag.Data = DateTime.Now.Date.ToString("dd.MM.yyyy");
            return View(programari);
        }
        // GET: Programares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programare programare = db.Programari.Find(id);
            if (programare == null)
            {
                return HttpNotFound();
            }
            return View(programare);
        }

        // GET: Programares/Create
        public ActionResult Create(int id)
        {
            Pacient pacient = db.Pacienti.Find(id);
            ViewBag.PacientID =id;
            ViewBag.Nume = pacient.Nume;
            ViewBag.Prenume = pacient.Prenume;
            return View();
        }

        // POST: Programares/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MedicID,PacientID,Data")] Programare programare)
        {
            Medic medic=db.Medici.FirstOrDefault(u => u.NumeUtilizator == User.Identity.Name);
            programare.MedicID = medic.ID;
            if (ModelState.IsValid)
            {
                db.Programari.Add(programare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MedicID = new SelectList(db.Medici, "ID", "Nume", programare.MedicID);
            ViewBag.PacientID = new SelectList(db.Pacienti, "ID", "Nume", programare.PacientID);
            return View(programare);
        }

        // GET: Programares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programare programare = db.Programari.Find(id);
            if (programare == null)
            {
                return HttpNotFound();
            }
            Pacient pacient = db.Pacienti.Find(programare.PacientID);
            ViewBag.Nume = pacient.Nume;
            ViewBag.Prenume = pacient.Prenume;
            Medic medic = db.Medici.Find(programare.MedicID);
            ViewBag.NumeM = medic.Nume;
            ViewBag.PrenumeM = medic.Prenume;
            //ViewBag.MedicID = new SelectList(db.Medici, "ID", "Nume", programare.MedicID);
            //ViewBag.PacientID = new SelectList(db.Pacienti, "ID", "Nume", programare.PacientID);
            return View(programare);
        }

        // POST: Programares/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MedicID,PacientID,Data")] Programare programare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicID = new SelectList(db.Medici, "ID", "Nume", programare.MedicID);
            ViewBag.PacientID = new SelectList(db.Pacienti, "ID", "Nume", programare.PacientID);
            return View(programare);
        }

        // GET: Programares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programare programare = db.Programari.Find(id);
            if (programare == null)
            {
                return HttpNotFound();
            }
            return View(programare);
        }

        // POST: Programares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Programare programare = db.Programari.Find(id);
            db.Programari.Remove(programare);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Find(string nume,string prenume)
        {
            var pacienti = from s in db.Pacienti
                           select s;
            if (!String.IsNullOrEmpty(nume) || !String.IsNullOrEmpty(prenume))
            {
                pacienti = pacienti.Where(s => s.Nume.Contains(nume)
                                       && s.Prenume.Contains(prenume));
            }

            return View(pacienti.ToList());
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
