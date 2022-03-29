using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepDetalle : IRepDetalle
    {
        public IEnumerable<Orden> GetOrdenes()
        {
            IEnumerable<Orden> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Ordens.ToList<Orden>();      
            }
            return lista;
        }
        
        public Orden GetOrdenByID(long id)
        {
            Orden inventario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    inventario = ctx.Ordens.Where(p => p.id == id).FirstOrDefault<Orden>();

                }
                return inventario;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public void GetOrdenDate(out string etiquetas, out string valores, DateTime fechainicial, DateTime fechafinal)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var resultado = ctx.Ordens.GroupBy(x => x.fecha).
                               Select(o => new {
                                   Count = o.Count(),
                                   FechaOrden = o.Key
                               }).Where(d => d.FechaOrden >= fechainicial && d.FechaOrden <= fechafinal);
                    foreach (var item in resultado)
                    {
                        varEtiquetas += String.Format("{0:dd/MM/yyyy}", item.FechaOrden) + ",";
                        varValores += item.Count + ",";
                    }
                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public Orden Save(Orden inventario)
        {
            int resultado = 0;
            Orden orden = null;
            try
            {
                // Salvar pero con transacción porque son 2 tablas
                // 1- Orden
                // 2- OrdenDetalle 
                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.Ordens.Add(inventario);
                        resultado = ctx.SaveChanges();
                        foreach (var detalle in inventario.DetalleOrdens)
                        {
                            detalle.idOrden = inventario.id;
                        }
                        foreach (var item in inventario.DetalleOrdens)
                        {
                            // Busco el producto que está en el detalle por IdLibro
                            Producto oProducto = ctx.Productoes.Find(item.idProd);

                            // Se indica que se alteró
                            ctx.Entry(oProducto).State = EntityState.Modified;
                            // Guardar                         
                            resultado = ctx.SaveChanges();
                        }

                        // Commit 
                        transaccion.Commit();
                    }
                }

                // Buscar la orden que se salvó y reenviarla
                if (resultado >= 0)
                    orden = GetOrdenByID(inventario.id);


                return orden;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }
    }
}
