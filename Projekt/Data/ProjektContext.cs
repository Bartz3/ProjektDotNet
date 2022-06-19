using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data
{
    public class ProjektContext : DbContext
    {
        public ProjektContext (DbContextOptions<ProjektContext> options)
            : base(options)
        {
        }

        public DbSet<Projekt.Models.Employee>? Employee { get; set; }

        public DbSet<Projekt.Models.Firm>? Firm { get; set; }
    }
}
