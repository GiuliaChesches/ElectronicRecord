using ElectronicRecord.DAL;
using ElectronicRecord.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ElectronicRecord.Controllers
{
    
    public class UserController : Controller
    {
        private ERecordContext db = new ERecordContext();
        // GET: User
        public ActionResult Index()
        {
            string userName= User.Identity.Name;
            Medic medic = db.Medici.FirstOrDefault(u => u.NumeUtilizator == userName);
            if (medic == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrasID = new SelectList(db.Orase, "ID", "Denumire", medic.OrasID);
            return View(medic);
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Medic user)
        {
                if (IsValid(user.NumeUtilizator, user.Parola))
                {
                    FormsAuthentication.SetAuthCookie(user.NumeUtilizator, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect.");
                }
            return View(user);
        }
        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.OrasID = new SelectList(db.Orase, "ID", "Denumire");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Medic medic)
        {
            var user = db.Medici.FirstOrDefault(u => u.NumeUtilizator == medic.NumeUtilizator);
            if(user!=null)
            {
                ModelState.AddModelError("", "Nume de utilizator existent. Sugestie: nume.prenume");
            }
            var userP = db.Medici.FirstOrDefault(u => u.NrTelefon == medic.NrTelefon);
            if (userP != null)
            {
                ModelState.AddModelError("", "Numar de telefon existent. Acesta trebuie sa fie unic.");
            }
            if (ModelState.IsValid)
            {
                db.Medici.Add(medic);
                db.SaveChanges();
                ViewBag.Message = "Contul a fost creat cu succes";
            }

            ViewBag.OrasID = new SelectList(db.Orase, "ID", "Denumire", medic.OrasID);
            return View(medic);
        }
        public ActionResult LogOut() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Edit()
        {
            string userName = User.Identity.Name;
            Medic medic = db.Medici.FirstOrDefault(u => u.NumeUtilizator == userName);
            ViewBag.OrasID = new SelectList(db.Orase, "ID", "Denumire", medic.OrasID);
            return View(medic);
        }

        [HttpPost]
        public ActionResult Edit(Medic medic)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(medic.NumeUtilizator, false);
                db.Entry(medic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrasID = new SelectList(db.Orase, "ID", "Denumire", medic.OrasID);
            return View(medic);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            string userName = User.Identity.Name;
            Medic medic = db.Medici.FirstOrDefault(u => u.NumeUtilizator == userName);
            if (medic == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrasID = new SelectList(db.Orase, "ID", "Denumire", medic.OrasID);
            return View(medic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            string userName = User.Identity.Name;
            Medic medic = db.Medici.FirstOrDefault(u => u.NumeUtilizator == userName);
            var programare = db.Programari.FirstOrDefault(p => p.MedicID == medic.ID);
            if (programare != null)
            {
                ViewBag.Error = "Exista programari,Medicul nu poate fi sters.";
                return View(medic);
            }
            else
            {
                db.Medici.Remove(medic);
                db.SaveChanges();
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

            
        }
        private bool IsValid(string userName, string password)
        {
            bool isValid = false;
            var user = db.Medici.FirstOrDefault(u => u.NumeUtilizator == userName);
            if (user != null)
            {
                if (user.Parola == password)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
    }
}