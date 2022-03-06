using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepTipoUnidad : IRepTipoUnidad
    {
        public IEnumerable<TipoUnidad> GetListaTipoUnidad()
        {
            IEnumerable<TipoUnidad> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.TipoUnidads.ToList<TipoUnidad>();
            }
            return lista;
        }

        public TipoUnidad GetTipoUnidadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(TipoUnidad tipo)
        {
            throw new NotImplementedException();
        }
    }
}
