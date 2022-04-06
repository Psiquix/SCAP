using Infrastructure.Models;
using AppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using System.Reflection;
using System.IO;
using Web.Security;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Create()
        {
            return View();
        }

        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        private SelectList ListaRoles()
        {
            IServiceRol _Service = new ServiceRol();
            IEnumerable<Rol> lista = _Service.GetListaRol();
            return new SelectList(lista, "id", "descripcion");
        }
        // POST: Usuario/Create
        [HttpPost]
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Create(Usuario pUsuario)
        {
            ViewBag.idRol = ListaRoles();

            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {

                if (ModelState.IsValid)
                {
                    Usuario oUsuario = _ServiceUsuario.Save(pUsuario);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Unauthorized()
        {
            try
            {
                ViewBag.Message = "No se encuentra autorizado";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario; //Siempre hay que parsear porque la sesión no lo reconoce
                    Log.Warn($"El usuario {oUsuario.nombre} {oUsuario.apellidos} con el rol {oUsuario.Rol.id}-{oUsuario.Rol.descripcion}, intentó acceder una página sin los permisos requeridos.");
                }
                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult LogOut()
        {
            try
            {
                Log.Info("La sesión se ha cerrado correctamente.");
                Session["User"] = null;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Usuario/Edit/5

        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
