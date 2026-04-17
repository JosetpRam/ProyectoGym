using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoGym.Data;

namespace ProyectoGym.ViewModel
{
    /// <summary>
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly GymStore _store = GymStore.Instance;

        [ObservableProperty]
        private string bienvenida = "";

        [ObservableProperty]
        private object? paginaActual;

        public MainViewModel()
        {
            Bienvenida   = $"Bienvenido ";
            PaginaActual = new ProyectoGym.Sesion();
        }

        [RelayCommand] 
        private void IrASesion() => PaginaActual = new ProyectoGym.Sesion(); 
       
        [RelayCommand] 
        private void IrADiario() => PaginaActual = new ProyectoGym.Diario();
       
        [RelayCommand] 
        private void IrAGestor() => PaginaActual = new ProyectoGym.Gestor();
        
        [RelayCommand] 
        private void IrAProgreso()=> PaginaActual = new ProyectoGym.Progreso();
       
        [RelayCommand] 
        private void IrAResultado() => PaginaActual = new ProyectoGym.Resultado();
       
        [RelayCommand] 
        private void IrAConfiguracion() => PaginaActual = new ProyectoGym.Configuracion();
    }
}
