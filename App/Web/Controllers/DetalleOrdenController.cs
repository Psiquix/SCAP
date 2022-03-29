using Infrastructure.Models;
using AppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
using System.Reflection;
using Web.ViewModel;

namespace Web.Controllers
{
    public class DetalleOrdenController : Controller
    {

        // GET: Encabezado_Inventario
        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult Index()
        {
            IEnumerable<Orden> lista = null;
            try
            {
                IServiceDetalle _ServiceProducto = new ServiceDetalle();
                lista = _ServiceProducto.GetOrdenes();


            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }

        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult IndexOrden()
        {
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            return View();
        }

        // GET: Encabezado_Inventario/Details/5
        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult Details(long? id)
        {
            ServiceDetalle _ServiceD = new ServiceDetalle();
            Orden encabezado = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                encabezado = _ServiceD.GetOrdenByID(id.Value);
                if (encabezado == null)
                {
                    return RedirectToAction("Index");
                }
                return View(encabezado);

            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
        }

        // GET: Encabezado_Inventario/Create
        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult Create()
        {
            return View();
        }

        // POST: Encabezado_Inventario/Create
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

        // GET: Encabezado_Inventario/Edit/5
        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Encabezado_Inventario/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Admin)]

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

        // GET: Encabezado_Inventario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Encabezado_Inventario/Delete/5
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

        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult Save(Orden inventario)
        {

            try
            {

                // Si no existe la sesión no hay nada
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    //TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Aviso", "Primero seleccione productos del inventario", SweetAlertMessageType.warning);
                    return RedirectToAction("IndexEncabezado");
                }
                else
                {
                    if (inventario != null)
                    {

                            IServiceProducto serviceProdu = new ServiceProducto();
                            Producto pro = new Producto();
                            var listaDetalle = Carrito.Instancia.Items;

                            foreach (var item in listaDetalle)
                            {
                                DetalleOrden detalle = new DetalleOrden();
                                detalle.idProd = item.idProd;
                                detalle.subtotal = item.precio;
                                detalle.cantidad = item.cantidad;
                                pro = serviceProdu.GetProductoByID(detalle.idProd);
                                inventario.fecha = DateTime.Now;
                                //inventario.total = (long)Carrito.Instancia.GetTotal();
                                inventario.DetalleOrdens.Add(detalle);
                                serviceProdu.actualizarCantidad(detalle.idProd, (int)detalle.cantidad, (int)inventario.id);
                            }

                            IServiceDetalle _ServiceMovimiento = new ServiceDetalle();
                            Orden inventarioSave = _ServiceMovimiento.Save(inventario);

                            Carrito.Instancia.eliminarCarrito();
                            //TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Orden", "Orden guardada satisfactoriamente!", SweetAlertMessageType.success);
                        

                    }
                    else
                    {
                        //TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Aviso", "Falta un combo por seleccionar", SweetAlertMessageType.warning);
                    }

                }

                // Reporte orden
                return RedirectToAction("IndexEncabezado");

            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexEncabezado";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult actualizarCantidad(int idProducto, int cantidad)
        {
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idProducto, cantidad);
            TempData.Keep();
            return PartialView("Detalle", Carrito.Instancia.Items);

        }

        //Actualizar solo la cantidad de libros que se muestra en el menú
        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Carrito.Instancia.Items.Count();
            return PartialView("Detalle");

        }

        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult ordenarProducto(int? idProducto)
        {
            int cantidadLibros = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idProducto);
            return PartialView("OrdenCantidad");
        }

        //Actualizar solo la cantidad de libros que se muestra en el menú
        [CustomAuthorize((int)Roles.Admin)]

        public ActionResult eliminarProducto(long? idProducto)
        {
            try
            {
                ViewBag.NotificationMessage = Carrito.Instancia.EliminarItem((long)idProducto);
                return PartialView("DetalleOrden", Carrito.Instancia.Items);
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return PartialView("DetalleOrden", Carrito.Instancia.Items);
        }
        //public ActionResult graficoMovimientos()
        //{
        //    ViewModelGrafico grafico = new ViewModelGrafico();
        //    string etiquetas = "";
        //    string valores = "";
        //    grafico.Etiquetas = etiquetas;
        //    grafico.Valores = valores;
        //    int cantidadValores = valores.Split(',').Length;
        //    grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
        //    grafico.titulo = "Ordenes por fecha";
        //    grafico.tituloEtiquetas = "Cantidad de movimientos registrados en el rango establecido";

        //    grafico.tipo = "polarArea";
        //    ViewBag.grafico = grafico;
        //    return View();
        //}
        //public ActionResult graficoEncabezado(DateTime fechaInicio, DateTime fechaFinal)
        //{
        //    //Documentación chartjs https://www.chartjs.org/docs/latest/
        //    IServiceMovimiento _ServicioMovimiento = new ServiceMovimiento();
        //    ViewModelGrafico grafico = new ViewModelGrafico();
        //    string etiquetas = "";
        //    string valores = "";
        //    _ServicioMovimiento.GetMovimientoCountDate(out etiquetas, out valores, fechaInicio, fechaFinal);
        //    grafico.Etiquetas = etiquetas;
        //    grafico.Valores = valores;
        //    int cantidadValores = valores.Split(',').Length;
        //    grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
        //    grafico.titulo = "Ordenes por fecha";
        //    grafico.tituloEtiquetas = "Cantidad de movimientos registrados en el rango establecido";

        //    grafico.tipo = "polarArea";
        //    ViewBag.grafico = grafico;
        //    return View("graficoMovimientos");
        //}

        public ActionResult appearEntrada()
        {
            return PartialView("_Entrada");
        }

        public ActionResult AppearSalida()
        {
            return PartialView("_Salida");
        }
    }
}

