using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoGym.Data;

namespace ProyectoGym.ViewModel
{
    public partial class ConfiguracionViewModel : ObservableObject
    {
        private readonly GymStore _store = GymStore.Instance;

        [ObservableProperty] 
        private bool esNovato = false;
        
        [ObservableProperty] 
        private bool esIntermedio = false;
        
        [ObservableProperty] 
        private bool esAvanzado  = false;
        
        [ObservableProperty] 
        private bool ganarMusculo = false;
        
        [ObservableProperty] 
        private bool ganarFuerza = false;
        
        [ObservableProperty] 
        private bool perderPeso  = false;
        
        [ObservableProperty] 
        private bool equipCompleto = false;
        
        [ObservableProperty] 
        private bool equipBasico  = false;
        
        [ObservableProperty] 
        private bool equipPesoCorporal = false;
        
        [ObservableProperty] 
        private double pesoActual = 0;
        
        [ObservableProperty] 
        private double pesoMeta  = 0;
        
        [ObservableProperty] 
        private double altura  = 0;
        
        [ObservableProperty] 
        private string mensaje  = "";

        public ConfiguracionViewModel()
        {
            var u = _store.UsuarioActivo; 
            EsNovato          = u.Nivel        == "Novato";
            EsIntermedio      = u.Nivel        == "Intermedio";
            EsAvanzado        = u.Nivel        == "Avanzado";
            GanarMusculo      = u.Objetivo     == "Ganar Musculo";
            GanarFuerza       = u.Objetivo     == "Ganar Fuerza";
            PerderPeso        = u.Objetivo     == "Perder Peso";
            EquipCompleto     = u.Equipamiento == "Completo";
            EquipBasico       = u.Equipamiento == "Basico";
            EquipPesoCorporal = u.Equipamiento == "Peso Corporal";
            PesoActual        = u.PesoActual;
            PesoMeta          = u.PesoMeta;
            Altura            = u.Altura;
        }

        [RelayCommand]
        private void Guardar() 
        {
            var u = _store.UsuarioActivo;
            u.Nivel        = EsNovato ? "Novato" : EsIntermedio ? "Intermedio" : EsAvanzado ? "Avanzado" : "";
            u.Objetivo     = GanarMusculo ? "Ganar Musculo" : GanarFuerza ? "Ganar Fuerza" : PerderPeso ? "Perder Peso" : "";
            u.Equipamiento = EquipCompleto ? "Completo" : EquipBasico ? "Basico" : EquipPesoCorporal ? "Peso Corporal" : "";
            u.PesoActual   = PesoActual;
            u.PesoMeta     = PesoMeta;
            u.Altura       = Altura;
            Mensaje        = "Perfil guardado correctamente.";
        }
    }
}
