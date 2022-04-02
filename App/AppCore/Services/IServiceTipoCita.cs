using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceTipoCita
    {
        IEnumerable<TipoCita> GetListaTipoCita();
        TipoCita GetTipoCitaById(int id);
    }
}
