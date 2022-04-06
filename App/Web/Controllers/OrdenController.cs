using AppCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
using Web.ViewModel;

namespace Web.Controllers
{
    public class OrdenController : Controller
    {
        IServiceOrden _ServiceOrden = new ServiceOrden();

        public double localImpuesto = 0.13;



        [CustomAuthorize((int)Roles.Admin, (int)Roles.Emp)]
        public ActionResult Index()
        {
            IEnumerable<Orden> lista = null;
            try
            {
                IServiceMarca _ServiceProducto = new ServiceMarca();
                lista = _ServiceOrden.GetListaOrdenAll();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            ViewBag.List = Carrito.Instancia.Items;
            return View();
        }

        [CustomAuthorize((int)Roles.Emp, (int)Roles.Admin)]
        public ActionResult Completar(int? id)
        {
            Orden oOrden = _ServiceOrden.GetOrdenById(id.Value);
            oOrden.estado = false;
            _ServiceOrden.Save(oOrden);
            return RedirectToAction("Index");
        }

        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult Edit(int? id)
        {
            Orden oOrden = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oOrden = _ServiceOrden.GetOrdenById(id.Value);

                if (oOrden == null)
                {
                    TempData["Message"] = "No existe la marca solicitada";
                    TempData["Redirect"] = "Marca";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                return View(oOrden);
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

        public ActionResult Cancel()
        {
            Carrito.Instancia.eliminarCarrito();
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public ActionResult Save(Orden orden)
        {
            Orden oOrden = _ServiceOrden.GetOrdenById(orden.id);
            try
            {
                if (ModelState.IsValid)
                {
                    if (oOrden != null)
                    {
                        orden.total = oOrden.total;
                        orden.fecha = oOrden.fecha;
                        orden.impuesto = oOrden.impuesto;
                    }
                    else
                    {
                        double subtotal = 0;
                        foreach (var item in Carrito.Instancia.Items)
                        {
                            subtotal += item.Subtotal;
                        }
                        orden.impuesto = localImpuesto;
                        if (orden.total != 0)
                        {
                            orden.total = subtotal + (subtotal * localImpuesto);
                        }
                        orden.estado = true;
                        orden.fecha = DateTime.Now;
                    }
                    _ServiceOrden.Save(orden);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);
                    return View("Create", orden);
                }
                return RedirectToAction("ConfirmacionOrden");
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

        public ActionResult ConfirmacionOrden()
        {
            return View();
        }
    }
}