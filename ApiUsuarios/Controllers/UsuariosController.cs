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

        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario){
            if(usuario == null)
                return BadRequest();

            _repositorio.Add(usuario);

            return CreatedAtRoute("GetUsuario", new Usuario{UsuarioId = usuario.UsuarioId}, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Usuario usuario){
            if(usuario == null || usuario.UsuarioId != id)
                return BadRequest();
            
            var _usuario = _repositorio.Find(id);
            if(usuario == null)
                return NotFound();
            
            _usuario.Email = usuario.Email;
            _usuario.Nome = usuario.Nome;

            _repositorio.Update(_usuario);

            //Retorna um 204(No Content)
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id){
            var usuario = _repositorio.Find(id);
            if(usuario == null)
                return NotFound();

            _repositorio.Remove(id);

            return new NoContentResult();
        }
    }
}