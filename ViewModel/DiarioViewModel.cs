using ClosedXML.Excel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using ProyectoGym.Data;
using ProyectoGym.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProyectoGym.ViewModel
{
    public partial class DiarioViewModel : ObservableObject
    {
        private readonly GymStore _store = GymStore.Instance;

        public ObservableCollection<RegistroDiario> Registros     { get; } = new();
        public ObservableCollection<int>  NivelesEnergia { get; } = new() { 1,2,3,4,5 };
        public ObservableCollection<int> NivelesFatiga  { get; } = new() { 1,2,3,4,5 };

        [ObservableProperty] 
        private DateTime fecha = DateTime.Today;
        
        [ObservableProperty] 
        private int      energia = 3;
        
        [ObservableProperty] 
        private double   horasSueno = 7;
        
        [ObservableProperty] 
        private int      fatiga = 3;
        
        [ObservableProperty] 
        private string   mensaje = "";

        public DiarioViewModel() => Recargar();

        private void Recargar()
        {
            Registros.Clear(); 
            int uid = _store.UsuarioActivo.Id; 
            foreach (var d in _store.Diarios
                         .Where(d => d.UsuarioId == uid)
                         .OrderByDescending(d => d.Fecha))
                Registros.Add(d);
        }

        [RelayCommand]
        private void GuardarRegistro()
        {
            _store.Diarios.Add(new RegistroDiario
            {
                Id        = _store.NextDiarioId(),
                UsuarioId = _store.UsuarioActivo.Id,
                Fecha     = Fecha,
                Energia   = Energia,
                HorasSueno = HorasSueno,
                Fatiga    = Fatiga
            });
            Recargar();
            Mensaje = "Registro diario guardado.";
        }

        [RelayCommand]
        private void ExportToExcel()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Exportar Diario a Excel"
            };
            if (dialog.ShowDialog() != true)
                return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Diario");

            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Usuario Id";
            ws.Cell(1, 3).Value = "Fecha";
            ws.Cell(1, 4).Value = "Energía";
            ws.Cell(1, 5).Value = "Horas de Sueño";
            ws.Cell(1, 6).Value = "Fatiga";
            ws.Row(1).Style.Font.Bold = true;

            int row = 2;
            foreach (var registro in Registros)
            {
                ws.Cell(row, 1).Value = registro.Id;
                ws.Cell(row, 2).Value = registro.UsuarioId;
                ws.Cell(row, 3).Value = registro.FechaDisplay;
                ws.Cell(row, 4).Value = registro.Energia;
                ws.Cell(row, 5).Value = registro.HorasSueno;
                ws.Cell(row, 6).Value = registro.Fatiga;
                row++;
            }

            ws.Columns().AdjustToContents();
            wb.SaveAs(dialog.FileName);
        }

    }
}

