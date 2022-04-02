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
    public class RepTipoCita : IRepTipoCita
    {
        public IEnumerable<TipoCita> GetListaTipoCita()
        {
            IEnumerable<TipoCita> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.TipoCitas.Include(c => c.Citas).ToList<TipoCita>();
                //lista = ctx.TipoCita.Include(c => c.Cita).ToList<TipoCita>();
            }
            return lista;
        }

        public TipoCita GetTipoCitaById(int id)
        {
            TipoCita oTipo = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipo = ctx.TipoCitas.Find(id);
                    //oTipo = ctx.TipoCita.Find(id);
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
    }
}
