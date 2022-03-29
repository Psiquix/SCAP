using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceOrden : IServiceOrden
    {

        public void Completar(int pId)
        {
            IRepOrden rep = new RepOrden();
            rep.Completar(pId);
        }

        public IEnumerable<Orden> GetListaOrdenActiva()
        {
            IRepOrden rep = new RepOrden();
            return rep.GetListaOrdenActiva();
        }

        public IEnumerable<Orden> GetListaOrdenAll()
        {
            IRepOrden rep = new RepOrden();
            return rep.GetListaOrdenAll();
        }

        public Orden GetOrdenById(int id)
        {
            IRepOrden rep = new RepOrden();
            return rep.GetOrdenById(id);
        }

        public void Save(Orden orden)
        {
            IRepOrden rep = new RepOrden();
            rep.Save(orden);
        }
    }
}
