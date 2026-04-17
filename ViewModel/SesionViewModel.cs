using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoGym.Data;
using ProyectoGym.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProyectoGym.ViewModel
{
    public partial class SesionViewModel : ObservableObject
    {
        private readonly GymStore _store = GymStore.Instance;

        public ObservableCollection<SesionEntrenamiento> Sesiones { get; } = new();
        public ObservableCollection<string> Rutinas  { get; } = new();

        [ObservableProperty] 
        private string? rutinaSeleccionada;
        
        [ObservableProperty] 
        private int duracion = 30;
        
        [ObservableProperty] 
        private string  notas= "";
        
        [ObservableProperty] 
        private string  mensaje  = "";

        [ObservableProperty] 
        private SesionEntrenamiento? sesionSeleccionada;

        public SesionViewModel()
        {
            foreach (var r in _store.Rutinas.Where(r => r.Activa))
                Rutinas.Add(r.Nombre);
            Recargar();
        }

        private void Recargar()
        {
            Sesiones.Clear();
            int uid = _store.UsuarioActivo.Id;
            foreach (var s in _store.Sesiones
                         .Where(s => s.UsuarioId == uid)
                         .OrderByDescending(s => s.Fecha))
                Sesiones.Add(s);
        }

        [RelayCommand]
        private void GuardarSesion()
        {
            Mensaje = "";
            if (string.IsNullOrWhiteSpace(RutinaSeleccionada))
            { Mensaje = "Selecciona una rutina."; return; }
            if (Duracion <= 0)
            { Mensaje = "La duracion debe ser mayor a 0 minutos."; return; }

            _store.Sesiones.Add(new SesionEntrenamiento
            {
                Id            = _store.NextSesionId(),
                UsuarioId     = _store.UsuarioActivo.Id,
                NombreUsuario = _store.UsuarioActivo.Nombre,
                Fecha         = DateTime.Today,
                Rutina        = RutinaSeleccionada,
                Duracion      = Duracion,
                Notas         = Notas
            });
            Recargar();
            Notas    = "";
            Duracion = 30;
            RutinaSeleccionada = null;
            Mensaje  = "Sesion guardada.";
        }

        [RelayCommand]
        private void EliminarSesion()
        {
            if (SesionSeleccionada == null) return;
            _store.Sesiones.Remove(SesionSeleccionada);
            Recargar();
        }
    }
}
