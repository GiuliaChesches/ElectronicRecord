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
    public class MedicsController : Controller
    {
        private ERecordContext db = new ERecordContext();

        // GET: Medics
        public ActionResult Index()
        {
            var medici = db.Medici.Include(m => m.Oras);
            return View(medici.ToList());
        }

        // GET: Medics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medic medic = db.Medici.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
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
