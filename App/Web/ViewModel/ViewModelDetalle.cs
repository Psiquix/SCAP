using AppCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelDetalle
    {
        public int idOrden { get; set; }
        public int idProd { get; set; }
        public double subtotal { get; set; }
        public int cantidad { get; set; }

        public double precio
        {
            get { return (double)Producto.precioUnitario; }
        }
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }

        public void Subtotal()
        {
            subtotal = cantidad * precio;
        }


        public ViewModelDetalle(int IdProducto)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            this.idProd = IdProducto;
            this.Producto = _ServiceProducto.GetProductoByID(IdProducto);
        }
    }
}