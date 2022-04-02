using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepCita
    {
        IEnumerable<Cita> GetListaCitaActive();
        IEnumerable<Cita> GetListaCitas();
        IEnumerable<Cita> GetCitasReporte();
        IEnumerable<Cita> GetCitaByFecha(DateTime pFecha);
        Cita GetCita(Cita pCita);
        Cita Save(Cita pCita);
        //void Delete(int pId);
        void Delete(Cita pCita);
    }
}
