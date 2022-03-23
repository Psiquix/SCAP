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



        [CustomAuthorize((int)Roles.Admin,(int)Roles.Emp )]
        public ActionResult Index()
        {
            IEnumerable<Marca> lista = null;
            try
            {
                IServiceMarca _ServiceProducto = new ServiceMarca();
                lista = _ServiceMarca.GetListaMarcaActive();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
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


        

        [HttpPost]
        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Save(Marca pMarca)
        {
            try
            {
                if (ModelState.IsValid)
                {
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

        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _ServiceMarca.Delete(id);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



        //Acceso solo admin 
        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult ListAll()
        {
            IEnumerable<Marca> lista = null;
            try
            {
                IServiceMarca _ServiceProducto = new ServiceMarca();
                lista = _ServiceMarca.GetListaMarcaAll();
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
            ViewBag.idTipoProd = ListaTipoProd();
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

        [HttpPost]
        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult SaveAll(Marca pMarca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ServiceMarca.Save(pMarca);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("Create", pMarca);
                }
                return RedirectToAction("ListAll");
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
    }
}
