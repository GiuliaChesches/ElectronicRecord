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
    public class PacientsController : Controller
    {
        private ERecordContext db = new ERecordContext();
        
        // GET: Pacients
        /*public ActionResult Index()
        {
            var pacienti = db.Pacienti.Include(p => p.Oras);
            return View(pacienti.ToList());
        }*/
        public ActionResult Index(string nume)
        {
            var pacienti = from s in db.Pacienti
                           select s;
            if (!String.IsNullOrEmpty(nume))
            {
                pacienti = pacienti.Where(s => s.Nume.Contains(nume)
                                       || s.Prenume.Contains(nume) || s.Oras.Denumire.Contains(nume) || s.Oras.Judet.Denumire.Contains(nume));
            }

            return View(pacienti.ToList());
        }

        // GET: Pacients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacienti.Find(id);
            var programare = db.Programari.Include(p=> p.Medic).Include(p=>p.Pacient).Where(p => p.PacientID == id).ToList();
            ViewBag.Programare = programare;
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // GET: Pacients/Create
        public ActionResult Create()
        {
            
            ViewBag.OrasId = new SelectList(db.Orase, "ID", "Denumire");
            
            return View();
        }

        // POST: Pacients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.Pacienti.Add(pacient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrasId = new SelectList(db.Orase, "ID", "Denumire", pacient.OrasId);
            return View(pacient);
        }

        // GET: Pacients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacienti.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrasId = new SelectList(db.Orase, "ID", "Denumire", pacient.OrasId);
            return View(pacient);
        }

        // POST: Pacients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nume,Prenume,Sex,Adresa,OrasId,DataNasterii,NrTelefon")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrasId = new SelectList(db.Orase, "ID", "Denumire", pacient.OrasId);
            return View(pacient);
        }

        // GET: Pacients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacienti.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DateTime data = DateTime.Now.Date;
            Pacient pacient = db.Pacienti.Find(id);
            var programare = db.Programari.FirstOrDefault(p => p.PacientID == id);
            if (programare != null)
            {
                ViewBag.Error="Exista programari,pacientul nu poate fi sters.";
                return View(pacient);
            }
            else
            {
                db.Pacienti.Remove(pacient);
                db.SaveChanges();
                return RedirectToAction("Index");
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
