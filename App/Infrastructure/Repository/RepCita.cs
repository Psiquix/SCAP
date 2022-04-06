using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepCita : IRepCita
    {
        //public void Delete(int pId)
        //{
        //    int salida;
        //    try
        //    {
        //        using (MyContext ctx = new MyContext())
        //        {
        //            /* La carga diferida retrasa la carga de datos relacionados,
        //             * hasta que lo solicite específicamente.*/
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            Cita oCita = GetCitaById(pId);
        //            oCita.estado = false;
        //            ctx.Entry(oCita).State = EntityState.Modified;
        //            salida = ctx.SaveChanges();
        //        }
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string msj = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
        //        throw new Exception(msj);
        //    }
        //    catch (Exception ex)
        //    {
        //        string msj = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
        //        throw;
        //    }
        //}

        public void Delete(Cita pCita)
        {
            int salida;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    /* La carga diferida retrasa la carga de datos relacionados,
                     * hasta que lo solicite específicamente.*/
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Cita oCita = GetCita(pCita);
                    oCita.estado = false;
                    ctx.Entry(oCita).State = EntityState.Modified;
                    salida = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string msj = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw new Exception(msj);
            }
            catch (Exception ex)
            {
                string msj = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw;
            }
        }
        public IEnumerable<Cita> GetListaAllCita()
        {
            IEnumerable<Cita> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //lista = ctx.Citas.Include(c => c.TipoCita).Where(x => x.estado == true).ToList<Cita>();
                    lista = ctx.Citas.ToList<Cita>();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
        public IEnumerable<Cita> GetListaCitaActive()
        {
            IEnumerable<Cita> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //lista = ctx.Citas.Include(c => c.TipoCita).Where(x => x.estado == true).ToList<Cita>();
                    lista = ctx.Citas.Include("TipoCita").Include("TipoCita.descripcion")
                        .Include(c => c.TipoCita).Where(x => x.estado == true).ToList<Cita>();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Cita> GetListaCitas()
        {
            try
            {
                IEnumerable<Cita> lista = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //lista = ctx.Citas.Include(c => c.TipoCita).ToList<Cita>();
                    lista = ctx.Citas.Where(p => p.fechaCita == DateTime.Today).Include("TipoCita")
                    .OrderByDescending(p => p.id).ToList<Cita>();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Cita GetCitaById(int pId)
        {
            Cita oCita = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //oCita = ctx.Citas.Find(pId);
                    oCita = ctx.Citas.Find(pId);
                }
                return oCita;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Cita Save(Cita pCita)
        {
            int salida = 0;
            Cita oCita = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCita = GetCita(pCita);
                    pCita.estado = true;
                    if (oCita == null)
                    {
                        //ctx.Citas.Add(pCita);
                        ctx.Citas.Add(pCita);
                    }
                    else
                    {
                        //Si existiera, actualiza los datos
                        ctx.Entry(pCita).State = EntityState.Modified;
                    }
                    salida = ctx.SaveChanges();
                }
                if (salida >= 0)
                {
                    oCita = GetCita(pCita);
                }
                return oCita;
            }
            catch (DbUpdateException dbEx)
            {
                string msj = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw new Exception(msj);
            }
            catch (Exception ex)
            {
                string msj = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw;
            }
        }

        public IEnumerable<Cita> GetCitasReporte()
        {
            try
            {
                IEnumerable<Cita> oCita = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCita = ctx.Citas.Include("TipoCita").Include("TipoCita.descripcion").
                        OrderByDescending(p => p.id).ToList<Cita>();
                }
                return oCita;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Cita> GetCitaByFecha(DateTime pFecha)
        {
            try
            {
                IEnumerable<Cita> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    //Definir el primer día del mes
                    DateTime fecha1 = pFecha;
                    while (fecha1.Day > 1)
                    {
                        fecha1 = fecha1.AddDays(-1);
                    }

                    //Definir el último día del mes
                    DateTime fecha2 = pFecha;
                    while (fecha2.Day < DateTime.DaysInMonth(pFecha.Year, pFecha.Month))
                    {
                        fecha2 = fecha2.AddDays(1);
                    }

                    lista = ctx.Citas.Include("TipoCita").Include("TipoCita.descripcion").
                               Where(p => p.fechaCita >= fecha1 && p.fechaCita <= fecha2).
                               OrderBy(p => p.fechaCita).ToList<Cita>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Cita GetCita(Cita pCita)
        {
            try
            {
                Cita oCita = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCita = ctx.Citas.Where(p => p.fechaCita == pCita.fechaCita && p.horaCita == pCita.horaCita)
                        .Include("TipoCita").FirstOrDefault();
                }
                return oCita;
            }
            catch (DbUpdateException dbEx)
            {
                string msj = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw new Exception(msj);
            }
            catch (Exception ex)
            {
                string msj = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw;
            }
        }
    }
}
