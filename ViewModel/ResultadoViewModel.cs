using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoGym.Data;
using QuestPDF.Fluent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ProyectoGym.ViewModel
{
    public partial class ResultadoViewModel : ObservableObject
    {
        private readonly GymStore _store = GymStore.Instance;

        public ObservableCollection<string> Resumen { get; } = new();

        [ObservableProperty] 
        private string rutinaGenerada = "";

        public ResultadoViewModel() => GenerarResumen();

        private void GenerarResumen()
        {
            Resumen.Clear();
            var u = _store.UsuarioActivo;
            Resumen.Add($"Usuario:             {u.Nombre}");
            Resumen.Add($"Objetivo:            {u.Objetivo}");
            Resumen.Add($"Nivel:               {u.Nivel}");
            Resumen.Add($"Equipamiento:        {u.Equipamiento}");
            Resumen.Add($"Peso actual:         {u.PesoActual} kg   Meta: {u.PesoMeta} kg");
            int sesiones = _store.Sesiones.Count(s => s.UsuarioId == u.Id);
            Resumen.Add($"Sesiones registradas:{sesiones}");
            var ultimo = _store.Progresos
                .Where(p => p.UsuarioId == u.Id)
                .OrderByDescending(p => p.Fecha)
                .FirstOrDefault();
            if (ultimo != null)
                Resumen.Add($"Ultimo peso:         {ultimo.PesoCorporal} kg ({ultimo.FechaDisplay})");
        }

        [RelayCommand]
        private void GenerarRutina()
        {
            var u = _store.UsuarioActivo;
            var sb = new StringBuilder();
            sb.AppendLine($"Rutina para {u.Nombre}  |  Objetivo: {u.Objetivo}  |  Nivel: {u.Nivel}");
            sb.AppendLine();

            if (u.Objetivo.Contains("Perder"))
            {
                sb.AppendLine("Lun: Cardio 30 min + Core");
                sb.AppendLine("Mie: Full Body circuito");
                sb.AppendLine("Vie: HIIT 20 min + Estiramientos");
            }
            else if (u.Objetivo.Contains("Musculo"))
            {
                sb.AppendLine("Lun: Pecho + Triceps");
                sb.AppendLine("Mar: Espalda + Biceps");
                sb.AppendLine("Jue: Piernas");
                sb.AppendLine("Vie: Hombros + Core");
            }
            else
            {
                sb.AppendLine("Lun: Sentadilla + Press banca");
                sb.AppendLine("Mie: Peso muerto + Remo");
                sb.AppendLine("Vie: Press militar + Dominadas");
            }
            RutinaGenerada = sb.ToString();
        }
       
        [RelayCommand]
        private void GenerateReport()
        {
            var u = _store.UsuarioActivo;

            var document = new Document();

            document.GeneratePdfAndShow();
        }
    }
}
