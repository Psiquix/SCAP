using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceTipoUnidad : IServiceTipoUnidad
    {
        public IEnumerable<TipoUnidad> GetListaTipoUnidad()
        {
            IRepTipoUnidad rep = new RepTipoUnidad();
            return rep.GetListaTipoUnidad();
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
