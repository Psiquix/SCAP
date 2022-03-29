using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public void actualizarCantidad(long id, int cantidad, long tipoMovimiento)
        {
            IRepProducto rep = new RepProducto();
            rep.actualizarCantidad(id,cantidad,tipoMovimiento);
        }

        public IEnumerable<Producto> GetProducto()
        {
            IRepProducto rep = new RepProducto();
            return rep.GetProducto();
        }

        public Producto GetProductoByID(long id)
        {
            IRepProducto rep = new RepProducto();
            return rep.GetProductoByID(id);
        }

        public IEnumerable<Producto> GetProductoByNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public void Save(Producto prod)
        {
            IRepProducto rep = new RepProducto();
            rep.Save(prod);
        }
    }
}
