using AppCore.Services;
using Infrastructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
//using Rotativa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ReportesController : Controller
    {
        static IEnumerable<Orden> listaOrdenesFiltro = null;
        static IEnumerable<Cita> listaCitasFiltro = null;
        static string mesOrdenes;
        static string mesCitas;

        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        //[CustomAuthorize((int)Roles.Admin)]
        //public ActionResult ReporteCitas() //AntesCierreDep
        //{
        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Salvar el error en un archivo 
        //        Log.Error(ex, MethodBase.GetCurrentMethod());
        //        TempData["Message"] = "Error al procesar los datos! " + ex.Message;
        //        TempData.Keep();
        //        // Redireccion a la captura del Error
        //        return RedirectToAction("Default", "Error");
        //    }
        //}

        //public ActionResult AjaxReporteCitas(ViewModelParametro parametro)
        //{
        //    IEnumerable<Cita> lista = null;
        //    try
        //    {

        //        IServiceCita _ServiceCita = new ServiceCita();
        //        lista = _ServiceCita.GetCitaByFecha(parametro.Fecha);
        //        //Llena la lista para el PDF
        //        listaCitasFiltro = lista;
        //        //Convierte en letra el mes consultado para mostrarlo en el pdf
        //        mesCitas = parametro.Fecha.ToString("MMMM yyyy");
        //        return PartialView("_ReporteCitas", lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Salvar el error en un archivo 
        //        Log.Error(ex, MethodBase.GetCurrentMethod());
        //        TempData["Message"] = "Error al procesar los datos! " + ex.Message;
        //        TempData.Keep();
        //        // Redireccion a la captura del Error
        //        return RedirectToAction("Default", "Error");
        //    }
        //}

        ////Creación de PDF de Citas con Rotativa
        //public ActionResult createPdfCitas()
        //{
        //    ViewBag.mesCitas = mesCitas;
        //    return new PartialViewAsPdf("PdfCitas", listaCitasFiltro)
        //    {
        //        FileName = "Reporte Citas " + mesCitas + ".pdf",
        //        PageSize = Rotativa.Options.Size.A4,
        //        PageMargins = new Rotativa.Options.Margins(10, 10, 20, 10),
        //        CustomSwitches = "--page-offset 0 --footer-right [page] --footer-font-size 10"
        //    };
        //}
    }
}