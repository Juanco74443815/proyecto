using System;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioDemo2.Models
{
    public class Comentario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del turista es obligatorio.")]
        public int TuristaId { get; set; }

        [Required(ErrorMessage = "El ID de la actividad es obligatorio.")]
        public int ActividadId { get; set; }

        [Required(ErrorMessage = "El texto del comentario es obligatorio.")]
        [StringLength(500, ErrorMessage = "El texto no puede tener más de 500 caracteres.")]
        public string Texto { get; set; } = string.Empty; // ✅ valor por defecto para evitar nulls

        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        public int Calificacion { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow; // ✅ valor por defecto
    }
}
