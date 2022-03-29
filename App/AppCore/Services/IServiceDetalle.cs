using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceDetalle
    {
        IEnumerable<Orden> GetOrdenes();


        Orden GetOrdenByID(long id);
        Orden Save(Orden inventario);

        void GetOrdenDate(out string etiquetas, out string valores, DateTime fechainicial, DateTime fechafinal);
    }
}
