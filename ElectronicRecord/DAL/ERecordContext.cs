using ElectronicRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ElectronicRecord.DAL
{
    public class ERecordContext : DbContext
    {
        public ERecordContext()
            : base("ERecordContext")
        { 
        }
        public DbSet<Judet> Judete { get; set; }
        public DbSet<Oras> Orase { get; set; }
        public DbSet<Medic> Medici { get; set; }
        public DbSet<Pacient> Pacienti { get; set; }
        public DbSet<Programare> Programari { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        } 
    }
}