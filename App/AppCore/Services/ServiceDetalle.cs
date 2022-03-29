using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Repository;

namespace AppCore.Services
{
    public class ServiceDetalle : IServiceDetalle
    {
        public Orden GetOrdenByID(long id)
        {
            IRepDetalle rep = new RepDetalle();
            return rep.GetOrdenByID(id);
        }

        public IEnumerable<Orden> GetOrdenes()
        {
            IRepDetalle rep = new RepDetalle();
            return rep.GetOrdenes();
        }

        public void GetOrdenDate(out string etiquetas1, out string valores1, DateTime fechainicial, DateTime fechafinal)
        {
            IRepDetalle repository = new RepDetalle();
            repository.GetOrdenDate(out string etiquetas, out string valores, fechainicial, fechafinal);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public Orden Save(Orden inventario)
        {
            IRepDetalle rep = new RepDetalle();
            return rep.Save(inventario);
        }
    }
}
