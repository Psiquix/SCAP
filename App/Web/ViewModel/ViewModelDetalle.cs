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
        public double IMP = 0.13;
        public int idOrden { get; set; }
        public int idProd { get; set; }
        public double Subtotal { get; set; }
        public int Cantidad { get; set; }

        public double precio
        {
            get { return (double)Producto.precioUnitario; }
        }
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }

        public void CalcSubtotal()
        {
            Subtotal = Cantidad * precio;
        }


        public ViewModelDetalle(int IdProducto)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            this.idProd = IdProducto;
            this.Producto = _ServiceProducto.GetProductoByID(IdProducto);
        }
    }
}