﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceOrden
    {
        IEnumerable<Orden> GetListaOrdenActiva();
        IEnumerable<Orden> GetListaOrdenAll();
        Orden GetOrdenById(int id);
        void Save(Orden orden);
        void Completar(int pId);
    }
}