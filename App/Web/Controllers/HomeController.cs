using AppCore.Services;
using Infrastructure.Models;
using SweetAlert.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Util;
using Web.Utils;
using WhatsAppApi;

namespace SCAP.Controllers
{
    public class HomeController : Controller
    {
        //Pag de inicio
        public ActionResult Index()
        {
            return View();
        }

        //Pag de Sobre nosotros
        public ActionResult About()
        {
            return View();
        }

        //Pag de contactos
        public ActionResult Contact()
        {
            return View();
        }


        //Pag de Log In
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        //Pag de inicio, resultado de log In
        public ActionResult LogIn(Usuario user)
        {
            IServiceUsuario _ServiceUser = new ServiceUsuario();
            SweetBaseController sweetBaseController = new SweetBaseController();    
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
                        //ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Bienvenido a AFK", "un gusto tenerte de vuelta", SweetAlertMessageType.success);
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


        //Pag no autorizado
        public ActionResult UnAuthorized()
        {
            try
            {
                ViewBag.Message = "No autorizado";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    Log.Warn($"El usuario {oUsuario.nombre}");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "LogIn";
                TempData["Redirect-Action"] = "Home";
                return RedirectToAction("Default", "Error");
            }
        }
    }
}