using System;
using System.Collections.Generic;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace MicroservicioDemo2.Models
{
    public class PdfService
    {
        public byte[] GenerarReportePDF(List<Comentario> comentarios)
        {
            using var document = new PdfDocument();
            var page = document.AddPage();
            var gfx  = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12, XFontStyle.Regular);

            int yPoint = 40;
            foreach (var c in comentarios)
            {
                gfx.DrawString(
                    $"Turista #{c.TuristaId}  Actividad #{c.ActividadId}  {c.Calificacion}/5",
                    font, XBrushes.Black,
                    new XRect(20, yPoint, page.Width - 40, page.Height)
                );
                yPoint += 20;

                gfx.DrawString(
                    c.Texto ?? string.Empty,
                    font, XBrushes.Gray,
                    new XRect(40, yPoint, page.Width - 60, page.Height)
                );
                yPoint += 40;

                if (yPoint > page.Height - 60)
                {
                    page   = document.AddPage();
                    gfx    = XGraphics.FromPdfPage(page);
                    yPoint = 40;
                }
            }

            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }
    }
}
