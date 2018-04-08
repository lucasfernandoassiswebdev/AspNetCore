using System.Collections.Generic;
using ApiUsuarios.models;
using ApiUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers
{
    [Route("api/[Controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _repositorio;
        
        public UsuariosController(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IEnumerable<Usuario> Get(){
            return _repositorio.GetAll();
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult GetById(long id){
            var usuario = _repositorio.Find(id);
            if(usuario == null)
                return NotFound();
            
            return new ObjectResult(usuario);
        }
    }
}