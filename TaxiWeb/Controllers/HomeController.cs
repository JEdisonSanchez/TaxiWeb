using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public string Login(string usuario, string password)
        {
            var user = db.Login.FirstOrDefault(l => l.NombreUsuario == usuario && l.Password == password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.NombreUsuario, false);
                HttpContext.Session.Add("usuario", user.NombreUsuario);

                return Newtonsoft.Json.JsonConvert.SerializeObject(user);
                //return View("Index");
            }
            else
            {
                return "{\"error\":\"El usuario no Esiste\"}";
                //return View("Error");
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }



    }
}