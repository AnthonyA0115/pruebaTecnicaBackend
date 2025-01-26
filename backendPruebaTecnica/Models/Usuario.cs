using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backendPruebaTecnica.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}