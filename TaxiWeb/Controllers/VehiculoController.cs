using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    public class VehiculoController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();

        // GET: Vehiculo
        public ActionResult Index()
        {
            var vehiculo = db.Vehiculo.Include(v => v.Conductor);

            ////Ejercicio 4  Seleccionar el vehículo con la placa más alta (según su valor Alfanumérico)
            //var placa = vehiculo.Max(v => v.Placa);
            //vehiculo = vehiculo.Where(v => v.Placa == placa);

            // Listar los vehículos con conductores que más veces aparecen asociados en vehículo
            //var grupoVehiculo = (from v in db.Vehiculo
            //             group v by v.Conductor into vehiculogrupo
            //             select new
            //             {
            //                 vehiculos = vehiculogrupo,
            //                 conteo = vehiculogrupo.Count()
            //             });
            //var max = grupoVehiculo.Max(a => a.conteo);
            //var lista = grupoVehiculo.Where(a => a.conteo == max).Select(a => a.vehiculos).FirstOrDefault();


            return View(vehiculo.ToList());
        }

        // GET: Vehiculo/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            var conductores = (from conductor in db.Conductor
                               select new
                               {
                                   id = conductor.Id,
                                   NombreCompleto = conductor.Nombre + " " + conductor.Apellido
                               });
            ViewBag.IdConductor = new SelectList(conductores, "Id", "NombreCompleto");
            return View();
        }

        // POST: Vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Placa,TipoVehiculo,Marca,Color,IdConductor")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                vehiculo.Placa = vehiculo.Placa.ToUpper();
                db.Vehiculo.Add(vehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Placa,TipoVehiculo,Marca,Color,IdConductor")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // GET: Vehiculo/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            db.Vehiculo.Remove(vehiculo);
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
