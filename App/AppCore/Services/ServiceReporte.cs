using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceReporte : IServiceReporte
    {
        public IEnumerable<Orden> GetEntradas()
        {
            IRepReporte repository = new RepReporte();
            return repository.GetEntradas();
        }

        public IEnumerable<DetalleOrden> GetSalida()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetalleOrden> mayorCantidadSalida()
        {
            throw new NotImplementedException();
        }
    }
}
