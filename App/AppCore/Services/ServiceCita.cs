using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class ServiceCita : IServiceCita
    {
        //public void Delete(int pId)
        //{
        //    IRepCita oRepository = new RepCita();
        //    oRepository.Delete(pId);
        //}

        public void Delete(Cita pCita)
        {
            IRepCita oRepository = new RepCita();
            oRepository.Delete(pCita);
        }

        public Cita GetCita(Cita pCita)
        {
            IRepCita oRepository = new RepCita();
            return oRepository.GetCita(pCita);
        }

        public IEnumerable<Cita> GetCitaByFecha(DateTime pFecha)
        {
            IRepCita oRepository = new RepCita();
            return oRepository.GetCitaByFecha(pFecha);
        }

        public IEnumerable<Cita> GetCitasReporte()
        {
            IRepCita oRepository = new RepCita();
            return oRepository.GetCitasReporte();
        }

        //public Cita GetCitaById(int pId)
        //{
        //    IRepCita oRepository = new RepCita();
        //    return oRepository.GetCitaById(pId);
        //}

        public IEnumerable<Cita> GetListaCitaActive()
        {
            IRepCita oRepository = new RepCita();
            return oRepository.GetListaCitaActive();
        }

        public IEnumerable<Cita> GetListaCitas()
        {
            IRepCita oRepository = new RepCita();
            return oRepository.GetListaCitas();
        }

        public Cita Save(Cita pCita)
        {
            IRepCita oRepository = new RepCita();
            return oRepository.Save(pCita);
        }
    }
}
