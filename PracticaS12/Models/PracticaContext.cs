using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PracticaS12.Models
{
    public class PracticaContext : DbContext
    {
        public PracticaContext() : base("PracticaContext") { } 

        public DbSet<Principal> Principal { get; set; }
        public DbSet<Abono> Abonos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Principal>().Property(p => p.Precio).HasPrecision(18, 5);
            modelBuilder.Entity<Principal>().Property(p => p.Saldo).HasPrecision(18, 5);
            modelBuilder.Entity<Abono>().Property(a => a.Monto).HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
    }
}