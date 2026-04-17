using System;

namespace ProyectoGym.Models
{
    public class RegistroDiario
    {
        public int    Id          { get; set; }
        public int    UsuarioId   { get; set; }
        public DateTime Fecha     { get; set; } = DateTime.Today;
        public int    Energia     { get; set; }   
        public double HorasSueno  { get; set; }
        public int    Fatiga      { get; set; }   
        public string FechaDisplay => Fecha.ToString("dd/MM/yyyy");
        public string Resumen => $"{FechaDisplay}  Energía:{Energia}  Sueño:{HorasSueno}h  Fatiga:{Fatiga}";
    }
}
