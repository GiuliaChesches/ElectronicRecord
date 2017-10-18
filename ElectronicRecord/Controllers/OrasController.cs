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
    public class OrasController : Controller
    {
        private ERecordContext db = new ERecordContext();

        // GET: Oras/Create
        public ActionResult Create()
        {
            ViewBag.JudetID = new SelectList(db.Judete, "ID", "Denumire");
            return View();
        }

        // POST: Oras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,JudetID,Denumire")] Oras oras)
        {
            if (ModelState.IsValid)
            {
                db.Orase.Add(oras);
                db.SaveChanges();
            }
            ViewBag.Message = "Orasul a fost adaugat cu succes";
            ViewBag.JudetID = new SelectList(db.Judete, "ID", "Denumire", oras.JudetID);
            return View(oras);
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
