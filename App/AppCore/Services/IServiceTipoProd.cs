using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceTipoProd
    {
        IEnumerable<TipoProducto> GetListaTipoActive();
        IEnumerable<TipoProducto> GetListaTipoAll();
        TipoProducto GetTipoById(int id);
        TipoProducto Save(TipoProducto tipo);
        void Delete(int pId);
    }
}
