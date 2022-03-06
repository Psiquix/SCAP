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
    public class RepTipoProd : IRepTipoProd
    {
        public void Delete(int pId)
        {
            int salida;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    /* La carga diferida retrasa la carga de datos relacionados,
                     * hasta que lo solicite específicamente.*/
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoProducto oTipo = new TipoProducto()
                    {
                        id = pId
                    };
                    ctx.Entry(oTipo).State = EntityState.Deleted;
                    salida = ctx.SaveChanges();
                }
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

        public IEnumerable<TipoProducto> GetListaTipo()
        {
            IEnumerable<TipoProducto> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.TipoProductoes.Include(x=> x.Productoes).ToList<TipoProducto>();
            }
            return lista;
        }

        public TipoProducto GetTipoById(int id)
        {
            TipoProducto oTipo = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipo = ctx.TipoProductoes.Find(id);
                }
                return oTipo;
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

        public TipoProducto Save(TipoProducto tipo)
        {
            int salida = 0;
            TipoProducto oTipo = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipo = GetTipoById(tipo.id);
                    if (oTipo == null)
                    {
                        ctx.TipoProductoes.Add(tipo);
                    }
                    else
                    {
                        ctx.Entry(tipo).State = EntityState.Modified;
                        //Si existiera, actualiza los datos
                    }
                    salida = ctx.SaveChanges();
                }
                if (salida >= 0)
                {
                    oTipo = GetTipoById(tipo.id);
                }
                return oTipo;
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
