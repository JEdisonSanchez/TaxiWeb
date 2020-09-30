using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    public class HomeController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario, string password)
        {
            var comparacion = db.Login.Where(l => l.NombreUsuario == usuario && l.Password == password).FirstOrDefault();

            if(comparacion != null)
            {
                return View("Index");
            }
            else
            {
                return View("Error");
            }
            
        }

        
        
    }
}