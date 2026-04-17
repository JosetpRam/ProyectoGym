using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoGym.Data;
using ProyectoGym.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProyectoGym.ViewModel
{
    public partial class ProgresoViewModel : ObservableObject
    {
        private readonly GymStore _store = GymStore.Instance;

        public ObservableCollection<string> HistorialPeso   { get; } = new();
        public ObservableCollection<string> HistorialRend   { get; } = new();
        public ObservableCollection<string> HistorialEstado { get; } = new();

        [ObservableProperty] 
        private double peso = 0;
        
        [ObservableProperty] 
        private string estadoFisico = "";
        
        [ObservableProperty] 
        private string rendimiento  = "";
        
        [ObservableProperty] 
        private string mensaje      = "";

        public ProgresoViewModel() => Recargar();

        private void Recargar()
        {
            HistorialPeso.Clear();
            HistorialRend.Clear();
            HistorialEstado.Clear();
            int uid = _store.UsuarioActivo.Id;
            foreach (var p in _store.Progresos
                         .Where(p => p.UsuarioId == uid)
                         .OrderByDescending(p => p.Fecha))
            {
                HistorialPeso.Add(p.ResumenPeso);
                HistorialRend.Add(p.ResumenRend);
                HistorialEstado.Add(p.ResumenEst);
            }
        }

        [RelayCommand]
        private void GuardarProgreso()
        {
            Mensaje = "";
            if (Peso <= 0) { Mensaje = "Ingresa un peso valido."; return; }

            _store.Progresos.Add(new ProgresoFisico
            {
                Id           = _store.NextProgresoId(),
                UsuarioId    = _store.UsuarioActivo.Id,
                Fecha        = DateTime.Today,
                PesoCorporal = Peso,
                EstadoFisico = EstadoFisico,
                Rendimiento  = Rendimiento
            });
            Recargar();
            Mensaje = "Progreso guardado.";
        }
    }
}
