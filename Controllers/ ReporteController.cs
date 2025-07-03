using MicroservicioDemo2.Data;
using MicroservicioDemo2.Models;     // ← Aquí importas PdfService
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioDemo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly CalificacionContext _context;
        private readonly PdfService _pdfService;

        public ReporteController(CalificacionContext context, PdfService pdfService)
        {
            _context     = context;
            _pdfService  = pdfService;
        }

        [HttpGet("ExportarPdf")]
        public async Task<IActionResult> ExportarPdf()
        {
            var comentarios = await _context.Comentarios.ToListAsync();
            var pdfBytes    = _pdfService.GenerarReportePDF(comentarios);
            return File(pdfBytes, "application/pdf", "reporte_calificaciones.pdf");
        }
    }
}
