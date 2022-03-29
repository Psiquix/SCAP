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
    public class CarritoController : Controller
    {
        public ActionResult Index()
        {
            return View(Carrito.Instancia.Items.ToList<ViewModelDetalle>());
        }
        public ActionResult IndexCatalogo()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
        public ActionResult Save(int? id)
        {
            Carrito.Instancia.AgregarItem((int)id);
            return RedirectToAction("IndexCatalogo");
        }
    }
}