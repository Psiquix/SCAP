using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceRol : IServiceRol
    {
        public IEnumerable<Rol> GetListaRol()
        {
            IRepRol oRepository = new RepRol();
            return oRepository.GetListaRol();
        }
    }
}
