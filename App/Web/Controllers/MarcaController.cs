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
    public class MarcaController : Controller
    {
        IServiceMarca _ServiceMarca = new ServiceMarca();



        //Pag de listado de marcas

        [CustomAuthorize((int)Roles.Admin,(int)Roles.Emp )]
        public ActionResult Index()
        {
            IEnumerable<Marca> lista = null;
            try
            {
                IServiceMarca _ServiceProducto = new ServiceMarca();
                lista = _ServiceMarca.GetListaMarca();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }


        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        //Pag de detalles de marca
        public ActionResult Details(int? id)
        {
            Marca oMarca = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oMarca = _ServiceMarca.GetMarcaById(id.Value);
                if (oMarca == null)
                {
                    TempData["Message"] = "No existe la marca solicitada";
                    TempData["Redirect"] = "Marca";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(oMarca);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Marca";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        //Pag de crear marca
        public ActionResult Create()
        {
            ViewBag.idTipoProd = ListaTipoProd();
            return View();
        }


        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]

        private SelectList ListaTipoProd()
        {
            IServiceTipoProd rep = new ServiceTipoProd();
            IEnumerable<TipoProducto> lista = rep.GetListaTipoActive();
            return new SelectList(lista, "id", "descripcionTipo");
        }

        
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        //Pag de edicion de marca
        public ActionResult Edit(int? id)
        {
            ViewBag.idTipoProd = ListaTipoProd();
            //Marca oMarca = _ServiceMarca.GetMarcaById(id);
            //if (oMarca == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //return View(oMarca);
            Marca oMarca = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oMarca = _ServiceMarca.GetMarcaById(id.Value);

                if (oMarca == null)
                {
                    TempData["Message"] = "No existe la marca solicitada";
                    TempData["Redirect"] = "Marca";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                return View(oMarca);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Marca";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Marca/Create
        [HttpPost]
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        //Pag de inicio
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

        

        [HttpPost]
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Save(Marca pMarca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //TipoProducto oType = _ServiceTipo.Save(pType);
                    _ServiceMarca.Save(pMarca);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("Create", pMarca);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Marca";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Marca/Edit/5
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

        // GET: Marca/Delete/5
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _ServiceMarca.Delete(id);
                return RedirectToAction("Index");
            }
            //else
            //{

            //}

            return RedirectToAction("Index");
        }

        // POST: Marca/Delete/5
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
