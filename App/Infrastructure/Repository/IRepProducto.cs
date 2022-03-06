using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(long id);
        void Save(Producto prod);
        IEnumerable<Producto> GetProductoByNombre(String nombre);
        //void Update();
        //voidn Disable()
    }
}
