using backendPruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace backendPruebaTecnica.Controllers
{
    public class TareasController : ApiController
    {
        private AppDbContext _context = new AppDbContext();
        // GET: api/Tareas
        public IHttpActionResult GetTareas()
        {
            var tareas = _context.Tareas.ToList();
            return Ok(tareas);
        }

        // GET: api/Tareas/Usuario/{usuarioId}
        // Este método te permitirá obtener las tareas de un usuario en específico
        public IHttpActionResult GetTareasPorUsuario(int usuarioId)
        {
            var tareas = _context.Tareas.Where(t => t.UsuarioId == usuarioId).ToList();
            if (tareas == null || tareas.Count == 0)
                return NotFound();
            return Ok(tareas);
        }

        // POST: api/Tareas
        public IHttpActionResult PostTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Tareas.Add(tarea);
            _context.SaveChanges();
            return Ok(tarea);
        }

        // PUT: api/Tareas/5
        public IHttpActionResult PutTarea(Tarea tarea)
        {
            var tareaEnBd = _context.Tareas.Find(tarea.Id);
            if (tareaEnBd == null)
                return NotFound();

            if (!string.IsNullOrEmpty(tarea.Estado))
                tareaEnBd.Estado = tarea.Estado;

            if (tarea.FechaEliminacion.HasValue)
                tareaEnBd.FechaEliminacion = tarea.FechaEliminacion;

            _context.SaveChanges();

            return Ok(tareaEnBd);
        }

    }
}