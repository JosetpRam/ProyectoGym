using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ProyectoGym.Data;
using System;
using System.Linq;

namespace ProyectoGym.Reports
{
    public class ResultadoPdfReport
    {
        private readonly GymStore _store = GymStore.Instance;

        public void GenerarReporte(string filePath)
        {
            var u = _store.UsuarioActivo;
            var sesiones = _store.Sesiones.Count(s => s.UsuarioId == u.Id);
            var ultimoPeso = _store.Progresos
                .Where(p => p.UsuarioId == u.Id)
                .OrderByDescending(p => p.Fecha)
                .FirstOrDefault();

            string rutina = GenerarRutinaTexto(u);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1.5f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    page.Header().Column(col =>
                    {
                        col.Item().BorderBottom(2).BorderColor(Colors.Grey.Darken2)
                            .PaddingBottom(8).Row(row =>
                            {
                                row.RelativeItem().Column(inner =>
                                {
                                    inner.Item().Text("PROYECTO GYM")
                                        .FontSize(20).Bold().FontColor(Colors.Grey.Darken3);
                                    inner.Item().Text("Reporte de Usuario")
                                        .FontSize(12).FontColor(Colors.Grey.Medium);
                                });
                                row.ConstantItem(120).AlignRight().Column(inner =>
                                {
                                    inner.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}")
                                        .FontSize(9).FontColor(Colors.Grey.Medium);
                                    inner.Item().Text($"Hora: {DateTime.Now:HH:mm}")
                                        .FontSize(9).FontColor(Colors.Grey.Medium);
                                });
                            });
                    });

                    page.Content().PaddingTop(16).Column(col =>
                    {
                        col.Item().Text("Datos del Usuario")
                            .FontSize(13).Bold().FontColor(Colors.Grey.Darken3);
                        col.Item().PaddingTop(6).Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(2);
                            });

                            AgregarFila(table, "Nombre", u.Nombre);
                            AgregarFila(table, "Objetivo", u.Objetivo);
                            AgregarFila(table, "Nivel", u.Nivel);
                            AgregarFila(table, "Equipamiento", u.Equipamiento);
                            AgregarFila(table, "Peso Actual", $"{u.PesoActual} kg");
                            AgregarFila(table, "Peso Meta", $"{u.PesoMeta} kg");
                            AgregarFila(table, "Sesiones Registradas", sesiones.ToString());

                            if (ultimoPeso != null)
                                AgregarFila(table, "Último Peso Registrado",
                                    $"{ultimoPeso.PesoCorporal} kg  ({ultimoPeso.FechaDisplay})");
                        });

                        col.Item().PaddingTop(20);

                        col.Item().Text("Rutina Sugerida")
                            .FontSize(13).Bold().FontColor(Colors.Grey.Darken3);
                        col.Item().PaddingTop(6)
                            .Border(1).BorderColor(Colors.Grey.Lighten1)
                            .Padding(10)
                            .Text(rutina)
                            .FontSize(11).LineHeight(1.6f);
                    });

                    page.Footer().AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ").FontSize(9).FontColor(Colors.Grey.Medium);
                            x.CurrentPageNumber().FontSize(9).FontColor(Colors.Grey.Medium);
                            x.Span(" de ").FontSize(9).FontColor(Colors.Grey.Medium);
                            x.TotalPages().FontSize(9).FontColor(Colors.Grey.Medium);
                        });
                });
            })
            .GeneratePdf(filePath);
        }


        private static void AgregarFila(TableDescriptor table, string etiqueta, string valor)
        {
            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(5).PaddingHorizontal(4)
                .Text(etiqueta).SemiBold().FontColor(Colors.Grey.Darken2);

            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(5).PaddingHorizontal(4)
                .Text(valor).FontColor(Colors.Black);
        }

        private static string GenerarRutinaTexto(dynamic u)
        {
            string objetivo = u.Objetivo?.ToString() ?? "";

            if (objetivo.Contains("Perder"))
                return "Lunes:    Cardio 30 min + Core\n" +
                       "Miércoles: Full Body circuito\n" +
                       "Viernes:  HIIT 20 min + Estiramientos";

            if (objetivo.Contains("Musculo"))
                return "Lunes:    Pecho + Tríceps\n" +
                       "Martes:   Espalda + Bíceps\n" +
                       "Jueves:   Piernas\n" +
                       "Viernes:  Hombros + Core";

            return "Lunes:    Sentadilla + Press banca\n" +
                   "Miércoles: Peso muerto + Remo\n" +
                   "Viernes:  Press militar + Dominadas";
        }
    }
}
