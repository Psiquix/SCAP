using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepRol : IRepRol
    {
        public IEnumerable<Rol> GetListaRol()
        {
            IEnumerable<Rol> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                //   lista = ctx.Productos.Include(x => x.idMarca).ToList<Producto>();
                lista = ctx.Rols.ToList<Rol>();
            }
            return lista;
        }
    }
}
