using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepProducto : IRepProducto
    {

        IRepTipoProd rTipoProducto = new RepTipoProd();
        IRepTipoUnidad rTipoUnidad = new RepTipoUnidad();
        IRepMarca rMarca = new RepMarca();

        public IEnumerable<Producto> GetProducto()
        {
            IEnumerable<Producto> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                //   lista = ctx.Productos.Include(x => x.idMarca).ToList<Producto>();
                lista = ctx.Productoes.ToList<Producto>();
            }
            return lista;
        }

        public Producto GetProductoByID(long id)
        {
            Producto oProducto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = ctx.Productoes.
                    Where(p => p.id == id).FirstOrDefault();
            }
            return oProducto;
        }

        public IEnumerable<Producto> GetProductoByNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public void Save(Producto prod)
        {
            Producto prodExist = GetProductoByID(prod.id);


            Marca mar;
            TipoProducto tipoP;
            TipoUnidad tipoU;

            using (MyContext cxt = new MyContext())
            {
                cxt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    if (prodExist == null)
                    {
                        //si producto no existe
                        prod.estado = true;

                        //salva el producto
                        cxt.Productoes.Add(prod);

                        cxt.SaveChanges();
                    }
                    else
                    {
                        //cxt.Productoes.Add(prod);
                        //cxt.Entry(prod).State = EntityState.Modified;


                        ////actualiza los proveedores a la tabla intermedia
                        //var proveedoresLista = new HashSet<string>(proveedor);
                        //cxt.Entry(prod).Collection(p => p.Proveedor).Load();
                        //var nuevoProveedorLista = cxt.Proveedor.Where(x => proveedoresLista.Contains(x.id.ToString())).ToList();
                        //prod.Proveedor = nuevoProveedorLista;
                        //cxt.Entry(prod).State = EntityState.Modified;

                        ////actualiza la tabla intermedia de ubicacion

                        //if (sucursal != null)
                        //{
                        //    //Obtener los estantes registrados del producto a modificar
                        //    List<Sucursal_Producto> sucursalesdelProducto = cxt.Sucursal_Producto.Where(x => x.idProducto == prod.id).ToList();
                        //    // Borrar los estantes existentes del producto
                        //    foreach (var item in sucursalesdelProducto)
                        //    {
                        //        prod.Sucursal_Producto.Remove(item);
                        //    }
                        //    //Registrar los estantes especificados
                        //    foreach (var suc in sucursal)
                        //    {
                        //        Sucursal_Producto su = new Sucursal_Producto();
                        //        su.idProducto = prod.id;
                        //        su.idSucursal = long.Parse(suc);
                        //        su.cantidad = prod.cantidadExistente;
                        //        cxt.Sucursal_Producto.Add(su);
                        //    }

                        //    cxt.SaveChanges();
                        //}
                    }
                }
                catch (Exception e)
                {
                    string mensaje = "Error" + e.Message;
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }

        }
    }
}
