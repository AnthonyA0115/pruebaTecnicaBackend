using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backendPruebaTecnica.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }  // 'Creada', 'Pendiente', 'Completada', 'Atrasada'
        public DateTime FechaLimite { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? FechaEliminacion { get; set; }  // Puede ser nulo si no está eliminada
    }
}