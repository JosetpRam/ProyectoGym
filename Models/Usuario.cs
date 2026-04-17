using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProyectoGym.Models
{
    public class Usuario : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Nombre    { get; set; } = "";
        public string NombreUsuario { get; set; } = "";
        public string Contrasena { get; set; } = "";
        public string Nivel     { get; set; } = "";   
        public string Objetivo  { get; set; } = "";   
        public string Equipamiento { get; set; } = "";
        public double PesoActual { get; set; }
        public double PesoMeta   { get; set; }
        public double Altura     { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged; //Es un avisor para la vista de que una propiedad ha cambiado y necesita actualizarse
        protected void OnPropertyChanged([CallerMemberName] string? n = null) // Permite llamar a este método sin especificar el nombre de la propiedad
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n)); 
    }
}
