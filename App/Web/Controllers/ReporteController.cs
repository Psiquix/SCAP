using AppCore.Services;
using Infrastructure.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetOrdenActivo()
        {
            IEnumerable<Orden> lista = null;
            try
            {

                IServiceReporte _ServiceReporte = new ServiceReporte();
                lista = _ServiceReporte.GetEntradas();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult GetOrdenDesactivo()
        {
            IEnumerable<DetalleOrden> lista = null;
            try
            {

                IServiceReporte _ServiceReporte = new ServiceReporte();
                lista = _ServiceReporte.GetSalida();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult OrdenPDF()
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<Orden> lista = null;
            try
            {
                // Extraer informacion
                IServiceReporte _ServiceReporte = new ServiceReporte();
                lista = _ServiceReporte.GetEntradas();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Reporte de órdenes")
                            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(20);
                doc.Add(header);

                Paragraph subheader = new Paragraph("Reporte de las órdenes guardadas en la base de datos y sus estados a la fecha: " + DateTime.Now.ToShortDateString())
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(15);

                doc.Add(subheader);


                // Crear tabla con 5 columnas 
                Table table = new Table(4, true);
                table.SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN));
                table.AddHeaderCell("Id de Orden");
                table.AddHeaderCell("Fecha");
                table.AddHeaderCell("Total Pagar");
                table.AddHeaderCell("Estado");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.id.ToString()));
                    table.AddCell(new Paragraph(item.fecha.Value.ToShortDateString())); 
                    table.AddCell(new Paragraph("CRC " + item.total.ToString()));
                    if (item.estado)
                    {
                        table.AddCell(new Paragraph("Activo"));
                    }
                    else
                    {
                        table.AddCell(new Paragraph("Completada"));
                    }
                }
                doc.Add(table);
                LineSeparator ls2 = new LineSeparator(new SolidLine());
                doc.Add(ls2);


                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("Pág {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reportePedidos"+DateTime.Now.ToShortDateString()+".pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }


        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult ProductoPDF()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                // Extraer informacion
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Reporte de productos")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetTextAlignment(TextAlignment.CENTER)
                                   .SetFontSize(20);
                doc.Add(header);
                Paragraph subheader = new Paragraph("Reporte de los productos guardados en la base de datos y sus estados a la fecha: " + DateTime.Now.ToShortDateString())
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(15);
                doc.Add(subheader);


                // Crear tabla con 5 columnas 
                Table table = new Table(5, true);
                table.SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN));
                table.AddHeaderCell("Id de Producto");
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Precio");
                table.AddHeaderCell("Existencia");
                table.AddHeaderCell("Estado");

                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.id.ToString()));
                    table.AddCell(new Paragraph(item.nombre));
                    table.AddCell(new Paragraph("CRC "+ item.precioUnitario.ToString()));
                    if (item.cantidadNum > 0)
                    {
                        table.AddCell(new Paragraph(item.cantidadNum + " Unids"));
                    }
                    else
                    {
                        table.AddCell(new Paragraph(item.cantidadPeso + " Kg"));
                    }
                    if (item.estado)
                    {
                        table.AddCell(new Paragraph("Activo"));
                    }
                    else
                    {
                        table.AddCell(new Paragraph("Desactivado"));
                    }
                }
                doc.Add(table);

                LineSeparator ls2 = new LineSeparator(new SolidLine());
                doc.Add(ls2);


                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("Pág {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporteProductos" + DateTime.Now.ToShortDateString() + ".pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }


        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult MarcaPDF()
        {
            IEnumerable<Marca> lista = null;
            try
            {
                // Extraer informacion
                IServiceMarca _ServiceProducto = new ServiceMarca();
                lista = _ServiceProducto.GetListaMarcaAll();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Reporte de marcas")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetTextAlignment(TextAlignment.CENTER)
                                   .SetFontSize(20);
                doc.Add(header);
                Paragraph subheader = new Paragraph("Reporte de las marcas registradas en la base de datos y sus estados a la fecha: " + DateTime.Now.ToShortDateString())
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(15);
                doc.Add(subheader);


                // Crear tabla con 5 columnas 
                Table table = new Table(2, true);
                table.SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN));
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Estado");

                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.descripcion));
                    if (item.estado)
                    {
                        table.AddCell(new Paragraph("Activo"));
                    }
                    else
                    {
                        table.AddCell(new Paragraph("Desactivado"));
                    }
                }
                doc.Add(table);

                LineSeparator ls2 = new LineSeparator(new SolidLine());
                doc.Add(ls2);


                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("Pág {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporteMarcas" + DateTime.Now.ToShortDateString() + ".pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }
        [CustomAuthorize((int)Roles.Admin)]
        public ActionResult CitasPDF()
        {
            IEnumerable<Cita> lista = null;
            try
            {
                // Extraer informacion
                IServiceCita _ServiceCita = new ServiceCita();
                lista = _ServiceCita.GetListaAllCita();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Reporte de citas")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetTextAlignment(TextAlignment.CENTER)
                                   .SetFontSize(20);
                doc.Add(header);
                Paragraph subheader = new Paragraph("Reporte de las citas registradas en la base de datos y sus estados a la fecha: " + DateTime.Now.ToShortDateString())
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(12);
                doc.Add(subheader);


                // Crear tabla con 5 columnas 
                Table table = new Table(4, true);
                table.SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN));
                table.AddHeaderCell("ID");
                table.AddHeaderCell("Nombre del cliente");
                table.AddHeaderCell("Tipo de cita");
                table.AddHeaderCell("Estado");

                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.id.ToString()));
                    
                    table.AddCell(new Paragraph(item.nombreCliente));
                    table.AddCell(new Paragraph(item.TipoCita.descripcion));
                    if (item.estado.Value)
                    {
                        table.AddCell(new Paragraph("Activa"));
                    }
                    else
                    {
                        table.AddCell(new Paragraph("Completada"));
                    }
                }
                doc.Add(table);

                LineSeparator ls2 = new LineSeparator(new SolidLine());
                doc.Add(ls2);


                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("Pág {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporteCitas" + DateTime.Now.ToShortDateString() + ".pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

    }
}