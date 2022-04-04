using Infrastructure.Models;
using AppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using Web.Util;
using System.Reflection;
using System.IO;
using Web.Security;

namespace Web.Controllers
{
    public class CitaController : Controller
    {
        private static String Action;
        // GET: Cita
        public ActionResult Index()
        {
            if (!String.IsNullOrEmpty(Action))
            {
                ViewBag.Action = Action;
            }
            IServiceTipoCita serviceTipo = new ServiceTipoCita();
            ViewBag.listaTipos = serviceTipo.GetListaTipoCita();
            Action = "";
            return View();
        }

        private SelectList ListaTipoCita()
        {
            IServiceTipoCita serviceTipo = new ServiceTipoCita();
            IEnumerable<TipoCita> lista = serviceTipo.GetListaTipoCita();
            return new SelectList(lista, "id", "descripcion");
        }

        // GET: Cita/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Cita/Create
        [HttpPost]
        public ActionResult Save(Cita cita)
        {
            string errores = "";
            try
            {
                IServiceTipoCita serviceTipo = new ServiceTipoCita();
                ViewBag.listaTipos = serviceTipo.GetListaTipoCita();
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceCita _ServiceCita = new ServiceCita();
                    if (cita.fechaCita == DateTime.Today)
                    {
                        if (cita.horaCita < TimeSpan.Parse(DateTime.Now.ToShortTimeString()))
                        {
                            Action = "F";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        if (cita.fechaCita < DateTime.Today)
                        {
                            Action = "F";
                            return RedirectToAction("Index");
                        }
                    }

                    if (_ServiceCita.GetCita(cita) == null)
                    {
                        _ServiceCita.Save(cita);
                    }
                    else
                    {
                        Action = "A";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Index", cita);
                }
                // redirigir
                return RedirectToAction("ConfirmacionCita");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        //Citas para el día actual

        public ActionResult List()
        {
            IEnumerable<Cita> lista = null;
            try
            {
                if (!String.IsNullOrEmpty(Action))
                {
                    ViewBag.Action = Action;
                }

                IServiceCita _ServiceCita = new ServiceCita();
                lista = _ServiceCita.GetListaCitas();
                Action = "";
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }

        public ActionResult ConfirmacionCita()
        {
            return View();
        }



        // GET: Cita/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cita/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
