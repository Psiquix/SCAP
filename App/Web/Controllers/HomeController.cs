using AppCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace SCAP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Usuario user)
        {

            IServiceUsuario _ServiceUser = new ServiceUsuario();
            Usuario oUser = null;
            try
            {
                
                if (ModelState.IsValid)
                {
                    oUser = _ServiceUser.LogIn(user.email, user.contrasena);
                    
                    if (oUser != null)
                    {
                        Session["User"] = oUser;
                        Log.Info($"Accede {oUser.nombre}");
                       // ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Bienvenido a AFK", "un gusto tenerte de vuelta", SweetAlertMessageType.success);
                        return RedirectToAction("Index");

                    }
                    else
                    {
                       // Log.Warn($"{usuario.correo} se intentó conectar  y falló");
                       ///ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Ups! no se ha podido crear su sesión", "revise sus credenciales e intente de nuevo", SweetAlertMessageType.warning);

                    }
                }

                return View("LogIn");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
               Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
    }
}