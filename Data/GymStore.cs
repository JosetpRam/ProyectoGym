using System.Collections.ObjectModel;
using ProyectoGym.Models;

namespace ProyectoGym.Data 
{
    /// <summary>
    /// </summary>
    public class GymStore 
    {
        private static GymStore? _instance;
        public static GymStore Instance => _instance ??= new GymStore(); //Utilizo el patrón singleton para asegurar que solo haya una instancia en toda la aplicacion lo que facilita el acceso centralizado a los datos y evita problemas de sincronización entre múltiples instancias y mejora la informacion entra pantallas

        public ObservableCollection<Usuario>              Usuarios  { get; } = new();
        public ObservableCollection<SesionEntrenamiento> Sesiones  { get; } = new();
        public ObservableCollection<RegistroDiario>      Diarios   { get; } = new();
        public ObservableCollection<ProgresoFisico>      Progresos { get; } = new();
        public ObservableCollection<Rutina>              Rutinas   { get; } = new();

        public Usuario UsuarioActivo { get; set; } //Guarda el usuario que está actualmente usando la app

        private int _nextUsuarioId  = 1;
        private int _nextSesionId   = 1;
        private int _nextDiarioId   = 1;
        private int _nextProgresoId = 1;
        private int _nextRutinaId   = 1; //Generar IDs únicos para cada tipo de objeto

        private GymStore()
        {
            
            var defecto = new Usuario //Como la base de datos no me conectaba requeri a este metodo parap poder simular algunoos datos
            {
                Id           = _nextUsuarioId++,
                Nombre       = "User",
                NombreUsuario = "User",
                Contrasena   = "",
                Nivel        = " ",
                Objetivo     = " ",
                Equipamiento = " ",
                PesoActual   = 00,
                PesoMeta     = 00,
                Altura       = 000
            };
            Usuarios.Add(defecto);
            UsuarioActivo = defecto;
            Rutinas.Add(new Rutina { Id = _nextRutinaId++, Nombre = "Rutina A",  Tipo = "RutinaA",  Activa = true }); //Rutinas predefinidas
            Rutinas.Add(new Rutina { Id = _nextRutinaId++, Nombre = "Rutina B",  Tipo = "RutinaB",  Activa = true });
            Rutinas.Add(new Rutina { Id = _nextRutinaId++, Nombre = "Full Body", Tipo = "FullBody",  Activa = true });
        }

        public int NextUsuarioId()  => _nextUsuarioId++;
        public int NextSesionId()   => _nextSesionId++;
        public int NextDiarioId()   => _nextDiarioId++;
        public int NextProgresoId() => _nextProgresoId++;
        public int NextRutinaId()   => _nextRutinaId++; //Estos metodos se utilizan para generar id unicos cada vez que se crea un nuevo objeto
    }
}
