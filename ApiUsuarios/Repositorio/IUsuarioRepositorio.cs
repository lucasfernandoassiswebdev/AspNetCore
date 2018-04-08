using System.Collections.Generic;
using ApiUsuarios.models;

namespace ApiUsuarios.Repositorio
{
    public interface IUsuarioRepositorio
    {
        void Add(Usuario user);
        IEnumerable<Usuario> GetAll();
        Usuario Find(long id);
        void Remove(long id);
        void Update(Usuario user);
    }
}