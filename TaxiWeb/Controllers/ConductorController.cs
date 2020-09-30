using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    public class ConductorController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();

        // GET: Conductor
        public ActionResult Index()
        {
            //var lista = db.Conductor.Where(q => q.Nombre.StartsWith("J"));
            //var fechamin = db.Conductor.Min(q => q.FechaNacimiento);
            //var lista = (from cond in db.Conductor
            //             where fechamin.Value == cond.FechaNacimiento.Value
            //             select cond);
            //var lista = (from cond in db.Conductor
            //             join veh in db.Vehiculo on cond.Id equals veh.IdConductor
            //             select cond);

            //Ejercicio 1  Conductores cuya licencia de conducción ya venció
            //var lista = db.Conductor.Where(q => q.ExpiracionLicencia < DateTime.Now);

            //Ejercicio 2  Conductores con licencia de conducción próxima a vencer (rango de X días)
            //var fechaActual = DateTime.Now;
            //var rango = DateTime.Now.AddDays(20);
            //var lista = db.Conductor.Where(q => q.ExpiracionLicencia > fechaActual && q.ExpiracionLicencia < rango);

            //var condIgual = (from c in db.Conductor
            //                 join a in db.Afiliacion on c.Cedula equals a.Cedula
            //                 select c.Cedula);

            //var lista = from cond in db.Conductor
            //           where !(condIgual).Contains(cond.Cedula)
            //           select cond;
            //-------------------------------------------------------------------------
            //Listar conductores que aparecen mínimo 2 veces inscritos en clases.
            //var claseAgrupada = (from clase in db.Clase
            //                       group clase by clase.IdConductor into grupoClases
            //                       select grupoClases);

            //claseAgrupada = claseAgrupada.Where(c => c.Count() >= 2);

            //var idConductores = claseAgrupada.Select(cA => cA.Key);

            //var lista = (from conductor in db.Conductor
            //             where idConductores.Contains(conductor.Id)
            //             select conductor);
            //-----------------------------------------------------------------------

            var conductor = db.Conductor.ToList();

            var lista = (from c in db.Conductor
                         join a in db.Afiliacion on c.Cedula equals a.Cedula
                         select new ConductorAfiliacion
                         {
                             Conductor = c,
                             Radicado = a.Id
                         }).Union(from c in db.Conductor
                                  join a in db.Afiliacion on c.Cedula equals a.Cedula into afilia
                                  from joinAfiliacion in afilia.DefaultIfEmpty()
                                  where joinAfiliacion == null
                                  select new ConductorAfiliacion
                                  {
                                      Conductor = c,
                                      Radicado = 0
                                  });
            
            return View(lista.ToList());

        }

        // GET: Conductor/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conductor conductor = db.Conductor.Find(id);
            if (conductor == null)
            {
                return HttpNotFound();
            }
            return View(conductor);
        }

        // GET: Conductor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Conductor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,Cedula,FechaNacimiento,LicenciaConduccion,ExpiracionLicencia")] Conductor conductor)
        {
            if (ModelState.IsValid)
            {
                db.Conductor.Add(conductor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conductor);
        }

        // GET: Conductor/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conductor conductor = db.Conductor.Find(id);
            if (conductor == null)
            {
                return HttpNotFound();
            }
            return View(conductor);
        }

        // POST: Conductor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,Cedula,FechaNacimiento,LicenciaConduccion,ExpiracionLicencia")] Conductor conductor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conductor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conductor);
        }

        // GET: Conductor/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conductor conductor = db.Conductor.Find(id);
            if (conductor == null)
            {
                return HttpNotFound();
            }
            return View(conductor);
        }

        // POST: Conductor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Conductor conductor = db.Conductor.Find(id);
            db.Conductor.Remove(conductor);
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
