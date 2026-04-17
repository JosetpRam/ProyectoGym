using System;

namespace ProyectoGym.Models
{
    public class SesionEntrenamiento
    {
        public int    Id       { get; set; }
        public int    UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = "";
        public DateTime Fecha  { get; set; } = DateTime.Today;
        public string Rutina   { get; set; } = "";
        public int    Duracion { get; set; }           
        public string Notas    { get; set; } = "";
        public string FechaDisplay => Fecha.ToString("dd/MM/yyyy");
        public string Resumen => $"{Rutina} – {Duracion} min";
    }
}
