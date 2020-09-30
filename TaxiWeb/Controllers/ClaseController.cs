using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    public class ClaseController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();

        // GET: Clase
        public ActionResult Index()
        {
            var clase = db.Clase.Include(c => c.Conductor);

            //var horaInicio = new TimeSpan(5, 0, 0);
            //var horaFin = new TimeSpan(12, 0, 0);

            //clase = (from clasesita in clase
            //         where clasesita.HoraInicio >= horaInicio &&
            //         clasesita.HoraFin <= horaFin
            //         select clasesita);

            // Listar las clases ordenadas ascendentemente por hora de inicio y fin.
            var claseOrdenada = clase.OrderBy(c => c.HoraInicio);

            //return View(claseOrdenada.ToList());
            return View(clase.ToList());
        }

        // GET: Clase/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase clase = db.Clase.Find(id);
            if (clase == null)
            {
                return HttpNotFound();
            }
            return View(clase);
        }

        // GET: Clase/Create
        public ActionResult Create()
        {
            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre");
            return View();
        }

        // POST: Clase/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdConductor,FechaClase,NombreInstructor,HoraInicio,HoraFin")] Clase clase)
        {
            if (ModelState.IsValid)
            {
                db.Clase.Add(clase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", clase.IdConductor);
            return View(clase);
        }

        // GET: Clase/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase clase = db.Clase.Find(id);
            if (clase == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", clase.IdConductor);
            return View(clase);
        }

        // POST: Clase/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdConductor,FechaClase,NombreInstructor,HoraInicio,HoraFin")] Clase clase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", clase.IdConductor);
            return View(clase);
        }

        // GET: Clase/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase clase = db.Clase.Find(id);
            if (clase == null)
            {
                return HttpNotFound();
            }
            return View(clase);
        }

        // POST: Clase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Clase clase = db.Clase.Find(id);
            db.Clase.Remove(clase);
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
