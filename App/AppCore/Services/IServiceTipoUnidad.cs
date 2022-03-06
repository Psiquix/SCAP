using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceTipoUnidad
    {
        IEnumerable<TipoUnidad> GetListaTipoUnidad();
        TipoUnidad GetTipoUnidadById(int id);
        void Save(TipoUnidad tipo);
    }
}
