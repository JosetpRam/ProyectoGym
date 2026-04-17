using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoGym.Data;
using ProyectoGym.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProyectoGym.ViewModel
{
    public partial class GestorViewModel : ObservableObject
    {
        private readonly GymStore _store = GymStore.Instance;

        public ObservableCollection<Rutina> Rutinas => _store.Rutinas;

        [ObservableProperty] 
        private Rutina? rutinaSeleccionada;
       
        [ObservableProperty] 
        private string  nuevaRutina = "";

        [RelayCommand]
        private void CrearRutina()
        {
            if (string.IsNullOrWhiteSpace(NuevaRutina)) return;
            _store.Rutinas.Add(new Rutina
            {
                Id     = _store.NextRutinaId(),
                Nombre = NuevaRutina,
                Tipo   = "Custom",
                Activa = true
            });
            NuevaRutina = "";
        }

        [RelayCommand]
        private void EliminarRutina()
        {
            if (RutinaSeleccionada == null) return;
            var r = MessageBox.Show($"¿Eliminar '{RutinaSeleccionada.Nombre}'?",
                                    "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
                _store.Rutinas.Remove(RutinaSeleccionada);
        }
    }
}
