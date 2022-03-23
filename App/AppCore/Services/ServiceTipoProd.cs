using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceTipoProd : IServiceTipoProd
    {
        public void Delete(int pId)
        {
            IRepTipoProd oRepository = new RepTipoProd();
            oRepository.Delete(pId);
        }

        public IEnumerable<TipoProducto> GetListaTipoActive()
        {
            IRepTipoProd rep = new RepTipoProd();
            return rep.GetListaTipoActive();
         }

        public IEnumerable<TipoProducto> GetListaTipoAll()
        {
            IRepTipoProd rep = new RepTipoProd();
            return rep.GetListaTipoAll();
        }



        public TipoProducto GetTipoById(int id)
        {
            IRepTipoProd oRepository = new RepTipoProd();
            return oRepository.GetTipoById(id);
        }

        public TipoProducto Save(TipoProducto tipo)
        {
            IRepTipoProd oRepository = new RepTipoProd();
            return oRepository.Save(tipo);
        }
    }
}
