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
    public class RepMarca : IRepMarca
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
                    Marca oMarca = GetMarcaById(pId);
                    oMarca.estado = false;
                    ctx.Entry(oMarca).State = EntityState.Modified;
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

        public IEnumerable<Marca> GetListaMarcaActive()
        {
            IEnumerable<Marca> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Marcas.Where(x => x.estado == true).ToList<Marca>();
            }
            return lista;
        }
        public IEnumerable<Marca> GetListaMarcaAll()
        {
            IEnumerable<Marca> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Marcas.ToList<Marca>();
            }
            return lista;
        }
        public Marca GetMarcaById(int id)
        {
            Marca oMarca = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMarca = ctx.Marcas.Find(id);// ctx.Marca.Find(id);
                }
                return oMarca;
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

        public void Save(Marca marca)
        {
            int salida = 0;
            Marca oMarca = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMarca = GetMarcaById(marca.id);
                    if (oMarca == null)
                    {
                        marca.estado = true;
                        ctx.Marcas.Add(marca);
                    }
                    else
                    {
                        ctx.Entry(marca).State = EntityState.Modified;
                        //Si existiera, actualiza los datos
                    }
                    salida = ctx.SaveChanges();
                }
                if (salida >= 0)
                {
                    oMarca = GetMarcaById(marca.id);
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
