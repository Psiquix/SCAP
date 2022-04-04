using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepReporte
    {
        IEnumerable<Orden> GetEntradas();
        IEnumerable<Orden> GetSalida();
        IEnumerable<Orden> mayorCantidadSalida();
    }
}
