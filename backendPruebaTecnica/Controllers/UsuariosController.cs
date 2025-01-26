using backendPruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace backendPruebaTecnica.Controllers
{
    public class UsuariosController : ApiController
    {
        private AppDbContext _context = new AppDbContext();

        // GET: api/Usuarios
        public IHttpActionResult GetUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            return Ok(usuarios);
        }

        // POST: api/Usuarios
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            var usuarioEnBd = _context.Usuarios.Find(id);
            if (usuarioEnBd == null)
                return NotFound();

            usuarioEnBd.Nombre = usuario.Nombre;
            usuarioEnBd.Correo = usuario.Correo;
            _context.SaveChanges();

            return Ok(usuarioEnBd);
        }

        // DELETE: api/Usuarios/5
        public IHttpActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok();
        }
    }
}
