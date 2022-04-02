using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceTipoCita : IServiceTipoCita
    {
        public IEnumerable<TipoCita> GetListaTipoCita()
        {
            IRepTipoCita oRepository = new RepTipoCita();
            return oRepository.GetListaTipoCita();
        }

        public TipoCita GetTipoCitaById(int id)
        {
            IRepTipoCita oRepository = new RepTipoCita();
            return oRepository.GetTipoCitaById(id);
        }
    }
}
