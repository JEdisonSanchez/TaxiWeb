using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    public class AfiliacionController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();

        // GET: Afiliacion
        public ActionResult Index()
        {

            //Ejercicio 3  Según un rango de fechas, indicar las afiliaciones del mes que más se hayan registrado afiliados.
            var fechaInicio = new DateTime(2020, 1, 1);
            var fechaFin = new DateTime(2020, 12, 31);

            var agrupada = (from afiliado in db.Afiliacion
                         where afiliado.FechaRadicado >= fechaInicio
                         && afiliado.FechaRadicado <= fechaFin
                         group afiliado by afiliado.FechaRadicado.Month into afiliadoGrupo
                         select new
                         { 
                             Mes = afiliadoGrupo.Key,
                             Afiliados = afiliadoGrupo,
                             Cont = afiliadoGrupo.Count()
                         });

            var max = agrupada.Max(a => a.Cont);
            var lista = agrupada.Where(a => a.Cont == max).Select(a => a.Afiliados).FirstOrDefault();

            return View(lista.ToList());
        }

        // GET: Afiliacion/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            if (afiliacion == null)
            {
                return HttpNotFound();
            }
            return View(afiliacion);
        }

        // GET: Afiliacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Afiliacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaRadicado,Cedula,NombreCompleto,Edad,Valor")] Afiliacion afiliacion)
        {
            if (ModelState.IsValid)
            {
                db.Afiliacion.Add(afiliacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(afiliacion);
        }

        // GET: Afiliacion/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            if (afiliacion == null)
            {
                return HttpNotFound();
            }
            return View(afiliacion);
        }

        // POST: Afiliacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaRadicado,Cedula,NombreCompleto,Edad,Valor")] Afiliacion afiliacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(afiliacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(afiliacion);
        }

        // GET: Afiliacion/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            if (afiliacion == null)
            {
                return HttpNotFound();
            }
            return View(afiliacion);
        }

        // POST: Afiliacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            db.Afiliacion.Remove(afiliacion);
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
