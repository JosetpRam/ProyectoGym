using System;

namespace ProyectoGym.Models
{
    public class ProgresoFisico
    {
        public int    Id           { get; set; }
        public int    UsuarioId    { get; set; }
        public DateTime Fecha      { get; set; } = DateTime.Today;
        public double PesoCorporal { get; set; }
        public string EstadoFisico { get; set; } = "";
        public string Rendimiento  { get; set; } = "";
        public string FechaDisplay => Fecha.ToString("dd/MM/yyyy");
        public string ResumenPeso  => $"{FechaDisplay}  {PesoCorporal} kg";
        public string ResumenRend  => $"{FechaDisplay}  {Rendimiento}";
        public string ResumenEst   => $"{FechaDisplay}  {EstadoFisico}";
    }
}
