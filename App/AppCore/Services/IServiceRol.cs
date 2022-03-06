using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceRol
    {
        IEnumerable<Rol> GetListaRol();
    }
}
