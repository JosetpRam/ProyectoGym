using System.Collections.Generic;
using ProyectoGym.Models;

namespace ProyectoGym.Repositories 
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll(); 
        void Add(Usuario usuario);
        bool ExisteNombreUsuario(string nombreUsuario); //Verifica si el nombre de usuario ya existe para evitar duplicados
    }
}
