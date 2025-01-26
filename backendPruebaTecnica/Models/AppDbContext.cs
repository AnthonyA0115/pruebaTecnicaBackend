using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace backendPruebaTecnica.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

    }
}