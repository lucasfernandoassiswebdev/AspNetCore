using System.Collections.Generic;
using System.Linq;
using ApiUsuarios.models;

namespace ApiUsuarios.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UsuarioDbContext _contexto;

        public UsuarioRepositorio(UsuarioDbContext cxt)
        {
            _contexto = cxt;
        }

        public void Add(Usuario user)
        {
            _contexto.Usuarios.Add(user);
            _contexto.SaveChanges();
        }

        public Usuario Find(long id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _contexto.Usuarios.ToList();
        }

        public void Remove(long id)
        {
            var entity = _contexto.Usuarios.First(u => u.UsuarioId == id);
            _contexto.Usuarios.Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(Usuario user)
        {   
            _contexto.Usuarios.Update(user);
            _contexto.SaveChanges();
        }
    }
}