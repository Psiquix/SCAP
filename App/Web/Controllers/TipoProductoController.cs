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

namespace Web.Controllers
{
    public class TipoProductoController : Controller
    {
        IServiceTipoProd _ServiceTipo = new ServiceTipoProd();
        // GET: TipoProducto
        public ActionResult Index()
        {
            IEnumerable<TipoProducto> lista = null;
            try
            {
                IServiceTipoProd _ServiceProducto = new ServiceTipoProd();
                lista = _ServiceTipo.GetListaTipo();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }

        // GET: TipoProducto/Details/5
        public ActionResult Details(int? id)
        {
            TipoProducto oTipo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oTipo = _ServiceTipo.GetTipoById(id.Value);
                if (oTipo == null)
                {
                    TempData["Message"] = "No existe el tipo solicitado";
                    TempData["Redirect"] = "TipoProducto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(oTipo);
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

        // GET: TipoProducto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoProducto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            //TipoProducto oType = _ServiceTipo.GetTipoById(id);

            //if (oType == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //return View(oType);
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
        public ActionResult Save(TipoProducto pType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TipoProducto oType = _ServiceTipo.Save(pType);
                    //_ServiceTipo.Save(pType);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
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

        // POST: TipoProducto/Edit/5
        [HttpPost]
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

        // GET: TipoProducto/Delete/5
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _ServiceTipo.Delete(id);
                return RedirectToAction("Index");
            }
            //else
            //{

            //}

            return RedirectToAction("Index");
        }

        // POST: TipoProducto/Delete/5
        [HttpPost]
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
