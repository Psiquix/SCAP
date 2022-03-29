﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelDetalle> Items { get; private set; }

        //Implementación Singleton

        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly Carrito Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Carrito() { }

        /**
         * AgregarItem (): agrega un artículo a la compra
         */
        public String AgregarItem(int productoID)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelDetalle nuevoItem = new ViewModelDetalle(productoID);
            // Si este artículo ya existe en lista de libros, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.idProd == productoID))

                {
                    ViewModelDetalle item = Items.Find(x => x.idProd == productoID);
                    item.cantidad++;
                    item.Subtotal();
                }
                else
                {
                    nuevoItem.cantidad = 1;
                    nuevoItem.Subtotal();
                    Items.Add(nuevoItem);
                    
                }
                //mensaje = SweetAlertHelper.Mensaje("Movimiento con Producto", "Producto agregado a la orden", SweetAlertMessageType.success);

            }
            else
            {
                //mensaje = SweetAlertHelper.Mensaje("Movimiento con Producto", "El producto solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }


        /**
         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
         */
        public String SetItemCantidad(int productoID, int Cantidad)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            if (Cantidad == 0)
            {
                EliminarItem(productoID);
                //mensaje = SweetAlertHelper.Mensaje("Orden Libro", "Producto eliminado", SweetAlertMessageType.success);

            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelDetalle actualizarItem = new ViewModelDetalle(productoID);
                if (Items.Exists(x => x.idProd == productoID))
                {
                    ViewModelDetalle item = Items.Find(x => x.idProd == productoID);
                    item.cantidad = Cantidad;
                    //mensaje = SweetAlertHelper.Mensaje("Orden Detalle", "Cantidad actualizada", SweetAlertMessageType.success);

                }
            }
            return mensaje;

        }

        /**
         * EliminarItem (): elimina un artículo del carrito de compras
         */
        public String EliminarItem(long productoID)
        {
            String mensaje = "El libro no existe";
            if (Items.Exists(x => x.idProd == productoID))
            {
                var itemEliminar = Items.Single(x => x.idProd == productoID);
                Items.Remove(itemEliminar);
                //mensaje = SweetAlertHelper.Mensaje("Orden Libro", "Producto eliminado", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        /**
         * GetTotal() - Devuelve el precio total de todos los libros.
         */

        public int GetCountItems()
        {
            int total = 0;
            total = (int)Items.Sum(x => x.cantidad);

            return total;
        }
        /**
         * GetTotalPeso() - Devuelve el total de peso de todos los libros.
         */

        public void eliminarCarrito()
        {
            Items.Clear();

        }
    }
}