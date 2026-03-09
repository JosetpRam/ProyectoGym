using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGym.Models
{
    public class Progreso
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public double PesoCorporal { get; set; }
        public string EstadoFisico { get; set; } = "";
        public string Rendimiento { get; set; } = "";
    }
}
