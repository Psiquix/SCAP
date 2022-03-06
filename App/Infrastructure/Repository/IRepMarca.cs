using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepMarca
    {
        IEnumerable<Marca> GetListaMarca();
        Marca GetMarcaById(int id);
        void Save(Marca marca);
        void Delete(int pId);

    }
}
