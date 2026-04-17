using System.Collections.Generic;
using System.Linq;
using ProyectoGym.Data;
using ProyectoGym.Models;

namespace ProyectoGym.Repositories 
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GymStore _store = GymStore.Instance; //Accedo a la instancia del GymStore para interactuar con los datos de los usuarios

        public IEnumerable<Usuario> GetAll() => _store.Usuarios;

        public void Add(Usuario usuario)
        {
            usuario.Id = _store.NextUsuarioId();
            _store.Usuarios.Add(usuario);
        }

        public bool ExisteNombreUsuario(string nombreUsuario) => //Verifico si el nombre de usuario ya existe en la lista de usuarios para evitar duplicados
            _store.Usuarios.Any(u => u.NombreUsuario == nombreUsuario); //Utilizo el método Any de LINQ para verificar si existe algún usuario con el mismo nombre de usuario en la colección de usuarios del GymStore
    }
}
