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
    public class TipoProductoController : Controller
    {
        IServiceTipoProd _ServiceTipo = new ServiceTipoProd();
   

        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Index()
        {
            IEnumerable<TipoProducto> lista = null;
            try
            {
                IServiceTipoProd _ServiceProducto = new ServiceTipoProd();
                lista = _ServiceTipo.GetListaTipoActive();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }

        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Create()
        {
            return View();
        }


        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Edit(int? id)
        {
            TipoProducto oType = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oType = _ServiceTipo.GetTipoById(id.Value);

                if (oType == null)
                {
                    TempData["Message"] = "No existe el tipo solicitado";
                    TempData["Redirect"] = "TipoProducto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                return View(oType);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "TipoProducto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [HttpPost]
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Save(TipoProducto pType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TipoProducto oType = _ServiceTipo.Save(pType);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);
                    return View("Create", pType);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "TipoProducto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _ServiceTipo.Delete(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //Acceso solo admin
        [HttpPost]
        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult SaveAll(TipoProducto pType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TipoProducto oType = _ServiceTipo.Save(pType);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);
                    return View("Create", pType);
                }
                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "TipoProducto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult ListAll()
        {
            IEnumerable<TipoProducto> lista = null;
            try
            {
                IServiceTipoProd _ServiceProducto = new ServiceTipoProd();
                lista = _ServiceTipo.GetListaTipoAll();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }

        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult EditAll(int? id)
        {
            TipoProducto oType = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oType = _ServiceTipo.GetTipoById(id.Value);

                if (oType == null)
                {
                    TempData["Message"] = "No existe el tipo solicitado";
                    TempData["Redirect"] = "TipoProducto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                return View(oType);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "TipoProducto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


    }
}
