using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepReporte : IRepReporte
    {
        //Get ordenes activas
        public IEnumerable<Orden> GetEntradas()
        {
            IEnumerable<Orden> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Ordens.ToList<Orden>();

            }
            return lista;
        }

        public IEnumerable<Orden> GetSalida()
        {
            IEnumerable<Orden> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Ordens.ToList<Orden>();
            }
            return lista;
        }

        public IEnumerable<Orden> mayorCantidadSalida()
        {
            throw new NotImplementedException();
        }
    }
}
