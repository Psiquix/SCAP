using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceReporte
    {
        IEnumerable<Orden> GetEntradas();
        IEnumerable<DetalleOrden> GetSalida();
        IEnumerable<DetalleOrden> mayorCantidadSalida();

    }
}
