using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(long id);
        void Save(Producto prod);
        IEnumerable<Producto> GetProductoByNombre(String nombre);
        void actualizarCantidad(long id, int cantidad, long tipoMovimiento);
        void Disable(int id);

    }
}
