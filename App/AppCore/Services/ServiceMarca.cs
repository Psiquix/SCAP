﻿using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceMarca : IServiceMarca
    {
        public void Delete(int pId)
        {
            IRepMarca oRepository = new RepMarca();
            oRepository.Delete(pId);
        }

        public IEnumerable<Marca> GetListaMarca()
        {
            IRepMarca rep = new RepMarca();
            return rep.GetListaMarca();
        }

        public Marca GetMarcaById(int id)
        {
            IRepMarca oRepository = new RepMarca();
            return oRepository.GetMarcaById(id);
        }

        public void Save(Marca marca)
        {
            IRepMarca oRepository = new RepMarca();
            oRepository.Save(marca);
        }
    }
}