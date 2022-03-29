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
    public class RepOrden : IRepOrden
    {
        public void Completar(int pId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Orden> GetListaOrdenActiva()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Orden> GetListaOrdenAll()
        {
            IEnumerable<Orden> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Ordens.ToList<Orden>();
            }
            return lista;
        }

        public Orden GetOrdenById(int id)
        {
            Orden Orden = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Orden = ctx.Ordens.Find(id);// ctx.Marca.Find(id);
                }
                return Orden;
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
                throw;
            }
        }

        public void Save(Orden orden)
        {
            int salida = 0;
            Orden oOrden = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oOrden = GetOrdenById(orden.id);
                    if (oOrden == null)
                    {
                        orden.estado = true;
                        ctx.Ordens.Add(orden);
                    }
                    else
                    {
                        ctx.Entry(orden).State = EntityState.Modified;
                        //Si existiera, actualiza los datos
                    }
                    salida = ctx.SaveChanges();
                }
                if (salida >= 0)
                {
                    oOrden = GetOrdenById(orden.id);
                }
                //return oMarca;
            }
            catch (DbUpdateException dbEx)
            {
                string msj = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw new Exception(msj);
            }
            catch (Exception ex)
            {
                string msj = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw;
            }
        }
    }
}
