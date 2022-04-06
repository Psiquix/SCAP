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

        public void actualizarCantidad(long id, int cantidad, long tipoMovimiento)
        {
            using (MyContext cdt = new MyContext())
            {
                try
                {
                    Producto oldProd = GetProductoByID(id);

                    if (tipoMovimiento == 1)
                    {
                        oldProd.cantidadNum += cantidad;

                    }
                    else
                    {
                        oldProd.cantidadNum -= cantidad;

                    }

                    cdt.Productoes.Add(oldProd);
                    cdt.Entry(oldProd).State = EntityState.Modified;
                    cdt.SaveChanges();
                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;

                }
            }
        }

        public void Disable(int id)
        {
            int salida = 0;
            Producto oProd = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProd = GetProductoByID(id);
                    if (oProd != null)
                    {
                        oProd.estado = false;
                        ctx.Entry(oProd).State = EntityState.Modified;
                    }
                    salida = ctx.SaveChanges();
                }
                if (salida >= 0)
                {
                    oProd = GetProductoByID(id);
                }
            }
            catch (Exception e)
            {
                string mensaje = "Error" + e.Message;
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

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
            int salida = 0;
            Producto oProd = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        oProd = GetProductoByID(prod.id);
                        if (oProd == null)
                        {
                            prod.estado = true;
                            ctx.Productoes.Add(prod);
                        }
                        else
                        {
                            ctx.Entry(prod).State = EntityState.Modified;
                            //Si existiera, actualiza los datos
                        }
                        salida = ctx.SaveChanges();
                }
                    if (salida >= 0)
                    {
                oProd = GetProductoByID(prod.id);
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
